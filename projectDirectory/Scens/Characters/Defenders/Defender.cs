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
        _damage = _defendersData.GetDefenderDamage(Object);
    }

    public override void Attack()
    {
        var shell = (Shell)Shell.Instance();
        AddChild(shell);
        shell.SetDamage(_damage);
        shell.SetDestination(_target.GlobalPosition);
    }
}
