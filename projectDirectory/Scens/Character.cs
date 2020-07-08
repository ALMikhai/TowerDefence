using Godot;
using System;
using projectDirectory.Scens;

public class Character : RigidBody2D
{
    protected AnimatedSprite _animatedSprite;
    protected DamageNode _damageNode;
    protected HealthNode _healthNode;

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

    public void SetTarget(HealthNode target)
    {
        _damageNode.SetTarget(target);
        if (!IsDeath())
            _animatedSprite.Animation = CharacterAnimationNames.GetAnimation(Names.ATACK);
    }

    public void Wait()
    {
        _animatedSprite.Animation = CharacterAnimationNames.GetAnimation(Names.IDLE);
    }

    private void _OnDeath()
    {
        _isDeath = true;
        OnDeath();
        EmitSignal(nameof(Death));
    }

    private void _OnAnimationFinished()
    {
        OnAnimationFinished();
        if (_animatedSprite.Animation == CharacterAnimationNames.GetAnimation(Names.ATACK))
            Attack();
        if (_animatedSprite.Animation == CharacterAnimationNames.GetAnimation(Names.DEATH))
            QueueFree();
    }

    protected virtual void Attack() { }
    protected virtual void OnAnimationFinished() { }
    protected virtual void Process(float delta) { }
    protected virtual void OnDeath() { }
}
