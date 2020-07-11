using Godot;

namespace projectDirectory.Scens.Charecters.SM
{
    public class State : Node
    {
        protected Character _character;
        protected StateMachine _stateMachine;
        protected bool _targetConnected = false;
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
            if (!_targetConnected)
            {
                _character.GetTarget().Connect(nameof(Character.Death), this, nameof(_OnTargetDeath));
                _targetConnected = true;
            }
        }

        protected void TargetDisconnect()
        {
            if (_targetConnected)
            {
                _character.GetTarget().Disconnect(nameof(Character.Death), this, nameof(_OnTargetDeath));
                _targetConnected = false;
            }
        }

        protected void _OnTargetDeath(Character character)
        {
            TargetDisconnect();
            _stateMachine.ChangeState(_character._idleState);
        }
    }
}
