namespace projectDirectory.Scens.Static.SceneSM
{
    public class MenuState : State
    {
        public MenuState(SceneChanger sceneChanger, StateMachine stateMachine) : base(sceneChanger, stateMachine) { }

        public override void Enter() 
        {
            _sceneChanger.GetTree().ChangeScene("res://Scens/MainMenu/Menu.tscn");
        }
    }
}