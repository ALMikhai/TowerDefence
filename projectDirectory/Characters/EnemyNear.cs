using Godot;
using System;

public class EnemyNear : Area2D
{
    [Export]
    public int Speed = 100;

    private Vector2 _target;
    private AnimatedSprite _animatedSprite;

    public void SetTarget(Vector2 target)
    {
        _target = target;
    }

    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
    }

    public override void _Process(float delta)
    {
        var velocity = _target - Position;
        if (velocity.Length() > 10)
        {
            _animatedSprite.Animation = "Walk";
            _animatedSprite.Play();
            Position += velocity.Normalized() * Speed * delta;
        } else {
            _animatedSprite.Animation = "Atack";
            _animatedSprite.Play();
        }
    }
}
