using Godot;

namespace projectDirectory.Scens.Charecters.SM
{
    public class State : Node
    {
        protected Character _character;
        protected StateMachine _stateMachine;

        protected bool TargetConnected
        {
            get => _character.GetTarget().IsConnected(nameof(Character.Death), this, nameof(_OnTargetDeath));
        }

        public State(Character character, StateMachine stateMachine)
        {
            _character = character;
            _stateMachine = stateMachine;
        }
        public virtual void Enter() { }

        public virtual void HandleInput() { }

        public virtual void LogicUpdate() { }

        public virtual void PhysicsUpdate(float delta) { }

        public virtual void Exit() { }

        public virtual void AnimationFinished() { }

        public virtual void SetTarget()
        {
            _stateMachine.ChangeState(_character._moveState);
        }

        public virtual void Death()
        {
            _stateMachine.ChangeState(_character._deathState);
        }

        protected void TargetConnect()
        {
            if (!TargetConnected)
            {
                _character.GetTarget().Connect(nameof(Character.Death), this, nameof(_OnTargetDeath));
            }
        }

        protected void TargetDisconnect()
        {
            if (TargetConnected)
            {
                _character.GetTarget().Disconnect(nameof(Character.Death), this, nameof(_OnTargetDeath));
            }
        }

        protected void _OnTargetDeath(Character character)
        {
            TargetDisconnect();
            _stateMachine.ChangeState(_character._idleState);
        }
    }
}
