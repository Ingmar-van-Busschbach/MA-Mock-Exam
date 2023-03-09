using System;
using UnityEngine;
using UnityEngine.UI;

namespace Route
{
    public class RouteButton : MonoBehaviour
    {

        private void Awake()
        {
            if (gameObject.TryGetComponent(out Button button))
            {
                button.onClick.AddListener(OnClick);
            }
        }

        private void OnClick()
        {
            TryStartRoute();
        }

        private void TryStartRoute()
        {
            //Before we can start we have to make sure we have GPS permission.
            GPSService.Instance.TryStartingLocationServices(() =>
            {
                Debug.Log("Starting route...");
            }, () =>
            {
                Debug.Log("Lacking required GPS permissions!");
            }, () =>
            {
                Debug.Log("Unspecified error occurred!");
            });
        }
    }
}