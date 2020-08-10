using Godot;
using System;

public class Crystal : Character
{
	[Export] 
	public int StartHp = 100;
	
	private Global _global;

	public override void _Ready()
	{
		base._Ready();
		_global = GetTree().Root.GetNode<Global>("Global");
		_healthNode.SetHealth(StartHp + (int)Math.Sqrt(_global.Level * 150));
	}
}
