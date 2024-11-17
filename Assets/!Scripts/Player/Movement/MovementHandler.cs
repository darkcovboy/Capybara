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
        private float _defaultMovementSpeed;
        private float _speed;
        private Vector3 _movementDirection;
        private float _currentSpeed;
        private float _velocity;
        private bool _canMove;
        private Transform _initialPosition;

        public void Init(float speed, IInput input, Transform initialPosition)
        {
            _speed = speed;
            _movementInput = input;
            _initialPosition = initialPosition;
            _defaultMovementSpeed = _movementSpeed;
            GetComponent<ForceMover>().Init(initialPosition);
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
            if (_movementInput.GetInputDirection().magnitude > 0)
            {
                float distanceToInitialPosition = Vector3.Distance(transform.position, _initialPosition.position);

                if (distanceToInitialPosition > 0.5f)
                {
                    _movementDirection = (_initialPosition.position - transform.position).normalized;
                }
                else
                {
                    _movementDirection = Vector3.zero;
                }
            }
            else
            {
                _movementDirection = Vector3.zero;
            }

            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            float speed = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z).magnitude;
            _playerAnimator.SetSpeed(speed);
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
            
            if ((_movementDirection.x > 0.1f || _movementDirection.x < -0.1f) || (_movementDirection.z > 0.1f || _movementDirection.z < -0.1f))
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
            targetRotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed *Time.deltaTime);
        }
    }
}