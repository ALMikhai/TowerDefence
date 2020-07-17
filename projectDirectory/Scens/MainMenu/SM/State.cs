using Godot;

namespace projectDirectory.Scens.MainMenu.SM
{
    public class State
    {
        protected Menu _menu;
        protected StateMachine _stateMachine;

        protected TextureRect _menuPanel;
        protected Strore _store;

        public State(Menu menu, StateMachine stateMachine)
        {
            _menu = menu;
            _stateMachine = stateMachine;
            _menuPanel = _menu.GetNode<TextureRect>("MenuPanel");
            _store = _menu.GetNode<Strore>("Strore");
        }

        public virtual void Enter() { }

        public virtual void Exit() { }
    }
}