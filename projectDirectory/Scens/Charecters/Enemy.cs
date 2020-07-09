using Godot;
using System;
using projectDirectory.Scens;

public class Enemy : Character
{
    [Export]
    public int Speed = 7000;

    protected override void OnDeath()
    {
        _animatedSprite.Animation = CharacterAnimationNames.GetAnimation(Names.DEATH);
        LinearVelocity = Vector2.Zero;
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true;
    }
}
