using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;


public class ARTapToPlaceObject : MonoBehaviour
{

    private ARSessionOrigin arOrigin;
    private Pose placementPose;
    
    // Start is called before the first frame update
    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint (new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        ARRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);
    }
}
