using System;
using UnityEngine;

namespace Player.Movement
{
    public class ForceMover : MonoBehaviour
    {
        [SerializeField] private float _distanceThreshold = 5f;
        [SerializeField] private float _speedMultiplier = 2f; 
        
        private Transform _initialPosition;
        private Rigidbody _rigidbody;

        public void Init(Transform initialPosition)
        {
            _initialPosition = initialPosition;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            float distance = Vector3.Distance(transform.position, _initialPosition.position);
            
            if (distance > _distanceThreshold)
            {
                Vector3 direction = (_initialPosition.position - transform.position).normalized;
                _rigidbody.velocity = direction * _speedMultiplier;
            }
        }
    }
}