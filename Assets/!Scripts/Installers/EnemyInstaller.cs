using System;
using System.Collections.Generic;
using Enemies;
using Enemies.Configs;
using Enemies.Fabric;
using Enemies.UI;
using Player;
using Sirenix.OdinInspector;
using UI.Screens;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class EnemyInstaller : MonoBehaviour
    {
        [SerializeField] private bool _haveEnemies = false;
        [ShowIf("_haveEnemies")]
        [SerializeField] private EnemyConfig _enemyConfig;
        [ShowIf("_haveEnemies")]
        [SerializeField] private EnemyFabric _enemyFabric;
        [ShowIf("_haveEnemies")] 
        [SerializeField] private List<EnemyStarterData> _enemyStarterDatas;

        private IWatch _watchObject;
        private ScreensHolder _screensHolder;

        [Inject]
        public void Constructor(IWatch watch, ScreensHolder screensHolder)
        {
            _watchObject = watch;
            _screensHolder = screensHolder;
            if(!_haveEnemies)
                return;

            CreateEnemies();
        }

        private void CreateEnemies()
        {
            foreach (var enemyStarterData in _enemyStarterDatas)
            {
                Enemy enemy = _enemyFabric.Get(enemyStarterData.StartPosition.position);
                EnemyPoint enemyPoint = _enemyFabric.Get(_screensHolder.GameplayScreen.transform);
                gameObject.AddComponent<EnemyIndicator>().Init(_watchObject.Transform, enemyPoint, enemy.transform);
                switch (enemyStarterData.MovementStrategy)
                {
                    case MovementStrategy.Random:
                        enemy.Init(_enemyFabric,_enemyConfig, enemyStarterData.StartPosition, enemyStarterData.RandomMovementConfig);
                        break;
                    case MovementStrategy.Waypoints:
                        enemy.Init(_enemyFabric,_enemyConfig, enemyStarterData.Waypoints, enemyStarterData.PointToPointMovementConfig);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

    [Serializable]
    public class EnemyStarterData
    {
        public Transform StartPosition;
        public MovementStrategy MovementStrategy;
        [ShowIf("MovementStrategy", MovementStrategy.Random)]
        public RandomMovementConfig RandomMovementConfig;
        [ShowIf("MovementStrategy", MovementStrategy.Waypoints)]
        public PointToPointMovementConfig PointToPointMovementConfig;
        [ShowIf("MovementStrategy", MovementStrategy.Waypoints)]
        public Transform[] Waypoints;
    }

    public enum MovementStrategy
    {
        Random,
        Waypoints
    }
}