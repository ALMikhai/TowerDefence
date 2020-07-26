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

        Damage = (int)(StartDamage + Math.Sqrt(350 * _defendersData.GetDefenderLevel(Type)));
        ReloadTime = StartReloadTime * (1 / (_defendersData.GetDefenderLevel(Type) * 0.5f));
        _reloadTimer.WaitTime = ReloadTime;
    }

    public override void Attack()
    {
        var shell = (Shell)Shell.Instance();
        AddChild(shell);
        shell.SetDamage(Damage);
        shell.SetDestination(_target.GlobalPosition);
    }
}
