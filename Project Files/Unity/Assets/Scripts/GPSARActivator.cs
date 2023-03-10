using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Generic;

public class GPSARActivator : MonoBehaviour
{
    [SerializeField] private GPSService gpsService;
    [SerializeField] private Route.SaveRoute saveRoute;
    [SerializeField] private double enableDistance = 5;
    [SerializeField] private double disableDistance = 10;
    [SerializeField] private TMP_Text distanceText;
    [SerializeField] private RectTransform compassTransform;

    private Coordinates nextCoordinate = new Coordinates(-1, -1, -1);
    private Coordinates currentCoordinate = new Coordinates(-1, -1, -1);

    private double nextDistance;
    private double currentDistance;
    private bool isActive = false;

    public UnityEvent activateAR = new UnityEvent();
    public UnityEvent deactivateAR = new UnityEvent();
    public UnityEvent routeCompleted = new UnityEvent();
    public UnityEvent<int> activateARImportant = new UnityEvent<int>();

    void Update()
    {
        if (!GPSService.Instance.GpsServiceEnabled) return;

        nextCoordinate = saveRoute.GetCurrentCoordinate();
        
        // skip this frame if the next coordinate is not valid.
        if (!nextCoordinate.IsValid())
        {
            distanceText.text = "No valid point to track.";
            return;
        }
        nextDistance = nextCoordinate.DistanceTo(Input.location.lastData.latitude, Input.location.lastData.longitude) * 1000;
        float roundedDistance = (float) nextDistance;
        roundedDistance = Mathf.Round(roundedDistance * 100) / 100;
        distanceText.text = "distance: " + roundedDistance + "m";

        // Somewhat clunky implementation hack, but this allows the user to navigate to the next point while the AR system is still tracking the old point.
        currentDistance = 0;
        if (currentCoordinate.IsValid())
        {
            currentDistance = currentCoordinate.DistanceTo(Input.location.lastData.latitude, Input.location.lastData.longitude) * 1000;
        }

        // Activate ONCE when near the next point.
        if (nextDistance <= enableDistance && isActive && nextDistance < currentDistance)
        {
            deactivateAR.Invoke();

            currentCoordinate = saveRoute.GetCurrentCoordinate();

            if (saveRoute.CoordinateReached())
            {
                routeCompleted.Invoke();
            }
            else if (currentCoordinate.IsImportant())
            {
                activateARImportant.Invoke(currentCoordinate.importance);
            }
            else
            {
                activateAR.Invoke();
            }

        }
        else if (nextDistance <= enableDistance && !isActive)
        {
            isActive = true;
            currentCoordinate = saveRoute.GetCurrentCoordinate();

            if (saveRoute.CoordinateReached())
            {
                routeCompleted.Invoke();
            }
            else if (currentCoordinate.IsImportant())
            {
                activateARImportant.Invoke(currentCoordinate.importance);
            }
            else
            {
                activateAR.Invoke();
            }
        }
        else if (currentDistance >= disableDistance && isActive)
        {
            isActive = false;
            deactivateAR.Invoke();
        }
    }
}
