using Godot;
using System;

public class Character : RigidBody2D
{
    protected enum Animations : int
    {
        ATACK = 0,
        DEATH = 1,
        WALK = 2,
        IDLE = 3
    };

    protected AnimatedSprite _animatedSprite;
    protected DamageNode _damageNode;
    protected HealthNode _healthNode;
    private bool _isDead = false;

    private string[] _animationsNames = { "Atack", "Death", "Walk", "Idle" };

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

    public virtual void Atack() { }
    public virtual void OnAnimationFinished() { }
    public virtual void Process(float delta) { }
    public virtual void OnDeath()
    {
        _isDead = true;
    }

    protected string _getAnimation(Animations animation) => _animationsNames[(int)animation];

    private void _onDeath()
    {
        OnDeath();
    }
    private void _onAnimatedSpriteAnimationFinished()
    {
        OnAnimationFinished();
        if (_animatedSprite.Animation == _getAnimation(Animations.ATACK))
            Atack();
        if (_animatedSprite.Animation == _getAnimation(Animations.DEATH))
            QueueFree();
    }
}
