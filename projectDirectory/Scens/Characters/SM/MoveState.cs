using Godot;

namespace projectDirectory.Scens.Charecters.SM
{
    public class MoveState : State
    {
        public MoveState(Character character, StateMachine stateMachine) : base(character, stateMachine) { }

        public override void Enter()
        {
            _character.SetAnimation(projectDirectory.Static.Character.WALK);
            TargetConnect();
        }

        public override void Exit()
        {
            _character.LinearVelocity = Vector2.Zero;
            TargetDisconnect();
        }

        public override void PhysicsUpdate(float delta)
        {
            _character.LinearVelocity = _character.Position.DirectionTo(_character.GetTarget().Position) * _character.Speed;

            if (_character.IsCloseToTarget())
            {
                _stateMachine.ChangeState(_character._preAttackState);
            }
        }
    }
}
