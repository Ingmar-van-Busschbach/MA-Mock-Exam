using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent (typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class Spawn : MonoBehaviour
{

    [SerializeField]
    private GameObject prefab;

    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        aRPlaneManager = GetComponent<ARPlaneManager>();
    }


   /// <summary>
   /// This function is called when the object becomes enabled and active.
   /// </summary>
   private void OnEnable()
   {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
   }

   /// <summary>
   /// This function is called when the behaviour becomes disabled or inactive.
   /// </summary>
   private void OnDisable()
   {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= FingerDown;
   }

   private void FingerDown(EnhancedTouch.Finger finger)
   {
        //if (finger.index != 0) return;

        //get coordinates of where the user touched on the screen
        if (aRRaycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            foreach (ARRaycastHit hit in hits)
            {
               Pose pose = hit.pose;
               GameObject obj = Instantiate(prefab, pose.position, pose.rotation);

               if (aRPlaneManager.GetPlane(hit.trackableId).alignment == PlaneAlignment.HorizontalUp)
               {
                    Vector3 position = obj.transform.position;

                    Vector3 cameraPosition = Camera.main.transform.position;

                    Vector3 direction = cameraPosition - position;
                    Vector3 targetRotationEuler = Quaternion.LookRotation(direction).eulerAngles;
                    Vector3 scaledEuler = Vector3.Scale(targetRotationEuler, obj.transform.up.normalized);
                    Quaternion targetRotation = Quaternion.Euler(scaledEuler);
                    obj.transform.rotation = obj.transform.rotation * targetRotation;
               }
            }
        }
   }


}