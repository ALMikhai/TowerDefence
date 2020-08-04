using Godot;
using System;

public class JumpScreen : Control
{
	[Signal]
	public delegate void AnimationFinished();

	private AnimationPlayer _animationPlayer;

	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	public void EnterAnimation()
	{
		Visible = true;
		_animationPlayer.Play("Enter");
	}
	
	public void ExitAnimation()
	{
		Visible = true;
		_animationPlayer.Play("Exit");
	}
	
	private void _OnAnimationFinished(string animName)
	{
		if (animName == "Enter")
			Visible = false;
		EmitSignal(nameof(AnimationFinished));
	}
}
