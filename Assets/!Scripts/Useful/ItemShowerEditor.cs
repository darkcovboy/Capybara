using Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DefaultNamespace.Useful
{
    public class ItemShowerEditor : MonoBehaviour
    {
        [Header("Item Count (Editor Only)")]
        [SerializeField] private int _itemCount; 

        private void OnValidate()
        {
            UpdateItemCount();
        }

        private void UpdateItemCount()
        {
            _itemCount = transform.GetComponentsInChildren<Item>().Length;
        }
    }
}