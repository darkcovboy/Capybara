using Enemies.Movement;
using Player;
using UnityEngine;

namespace Enemies.StateMachine.States
{
    public class MovementState : IUpdatableState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly Enemy _enemy;
        private readonly IMovement _movement;

        public MovementState(IStateSwitcher stateSwitcher, Enemy enemy)
        {
            _stateSwitcher = stateSwitcher;
            _enemy = enemy;
            _movement = enemy.Movement;
            _enemy.CatcherTrigger.Enter += CatchCharacter;
        }

        public void Enter()
        {
            Debug.Log(GetType());
            
            _enemy.EnemyAnimation.PlayMovement();
            _movement.StartMovement();
        }


        public void Exit()
        {
            _movement.StopMovement();
        }

        public void Update()
        {
            _movement.Move();
        }

        private void CatchCharacter(Character character)
        {
            _stateSwitcher.SwitchState<CatchState>(character);
        }
    }
}