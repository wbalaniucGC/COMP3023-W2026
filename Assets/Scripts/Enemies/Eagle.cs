using UnityEngine;

public class Eagle : Enemy
{
    public override void TakeDamage()
    {
        Debug.Log("Eagle was defeated!");

        GameManager.AddScore(scoreValue);

        Destroy(gameObject);
    }
}
