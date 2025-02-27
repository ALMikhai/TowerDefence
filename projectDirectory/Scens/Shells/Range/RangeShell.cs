using Godot;
using System.Collections.Generic;

public class RangeShell : Shell
{
	[Export] 
	public PackedScene Explosion;

	private List<Node> _inRangeAreaNods;
	private AnimatedSprite _animatedSprite;
	
	public override void _Ready()
	{
		_inRangeAreaNods = new List<Node>();
		_animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
	}

	private void _OnShellBodyEntered(Node body)
	{
		foreach (var node in _inRangeAreaNods)
		{
			var enemy = node as Enemy;
			enemy.GetNode<HealthNode>("HealthNode").ApplyDamage(_damage);
		}

		var explosion = Explosion.Instance() as Explosion;
		GetParent().AddChild(explosion);
		explosion.GlobalPosition = GlobalPosition;
		
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
