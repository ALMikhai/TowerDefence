using Godot;
using System;

public class Enemy : Character
{
    [Export]
    public int Speed = 7000;
    [Export]
    public int Damage = 25;
    private HealthNode _target;
    private AnimatedSprite _animatedSprite;

    public void SetTarget(HealthNode target)
    {
        _target = target;
        _animatedSprite.Animation = "Walk";
    }


    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
    }

    public void OnAnimatedSpriteAnimationFinished()
    {
        if (_animatedSprite.Animation == "Atack")
            _target.Hit(Damage);
    }

    public override void _Process(float delta)
    {
        var velocity = _target.GlobalPosition - Position;
        if (velocity.Length() > 80)
        {
            _animatedSprite.Animation = "Walk";
            LinearVelocity = velocity.Normalized() * Speed * delta;
        }
        else
        {
            _animatedSprite.Animation = "Atack";
            LinearVelocity = Vector2.Zero;
        }
    }
}
