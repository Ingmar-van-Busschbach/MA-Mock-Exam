using Generic;
using UnityEngine.Events;

namespace Route
{
    public class RoutePoint
    {
        public string pointName = "default";
        public Coordinates Coordinates;
        
        public bool HasTriggered = false;
        public UnityEvent onPointReached = new UnityEvent();
    }
}