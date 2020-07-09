using Godot;
using System;
using projectDirectory.Static;

public class EnemyNear : Enemy
{
    protected override void Attack()
    {
        _damageNode.ApplyDamage();
    }

    protected override void Process(float delta)
    {
        var distance = _target.GlobalPosition - Position;
        if (distance.Length() > 80)
        {
            SetAnimation(projectDirectory.Static.Character.WALK);
            LinearVelocity = distance.Normalized() * Speed * delta;
        }
        else
        {
            SetAnimation(projectDirectory.Static.Character.ATACK);
            LinearVelocity = Vector2.Zero;
        }
    }
}
