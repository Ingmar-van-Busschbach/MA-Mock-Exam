using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class SpawnPrefab : MonoBehaviour
{

    public GameObject Animal0;
    public GameObject Animal1;
    public GameObject Animal2;
    public GameObject Animal3;

    // List of prefabs to instantiate 
    public GameObject[] Tracks;

     // Keep dictionary array of created prefabs
    private readonly Dictionary<string, GameObject> _instantiatedPrefabs = new Dictionary<string, GameObject>();
    
    void InstantiateAnimal1()
    {
        // Now loop over the array of prefabs
            foreach (var curPrefab in Tracks) {

                // Instantiate the prefab
                    var newPrefab = Instantiate(curPrefab);


                    // Instantiate the prefab, parenting it to something, this is merely reference code and should not be uncommented or removed without my approval please and thanks
                    //var newPrefab = Instantiate(curPrefab, xxx.transform);

                    // Add the created prefab to our array
                    _instantiatedPrefabs[curPrefab.name] = newPrefab;
            }

        Instantiate(Animal1);
    }

    
    void Destroy()
    {


        // If the AR subsystem has given up looking for a tracked image
        foreach (var curPrefab in Tracks) {
            // Destroy its prefab
            Destroy(_instantiatedPrefabs[curPrefab.name]);
            // Also remove the instance from our array
            _instantiatedPrefabs.Remove(curPrefab.name);
            // Or, simply set the prefab instance to inactive
            //_instantiatedPrefabs[trackedImage.referenceImage.name].SetActive(false);
        }

        //Destroy(Animal1);
    }
}
