using System;
using UnityEngine;

namespace Items.Collector.Numbers
{
    public class NumberSpawner : MonoBehaviour
    {
        [SerializeField] private ItemCollector _itemCollector;
        [SerializeField] private NumberObjectPool _numberObjectPool;
        [SerializeField] private Transform _spawnPoint;

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
            numberObject.Initialize(value);
        }
    }
}