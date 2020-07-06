using Godot;
using System;
using projectDirectory.Scens;

public class EnemyNear : Enemy
{
    protected override void Atack()
    {
        _damageNode.ApplyDamage();
    }

    protected override void Process(float delta)
    {
        var target = _damageNode.GetTarget();
        var velocity = target.GlobalPosition - Position;
        if (velocity.Length() > 80)
        {
            _animatedSprite.Animation = CharacterAnimationNames.GetAnimation(Names.WALK);
            LinearVelocity = velocity.Normalized() * Speed * delta;
        }
        else
        {
            _animatedSprite.Animation = CharacterAnimationNames.GetAnimation(Names.ATACK);
            LinearVelocity = Vector2.Zero;
        }
    }
}
