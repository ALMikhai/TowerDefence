namespace projectDirectory.Scens.Static.SceneSM
{
    public class ExitState : State
    {

        private Global _global;

        public ExitState(SceneChanger sceneChanger, StateMachine stateMachine) : base(sceneChanger, stateMachine) 
        {
            _global = _sceneChanger.GetTree().Root.GetNode<Global>("Global");
        }

        public override void Enter()
        {
            _global.Save();
            _global.GetMoneyNode().QueueFree();
            _sceneChanger.GetTree().Quit();
        }
    }
}