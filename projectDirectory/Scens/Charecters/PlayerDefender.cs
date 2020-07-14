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
    private Timer _reloadTimer;

    public override void _Ready()
    {
        _reloadTimer = GetNode<Timer>("ReloadTimer");
    }

    public override void _Input(InputEvent @event) // Возможно придется перенести при добавлении меню.
    {
        if (@event is InputEventScreenTouch screenTouchEvent && screenTouchEvent.IsPressed())
        {
            Attack(screenTouchEvent.Position);
        }
    }

    public void _OnReloadTimerTimeout()
    {
        _shellsNum = _maxShellsNum;
        EmitSignal(nameof(ShellsUpdate), _maxShellsNum, _shellsNum);
    }

    private void Attack(Vector2 destination)
    {
        if (_reloadTimer.IsStopped())
        {
            _shellsNum--;
            EmitSignal(nameof(ShellsUpdate), _maxShellsNum, _shellsNum);
            var shell = (Shell)Shell.Instance();
            AddChild(shell);
            shell.SetDestination(destination);

            if (_shellsNum == 0)
            {
                _reloadTimer.Start();
            }
        }
    }

    public int GetShellsNum() => _shellsNum;

    public int GetMaxShellsNum() => _maxShellsNum;
}
