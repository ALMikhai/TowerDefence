using Godot;
using System;
using Static = projectDirectory.Static;

public class Defender : Character
{
    [Export]
    public PackedScene Shell;

    public override void Attack()
    {
        var fireBall = (Shell)Shell.Instance();
        AddChild(fireBall);
        fireBall.SetTarget(_target);
    }
}
