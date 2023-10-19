
public class GameEvents
{
    public delegate void GameOver();
    public delegate void PlayerDied();
    
    public event GameOver OnGameOver;
    public event PlayerDied OnPlayerDied;
    
    public void GameOverEvent()
    {
        OnGameOver?.Invoke();
    }
    
    public void PlayerDiedEvent()
    {
        OnPlayerDied?.Invoke();
    }
}