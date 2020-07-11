using Godot;
using System;
using projectDirectory.Static;

public class Shell : RigidBody2D
{
    [Export]
    public int Speed = 1000;

    private DamageNode _damageNode;
    private AnimatedSprite _animatedSprite;

    public override void _Ready()
    {
        _damageNode = GetNode<DamageNode>("DamageNode");
        _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
    }
    
    public void _OnShellBodyEntered(Node body)
    {
        var enemy = (Enemy)body;
        _damageNode.SetTarget(enemy.GetNode<HealthNode>("HealthNode"));
        _damageNode.ApplyDamage();
        CallDeferred("queue_free");
    }

    public void SetTarget(Character target)
    {
        LinearVelocity = GlobalPosition.DirectionTo(target.GlobalPosition) * Speed;
        LinearDamp = 0;
    }
}
