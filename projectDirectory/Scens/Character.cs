using Godot;
using System;
using projectDirectory.Scens;

public class Character : RigidBody2D
{
    protected AnimatedSprite _animatedSprite;
    protected DamageNode _damageNode;
    protected HealthNode _healthNode;

    private bool _isDead = false;

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
        if (_isDead)
            return;
        Process(delta);
    }

    private void _OnDeath()
    {
        OnDeath();
        EmitSignal(nameof(Death));
    }

    private void _OnAnimationFinished()
    {
        OnAnimationFinished();
        if (_animatedSprite.Animation == CharacterAnimationNames.GetAnimation(Names.ATACK))
            Atack();
        if (_animatedSprite.Animation == CharacterAnimationNames.GetAnimation(Names.DEATH))
            QueueFree();
    }

    protected virtual void Atack() { }
    protected virtual void OnAnimationFinished() { }
    protected virtual void Process(float delta) { }
    protected virtual void OnDeath()
    {
        _isDead = true;
    }
}
