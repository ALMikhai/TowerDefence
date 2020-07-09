using Godot;
using System;
using Static = projectDirectory.Static;

public class Enemy : Character
{
    public override void _PhysicsProcess(float delta)
    {
        if (!IsDeath())
            LinearVelocity = Position.DirectionTo(_target.Position) * Speed;
        else
            LinearVelocity = Vector2.Zero;
    }

    public void GoToTarget()
    {
        SetAnimation(Static.Character.WALK);
    }

    protected override void OnDeath()
    {
        SetAnimation(Static.Character.DEATH);
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true;
    }
}
