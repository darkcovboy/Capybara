using System;
using Items;
using Player.Animation;
using UnityEngine;

namespace Player.ItemsAction
{
    public class ItemHandler : MonoBehaviour
    {
        [Header("Dependency")]
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private PlayerItemObserver playerItem;
        [SerializeField] private Transform _itemPointHandler;
        
        public bool HaveItem => CurrentItem != null;
        public Item CurrentItem { get; private set; }

        private void Start()
        {
            CurrentItem = null;
            playerItem.Enter += CheckAndPickItem;
        }

        private void OnValidate()
        {
            if (_animator == null)
                _animator = GetComponent<PlayerAnimator>();
        }

        private void OnDestroy()
        {
            playerItem.Enter -= CheckAndPickItem;
        }

        public void GiveItem()
        {
            CurrentItem.Disconnect();
            CurrentItem = null;
            _animator.SetItem(HaveItem);
        }

        private void CheckAndPickItem(Item item)
        {
            if(HaveItem)
                return;

            CurrentItem = item;
            
            _animator.SetItem(HaveItem);
            CurrentItem.ConnectTo(_itemPointHandler);
        }
    }
}