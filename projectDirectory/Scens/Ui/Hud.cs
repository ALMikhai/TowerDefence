using Godot;
using System;

public class Hud : Control
{
    [Signal]
    public delegate void OnFireButtonTouch();
    [Signal]
    public delegate void OnWindButtonTouch();
    [Signal]
    public delegate void PauseButtonPressed();
    
    private Label _shellsLabel;
    private TextureProgress _hpBar;

    public override void _Ready()
    {
        _shellsLabel = GetNode<Label>("Stats/Shells/Current");
        _hpBar = GetNode<TextureProgress>("HpBar");
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

    public void _OnPauseButtonPressed()
    {
        EmitSignal(nameof(PauseButtonPressed));
    }
}
