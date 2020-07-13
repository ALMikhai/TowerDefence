using Godot;
using System;

public class Hud : Control
{
    [Signal]
    public delegate void OnFireButtonTouch();
    [Signal]
    public delegate void OnWindButtonTouch();

    private Label _moneyLabel;
    private Label _shellsLabel;
    private TextureProgress _hpBar;

    public override void _Ready()
    {
        _moneyLabel = GetNode<Label>("Stats/Money/Current");
        _shellsLabel = GetNode<Label>("Stats/Shells/Current");
        _hpBar = GetNode<TextureProgress>("HpBar");
    }

    public void _OnMoneyUpdate(int current)
    {
        _moneyLabel.Text = current.ToString();
    }

    public void _OnShellsUpdate(int max, int current)
    {
        _shellsLabel.Text = $"{current} / {max}";
    }

    public void _OnCrystalHpUpdate(int max, int current)
    {
        _hpBar.MaxValue = max;
        _hpBar.Value = current;
    }
}
