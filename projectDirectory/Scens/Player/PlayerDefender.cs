using Godot;
using System;

public class PlayerDefender : Node2D
{
    [Export]
    public PackedScene Shell;

    [Signal]
    public delegate void ShellsUpdate(int max, int current);

    private int _maxShellsNum = 5;
    private int _shellsNum = 5;
    private int _damage = 25; // Закинуть в DefendersData.
    private Timer _reloadTimer;
    private AudioStreamPlayer _attackSound;

    private Global _global;

    public override void _Ready()
    {
        _reloadTimer = GetNode<Timer>("ReloadTimer");
        _global = GetTree().Root.GetNode<Global>("Global");
        _attackSound = GetNode<AudioStreamPlayer>("AttackSound");
    }

    public void _OnReloadTimerTimeout()
    {
        _shellsNum = _maxShellsNum;
        EmitSignal(nameof(ShellsUpdate), _maxShellsNum, _shellsNum);
    }

    public void Attack(Vector2 destination)
    {
        if (_reloadTimer.IsStopped())
        {
            _shellsNum--;
            EmitSignal(nameof(ShellsUpdate), _maxShellsNum, _shellsNum);
            var shell = (Shell)Shell.Instance();
            AddChild(shell);
            shell.SetDamage(_damage);
            shell.SetDestination(destination);
            _attackSound.Play();

            if (_shellsNum == 0)
            {
                _reloadTimer.Start();
            }
        }
    }

    public int GetShellsNum() => _shellsNum;

    public int GetMaxShellsNum() => _maxShellsNum;
}
