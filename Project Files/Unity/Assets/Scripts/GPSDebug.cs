using TMPro;
using UnityEngine;
using Generic;
using System;

public class GPSDebug : MonoBehaviour
{
    [SerializeField] private GPSService gpsService;
    [SerializeField] private RouteTrackingButton routeTrackingButton;

    [SerializeField] private TMP_Text latitudeText;
    [SerializeField] private TMP_Text longitudeText;
    [SerializeField] private TMP_Text altitudeText;
    [SerializeField] private TMP_Text horizontalAccuracyText;
    [SerializeField] private TMP_Text timestampText;
    [SerializeField] private TMP_Text distanceText;

    private void Update()
    {
        if (!GPSService.Instance.GpsServiceEnabled) return;

        latitudeText.text = "latitude: " + Input.location.lastData.latitude + "N";
        longitudeText.text = "longitude: " + Input.location.lastData.longitude + "W";
        altitudeText.text = "altitude: " + Input.location.lastData.altitude + "m";
        horizontalAccuracyText.text = "horizontal accuracy: " + Input.location.lastData.horizontalAccuracy + "m";
        timestampText.text = "timestamp: " + UnixTimeStampToDateTime(Input.location.lastData.timestamp);
        if (routeTrackingButton.coordinate.IsValid())
        {
            distanceText.text = "distance: " + routeTrackingButton.coordinate.DistanceTo(Input.location.lastData.latitude, Input.location.lastData.longitude) * 1000 + "m";
        }
        else
        {
            distanceText.text = "No valid point to track.";
        }
    }

    public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dateTime;
    }
}
