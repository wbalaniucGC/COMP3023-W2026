using UnityEngine;

public class Eagle : Enemy
{
    public override void TakeDamage()
    {
        Debug.Log("Eagle was defeated!");

        GameManager.AddScore(scoreValue);

        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
