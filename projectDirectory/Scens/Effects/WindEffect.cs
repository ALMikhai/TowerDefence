using Godot;
using System;

public class WindEffect : Effect
{
	protected override void Action()
	{
		var enemies = GetTree().GetNodesInGroup("Enemy");

		foreach (var node in enemies)
		{
			var enemy = node as Enemy;
			var velocity = enemy.LinearVelocity.Normalized() * -1 * 10;
			enemy.ApplyCentralImpulse(velocity);
		}
	}
}
