using Godot;
using System;
using Static = projectDirectory.Static;

public class Defender : Character
{
    [Export]
    public PackedScene Shell;

    public void Wait()
    {
        SetAnimation(Static.Character.IDLE);
    }

    protected override void Attack()
    {
        var fireBall = (Shell)Shell.Instance();
        AddChild(fireBall);
        fireBall.SetTarget(_target);
    }
}
