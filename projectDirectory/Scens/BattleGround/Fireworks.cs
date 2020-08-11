using Godot;
using System;

public class Fireworks : Node
{
	[Export] 
	public PackedScene Firework;

	[Signal]
	public delegate void FireworksEnd();
	
	private Random _random;
	private int _fireworksNum = 7;
	
	public override void _Ready()
	{
		_random = new Random();
	}

	private void _OnReloadTimeout()
	{
		_fireworksNum--;
		
		if (_fireworksNum == 0)
		{
			EmitSignal(nameof(FireworksEnd));
		}
		
		var firework = Firework.Instance() as Firework;
		AddChild(firework);
		firework.Position = new Vector2(
			_random.Next(100, Convert.ToInt32(GetViewport().Size.x) - 100), 
			_random.Next(100, Convert.ToInt32(GetViewport().Size.y) - 100)
			);
	}

	public void Start()
	{
		GetNode<Timer>("Reload").Start();
	}
}
