namespace Enemies.StateMachine.States
{
    public abstract class StateBase : IState
    {
        protected readonly IStateSwitcher StateSwitcher;
        protected readonly Enemy Enemy;

        public StateBase(IStateSwitcher stateSwitcher, Enemy enemy)
        {
            StateSwitcher = stateSwitcher;
            Enemy = enemy;
        }
        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }

        public void Update()
        {
            
        }
    }
}