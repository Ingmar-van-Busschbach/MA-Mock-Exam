using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent (typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    //[SerializeField] private GameObject prefab;



    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private List<GameObject> instantiatedObjects = new List<GameObject>();

    // Start is called before the first frame update
    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        aRPlaneManager = GetComponent<ARPlaneManager>();
    }

    public void SpawnPrefab()
    {
         //if (finger.index != 0) return;
    
         //get coordinates of where the user touched on the screen
         if (!aRRaycastManager.Raycast(Camera.main.transform.forward, hits, TrackableType.PlaneWithinPolygon))
         {
             return;
         }
         foreach (ARRaycastHit hit in hits)
         {
              Pose pose = hit.pose;
              GameObject obj = Instantiate(prefabs[0], pose.position, pose.rotation);
             instantiatedObjects.Add(obj);
              
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

    public void SpawnPrefabImportant()
    {
        //if (finger.index != 0) return;

        //get coordinates of where the raycast hit
        if (!aRRaycastManager.Raycast(Camera.main.transform.forward, hits, TrackableType.PlaneWithinPolygon))
        {
            return;
        }
        foreach (ARRaycastHit hit in hits)
        {
            Pose pose = hit.pose;
            GameObject obj = Instantiate(prefabs[1], pose.position, pose.rotation);
            instantiatedObjects.Add(obj);

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
    

    public void DestroyPrefab()
   {
     
          foreach (GameObject instantiatedObject in instantiatedObjects)
          {
               Debug.Log("deleted");
               Destroy(instantiatedObject);
          }
   }


}
