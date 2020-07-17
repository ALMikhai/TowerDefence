namespace projectDirectory.Scens.MainMenu.SM
{
    public class StateMachine
    {
        public State CurrentState;

        public void Initialize(State startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }

        public void ChangeState(State newState)
        {
            CurrentState.Exit();

            CurrentState = newState;
            newState.Enter();
        }
    }
}