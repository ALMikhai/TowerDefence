using Godot;
using System;

public class PauseMenu : Control
{
    [Signal]
    public delegate void ContinueButtonPressed();
    [Signal]
    public delegate void ExitButtonPressed();

    public void _OnContinuePressed()
    {
        EmitSignal(nameof(ContinueButtonPressed));
    }

    public void _OnExitPressed()
    {
        EmitSignal(nameof(ExitButtonPressed));
    }
}
