using System;
using Extension;
using UnityEngine;

namespace Items.Collector.Numbers
{
    public class NumberSpawner : MonoBehaviour
    {
        [SerializeField] private ItemCollector _itemCollector;
        [SerializeField] private NumberObjectPool _numberObjectPool;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private NumberColors _numberColors;

        private void Awake()
        {
            _itemCollector.OnRewardCollected += SpawnNumber;
        }

        private void OnDestroy()
        {
            _itemCollector.OnRewardCollected -= SpawnNumber;
        }

        public void SpawnNumber(int value)
        {
            NumberObject numberObject = _numberObjectPool.Get();
            numberObject.transform.position = _spawnPoint.position;
            numberObject.gameObject.SetActive(true);
            Color color = _numberColors.Colors.PickRandom();
            numberObject.Initialize(value, color);
        }
    }
}