using System.Collections;
using System.Threading.Tasks;
using Enemies.Catcher;
using Player;
using UnityEngine;

namespace Enemies.StateMachine.States
{
    public class CatchState : IStateWithData
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly Enemy _enemy;

        public CatchState(IStateSwitcher stateSwitcher, Enemy enemy)
        {
            _stateSwitcher = stateSwitcher;
            _enemy = enemy;
        }
        
        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }

        public async void Enter(object param)
        {
            Debug.Log("Enter" + GetType());
            
            if (param is Character character)
            {
                Debug.Log($"Catching character: {character.name}");
                character.Defeat();
                _enemy.EnemyAnimation.PlayAttack();
                await Task.Delay(200);
                Cage cage =_enemy.EnemyFabric.GetCage(character.transform.position);
                cage.Take(character.transform);
                _stateSwitcher.SwitchState<BreakAfterCatchState>();
            }
        }
    }
}