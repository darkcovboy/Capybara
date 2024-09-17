using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemies.StateMachine.States;
using Player;
using UnityEngine;

namespace Enemies.StateMachine
{
    public class EnemyStateMachine: IStateSwitcher
    {
        private readonly List<IState> _states;
        private readonly Enemy _enemy;
        private IState _currentState;
        private Coroutine _breakCoroutine; 

        public EnemyStateMachine(Enemy enemy)
        {
            _enemy = enemy;
            
            _states = new List<IState>()
            {
                new MovementState(this, enemy),
                new BreakState(this,enemy),
                new CatchState(this, enemy),
                new BreakAfterCatchState(this, enemy)
            };

            if (enemy.EnemyConfig.HaveBreak)
            {
                _breakCoroutine = enemy.StartCoroutine(CheckForBreak(enemy.EnemyConfig.TimeBeforeBreak));
            }

            _currentState = _states[0];
            _currentState.Enter();
        }

        public void SwitchState<TState>(object data = null) where TState : IState
        {
            IState state = _states.FirstOrDefault(state => state is TState);

            _currentState.Exit();
            _currentState = state;
            
            if (data != null && _currentState is IStateWithData stateWithData)
            {
                Debug.Log("Enter with data");
                stateWithData.Enter(data);
            }
            else
            {
                Debug.Log("Enter without data");
                _breakCoroutine = _enemy.StartCoroutine(CheckForBreak(_enemy.EnemyConfig.TimeBeforeBreak)); 
                _currentState.Enter();
            }
        }

        public void Update()
        {
            if(_currentState == null)
                return;
            
            UpdateStates();
        }

        private IEnumerator CheckForBreak(float timeBeforeBreak)
        {
            while (true)
            {
                yield return new WaitForSeconds(timeBeforeBreak);
                SwitchState<BreakState>();
            }
        }

        private void UpdateStates()
        {
            if (_currentState is IUpdatableState updatableState)
            {
                updatableState.Update();
            }
        }
    }
}