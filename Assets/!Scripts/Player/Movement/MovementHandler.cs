using System;
using Player.Animation;
using Player.ItemsAction;
using UnityEngine;
using UnityEngine.AI;

namespace Player.Movement
{
    [RequireComponent(typeof(CharacterController), typeof(ItemHandler), typeof(PlayerAnimator))]
    public class MovementHandler : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private ItemHandler _itemHandler;
        [SerializeField] private PlayerAnimator _playerAnimator;

        private IInput _movementInput;
        private float _speed;

        private void OnValidate()
        {
            if (_characterController == null)
                _characterController = GetComponent<CharacterController>();

            if (_playerAnimator == null)
                _playerAnimator = GetComponent<PlayerAnimator>();

            if (_itemHandler == null)
                _itemHandler = GetComponent<ItemHandler>();
        }

        private void Update()
        {
            Vector3 direction = _movementInput.GetInputDirection();
            Move(direction);
        }

        public void Init(float speed, IInput input)
        {
            _speed = speed;
            _movementInput = input;
        }

        private void Move(Vector3 direction)
        {
            Vector3 moveDirection = direction * _speed;
            _characterController.Move(moveDirection * Time.deltaTime);

            if (direction != Vector3.zero)
            {
                gameObject.transform.forward = direction;
            }

            
            _playerAnimator.SetSpeed(_characterController.velocity.magnitude);
        }
    }
}