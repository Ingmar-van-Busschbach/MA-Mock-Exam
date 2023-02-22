using System;
using UnityEngine;
using UnityEngine.UI;

namespace Route
{
    public class RouteButton : MonoBehaviour
    {
        [SerializeField][Multiline] private string routeJson;
        private Route route;

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
            if (route == null)
            {
                //try set route from routeJson
                try
                {
                    route = JsonUtility.FromJson<Route>(routeJson);
                }
                catch (Exception e)
                {
                    return;
                }
            }

            if (route == null) return;

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
        public void SaveRoute(string path)
        {
            Route serializedRoute = new Route();
            serializedRoute.routeName = "Test123";
            string json = JsonUtility.ToJson(serializedRoute);
            System.IO.File.WriteAllText(path, json);
        }
        public void LoadRoute(string path)
        {
            routeJson = System.IO.File.ReadAllText(path);
        }
    }
}