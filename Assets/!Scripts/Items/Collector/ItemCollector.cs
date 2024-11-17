using System;
using LevelStates;
using Player.Counter;
using SaveSystem;
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
        private SaveManager _saveManager;
        private int _amountOfCollected = 0;
        private int _maxItems;

        public int MAXItems => _maxItems;


        [Inject]
        public void Constructor(MoneyCounter moneyCounter, SaveManager saveManager)
        {
            _moneyCounter = moneyCounter;
            _saveManager = saveManager;
        }

        private void Start()
        {
            _collectorItemObserver.Enter += CollectItem;
        }

        private void OnDestroy()
        {
            _collectorItemObserver.Enter -= CollectItem;
        }

        public void CalculateItems()
        {
            _maxItems = (int)(FindObjectsOfType<Item>().Length * 0.9f);
        }

        private void CollectItem(Item item)
        {
            int reward = item.ItemData.Reward * _saveManager.PlayerData.MoneyMultiplier;
            _moneyCounter.Add(reward);
            OnRewardCollected?.Invoke(reward);
            item.ConnectTo(_pointForItem, true);

            _amountOfCollected++;
            OnItemCollected?.Invoke(_amountOfCollected, _maxItems);
            
            if (_amountOfCollected >= _maxItems)
            {
                OnGameWin?.Invoke();
            }
        }
    }
}