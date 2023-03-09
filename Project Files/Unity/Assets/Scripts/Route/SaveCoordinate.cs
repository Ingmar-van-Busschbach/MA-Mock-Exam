using UnityEngine;
using UnityEngine.UI;
using Generic;

public class SaveCoordinate : MonoBehaviour
{
    public Route.SaveRoute saveRoute;
    [SerializeField] private int importance = 0;

    private void Awake()
    {
        if (gameObject.TryGetComponent(out Button button))
        {
            button.onClick.AddListener(OnClick);
        }
    }

    private void OnClick()
    {
        SaveCurrentCoordinate();
    }

    private void SaveCurrentCoordinate()
    {
        if (!GPSService.Instance.GpsServiceEnabled) return;
        saveRoute.AddRoutePoint(new Coordinates(Input.location.lastData.latitude, Input.location.lastData.longitude, Input.location.lastData.altitude, importance));
    }

    
}
