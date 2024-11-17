using System;
using Player.Animation;
using Player.ItemsAction;
using Player.Movement;
using Player.PlayerStaticData;
using UnityEngine;

namespace Player
{
    public class Character : MonoBehaviour
    {
        public Action<Character> OnDestroy;

        [SerializeField] private Collider _collider;
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private MovementHandler _movementHandler;
        [SerializeField] private ItemHandler _itemHandler;
        
        private CharacterData _characterData;
        private IInput _input;

        public void Init(CharacterData characterData, IInput input, Transform initialPosition)
        {
            _characterData = characterData;
            _input = input;

            Setup(initialPosition);
        }

        public void UnblockMovement() => _movementHandler.OnMovement();

        public void Win()
        {
            _movementHandler.OffMovement();
            _collider.enabled = false;
            if (_itemHandler.CurrentItem != null)
            {
                _itemHandler.CurrentItem.Disconnect();
            }
            _animator.PlayVictory();
        }

        public void Defeat()
        {
            _movementHandler.OffMovement();
            _itemHandler.DisableTakingItems();
            _collider.enabled = false;
            if (_itemHandler.CurrentItem != null)
            {
                _itemHandler.CurrentItem.Disconnect();
            }
            _animator.PlayDeath();
            OnDestroy?.Invoke(this);
        }

        private void Setup(Transform initialPosition)
        {
            _movementHandler.Init(_characterData.Speed, _input, initialPosition);
        }
    }
}