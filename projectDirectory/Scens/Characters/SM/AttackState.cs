using Godot;

namespace projectDirectory.Scens.Charecters.SM
{
    public class AttackState : State
    {
        private AudioStreamPlayer _attackSound;
        private Timer _reloadTimer;

        public AttackState(Character character, StateMachine stateMachine) : base(character, stateMachine) { }

        public override void Enter()
        {
            if (_attackSound == null)
            {
                _attackSound = _character.GetNode<AudioStreamPlayer>("AttackSound");
            }

            if (_reloadTimer == null)
            {
                _reloadTimer = _character.GetNode<Timer>("ReloadTimer");
            }

            _character.SetAnimation(projectDirectory.Static.Character.ATTACK);
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
            }
        }

        public override void AnimationFinished()
        {
            _attackSound.Play();
            _character.Attack();
            _reloadTimer.Start();
            _stateMachine.ChangeState(_character._preAttackState);
        }
    }
}
