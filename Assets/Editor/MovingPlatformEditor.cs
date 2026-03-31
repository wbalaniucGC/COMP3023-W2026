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

        // Safety Check: If the list is null for any reason, initialize it now
        if (platform.waypoints == null)
        {
            platform.waypoints = new List<Transform>();
        }

        GUILayout.Space(10);
        if (GUILayout.Button("Add Waypoint"))
        {
            GameObject newWaypoint = new GameObject("Waypoint_" + platform.waypoints.Count);

            // Safety check for position calculation
            Vector3 spawnPos = platform.transform.position;
            if (platform.waypoints.Count > 0 && platform.waypoints[platform.waypoints.Count - 1] != null)
            {
                spawnPos = platform.waypoints[platform.waypoints.Count - 1].position + Vector3.right;
            }

            newWaypoint.transform.position = spawnPos;
            newWaypoint.transform.SetParent(platform.transform);

            Undo.RegisterCreatedObjectUndo(newWaypoint, "Create Waypoint");
            platform.waypoints.Add(newWaypoint.transform);

            // Mark the object as "dirty" so Unity knows to save the new list data
            EditorUtility.SetDirty(platform);
        }

        if (GUILayout.Button("Clear All Waypoints"))
        {
            foreach (Transform t in platform.waypoints)
            {
                // Only destroy if the reference isn't already null
                if (t != null)
                {
                    Undo.DestroyObjectImmediate(t.gameObject);
                }
            }
            platform.waypoints.Clear();
            EditorUtility.SetDirty(platform);
        }
    }
}