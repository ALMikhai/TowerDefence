namespace projectDirectory.Scens.MainMenu.SM
{
    public class StoreState : State
    {
        public StoreState(Menu menu, StateMachine stateMachine) : base(menu, stateMachine) { }

        public override void Enter()
        {
            _store.Visible = true;
        }

        public override void Exit()
        {
            _store.Visible = false;
        }
    }
}