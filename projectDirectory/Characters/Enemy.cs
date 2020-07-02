using Godot;
using System;

public class Enemy : Character
{
    [Export]
    public int Speed = 7000;
    [Export]
    public float Damage = 25f;
    private Barrier _target;
    private AnimatedSprite _animatedSprite;
    private Timer _atackTimer;

    public void SetTarget(Barrier target)
    {
        _target = target;
    }

    public void OnAtackTimerTimeout()
    {
        _target.Hit(Damage);
    }

    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        _atackTimer = GetNode<Timer>("AtackTimer");
    }

    public override void _Process(float delta)
    {
        var velocity = _target.Position - Position;
        if (velocity.Length() > 80)
        {
            _atackTimer.Stop();
            _animatedSprite.Animation = "Walk";
            _animatedSprite.Play();
            LinearVelocity = velocity.Normalized() * Speed * delta;
        }
        else
        {
            if (_atackTimer.IsStopped())
                _atackTimer.Start();
            _animatedSprite.Animation = "Atack";
            _animatedSprite.Play();
            LinearVelocity = Vector2.Zero;
        }
    }
}
