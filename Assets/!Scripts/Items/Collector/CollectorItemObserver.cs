using System;
using Player.ItemsAction;
using UnityEngine;

namespace Items.Collector
{
    [RequireComponent(typeof(Collider))]
    public class CollectorItemObserver : MonoBehaviour, ITriggerObserver<Item>
    {
        public event Action<Item> Enter;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ItemHandler itemHandler) && itemHandler.HaveItem)
            {
                Item currentItem = itemHandler.CurrentItem;
                itemHandler.GiveItem();
                Enter?.Invoke(currentItem);
            }
        }
    }
}