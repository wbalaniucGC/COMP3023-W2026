using UnityEngine;

public class Opossum : Enemy
{
    protected override void Awake()
    {
        // base.Awake handles finidhg the sprite renderer and detaching the waypoints
        base.Awake();

        // Opossum-specific defaults
        // Starts facing left
        direction = -1;
        if(sRend != null)
        {
            sRend.flipX = false;
        }
    }

    // This is the implementation of the abstract method from the base class
    public override void TakeDamage()
    {
        Debug.Log("Opossum was stomped!");

        Destroy(gameObject);
    }
}
