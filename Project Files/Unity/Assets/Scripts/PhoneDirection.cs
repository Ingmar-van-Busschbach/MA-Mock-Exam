using System;
using Toolbox.Utilities;
using UnityEngine;
using UnityEngine.Events;
using Generic;

public class PhoneDirection : MonoSingleton<PhoneDirection>
{
    [SerializeField] private float rotationTolerance = 1f;
    [SerializeField] private Route.SaveRoute saveRoute;

    private Vector2 currentPos;
    private Vector2 nextPos;

    private float lastCompassRotation = 0;

    public UnityEvent<float> onCompassChange = new UnityEvent<float>();

    private void Awake()
    {
        Input.compass.enabled = true;
    }

    void Update()
    {
        if (!GPSService.Instance.GpsServiceEnabled) { return; }
        Coordinates coordinate = saveRoute.GetCurrentCoordinate();
        if (!coordinate.IsValid()) { return; }

        float northHeadingDegrees = Input.compass.magneticHeading;

        float currentLongitude = Input.location.lastData.longitude;
        float currentLatitude = Input.location.lastData.latitude;
        float nextLongitude = (float)coordinate.longitude;
        float nextLatitude = (float)coordinate.latitude;

        currentPos = new Vector2(currentLongitude, currentLatitude);
        nextPos = new Vector2(nextLongitude, nextLatitude);

        Vector2 lookDirection = (nextPos - currentPos).normalized;

        float directionInDegrees = Mathf.Atan2(lookDirection.y, lookDirection.x);
        directionInDegrees = directionInDegrees * 180 / Mathf.PI;
        directionInDegrees = (directionInDegrees - 90);

        float nextPointDegrees = northHeadingDegrees + directionInDegrees;

        if (Math.Abs(nextPointDegrees - lastCompassRotation) > rotationTolerance) onCompassChange.Invoke(nextPointDegrees);

        lastCompassRotation = nextPointDegrees;
    }
}