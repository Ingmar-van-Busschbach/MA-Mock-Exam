using TMPro;
using UnityEngine;
using Generic;
using System;

public class GPSDebug : MonoBehaviour
{
    [SerializeField] private GPSService gpsService;
    [SerializeField] private Route.SaveRoute saveRoute;

    [SerializeField] private TMP_Text latitudeText;
    [SerializeField] private TMP_Text longitudeText;
    [SerializeField] private TMP_Text altitudeText;
    [SerializeField] private TMP_Text horizontalAccuracyText;
    [SerializeField] private TMP_Text timestampText;
    [SerializeField] private TMP_Text currentIndexText;
    [SerializeField] private TMP_Text maxIndexText;

    private void Update()
    {
        if (!GPSService.Instance.GpsServiceEnabled) return;

        latitudeText.text = "latitude: " + Input.location.lastData.latitude + "N";
        longitudeText.text = "longitude: " + Input.location.lastData.longitude + "W";
        altitudeText.text = "altitude: " + Input.location.lastData.altitude + "m";

        horizontalAccuracyText.text = "horizontal accuracy: " + Input.location.lastData.horizontalAccuracy + "m";
        timestampText.text = "timestamp: " + UnixTimeStampToDateTime(Input.location.lastData.timestamp);

        currentIndexText.text = saveRoute.currentCoordinateIndex.ToString();
        maxIndexText.text = saveRoute.trackedCoordinates.ToString();
    }

    public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch (01-01-1970), which we have to convert to a logical time and date.
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dateTime;
    }
}
