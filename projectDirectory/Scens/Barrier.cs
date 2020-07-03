using Godot;
using System;

public class Barrier : StaticBody2D
{
    [Signal]
    public delegate void Broken();
    private HealthNode _hp;
    private Timer _brokenTimer;

    public void OnDeath()
    {
        EmitSignal(nameof(Broken));
        _brokenTimer.Start();
    }

    public void OnBrokenTimerTimeout()
    {
        QueueFree();
    }

    public override void _Ready()
    {
        _brokenTimer = GetNode<Timer>("BrokenTimer");
        _hp = GetNode<HealthNode>("Hp");
    }
}
