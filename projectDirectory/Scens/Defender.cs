using Godot;
using System;

public class Defender : Character
{
    [Export]
    public PackedScene Shell;
    private HealthNode _target;
    public override void Atack()
    {
        if (_target == null)
            return;
        var fireBall = (Fireball)Shell.Instance();
        var damageNode = fireBall.GetNode<DamageNode>("DamageNode");
        damageNode.SetTarget(_target);
        AddChild(fireBall);
    }

    public override void Process(float delta)
    {
        var enemies = GetTree().GetNodesInGroup("EnemyBody");
        if (enemies.Count == 0)
        {
            _animatedSprite.Animation = _getAnimation(Animations.IDLE);
        }
        else
        {
            _target = (HealthNode)enemies[0];
            _animatedSprite.Animation = _getAnimation(Animations.ATACK);
        }
    }
}
