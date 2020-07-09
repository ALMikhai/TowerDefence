using Godot;
using System;
using Static = projectDirectory.Static;

public class Enemy : Character
{
    [Export]
    public int Speed = 7000;

    protected override void OnDeath()
    {
        SetAnimation(Static.Character.DEATH);
        LinearVelocity = Vector2.Zero;
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true;
    }
}
