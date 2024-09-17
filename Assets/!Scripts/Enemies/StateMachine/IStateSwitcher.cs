using Enemies.StateMachine.States;

namespace Enemies.StateMachine
{
    public interface IStateSwitcher
    {
        void SwitchState<TState>(object data = null) where TState : IState;
    }
}