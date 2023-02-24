using System;
using UnityEngine;
using UnityEngine.UI;
using Generic;

namespace Route
{
    public class RouteCreator : MonoBehaviour
    {
        [SerializeField] private string dataPath;
        [SerializeField] [Multiline] private string routeJson;
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
            TrySavePoint();
        }

        private void TrySavePoint()
        {
            if (!GPSService.Instance.GpsServiceEnabled) return;
            LoadRoute(dataPath);
            try
            {
                route = JsonUtility.FromJson<Route>(routeJson);
            }
            catch (Exception e)
            {
                return;
            }
            if (route == null) return;

            Coordinates newPoint = new Coordinates(Input.location.lastData.latitude, Input.location.lastData.longitude, Input.location.lastData.altitude);
            route.routePoints.Add(newPoint);

            SaveRoute(dataPath);
        }

        public void SaveRoute(string path)
        {
            if (route == null) { return; }
            dataPath = path;
            string json = JsonUtility.ToJson(route);
            System.IO.File.WriteAllText(path, json);
        }

        public void LoadRoute(string path)
        {
            dataPath = path;
            routeJson = System.IO.File.ReadAllText(path);
        }
    }
}
