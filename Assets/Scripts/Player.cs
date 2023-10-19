using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float maximumStationaryTime = 1.5f;
    
    private bool _isMoving = false;
    private float _remainingTimeStationary = 1.5f;
    private Camera _mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        _handlePlayerInput();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _die();
        }
    }

    private void _die()
    {
        GameManager.Instance.Events.PlayerDiedEvent();
        GameObject.Destroy(gameObject);
    }

    private void _doXMovement(float direction)
    {
        if (direction < 0)
        {
            transform.position += Vector3.right * (speed * Time.deltaTime);
        }
        else if (direction > 0)
        {
            transform.position += Vector3.left * (speed * Time.deltaTime);
        }
    }

    private void _doYMovement(float direction)
    {
        if (direction < 0)
        {
            transform.position += Vector3.up * (speed * Time.deltaTime);
        }
        else if (direction > 0)
        {
            transform.position += Vector3.down * (speed * Time.deltaTime);
        }
    }

    private void _handleStationaryMovement()
    {
        if (_isMoving)
        {
            _remainingTimeStationary = maximumStationaryTime;
            _isMoving = false;
        }
            
        _remainingTimeStationary -= Time.deltaTime;
            
        if (_remainingTimeStationary <= 0)
        {
            _die();
        }
    }

    private void _handlePlayerInput()
    {
        if (!Input.GetMouseButton(0))
        {
            _handleStationaryMovement();
            return;
        }

        _isMoving = true;
        
        Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        // Move away from mouse position depending on orientation of self
        // -> mouse below: move up
        // -> mouse above: move down
        // -> mouse left: move right
        // -> mouse right: move left
        
        Vector3 direction = mousePosition - transform.position;

        if (Math.Abs(direction.x) > Math.Abs(direction.y))
        {
            _doXMovement(direction.x);   
        }
        else
        {
            _doYMovement(direction.y);
        }

    }
}
