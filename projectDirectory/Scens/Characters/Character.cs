using Godot;
using System;
using projectDirectory.Static;
using Static = projectDirectory.Static;
using projectDirectory.Scens.Charecters.SM;

public class Character : RigidBody2D
{
    [Export]
    public int AttackRange = 10;
    [Export]
    public float StartReloadTime = 3;
    [Export]
    public int StartDamage = 25;
    [Export]
    public ObjectCreator.Objects Type;

    [Signal]
    public delegate void HpUpdate(int max, int current);
    [Signal]
    public delegate void Death(Character character);

    public float ReloadTime { get; protected set; }
    public int Damage { get; protected set; }
    public int Speed { get; protected set; }

    public StateMachine StateMachine;
    public IdleState _idleState;
    public MoveState _moveState;
    public AttackState _attackState;
    public DeathState _deathState;
    public PreAttackState _preAttackState;

    protected AnimatedSprite _animatedSprite;
    protected HealthNode _healthNode;
    protected Character _target;
    protected Timer _reloadTimer;

    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        _healthNode = GetNode<HealthNode>("HealthNode");
        _reloadTimer = GetNode<Timer>("ReloadTimer");

        StateMachine = new StateMachine();
        _idleState = new IdleState(this, StateMachine);
        _moveState = new MoveState(this, StateMachine);
        _attackState = new AttackState(this, StateMachine);
        _deathState = new DeathState(this, StateMachine);
        _preAttackState = new PreAttackState(this, StateMachine);

        StateMachine.Initialize(_idleState);
    }

    public override void _PhysicsProcess(float delta)
    {
        StateMachine.CurrentState.PhysicsUpdate(delta);
    }

    private void _OnDeath()
    {
        StateMachine.CurrentState.Death();
        EmitSignal(nameof(Death), this);
    }

    private void _OnAnimationFinished()
    {
        StateMachine.CurrentState.AnimationFinished();
    }

    private void _OnHpValueUpdate(int max, int current)
    {
        EmitSignal(nameof(HpUpdate), max, current);
    }

    public void SetAnimation(Static.Character animation)
    {
        _animatedSprite.Animation = AnimationNames.GetCharacterAnimation(animation);
    }

    public void SetTarget(Character target)
    {
        _target = target;
        StateMachine.CurrentState.SetTarget();
    }

    public Character GetTarget()
    {
        if (_target == null)
            throw new NullReferenceException();
        return _target;
    }

    public int GetAttackRange() => AttackRange;

    public bool IsCloseToTarget() => Position.DistanceTo(GetTarget().Position) < GetAttackRange();

    public virtual void Attack() { }
}
