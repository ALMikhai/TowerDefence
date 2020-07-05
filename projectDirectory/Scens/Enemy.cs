using Godot;
using System;

public class Enemy : Character
{
    [Export]
    public int Speed = 7000;

    public override void OnDeath()
    {
        base.OnDeath();
        _animatedSprite.Animation = _getAnimation(Animations.DEATH);
        LinearVelocity = Vector2.Zero;
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true;
    }
}
