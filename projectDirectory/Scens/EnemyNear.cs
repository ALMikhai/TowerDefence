using Godot;
using System;

public class EnemyNear : Enemy
{
    public override void Atack()
    {
        _damageNode.TakeDamage();
    }

    public override void Process(float delta)
    {
        var target = _damageNode.GetTarget();
        var velocity = target.GlobalPosition - Position;
        if (velocity.Length() > 80)
        {
            _animatedSprite.Animation = _getAnimation(Animations.WALK);
            LinearVelocity = velocity.Normalized() * Speed * delta;
        }
        else
        {
            _animatedSprite.Animation = _getAnimation(Animations.ATACK);
            LinearVelocity = Vector2.Zero;
        }
    }
}
