using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [Header("Parallax Settings")]
    [Range(0, 1)]
    public float parallaxStrength;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        // CRITICAL: You must capture the initial position to avoid NaN errors
        lastCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        // Apply movement: 1 - strength means:
        // strength 1 = 0 movement (static)
        // strength 0 = full movement (follows camera)
        transform.position += new Vector3(deltaMovement.x * (1 - parallaxStrength), deltaMovement.y * (1 - parallaxStrength), 0);

        lastCameraPosition = cameraTransform.position;
    }
}