using Godot;
using System;

public class WindEffect : Effect
{
	[Export] 
	public int PushPower = 10000;
	
	protected override void Action()
	{
		var enemies = GetTree().GetNodesInGroup("Enemy");

		foreach (var node in enemies)
		{
			var enemy = node as Enemy;
			enemy.MoveAndSlide(Vector2.Right * PushPower);
		}
	}
}
