using UnityEngine;

public static class GameManager
{
    // Backing fields (private so they can't be messed with directly)
    private static int _totalScore = 0;
    private static int _playerLives = 3;
    private static int _startingLives = 3;

    // Public Properties (Read-only for other scripts)
    public static int TotalScore => _totalScore;
    public static int PlayerLives => _playerLives;

    // Logic for Score
    public static void AddScore(int amount)
    {
        if (amount > 0)
        {
            _totalScore += amount;
            Debug.Log($"Score Added! Current Total: {_totalScore}");
        }
    }

    // Logic for Lives
    public static void RemoveLife()
    {
        _playerLives--;

        if (_playerLives <= 0)
        {
            GameOver();
        }
        else
        {
            Debug.Log($"Life Lost. Remaining: {_playerLives}");
        }
    }

    private static void GameOver()
    {
        Debug.Log("--- GAME OVER ---");
        // We can call our reset logic here
        ResetSession();
    }

    public static void ResetSession()
    {
        _totalScore = 0;
        _playerLives = _startingLives;
        Debug.Log("Game Manager has been reset to defaults.");
    }
}