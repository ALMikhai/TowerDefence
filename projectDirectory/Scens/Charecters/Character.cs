using Godot;
using System;
using projectDirectory.Static;
using Static = projectDirectory.Static;

public class Character : RigidBody2D
{
    protected AnimatedSprite _animatedSprite;
    protected DamageNode _damageNode;
    protected HealthNode _healthNode;
    protected Character _target;

    private bool _isDeath = false;

    [Signal]
    public delegate void Death();

    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        _damageNode = GetNode<DamageNode>("DamageNode");
        _healthNode = GetNode<HealthNode>("HealthNode");
    }

    public override void _Process(float delta)
    {
        if (IsDeath())
            return;
        Process(delta);
    }

    public bool IsDeath() => _isDeath;

    public void SetTarget(Character target)
    {
        _target = target;
        _damageNode.SetTarget(target.GetNode<HealthNode>("HealthNode"));
        SetAnimation(Static.Character.ATACK);
    }

    protected void SetAnimation(Static.Character animation)
    {
        if (!IsDeath())
            _animatedSprite.Animation = AnimationNames.GetCharacterAnimation(animation);
    }

    public void Wait()
    {
        SetAnimation(Static.Character.IDLE);
    }

    private void _OnDeath()
    {
        OnDeath();
        EmitSignal(nameof(Death));
        _isDeath = true;
    }

    private void _OnAnimationFinished()
    {
        OnAnimationFinished();
        if (_animatedSprite.Animation == AnimationNames.GetCharacterAnimation(Static.Character.ATACK))
            Attack();
        if (_animatedSprite.Animation == AnimationNames.GetCharacterAnimation(Static.Character.DEATH))
            QueueFree();
    }

    protected virtual void Attack() { }
    protected virtual void OnAnimationFinished() { }
    protected virtual void Process(float delta) { }
    protected virtual void OnDeath() { }
}
