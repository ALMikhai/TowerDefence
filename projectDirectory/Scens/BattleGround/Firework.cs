using Godot;
using System;
using System.Runtime.CompilerServices;

public class Firework : Particles2D
{
	private Random _random;
	
	public override void _Ready()
	{
		_random = new Random();

		Amount = _random.Next(50, 70);
		OneShot = true;
		Lifetime = ((float)_random.NextDouble() * 1000) % 2.0f + 1f;
		Modulate = new Color((float)_random.NextDouble(), (float)_random.NextDouble(), (float)_random.NextDouble());
	}
}
