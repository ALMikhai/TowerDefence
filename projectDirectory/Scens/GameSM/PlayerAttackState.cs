using Godot;

namespace projectDirectory.Scens.GameSM
{
    public class PlayerAttackState : State
    {
        private PlayerDefender _playerDefender;

        public PlayerAttackState(BattleGround battleGround, StateMachine stateMachine) : base(battleGround, stateMachine) { }

        public override void Enter()
        {
            if (_playerDefender == null)
                _playerDefender = _battleGround.GetNode<PlayerDefender>("PlayerDefender");
        }

        public override void Exit()
        {

        }

        public override void HandleInput(InputEvent @event)
        {
            if (@event.IsActionPressed("pause"))
                _stateMachine.ChangeState(_battleGround._pauseState);

            if (@event is InputEventScreenTouch screenTouchEvent && screenTouchEvent.IsPressed())
            {
                _playerDefender.Attack(screenTouchEvent.Position);
            }
        }
    }
}