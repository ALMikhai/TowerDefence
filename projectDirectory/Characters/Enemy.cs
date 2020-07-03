using Godot;
using System;

public class Enemy : Character
{
    [Export]
    public int Speed = 7000;
    private DamageNode _damageNode;
    private AnimatedSprite _animatedSprite;

    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        _damageNode = GetNode<DamageNode>("DamageNode");
    }

    public void OnAnimatedSpriteAnimationFinished()
    {

        if (_animatedSprite.Animation == "Atack")
            _damageNode.TakeDamage();
    }

    public override void _Process(float delta)
    {
        var target = _damageNode.GetTarget();
        var velocity = target.GlobalPosition - Position;
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
