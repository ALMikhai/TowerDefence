namespace projectDirectory.Scens.MainMenu.SM
{
    public class MenuState : State
    {
        public MenuState(Menu menu, StateMachine stateMachine) : base(menu, stateMachine) { }

        public override void Enter()
        {
            _menuPanel.Visible = true;
        }

        public override void Exit()
        {
            _menuPanel.Visible = false;
        }
    }
}