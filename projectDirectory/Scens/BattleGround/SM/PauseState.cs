using Godot;

namespace projectDirectory.Scens.GameSM
{
    public class PauseState : State
    {
        private PauseMenu _pauseMenu;

        public PauseState(BattleGround battleGround, StateMachine stateMachine) : base(battleGround, stateMachine) {}

        public override void Enter()
        {
            if (_pauseMenu == null)
                _pauseMenu = _battleGround.GetNode<PauseMenu>("PauseMenu");

            _battleGround.GetTree().Paused = true;
            _pauseMenu.Visible = true;
        }

        public override void Exit()
        {
            _battleGround.GetTree().Paused = false;
            _pauseMenu.Visible = false;
        }

        public override void HandleInput(InputEvent @event)
        {
            if (@event.IsActionPressed("pause"))
                _stateMachine.ChangeState(_battleGround._playerAttackState);
        }
    }
}