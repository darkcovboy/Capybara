using System;
using System.Collections;
using UnityEngine;

namespace Enemies.StateMachine.States
{
    public class BreakState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly Enemy _enemy;

        public BreakState(IStateSwitcher stateSwitcher, Enemy enemy)
        {
            _stateSwitcher = stateSwitcher;
            _enemy = enemy;
        }
        
        public void Enter()
        {
            Debug.Log(GetType());
            
            _enemy.EnemyAnimation.PlayBreaking();

            _enemy.CatcherTrigger.Switch(false);
            _enemy.EnemyParticleShower.SwitchSleepParticle(true);

            _enemy.StartCoroutine(Breaking());
        }

        public void Exit()
        {
            _enemy.EnemyParticleShower.SwitchSleepParticle(false);
            _enemy.CatcherTrigger.Switch(true);
        }

        private IEnumerator Breaking()
        {
            yield return new WaitForSeconds(_enemy.EnemyConfig.TimeToBreak);
            
            _stateSwitcher.SwitchState<MovementState>();
        }
    }
}