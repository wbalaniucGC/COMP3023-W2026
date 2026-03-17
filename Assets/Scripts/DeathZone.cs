using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class DeathZone : MonoBehaviour
{
    [Header("Gizmo Settings")]
    [SerializeField] private Color zoneColor = new Color(1f, 0f, 0f, 0.3f);

    private BoxCollider2D bc;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // Tell the static manager we lost a life
            GameManager.RemoveLife();

            // Restart the level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void OnDrawGizmos()
    {
        if(bc == null) bc = GetComponent<BoxCollider2D>();

        if(bc != null)
        {
            // Set the color for the Gizmo
            Gizmos.color = zoneColor;

            // Draw a solid cube that matches the Collider's bounds
            // We use bc.bounds.center and bc.bounds.size to ensure it's accurate
            Gizmos.DrawCube(bc.bounds.center, bc.bounds.size);

            // Optional: Draw a wireframe outline to make the edges sharper
            Gizmos.color = new Color(zoneColor.r, zoneColor.g, zoneColor.b, 1f);
            Gizmos.DrawWireCube(bc.bounds.center, bc.bounds.size);
        }
    }
}
