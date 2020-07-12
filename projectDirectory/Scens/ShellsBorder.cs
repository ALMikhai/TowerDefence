using Godot;
using System;

public class ShellsBorder : Area2D
{
    public void _OnShellBodyExited(Node body)
    {
        var shell = (Shell)body;
        shell.QueueFree();
    }
}
