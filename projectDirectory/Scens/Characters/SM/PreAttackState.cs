using Godot;

namespace projectDirectory.Scens.Charecters.SM
{
    public class PreAttackState : State
    {
        private Timer _reloadTimer;

        public PreAttackState(Character character, StateMachine stateMachine) : base(character, stateMachine) { }

        public override void Enter()
        {
            if (_reloadTimer == null)
            {
                _reloadTimer = _character.GetNode<Timer>("ReloadTimer");
            }

            _character.SetAnimation(projectDirectory.Static.Character.IDLE);
            TargetConnect();
        }

        public override void Exit()
        {
            TargetDisconnect();
        }

        public override void PhysicsUpdate(float delta)
        {
            if (!_character.IsCloseToTarget())
            {
                _stateMachine.ChangeState(_character._moveState);
                return;
            }

            if (_reloadTimer.IsStopped())
            {
                _stateMachine.ChangeState(_character._attackState);
                return;
            }
        }
    }
}
