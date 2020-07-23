using Godot;

namespace projectDirectory.Scens.GameSM
{
    public class State
    {
        protected BattleGround _battleGround;
        protected StateMachine _stateMachine;

        public State(BattleGround battleGround, StateMachine stateMachine)
        {
            _battleGround = battleGround;
            _stateMachine = stateMachine;
        }

        public virtual void Enter() { }

        public virtual void HandleInput(InputEvent @event) { }

        public virtual void Exit() { }
    }
}