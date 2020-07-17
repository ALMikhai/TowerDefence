using Godot;
using System;
using projectDirectory.Scens.MainMenu.SM;

public class Menu : Control
{
    private StateMachine _stateMachine;
    private MenuState _menuState;
    private StoreState _storeState;

    public override void _Ready()
    {
        _stateMachine = new StateMachine();
        _menuState = new MenuState(this, _stateMachine);
        _storeState = new StoreState(this, _stateMachine);

        _stateMachine.Initialize(_menuState);
    }
    
    private void _OnStorePressed()
    {
        _stateMachine.ChangeState(_storeState);
    }
    
    private void _OnExitPressed()
    {

    }

    private void _OnPlayPressed()
    {

    }

    private void _OnBackPressed()
    {
        _stateMachine.ChangeState(_menuState);
    }
}
