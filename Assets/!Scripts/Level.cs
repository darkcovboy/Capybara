using Installers;
using UnityEngine;

namespace DefaultNamespace
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private EnemyInstaller _enemyInstaller;
        [SerializeField] private Transform _collectorPosition;
        [SerializeField] private Transform _playerPosition;

        public Transform CollectorPosition => _collectorPosition;

        public Transform PlayerPosition => _playerPosition;

        public EnemyInstaller EnemyInstaller => _enemyInstaller;
    }
}