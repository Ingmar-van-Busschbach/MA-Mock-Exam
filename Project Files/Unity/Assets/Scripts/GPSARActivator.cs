using UnityEngine;
using UnityEngine.Events;

public class GPSARActivator : MonoBehaviour
{
    [SerializeField] private GPSService gpsService;
    [SerializeField] private RouteTrackingButton routeTrackingButton;
    [SerializeField] private double enableDistance = 5;
    [SerializeField] private double disableDistance = 10;

    private double currentDistance;
    private bool isActive = false;

    public UnityEvent activateAR = new UnityEvent();
    public UnityEvent deactivateAR = new UnityEvent();

    void Update()
    {
        if (routeTrackingButton.coordinate.IsValid())
        {
            currentDistance = routeTrackingButton.coordinate.DistanceTo(Input.location.lastData.latitude, Input.location.lastData.longitude) * 1000;
            if(currentDistance <= enableDistance && !isActive)
            {
                isActive = true;
                activateAR.Invoke();
            }
            else if(currentDistance >= disableDistance && isActive)
            {
                isActive = false;
                deactivateAR.Invoke();
            }
        }
    }
}
