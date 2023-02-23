using UnityEditor;
using UnityEngine;

namespace Route
{
    [ExecuteInEditMode]
    [CustomEditor(typeof(RouteButton))]
    [CanEditMultipleObjects]
    public class RouteButtonEditor : Editor
    {
        RouteButton routeButton;

        public override void OnInspectorGUI()
        {
            routeButton = (RouteButton)target;

            if (GUILayout.Button("Save Route Data"))
            {
                string path = EditorUtility.SaveFilePanel("Save new", Application.dataPath, "RouteData", "json");
                routeButton.SaveRoute(path);
            }
            if (GUILayout.Button("Load Route Data"))
            {
                string path = EditorUtility.OpenFilePanel("Load new ", Application.dataPath, "json");
                if (string.IsNullOrEmpty(path)) { return; }
                routeButton.LoadRoute(path);
            }

            DrawDefaultInspector();
        }
    }
}
