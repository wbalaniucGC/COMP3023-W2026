using UnityEngine;
using TMPro;    // Modern Unity UI

public class ResourceManager : MonoBehaviour
{
    // Singleton Manager
    public static ResourceManager Instance;
    public TextMeshProUGUI cherryText;

    private int cherrysCollected = 0;

    private void Awake()
    {
        // Setup Singleton Pattern
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddCherry()
    {
        cherrysCollected += 100;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if(cherryText != null)
        {
            cherryText.text = cherrysCollected.ToString();
        }
        else
        {
            Debug.LogWarning("ResourceManager: No TextMeshPro reference assigned");
        }
    }
}
