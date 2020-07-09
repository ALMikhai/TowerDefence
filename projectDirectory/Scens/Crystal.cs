using Godot;
using System;

public class Crystal : StaticBody2D
{
    [Signal]
    public delegate void Broken();
    private HealthNode _healthNode;

    public void OnDeath()
    {
        EmitSignal(nameof(Broken));
        QueueFree();
    }

    public override void _Ready()
    {
        _healthNode = GetNode<HealthNode>("HealthNode");
    }
}
