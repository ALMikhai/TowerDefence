using Godot;
using System;
using Static = projectDirectory.Static;

public class Enemy : Character
{
    [Export]
    public int Cost = 25;
    [Export]
    public int StartSpeed = 50;
    [Export]
    public int StartHp = 100;

    private TextureProgress _hpBar;
    protected Global _global;

    public override void _Ready()
    {
        base._Ready();
        _hpBar = GetNode<TextureProgress>("HpBar");
        _global = GetTree().Root.GetNode<Global>("Global");

        StartDamage = StartDamage + (int)Math.Sqrt(_global.GetLevel() * 5);
        Speed = StartSpeed + (int)Math.Sqrt(_global.GetLevel() * 500);
        _healthNode.SetHealth(StartHp + (int)Math.Sqrt(_global.GetLevel() * 150));
        _reloadTimer.WaitTime = 0;
    }
    
    public int GetCost() => Cost;

    private void _OnHpValueUpdate(int max, int current)
    {
        _hpBar.MaxValue = max;
        _hpBar.Value = current;
    }
}
