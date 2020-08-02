using Godot;
using System;

public class DamageView : Node2D
{
	private Label _value;
	private int _speed = 65;

	public override void _Ready()
	{
		_value = GetNode<Label>("Value");
	}

	public override void _PhysicsProcess(float delta)
	{
		Position += Vector2.Up * _speed * delta;
	}

	public void _OnTimeToLiveTimeout()
	{
		CallDeferred("queue_free");
	}

	public void SetValue(int damage)
	{
		_value.Text = damage.ToString();
	}
}
