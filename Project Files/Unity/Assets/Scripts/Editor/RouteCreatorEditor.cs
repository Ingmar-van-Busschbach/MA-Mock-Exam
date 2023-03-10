using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Route
{
    [ExecuteInEditMode]
    [CustomEditor(typeof(RouteCreator))]
    [CanEditMultipleObjects]
    public class RouteCreatorEditor : Editor
    {
        RouteCreator routeCreator;

        public override void OnInspectorGUI()
        {
            routeCreator = (RouteCreator)target;

            if (GUILayout.Button("Save Route Data"))
            {
                string path = EditorUtility.SaveFilePanel("Save new", Application.dataPath, "RouteData", "json");
                routeCreator.SaveRoute(path);
            }
            if (GUILayout.Button("Load Route Data"))
            {
                string path = EditorUtility.OpenFilePanel("Load new ", Application.dataPath, "json");
                if (string.IsNullOrEmpty(path)) { return; }
                routeCreator.LoadRoute(path);
            }

            DrawDefaultInspector();
        }
    }
}
