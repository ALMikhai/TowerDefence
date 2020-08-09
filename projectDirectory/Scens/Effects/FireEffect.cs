using Godot;
using System;

public class FireEffect : Effect
{
	[Export] 
	public int Damage = 20;
	
	protected override void Action()
	{
		var enemies = GetTree().GetNodesInGroup("Enemy");

		foreach (var node in enemies)
		{
			var enemy = node as Enemy;
			enemy.GetNode<HealthNode>("HealthNode").ApplyDamage(Damage);

			var damageView = (DamageView)GD.Load<PackedScene>("res://Scens/Shells/DamageView.tscn").Instance();
			GetParent().AddChild(damageView);
			damageView.SetValue(Damage);
			damageView.GlobalPosition = enemy.GlobalPosition;
		}
	}
}
