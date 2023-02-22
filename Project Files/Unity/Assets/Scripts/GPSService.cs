using System;
using System.Collections;
using Toolbox.Utilities;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using UnityEngine.Events;

public class GPSService : MonoSingleton<GPSService>
{
    [SerializeField] float accuracyInMeters = 8f;
    [SerializeField] float updateDistanceInMeters = 8f;
    [SerializeField] private int maxWaitInSeconds = 15;

    private bool _hasFineLocationPermission = false;
    private PermissionCallbacks _permissionCallbacks;

    public UnityEvent onLocationServicesStarted = new UnityEvent();

    public bool GpsServiceEnabled { get; private set; } = false;

    public override void Awake()
    {
        base.Awake();
        Debug.Log("Checking permissions...");
        _hasFineLocationPermission = Permission.HasUserAuthorizedPermission(Permission.FineLocation);
    }

    public void OnApplicationQuit()
    {
        StopLocationServices();
    }

    public void TryStartingLocationServices(Action startedCallback = null, Action noPermissionCallback = null, Action errorCallback = null)
    {
        Debug.Log("Starting GPS Service...");
        RequestLocationPermission(() =>
        {
            StartLocationServices(() =>
            {
                startedCallback?.Invoke();
            }, () =>
            {
                errorCallback?.Invoke();
            });
        }, () =>
        {
            noPermissionCallback?.Invoke();
            errorCallback?.Invoke();
        });
    }

    public void StartLocationServices(Action startedCallback = null, Action errorCallback = null)
    {
        StartCoroutine(StartLocationServicesEnumerator(startedCallback, errorCallback));
    }

    public void RequestLocationPermission(Action onPermissionConfirmed, Action onPermissionDenied)
    {
        //CHECK IF WE ALREADY HAVE PERMISSION
        _hasFineLocationPermission = Permission.HasUserAuthorizedPermission(Permission.FineLocation);
        if (_hasFineLocationPermission)
        {
            onPermissionConfirmed?.Invoke();
            return;
        }

        //WE DON'T HAVE PERMISSION SO WE REQUEST IT AND START SERVICES ON GRANTED.
        _permissionCallbacks = new PermissionCallbacks();

        _permissionCallbacks.PermissionGranted += s => { onPermissionConfirmed.Invoke(); };
        _permissionCallbacks.PermissionDenied += s => { onPermissionDenied.Invoke(); };
        _permissionCallbacks.PermissionDeniedAndDontAskAgain += s => { onPermissionDenied.Invoke(); };

        Permission.RequestUserPermission(Permission.FineLocation, _permissionCallbacks);
    }

    private IEnumerator StartLocationServicesEnumerator(Action startedCallback = null, Action errorCallback = null)
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            errorCallback?.Invoke();
            Debug.LogFormat("Location not enabled on Android!");
            yield break;
        }

        // Start service before querying location
        Input.location.Start(accuracyInMeters, updateDistanceInMeters);

        // Wait until service initializes
        while (Input.location.status == LocationServiceStatus.Initializing && maxWaitInSeconds > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            maxWaitInSeconds--;
        }

        // Service didn't initialize in 15 seconds
        if (maxWaitInSeconds < 1)
        {
            errorCallback?.Invoke();
            Debug.LogFormat("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status != LocationServiceStatus.Running)
        {
            errorCallback?.Invoke();
            Debug.LogFormat("Unable to determine device location. Failed with status {0}", Input.location.status);
            yield break;
        }

        GpsServiceEnabled = true;
        onLocationServicesStarted.Invoke();
        startedCallback?.Invoke();
    }

    public void StopLocationServices()
    {
        Input.location.Stop();
        GpsServiceEnabled = false;
    }

    public void OnAccuracyChanged(Slider slider)
    {
        accuracyInMeters = slider.value;
    }
    public void OnDistanceChanged(Slider slider)
    {
        updateDistanceInMeters = slider.value;
    }
}
