using Godot;
using System;
using Static = projectDirectory.Static;

public class Enemy : Character
{
    [Export]
    public int Cost = 25;

    private TextureProgress _hpBar;

    public override void _Ready()
    {
        base._Ready();
        _hpBar = GetNode<TextureProgress>("HpBar");
    }
    public int GetCost() => Cost;

    private void _OnHpValueUpdate(int max, int current)
    {
        _hpBar.MaxValue = max;
        _hpBar.Value = current;
    }
}
