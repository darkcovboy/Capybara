using System;
using Items;
using UnityEngine;

namespace Player.ItemsAction
{
    [RequireComponent(typeof(Collider))]
    public class PlayerItemObserver : MonoBehaviour, ITriggerObserver<Item>
    {
        public event Action<Item> Enter;
        
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Item item))
            {
                Enter?.Invoke(item);
            }
        }
    }
}