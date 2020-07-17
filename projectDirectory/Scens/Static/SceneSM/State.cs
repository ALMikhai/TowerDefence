using Godot;

namespace projectDirectory.Scens.Static.SceneSM
{
    public class State
    {
        protected SceneChanger _sceneChanger;
        protected StateMachine _stateMachine;

        public State(SceneChanger sceneChanger, StateMachine stateMachine)
        {
            _sceneChanger = sceneChanger;
            _stateMachine = stateMachine;
        }

        public virtual void Enter() { }

        async public virtual void Exit() { }
    }
}