using Godot;
using System;
using projectDirectory.Scens.MainMenu.SM;

public class Menu : Control
{
	private StateMachine _stateMachine;
	private MenuState _menuState;
	private StoreState _storeState;
	private JumpScreen _jumpScreen;
	private SceneChanger _sceneChanger;

	private Label _levelLabel;
	private Global _global;

	public override void _Ready()
	{
		_stateMachine = new StateMachine();
		_menuState = new MenuState(this, _stateMachine);
		_storeState = new StoreState(this, _stateMachine);
		_sceneChanger = GetTree().Root.GetNode<SceneChanger>("SceneChanger");

		_levelLabel = GetNode<Label>("MenuPanel/Level");
		_global = GetTree().Root.GetNode<Global>("Global");
		_jumpScreen = GetNode<JumpScreen>("JumpScreen");
		
		_stateMachine.Initialize(_menuState);
		UpdateView();
		
		_jumpScreen.EnterAnimation();
	}
	
	private void _OnStorePressed()
	{
		_stateMachine.ChangeState(_storeState);
	}
	
	private async void _OnExitPressed()
	{
		_jumpScreen.ExitAnimation();
		await ToSignal(_jumpScreen, nameof(JumpScreen.AnimationFinished));
		
		_sceneChanger._stateMachine.ChangeState(_sceneChanger._exitState);
	}

	private void _OnPlayPressed()
	{
		_sceneChanger._stateMachine.ChangeState(_sceneChanger._gameState);
	}

	private void _OnBackPressed()
	{
		_stateMachine.ChangeState(_menuState);
	}

	private void UpdateView()
	{
		_levelLabel.Text = $"Level {_global.GetLevel()}";
	}
}
