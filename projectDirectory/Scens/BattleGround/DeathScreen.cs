using Godot;

public class DeathScreen : Control
{
	private SceneChanger _sceneChanger;

	public override void _Ready()
	{
		_sceneChanger = GetTree().Root.GetNode<SceneChanger>("SceneChanger");
		Visible = false;
	}
	
	private void _OnExitButtonPressed()
	{
		_sceneChanger._stateMachine.ChangeState(_sceneChanger._menuState);
		_sceneChanger.GetTree().Paused = false;
	}
	
	private void _OnRestartButtonPressed()
	{
		_sceneChanger._stateMachine.ChangeState(_sceneChanger._gameState);
		_sceneChanger.GetTree().Paused = false;
	}

	public void Start()
	{
		Visible = true;
		_sceneChanger.GetTree().Paused = true;
		GetNode<AnimationPlayer>("AnimationPlayer").Play("Enter");
	}
}
