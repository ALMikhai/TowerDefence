using Godot;
using System;

public class Fireball : Node2D
{
    [Export]
    public int Speed = 100;

    private DamageNode _damageNode;
    private AnimatedSprite _animatedSprite;

    public override void _Ready()
    {
        _damageNode = GetNode<DamageNode>("DamageNode");
        _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
    }

    public void OnAnimatedSpriteAnimationFinished()
    {
        if (_animatedSprite.Animation == "Falling")
        {
            QueueFree();
        }
    }

    public override void _Process(float delta)
    {
        if (_animatedSprite.Animation == "Default")
        {
            try
            {
                var target = _damageNode.GetTarget();
                var distance = target.GlobalPosition - GlobalPosition;
                if (distance.Length() > 10)
                {
                    GlobalPosition += distance.Normalized() * Speed * delta;
                }
                else
                {
                    _damageNode.TakeDamage();
                    QueueFree();
                }
            }
            catch
            {
                _animatedSprite.Animation = "Falling";
            }
        }

    }
}
