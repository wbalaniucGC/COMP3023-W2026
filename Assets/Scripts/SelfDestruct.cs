using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    /*
    public float delay = 1f;

    private void Start()
    {
        Destroy(gameObject, delay);
    }
    */

    public void DestroyMyself()
    {
        Destroy(gameObject);
    }
}
