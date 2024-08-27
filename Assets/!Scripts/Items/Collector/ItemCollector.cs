using System;
using LevelStates;
using Player.Counter;
using UnityEngine;
using Zenject;

namespace Items.Collector
{
    public class ItemCollector : MonoBehaviour, IItemCollected, IWinNotification
    {
        public event Action<int> OnRewardCollected;
        public event Action<int, int> OnItemCollected;

        public event Action OnGameWin;

        [Header("Prefab dependencies")]
        [SerializeField] private CollectorItemObserver _collectorItemObserver;

        [SerializeField] private Transform _pointForItem;

        private MoneyCounter _moneyCounter;
        private int _amountOfCollected = 0;
        private int _maxItems;

        public int MAXItems => _maxItems;


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
            OnItemCollected?.Invoke(_amountOfCollected, _maxItems);
            
            if (_amountOfCollected >= _maxItems)
            {
                OnGameWin?.Invoke();
            }
        }

        private int CalculateItems() => FindObjectsOfType<Item>().Length;
    }
}