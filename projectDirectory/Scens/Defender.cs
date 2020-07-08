using Godot;
using System;
using projectDirectory.Scens;

public class Defender : Character
{
    [Export]
    public PackedScene Shell;

    protected override void Atack()
    {
        var fireBall = (Fireball)Shell.Instance();
        var damageNode = fireBall.GetNode<DamageNode>("DamageNode");
        damageNode.SetTarget(_damageNode.GetTarget());
        AddChild(fireBall);
    }
}
