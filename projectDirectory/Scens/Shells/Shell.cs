using Godot;
using System;
using projectDirectory.Static;

public class Shell : RigidBody2D
{
    [Export]
    public int Speed = 1000;

    private DamageNode _damageNode;

    public override void _Ready()
    {
        _damageNode = GetNode<DamageNode>("DamageNode");
    }
    
    public void _OnShellBodyEntered(Node body)
    {
        var enemy = (Enemy)body;
        _damageNode.SetTarget(enemy.GetNode<HealthNode>("HealthNode"));
        _damageNode.ApplyDamage();
        CallDeferred("queue_free");
    }

    public void SetDestination(Vector2 point)
    {
        LinearVelocity = GlobalPosition.DirectionTo(point) * Speed;
        Rotation = GlobalPosition.AngleToPoint(point);
        LinearDamp = 0;
    }
}
