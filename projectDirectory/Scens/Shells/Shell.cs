using Godot;
using System;
using projectDirectory.Static;

public class Shell : Node2D
{
    [Export]
    public int Speed = 1000;

    private DamageNode _damageNode;
    private AnimatedSprite _animatedSprite;
    private Character _target;

    public override void _Ready()
    {
        _damageNode = GetNode<DamageNode>("DamageNode");
        _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
    }

    public void SetTarget(Character target)
    {
        _target = target;
        _damageNode.SetTarget(_target.GetNode<HealthNode>("HealthNode"));
        _target.Connect(nameof(Character.Death), this, nameof(Fall));
    }

    private void Fall()
    {
        _target.Disconnect(nameof(Character.Death), this, nameof(Fall));
        _animatedSprite.Animation = AnimationNames.GetShellAnimation(projectDirectory.Static.Shell.FALLING);
    }

    public void _OnAnimationFinished()
    {
        if (_animatedSprite.Animation == AnimationNames.GetShellAnimation(projectDirectory.Static.Shell.FALLING))
        {
            QueueFree();
        }
    }

    public override void _Process(float delta)
    {
        if (_animatedSprite.Animation == AnimationNames.GetShellAnimation(projectDirectory.Static.Shell.DEFAULT))
        {
            var distance = _target.GlobalPosition - GlobalPosition;
            if (distance.Length() > 10)
            {
                GlobalPosition += distance.Normalized() * Speed * delta;
            }
            else
            {
                _damageNode.ApplyDamage();
                QueueFree();
            }
        }
    }
}
