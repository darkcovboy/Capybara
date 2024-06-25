using System;
using Player.Counter;
using UnityEngine;
using Zenject;

namespace Items.Collector
{
    public class ItemCollector : MonoBehaviour
    {
        public Action<int> OnRewardCollected;

        [Header("Prefab dependencies")]
        [SerializeField] private CollectorItemObserver _collectorItemObserver;
        [SerializeField] private Transform _pointForItem;

        private MoneyCounter _moneyCounter;
        private int _amountOfCollected = 0;
        private int _maxItems;
        
        [Inject]
        public void Constructor(MoneyCounter moneyCounter)
        {
            _moneyCounter = moneyCounter;
            _maxItems = CalculateItems();
        }

        private void Start()
        {
            _collectorItemObserver.Enter += CollectItem;
        }

        private void OnDestroy()
        {
            _collectorItemObserver.Enter -= CollectItem;
        }

        private void CollectItem(Item item)
        {
            _moneyCounter.Add(item.ItemData.Reward);
            OnRewardCollected?.Invoke(item.ItemData.Reward);
            item.ConnectTo(_pointForItem, true);

            _amountOfCollected++;
            
            if (_amountOfCollected >= _maxItems)
            {
                //Мы победили
            }
        }

        private int CalculateItems() => FindObjectsOfType<Item>().Length;
    }
}