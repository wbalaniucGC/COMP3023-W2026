using UnityEngine;
using System.Collections.Generic;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2.0f;
    public float waitTime = 1.0f;

    [HideInInspector] // We hide this because the Custom Editor will manage it
    public List<Transform> waypoints = new List<Transform>();

    private int currentIndex = 0;
    private bool isWaiting = false;

    private void Awake()
    {
        // Safety check to ensure the list exists
        if (waypoints == null || waypoints.Count == 0) return;

        // Create a hidden container in the hierarchy to keep the scene clean at runtime
        GameObject pathContainer = new GameObject(gameObject.name + "_Path");

        foreach (Transform waypoint in waypoints)
        {
            if (waypoint != null)
            {
                // Move the waypoint out from under the platform
                waypoint.SetParent(pathContainer.transform);
            }
        }
    }

    private void Update()
    {
        // Safety Check: Don't run if we don't have at least 2 points
        if (waypoints == null || waypoints.Count < 2 || isWaiting) return;

        // Safety Check: Ensure the current waypoint hasn't been deleted in the hierarchy
        if (waypoints[currentIndex] == null)
        {
            Debug.LogError($"Waypoint at index {currentIndex} is missing!");
            return;
        }

        Transform target = waypoints[currentIndex];
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            StartCoroutine(WaitAtWaypoint());
        }
    }

    private System.Collections.IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        currentIndex = (currentIndex + 1) % waypoints.Count;
        isWaiting = false;
    }

    // This handles the "Sticky" player logic we discussed earlier
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(null);
    }
}