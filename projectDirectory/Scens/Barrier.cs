using Godot;
using System;

public class Barrier : StaticBody2D
{
    [Signal]
    public delegate void Broken();
    private float _hp = 100f;
    private Timer _brokenTimer;

    public void Hit(float damage)
    {
        _hp -= damage;

        if (_hp <= 0.0)
        {
            EmitSignal(nameof(Broken));
            _brokenTimer.Start();
        }
    }

    public void OnBrokenTimerTimeout()
    {
        QueueFree();
    }

    public override void _Ready()
    {
        _brokenTimer = GetNode<Timer>("BrokenTimer");
    }
}
