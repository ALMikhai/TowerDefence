using Godot;
using System;
using projectDirectory.Scens;

public class Defender : Character
{
    [Export]
    public PackedScene Shell;
    private HealthNode _target;
    protected override void Atack()
    {
        var fireBall = (Fireball)Shell.Instance();
        var damageNode = fireBall.GetNode<DamageNode>("DamageNode");
        damageNode.SetTarget(_target);
        AddChild(fireBall);
    }

    private void _OnEnemiesUpdeted()
    {
        var enemies = GetTree().GetNodesInGroup("EnemyBody");
        if (enemies.Count == 0)
        {
            _animatedSprite.Animation = CharacterAnimationNames.GetAnimation(Names.IDLE);
        }
        else
        {
            _target = (HealthNode)enemies[0];
            _animatedSprite.Animation = CharacterAnimationNames.GetAnimation(Names.ATACK);
        }
    }
}
