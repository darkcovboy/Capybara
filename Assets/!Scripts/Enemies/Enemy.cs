using System;
using Const;
using Enemies.Animation;
using Enemies.Catcher;
using Enemies.Configs;
using Enemies.Fabric;
using Enemies.Movement;
using Enemies.StateMachine;
using Extension;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(EnemyAnimation))]
    public class Enemy : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private CatcherTriggerObserver _catcherTrigger;
        [SerializeField] private EnemyAnimation _enemyAnimation;
        [SerializeField] private EnemyParticleShower _enemyParticleShower;
        
        public EnemyAnimation EnemyAnimation => _enemyAnimation;

        public EnemyParticleShower EnemyParticleShower => _enemyParticleShower;
        public CatcherTriggerObserver CatcherTrigger => _catcherTrigger;
        public IMovement Movement { get; private set; }
        public EnemyFabric EnemyFabric { get; private set; }

        public EnemyConfig EnemyConfig { get; private set; }


        private EnemyStateMachine _enemyStateMachine;
        
        public void Init(EnemyFabric enemyFabric, EnemyConfig enemyConfig, Transform startTransform, RandomMovementConfig config)
        {
            EnemyFabric = enemyFabric;
            EnemyConfig = enemyConfig;
            Movement = new RandomMovement(_agent, startTransform, config);
            _enemyStateMachine = new EnemyStateMachine(this);
        }
        
        public void Init(EnemyFabric enemyFabric, EnemyConfig enemyConfig, Transform[] waypoints, PointToPointMovementConfig config)
        {
            EnemyFabric = enemyFabric;
            EnemyConfig = enemyConfig;
            Movement = new PointToPointMovement(_agent, waypoints, config);
            _enemyStateMachine = new EnemyStateMachine(this);
        }

        private void Update()
        {
            _enemyStateMachine.Update();
        }
    }
}