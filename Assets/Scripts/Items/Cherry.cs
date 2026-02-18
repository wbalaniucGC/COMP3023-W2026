using UnityEngine;

public class Cherry : MonoBehaviour
{
    public GameObject itemFeedbackPrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Instantiate the item feedback effect
            // Safety check!
            if (itemFeedbackPrefab != null)
            {
                Instantiate(itemFeedbackPrefab, transform.position, Quaternion.identity);
            }

            // Remove the Cherry
            Destroy(gameObject);
        }
    }
}
