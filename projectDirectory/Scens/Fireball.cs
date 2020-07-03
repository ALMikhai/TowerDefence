using Godot;
using System;

public class Fireball : Node2D
{
    [Export]
    public int Speed = 100;

    private DamageNode _damageNode;

    public override void _Ready()
    {
        _damageNode = GetNode<DamageNode>("DamageNode");
    }

    public override void _Process(float delta)
    {
        var target = _damageNode.GetTarget();
        try
        {
            var velocity = target.GlobalPosition - Position;
            if (velocity.Length() > 10)
            {
                Position += velocity.Normalized() * Speed * delta;
            }
            else
            {
                _damageNode.TakeDamage();
                QueueFree();
            }
        }
        catch
        {
            QueueFree();
        }
    }
}
