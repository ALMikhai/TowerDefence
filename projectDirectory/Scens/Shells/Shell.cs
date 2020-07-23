using Godot;
using System;
using projectDirectory.Static;

public class Shell : RigidBody2D
{
    [Export]
    public int Speed = 1000;
    private int _damage = 25;

    public void _OnShellBodyEntered(Node body)
    {
        var enemy = (Enemy)body;
        enemy.GetNode<HealthNode>("HealthNode").ApplyDamage(_damage);
        CallDeferred("queue_free");
    }

    public void SetDestination(Vector2 point)
    {
        LinearVelocity = GlobalPosition.DirectionTo(point) * Speed;
        Rotation = GlobalPosition.AngleToPoint(point);
        LinearDamp = 0;
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }
}
