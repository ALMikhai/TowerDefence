using Godot;
using System;

public class Explosion : Node2D
{
	private void _OnAnimationFinished()
	{
		CallDeferred("queue_free");
	}
}
