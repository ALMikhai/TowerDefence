using Godot;
using System;
using Static = projectDirectory.Static;

public class Defender : Character
{
    [Export]
    public PackedScene Shell;

    private DefendersData _defendersData;

    public override void _Ready()
    {
        base._Ready();
        _defendersData = GetTree().Root.GetNode<DefendersData>("DefendersData");

        StartDamage = _defendersData.GetDefenderDamage(Type);
        _reloadTimer.WaitTime = StartReloadTime * (1 / (_defendersData.GetDefenderLevel(Type) * 0.5f));
    }

    public override void Attack()
    {
        var shell = (Shell)Shell.Instance();
        AddChild(shell);
        shell.SetDamage(StartDamage);
        shell.SetDestination(_target.GlobalPosition);
    }
}
