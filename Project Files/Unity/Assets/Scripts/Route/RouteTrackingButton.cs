using UnityEngine;
using UnityEngine.UI;
using Generic;

public class RouteTrackingButton : MonoBehaviour
{
    public Coordinates coordinate;
    private void Awake()
    {
        if (gameObject.TryGetComponent(out Button button))
        {
            button.onClick.AddListener(OnClick);
        }
    }

    private void OnClick()
    {
        SaveCoordinate();
    }

    private void SaveCoordinate()
    {
        if (!GPSService.Instance.GpsServiceEnabled) return;
        coordinate = new Coordinates(Input.location.lastData.latitude, Input.location.lastData.longitude, Input.location.lastData.altitude);
    }

    
}
