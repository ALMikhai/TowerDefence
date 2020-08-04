using Godot;
using System;
using projectDirectory.Scens.Static.SceneSM;

public class SceneChanger : Node
{
    public StateMachine _stateMachine;
    public MenuState _menuState;
    public GameState _gameState;
    public ExitState _exitState;

    public override void _Ready()
    {
        _stateMachine = new StateMachine();
        _menuState = new MenuState(this, _stateMachine);
        _gameState = new GameState(this, _stateMachine);
        _exitState = new ExitState(this, _stateMachine);

        _stateMachine.Initialize(_menuState);
    }
}
