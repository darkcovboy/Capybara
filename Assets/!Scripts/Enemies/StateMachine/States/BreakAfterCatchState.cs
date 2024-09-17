using System.Collections;
using UnityEngine;

namespace Enemies.StateMachine.States
{
    public class BreakAfterCatchState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly Enemy _enemy;

        public BreakAfterCatchState(IStateSwitcher stateSwitcher, Enemy enemy)
        {
            _stateSwitcher = stateSwitcher;
            _enemy = enemy;
        }
        
        public void Enter()
        {
            Debug.Log(GetType());
            
            _enemy.EnemyAnimation.PlayBreakAfterAttack();
            
            _enemy.EnemyParticleShower.SwitchHappyParticle(true);

            _enemy.CatcherTrigger.Switch(false);

            _enemy.StartCoroutine(Breaking());
        }

        public void Exit()
        {
            _enemy.EnemyParticleShower.SwitchHappyParticle(false);
            _enemy.CatcherTrigger.Switch(true);
        }

        private IEnumerator Breaking()
        {
            yield return new WaitForSeconds(_enemy.EnemyConfig.TimeToBreak);
            
            _stateSwitcher.SwitchState<MovementState>();
        }
    }
}