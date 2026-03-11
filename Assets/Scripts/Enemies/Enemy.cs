using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *  Enemy will move between waypoints. When the enemy arrives at a waypoint, it will wait, then move
 *  to the next waypoint. 
 */ 
public abstract class Enemy : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2.0f;              // Movement speed
    public float waitTime = 1.0f;           // How long do I wait at the waypoint
    public float arrivalThreshold = 0.1f;   // How close to the waypoint before I have "arrived"

    protected List<Transform> waypoints = new List<Transform>();        // Collection of waypoints
    protected int currentIndex = 0;                                     // Current waypoint
    protected bool isWaiting = false;                                   // Am I waiting for "waitTime" at the waypoint?
    protected int direction = -1;                                       // -1 is for left, 1 is for right
    protected SpriteRenderer sRend;                                     // Reference to flip the sprite

    protected virtual void Awake()
    {
        // Initialization
        // Find the SpriteRenderer
        sRend = GetComponentInChildren<SpriteRenderer>();

        // Find the "Path" child and detach waypoints so they are still in the world.
        Transform pathContainer = transform.Find("Path");

        // Safety check
        if(pathContainer != null)
        {
            // If my path container exists!
            // Cycle through all child objects of my path container and add it to my waypoints.
            foreach (Transform child in pathContainer)
            {
                waypoints.Add(child);
            }

            // Detach the Path from the enemy
            pathContainer.SetParent(null);
        }

        // Initialize position at my first waypoint
        if(waypoints.Count > 0)
        {
            transform.position = waypoints[0].position; // Sets the enemy position to the first waypoint position
        }
    }

    protected virtual void Update()
    {
        // Don't move if we don't have enough waypoints or we are waiting.
        if(waypoints.Count < 2 || isWaiting)
        {
            return;
        }

        HandleMovement();
    }

    // Move to the next waypoint
    protected virtual void HandleMovement()
    {
        Transform target = waypoints[currentIndex]; // Save my current target waypoint

        // Create a horizonal-only target to ignore waypoint height.
        Vector2 horizontalTarget = new Vector2(target.position.x, transform.position.y);
        
        // Use below if your game is a top-down game. Use above if your game is a 2d platformer.
        // Vector2 horizontalTarget = new Vector2(target.position.x, target.position.y);

        // Move the object toward the waypoint.
        transform.position = Vector2.MoveTowards(transform.position, horizontalTarget, speed * Time.deltaTime);

        // Check distance only on the x axis to trigger the next point
        if(Mathf.Abs(transform.position.x - target.position.x) < arrivalThreshold)
        {
            // Reached my destination. Wait at the waypoint
            StartCoroutine(WaitAtWaypoint());
        }
    }

    protected virtual IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        // After waiting for "waitTime" seconds
        currentIndex = (currentIndex + 1) % waypoints.Count;    // Cycles through waypoints

        UpdateDirection();
        isWaiting = false;
    }

    protected virtual void UpdateDirection()
    {
        // Determine if the next waypoint is to the right or left of the current position
        direction = (waypoints[currentIndex].position.x > transform.position.x) ? 1 : -1;   // Ternary 

        // Flip the sprite as needed
        // Safety check!
        if(sRend != null)
        {
            // Flip the sprite based on the calculate direction
            sRend.flipX = (direction == 1);
        }
    }

    public abstract void TakeDamage();

    protected virtual void OnDrawGizmos()
    {
        // Visualize the path and turn zones in the Unity editor
        Transform path = transform.Find("Path");

        // Safety check!
        if (path == null || path.childCount < 2) return;

        for(int i = 0; i < path.childCount; i++)
        {
            Vector3 pos = path.GetChild(i).position;

            // Draw a vertical line to show the "Horizontal Turn Zone"
            Gizmos.color = Color.purple;
            Gizmos.DrawLine(pos + Vector3.up * 2, pos + Vector3.down * 2);

            // Draw the red path lines between waypoints
            Gizmos.color = Color.red;
            Vector3 next = path.GetChild((i + 1) % path.childCount).position;
            Gizmos.DrawLine(pos, next);

            // Draw a small wire sphere at the point
            Gizmos.DrawWireSphere(pos, 0.2f);
        }
    }
}
