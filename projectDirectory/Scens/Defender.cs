using Godot;
using System;

public class Defender : RigidBody2D
{
    [Export]
    public PackedScene Shell;
    private AnimatedSprite _animatedSprite;

    public void OnAnimatedSpriteAnimationFinished()
    {
        if (_animatedSprite.Animation == "Atack")
        {
            var target = (HealthNode)GetTree().GetNodesInGroup("EnemyBody")[0];
            var fireBall = (Fireball)Shell.Instance();
            var damageNode = fireBall.GetNode<DamageNode>("DamageNode");
            damageNode.SetTarget(target);
            AddChild(fireBall);
        }
    }

    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
    }

    public override void _Process(float delta)
    {
        if (GetTree().GetNodesInGroup("EnemyBody").Count == 0)
        {
            _animatedSprite.Animation = "Idle";
        }
        else
        {
            _animatedSprite.Animation = "Atack";
        }
    }
}
