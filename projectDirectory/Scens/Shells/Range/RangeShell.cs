using Godot;
using System.Collections.Generic;

public class RangeShell : Shell
{
	private List<Node> _inRangeAreaNods;
	
	public override void _Ready()
	{
		_inRangeAreaNods = new List<Node>();
	}

	private void _OnShellBodyEntered(Node body)
	{
		foreach (var node in _inRangeAreaNods)
		{
			var enemy = node as Enemy;
			enemy.GetNode<HealthNode>("HealthNode").ApplyDamage(_damage);

			var damageView = (DamageView)GD.Load<PackedScene>("res://Scens/Shells/DamageView.tscn").Instance();
			GetParent().AddChild(damageView);
			damageView.SetValue(_damage);
			damageView.GlobalPosition = enemy.GlobalPosition;
		}
		
		CallDeferred("queue_free");
	}
	
	private void _OnRangeAreaBodyEntered(Node body)
	{
		_inRangeAreaNods.Add(body);
	}
	
	private void _OnRangeAreaBodyExited(Node body)
	{
		_inRangeAreaNods.Remove(body);
	}
}
