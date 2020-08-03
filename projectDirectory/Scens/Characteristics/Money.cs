using Godot;
using System;

public class Money : Node2D
{
	private Sprite _sprite;
	private MoneyNode _moneyNode;
	private bool _animationFinished = false;
	private int _speed = 700;
	private int _cost = 0;

	public override void _Ready()
	{
		_sprite = GetNode<Sprite>("Sprite");
	}

	public override void _PhysicsProcess(float delta)
	{
		if (_moneyNode == null || !_animationFinished)
			return;

		_sprite.GlobalPosition += (_moneyNode.GlobalPosition - _sprite.GlobalPosition).Normalized() * delta * _speed;
		if (_sprite.GlobalPosition.DistanceTo(_moneyNode.GlobalPosition) <= 10)
		{
			_moneyNode.Add(_cost);
			_moneyNode.UpdateLabel();
			QueueFree();
		}
	}

	private void _OnAnimationPlayerAnimationFinished(string animationName)
	{
		_animationFinished = true;
	}
	
	public void SetMoneyNode(MoneyNode node)
	{
		_moneyNode = node;
	}

	public void SetCost(int cost)
	{
		_cost = cost;
	}
}
