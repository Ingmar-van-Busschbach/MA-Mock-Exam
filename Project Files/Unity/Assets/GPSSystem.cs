using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GPSSystem : MonoBehaviour
{
    [SerializeField] float accuracyInMeters = 8f;
    [SerializeField] float updateDistanceInMeters = 8f;
    [SerializeField] int timeOutTimeInSeconds = 15;
    [SerializeField] Canvas canvas;
    [SerializeField] Text locationText;
    [SerializeField] Text debugText;
    void Start()
    {
        debugText.text = "Starting Program...";
        Debug.Log("Starting program...");
        StartCoroutine(Initialize(accuracyInMeters, updateDistanceInMeters, timeOutTimeInSeconds));
    }
    void Update()
    {
        if (UnityEngine.Input.location.status == LocationServiceStatus.Running)
        {
            if (Time.realtimeSinceStartup % 2 == 0)
            {
                debugText.text = "Actively tracking location...";
                Debug.Log("Actively tracking location...");
                // Access granted and location value could be retrieved
                locationText.text = "Location: "
                + UnityEngine.Input.location.lastData.latitude + "\n"
                + UnityEngine.Input.location.lastData.longitude + "\n"
                + UnityEngine.Input.location.lastData.altitude + "\n"
                + UnityEngine.Input.location.lastData.horizontalAccuracy + "\n"
                + UnityEngine.Input.location.lastData.timestamp;
                Debug.LogFormat("Location: "
                    + UnityEngine.Input.location.lastData.latitude + " "
                    + UnityEngine.Input.location.lastData.longitude + " "
                    + UnityEngine.Input.location.lastData.altitude + " "
                    + UnityEngine.Input.location.lastData.horizontalAccuracy + " "
                    + UnityEngine.Input.location.lastData.timestamp);
            }
        } 
    }
    void OnApplicationQuit()
    {
        Quit();
    }
    void Quit()
    {
        UnityEngine.Input.location.Stop();
    }
    IEnumerator Initialize(float accuracy = 8f, float updateDistance = 8f, int timeOutTime = 15)
    {
        debugText.text = "Checking permissions...";
        Debug.Log("Checking permissions...");
#if UNITY_EDITOR
        // No permission handling needed in Editor
#elif UNITY_ANDROID
    if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.CoarseLocation)) {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.CoarseLocation);
        }

        // First, check if user has location service enabled
        if (!UnityEngine.Input.location.isEnabledByUser) {
            // TODO Failure
            Debug.LogFormat("Android and Location not enabled");
            yield break;
        }
#elif UNITY_IOS
if (!UnityEngine.Input.location.isEnabledByUser) {
            // TODO Failure
            Debug.LogFormat("IOS and Location not enabled");
            yield break;
        }
#endif
        debugText.text = "Initializing...";
        Debug.Log("Initializing...");

        debugText.text = "Starting GPS service...";
        Debug.Log("Starting GPS service...");
        // Start service before querying location
        UnityEngine.Input.location.Start(8f, 8f);

        // Wait until service initializes
        int maxWait = timeOutTime;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            debugText.text = "Waiting for GPS service to initialize..." + maxWait;
            Debug.Log("Waiting for GPS service to initialize..." + maxWait);
            yield return new WaitForSecondsRealtime(1);
            maxWait--;
        }

        // Editor has a bug which doesn't set the service status to Initializing. So extra wait in Editor.
#if UNITY_EDITOR
        int editorMaxWait = timeOutTime;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Stopped && editorMaxWait > 0)
        {
            debugText.text = "Waiting for GPS service to initialize within editor..." + editorMaxWait;
            Debug.Log("Waiting for GPS service to initialize within editor..." + editorMaxWait);
            yield return new WaitForSecondsRealtime(1);
            editorMaxWait--;
        }
#endif

        // Service didn't initialize in 15 seconds
        if (maxWait < 1)
        {
            // TODO Failure
            debugText.text = "Timed out";
            Debug.LogFormat("Timed out");
            yield break;
        }

        // Connection has failed
        if (UnityEngine.Input.location.status != LocationServiceStatus.Running)
        {
            // TODO Failure
            debugText.text = "Unable to determine device location. Failed with status " + UnityEngine.Input.location.status;
            Debug.LogFormat("Unable to determine device location. Failed with status {0}", UnityEngine.Input.location.status);
            yield break;
        }
        else
        {
            debugText.text = "Location service live. status " + UnityEngine.Input.location.status;
            Debug.LogFormat("Location service live. status {0}", UnityEngine.Input.location.status);
            // Access granted and location value could be retrieved
            locationText.text = "Location: "
                + UnityEngine.Input.location.lastData.latitude + "\n"
                + UnityEngine.Input.location.lastData.longitude + "\n"
                + UnityEngine.Input.location.lastData.altitude + "\n"
                + UnityEngine.Input.location.lastData.horizontalAccuracy + "\n"
                + UnityEngine.Input.location.lastData.timestamp;
            Debug.LogFormat("Location: "
                + UnityEngine.Input.location.lastData.latitude + " "
                + UnityEngine.Input.location.lastData.longitude + " "
                + UnityEngine.Input.location.lastData.altitude + " "
                + UnityEngine.Input.location.lastData.horizontalAccuracy + " "
                + UnityEngine.Input.location.lastData.timestamp);

        var _latitude = UnityEngine.Input.location.lastData.latitude;
        var _longitude = UnityEngine.Input.location.lastData.longitude;
        // TODO success do something with location
        }

    // Stop service if there is no need to query location updates continuously
    }
}
