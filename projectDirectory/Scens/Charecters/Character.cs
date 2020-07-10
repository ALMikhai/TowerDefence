using Godot;
using System;
using projectDirectory.Static;
using Static = projectDirectory.Static;
using projectDirectory.Scens.Charecters.SM;

public class Character : RigidBody2D
{
    [Export]
    public int Speed = 70;
    [Export]
    public int AttackRange = 10;

    public StateMachine StateMachine;
    public IdleState _idleState;
    public MoveState _moveState;
    public AttackState _attackState;
    public DeathState _deathState;

    protected AnimatedSprite _animatedSprite;
    protected DamageNode _damageNode;
    protected HealthNode _healthNode;
    protected Character _target;

    [Signal]
    public delegate void Death();

    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        _damageNode = GetNode<DamageNode>("DamageNode");
        _healthNode = GetNode<HealthNode>("HealthNode");

        StateMachine = new StateMachine();
        _idleState = new IdleState(this, StateMachine);
        _moveState = new MoveState(this, StateMachine);
        _attackState = new AttackState(this, StateMachine);
        _deathState = new DeathState(this, StateMachine);

        StateMachine.Initialize(_idleState);
    }

    public override void _PhysicsProcess(float delta)
    {
        StateMachine.CurrentState.PhysicsUpdate(delta);
    }

    private void _OnDeath()
    {
        StateMachine.CurrentState.Death();
        EmitSignal(nameof(Death));
    }

    private void _OnAnimationFinished()
    {
        StateMachine.CurrentState.AnimationFinished();
    }

    public void SetAnimation(Static.Character animation)
    {
        _animatedSprite.Animation = AnimationNames.GetCharacterAnimation(animation);
    }

    public void SetTarget(Character target)
    {
        _target = target;
        _damageNode.SetTarget(target.GetNode<HealthNode>("HealthNode"));
        StateMachine.CurrentState.SetTarget();
    }

    public Character GetTarget()
    {
        if (_target == null)
            throw new NullReferenceException();
        return _target;
    }

    public int GetAttackRange()
    {
        return AttackRange;
    }

    public bool IsCloseToTarget() => Position.DistanceTo(GetTarget().Position) < GetAttackRange();

    public virtual void Attack() { }
}
