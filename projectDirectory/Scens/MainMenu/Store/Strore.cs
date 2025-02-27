using Godot;
using System;

public class Strore : Control
{
	private Global _global;
	private HBoxContainer _moneyContainer;

	public override void _Ready()
	{
		_global = GetTree().Root.GetNode<Global>("Global");
		_moneyContainer = GetNode<HBoxContainer>("MoneyContainer");
	}

	public void Enter()
	{
		_moneyContainer.AddChild(_global.Money);
	}

	public void Exit()
	{
		_moneyContainer.RemoveChild(_global.Money);
	}
}
