using Godot;
using System;
using Static = projectDirectory.Static;

public class Defender : Character
{
    [Export]
    public PackedScene Shell;

    public override void Attack()
    {
        var shell = (Shell)Shell.Instance();
        AddChild(shell);
        shell.SetDestination(_target.GlobalPosition);
    }
}
