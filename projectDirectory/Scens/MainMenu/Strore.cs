using Godot;
using System;

public class Strore : Control
{
    private Label _money;
    private Global _global;

    public override void _Ready()
    {
        _global = GetTree().Root.GetNode<Global>("Global");
        _money = GetNode<Label>("Money/Value");


        _global.Connect(nameof(Global.Update), this, nameof(_OnGlobalUpdate));

        _OnGlobalUpdate();
    }

    private void _OnGlobalUpdate()
    {
        _money.Text = _global.GetMoney().ToString();
    }
}
