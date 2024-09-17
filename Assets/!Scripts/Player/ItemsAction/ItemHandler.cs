using System;
using Items;
using Player.Animation;
using UnityEngine;

namespace Player.ItemsAction
{
    public class ItemHandler : MonoBehaviour
    {
        public event Action<RewardType?> OnItemCollected; 
        
        [Header("Dependency")]
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private PlayerItemObserver _playerItem;
        [SerializeField] private Transform _itemPointHandler;
        
        public bool HaveItem => CurrentItem != null;
        public Item CurrentItem { get; private set; }

        private void Start()
        {
            CurrentItem = null;
            _playerItem.Enter += CheckAndPickItem;
        }

        private void OnValidate()
        {
            if (_animator == null)
                _animator = GetComponent<PlayerAnimator>();
        }

        private void OnDestroy()
        {
            _playerItem.Enter -= CheckAndPickItem;
        }

        public void DisableTakingItems()
        {
            _playerItem.gameObject.SetActive(false);
        }

        public void GiveItem()
        {
            CurrentItem.Disconnect();
            CurrentItem = null;
            _animator.SetItem(HaveItem);
            OnItemCollected?.Invoke(null);
        }

        private void CheckAndPickItem(Item item)
        {
            if(HaveItem)
                return;

            CurrentItem = item;
            OnItemCollected?.Invoke(CurrentItem.RewardType);
            
            _animator.SetItem(HaveItem);
            CurrentItem.ConnectTo(_itemPointHandler);
        }
    }
}