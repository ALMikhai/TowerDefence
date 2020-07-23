namespace projectDirectory.Scens.Static.SceneSM
{
    public class GameState : State
    {
        public GameState(SceneChanger sceneChanger, StateMachine stateMachine) : base(sceneChanger, stateMachine) { }

        public override void Enter()
        {
            _sceneChanger.GetTree().ChangeScene("res://Scens/BattleGround/BattleGround.tscn");
        }
    }
}