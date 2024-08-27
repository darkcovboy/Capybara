using System;
using System.Collections;
using System.Collections.Generic;
using Const;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Rigidbody _rigidbody;
    
    [Header("Movement settings")]
    [SerializeField] float _moveSpeed = 6f;
    [SerializeField] float _smoothTime = 0.2f;
    [SerializeField] private float _rotationSpeed = 15f;
    private Vector3 _movement;
    private float _velocity;
    private float _currentSpeed;

    private void Awake()
    {
        _rigidbody.freezeRotation = true;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis(UsefulConst.HorizontalAxis);
        float verticalInput = Input.GetAxis(UsefulConst.VerticalAxis);
        _movement = new Vector3(horizontalInput, 0f, verticalInput);
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (_movement.magnitude > 0f)
        {
            HandleRotation(_movement);
            HandleHorizontalMovement(_movement);
            SmoothSpeed(_movement.magnitude);
        }
        else
        {
            SmoothSpeed(0f);
            _rigidbody.velocity = new Vector3(0f, _rigidbody.velocity.y, 0f);
        }
    }

    private void SmoothSpeed(float value)
    {
        _currentSpeed = Mathf.SmoothDamp(_currentSpeed, value, ref _velocity, _smoothTime);
    }

    private void HandleHorizontalMovement(Vector3 movement)
    {
        Vector3 velocity = movement * _moveSpeed * Time.fixedDeltaTime;
        _rigidbody.velocity = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);
    }

    private void HandleRotation(Vector3 value)
    {
        var targetRotation = Quaternion.LookRotation(value);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed *Time.deltaTime);
    }
}
