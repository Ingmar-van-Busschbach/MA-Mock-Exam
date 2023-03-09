using UnityEngine;
using UnityEngine.UI;
using Generic;

namespace Route
{
    public class SaveRoute : MonoBehaviour
    {
        private Route route = new Route();
        public int trackedCoordinates = 0;
        public int currentCoordinateIndex = 0;

        private void Awake()
        {// Automatically fetch the button this script is attached to, which will serve as the button we use to save routes.
            if (gameObject.TryGetComponent(out Button button))
            {
                button.onClick.AddListener(OnClick);
            }
        }

        private void OnClick()
        {
            SaveCurrentRoute();
        }

        public void ResetRoute()
        {
            route = new Route();
            trackedCoordinates = 0;
            currentCoordinateIndex = 0;
        }

        public void AddRoutePoint(Coordinates coordinate)
        {
            route.routePoints.Add(coordinate);
            trackedCoordinates++;
        }

        public void SaveCurrentRoute()
        {
            string json = JsonUtility.ToJson(route);
            PlayerPrefs.SetString("Route", json);
            PlayerPrefs.Save();
        }

        public void LoadRoute()
        {
            if (!PlayerPrefs.HasKey("Route")) { return; }
            
            string json = PlayerPrefs.GetString("Route");
            route = JsonUtility.FromJson<Route>(json);
            trackedCoordinates = route.routePoints.Count;
        }

        public Coordinates GetCurrentCoordinate()
        {
            if(route.routePoints.Count == 0) { return new Coordinates(-1, -1, -1); }

            return route.routePoints[currentCoordinateIndex];
        }
        
        // Currently returns a bool for debugging reasons.
        public bool CoordinateReached()
        {
            if(currentCoordinateIndex >= route.routePoints.Count - 1)
            {
                OnRouteCompleted();
                return true;
            }

            currentCoordinateIndex++;
            return false;
        }

        public void OnRouteCompleted()
        {// Logic on route completed.

        }
    }
}
