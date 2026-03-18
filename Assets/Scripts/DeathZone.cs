using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    [Header("Gizmo Settings")]
    public Color zoneColor = new Color(1.0f, 0.0f, 0.0f, 0.3f);

    private BoxCollider2D bc;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index);
        }
    }

    public void OnDrawGizmos()
    {
        // Safety check
        if(bc == null)
        {
            bc = GetComponent<BoxCollider2D>();
        }

        if(bc != null)
        {
            // Set the colour of the gizmo
            Gizmos.color = zoneColor;

            Gizmos.DrawCube(bc.bounds.center, bc.bounds.size);
        }
    }
}
