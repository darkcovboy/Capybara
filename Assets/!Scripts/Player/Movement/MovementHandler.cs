using System;
using Player.Animation;
using Player.ItemsAction;
using UnityEngine;
using UnityEngine.AI;

namespace Player.Movement
{
    public class MovementHandler : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private PlayerAnimator _playerAnimator;

        [Header("Movement Settings")]
        [SerializeField] private float _movementSpeed = 350f;
        [SerializeField] private float _smoothTime = 0.2f;
        [SerializeField] private float _rotationSpeed = 400f;
 
        private IInput _movementInput;
        private float _speed;
        private Vector3 _movementDirection;
        private float _currentSpeed;
        private float _velocity;
        private bool _canMove;

        public void Init(float speed, IInput input)
        {
            _speed = speed;
            _movementInput = input;
        }

        private void OnValidate()
        {
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();

            if (_playerAnimator == null)
                _playerAnimator = GetComponent<PlayerAnimator>();
        }

        private void Awake()
        {
            _rigidbody.freezeRotation = true;
        }

        private void Update()
        {
            _movementDirection = _movementInput.GetInputDirection();
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            _playerAnimator.SetSpeed(_currentSpeed);
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void OnMovement()
        {
            _canMove = true;
            _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            _rigidbody.isKinematic = false;
        }

        public void OffMovement()
        {
            _canMove = false;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            _rigidbody.interpolation = RigidbodyInterpolation.None;
            _rigidbody.isKinematic = true;
        }

        private void Move()
        {
            if(!_canMove)
                return;
            
            if (_movementDirection.magnitude > 0f)
            {
                HandleRotation(_movementDirection);
                HandleHorizontalMovement(_movementDirection);
                SmoothSpeed(_movementDirection.magnitude);
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
            Vector3 velocity = movement * _movementSpeed * Time.fixedDeltaTime;
            _rigidbody.velocity = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);
        }
        
        private void HandleRotation(Vector3 value)
        {
            var targetRotation = Quaternion.LookRotation(value);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed *Time.deltaTime);
        }
    }
}