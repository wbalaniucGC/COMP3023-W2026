using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MovingPlatform))]
public class MovingPlatformEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MovingPlatform platform = (MovingPlatform)target;

        // Safety check
        if(platform.waypoints == null)
        {
            platform.waypoints = new List<Transform>();
        }

        // GUI Code
        GUILayout.Space(10);
        if(GUILayout.Button("Add Waypoint"))
        {
            GameObject newWaypointObject = new GameObject("Waypoint_" + platform.waypoints.Count);

            // Safety check for position
            Vector3 spawnPos = platform.transform.position;
            if(platform.waypoints.Count > 0 && platform.waypoints[platform.waypoints.Count - 1] != null)
            {
                spawnPos = platform.waypoints[platform.waypoints.Count - 1].position + Vector3.right;
            }

            newWaypointObject.transform.position = spawnPos;
            newWaypointObject.transform.SetParent(platform.transform);

            Undo.RegisterCreatedObjectUndo(newWaypointObject, "Create Waypoint");

            platform.waypoints.Add(newWaypointObject.transform);

            // Mark the object as "dirty" so unity knows to save the new list data
            EditorUtility.SetDirty(platform);
        }
        if(GUILayout.Button("Clear all waypoints"))
        {
            foreach(Transform t in platform.waypoints)
            {
                // Only destroy if they aren't already null
                if(t != null)
                {
                    Undo.DestroyObjectImmediate(t.gameObject);
                }
            }

            platform.waypoints.Clear();
            EditorUtility.SetDirty(platform);
        }
    }
}
