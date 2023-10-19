using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed;
    
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
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

    private void _handlePlayerInput()
    {
        if (!Input.GetMouseButton(0))
        {
            return;
        }
        
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
