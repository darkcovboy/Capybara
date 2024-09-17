namespace Enemies.StateMachine.States
{
    public interface IStateWithData : IState
    {
        void Enter(object param);
    }
}