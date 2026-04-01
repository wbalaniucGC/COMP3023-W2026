using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2.0f;
    public float waitTime = 1.0f;

    public List<Transform> waypoints = new List<Transform>();

    // Private variables
    private int currentIndex = 0;
    private bool isWaiting = false;

    private void Awake()
    {
        // Safet check to ensure the list exists
        if(waypoints == null || waypoints.Count < 2) return;

        // TO-DO: STUFF
        // Create a hidden container in the hierarchy to keep the scene clean at runtime
        GameObject pathContainer = new GameObject(gameObject.name + "_Path");

        foreach(Transform waypoint in waypoints)
        {
            if(waypoint != null)
            {
                waypoint.SetParent(pathContainer.transform);
            }
        }
    }

    private void Update()
    {
        if(waypoints == null || waypoints.Count < 2 || isWaiting) return;

        Transform target = waypoints[currentIndex];
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Check if I've reached my destination
        if(Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            // I've reached my destination
            // Wait for a bit, then move
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

    // "Sticky" player logic
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            // Set the players parent object to be the platform
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
