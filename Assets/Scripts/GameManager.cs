using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameEvents Events { get; private set; }
    public static GameManager Instance;

    public void Start()
    {
        if (Instance != null)
        {
            throw new Exception("GameManager already exists.");
        }
        
        Events = new GameEvents();
        Instance = this;

        Events.OnPlayerDied += () =>
        {
            Events.GameOverEvent();
        };
    }
    
    public void OnDestroy()
    {
        Instance = null;
    }
}