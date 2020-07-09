using Godot;
using System;

public class Defender : Character
{
    [Export]
    public PackedScene Shell;

    protected override void Attack()
    {
        var fireBall = (Shell)Shell.Instance();
        AddChild(fireBall);
        fireBall.SetTarget(_target);
    }
}
