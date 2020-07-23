namespace projectDirectory.Scens.Static.SceneSM
{
    public class ExitState : State
    {

        private Global _global;
        private DefendersData _defendersData;

        public ExitState(SceneChanger sceneChanger, StateMachine stateMachine) : base(sceneChanger, stateMachine) 
        {
            _global = _sceneChanger.GetTree().Root.GetNode<Global>("Global");
            _defendersData = _sceneChanger.GetTree().Root.GetNode<DefendersData>("DefendersData");
        }

        public override void Enter()
        {
            _global.Save();
            _global.Money.QueueFree();
            _defendersData.Save();
            _sceneChanger.GetTree().Quit();
        }
    }
}