using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private const string LEVEL1 = "Level 1";

    public enum GameScene
    {
        MainMenu = 0,
        Level1 = 1
    }

    // Start button
    // Load the next scene available to me
    public void StartGame()
    {
        // SceneManager.LoadScene(LEVEL1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // SceneManager.LoadScene((int)GameScene.MainMenu);
    }


    // Quit button
    // Exit the application. 
    public void QuitGame()
    {
        Application.Quit(); // Quits my game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
