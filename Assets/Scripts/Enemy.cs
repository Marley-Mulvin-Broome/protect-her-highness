using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    [FormerlySerializedAs("_speed")] [SerializeField]
    private float speed = 1.5f;

    public Vector3 Direction { get; set; }
    
    void Update()
    {
        _handleEnemyMovement();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void _handleEnemyMovement()
    {
        transform.position += Direction * (speed * Time.deltaTime);
    }
}