using UnityEngine;

public static class GameManager
{
    // Backing fields
    private static int _totalScore = 0;
    private static int _playerLives = 3;
    private static int _startingLives = 3;

    // Public properties (Read)
    public static int TotalScore
    {
        get
        {
            return _totalScore;
        }
    }
    public static int PlayerLives => _playerLives;

    // Add to the score
    public static void AddScore(int amount)
    {
        if(amount > 0 && amount < 1000)
        {
            _totalScore += amount;
            Debug.Log($"Score Added! Current Total: {_totalScore}");
        }
    }

    // Remove a life
    public static void RemoveLife()
    {
        _playerLives--;

        if(_playerLives <= 0)
        {
            GameOver();
        }
        else
        {
            Debug.Log($"Life Lost! Lives remaining: {_playerLives}");
        }
    }

    // Game Over
    private static void GameOver()
    {
        Debug.Log("--- GAME OVER ---");
        ResetSession();
    }

    // Reset Session
    private static void ResetSession()
    {
        _totalScore = 0;
        _playerLives = _startingLives;
        Debug.Log("Game manager has reset to default values");   
    }
}
