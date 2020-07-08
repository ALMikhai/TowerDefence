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
        var distance = target.GlobalPosition - Position;
        if (distance.Length() > 80)
        {
            _animatedSprite.Animation = CharacterAnimationNames.GetAnimation(Names.WALK);
            LinearVelocity = distance.Normalized() * Speed * delta;
        }
        else
        {
            _animatedSprite.Animation = CharacterAnimationNames.GetAnimation(Names.ATACK);
            LinearVelocity = Vector2.Zero;
        }
    }
}
