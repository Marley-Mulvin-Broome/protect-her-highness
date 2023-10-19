using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [FormerlySerializedAs("_spawnRate")] [SerializeField]
    private float spawnRate = 1.0f;
    [FormerlySerializedAs("_spawnPointOffset")] [SerializeField]
    private float spawnPointOffset = 0.5f;

    private float _currentSpawnTime;
    
    private float _spawnHorizontalMinimumX;
    private float _spawnHorizontalMaximumX;
    private float _spawnVerticalMinimumY;
    private float _spawnVerticalMaximumY;

    [SerializeField]
    private GameObject _enemyPrefab;

    public enum Orientation
    {
        Top=0,
        Bottom=180,
        Right=90,
        Left=-90,
    }
        
    void Start()
    {
        Camera mainCam = Camera.main;

        if (mainCam == null)
        {
            throw new NullReferenceException(
                "EnemySpawner requires a Camera tagged \"MainCamera\" to be present in the scene.");
        }
            
        // Screen left and right world position edges
        _spawnHorizontalMinimumX = mainCam.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        _spawnHorizontalMaximumX = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        _spawnVerticalMinimumY = mainCam.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        _spawnVerticalMaximumY = mainCam.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;

        _currentSpawnTime = spawnRate;
    }
    
    void Update()
    {
        _handleEnemySpawning();
    }

    private void _handleEnemySpawning()
    {
        _currentSpawnTime -= Time.deltaTime;

        if (_currentSpawnTime <= 0)
        {
            _spawnEnemy();
            _currentSpawnTime = spawnRate;
        }
    }

    private void _spawnEnemy()
    {
        if (Utility.CoinFlip())
        {
            _spawnEnemyHorizontal();
            return;
        }
        
        _spawnEnemyVertical();
    }

    private void _spawnEnemyHorizontal()
    {
        float randomX = Random.Range(_spawnHorizontalMinimumX, _spawnHorizontalMaximumX);
        float y = _spawnVerticalMinimumY;
        Orientation orientation = Orientation.Top;

        if (Utility.CoinFlip())
        {
            y = _spawnVerticalMaximumY;
            orientation = Orientation.Bottom;
        }
        
        Vector3 spawnPosition = new Vector3(randomX, y, 0);
        
        _spawnEnemyAt(
            spawnPosition,
            orientation
            );
        
    }

    private void _spawnEnemyVertical()
    {
        float randomY = Random.Range(_spawnVerticalMinimumY, _spawnVerticalMaximumY);
        float x = _spawnHorizontalMinimumX;
        Orientation orientation = Orientation.Right;

        if (Utility.CoinFlip())
        {
            x = _spawnHorizontalMaximumX;
            orientation = Orientation.Left;
        }
        
        Vector3 spawnPosition = new Vector3(x, randomY, 0);
        
        _spawnEnemyAt(
            spawnPosition,
            orientation
            );
    }

    private void _spawnEnemyAt(Vector3 position, Orientation orientation)
    {
        if (_enemyPrefab is null)
        {
            throw new NullReferenceException("EnemySpawner requires a reference to an enemy prefab.");
        }

        GameObject enemy = GameObject.Instantiate<GameObject>(_enemyPrefab, transform);
        
        enemy.transform.position = position;
        enemy.transform.Rotate(0f, 0f, (float) orientation);
    }
}