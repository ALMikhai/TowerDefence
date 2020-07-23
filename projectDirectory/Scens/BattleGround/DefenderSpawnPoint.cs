using Godot;
using System;

public class DefenderSpawnPoint : Node2D
{
    private Global _global;

    public override void _Ready()
    {
        _global = GetTree().Root.GetNode<Global>("Global");
        AddDefenders();
    }

    private void AddDefenders()
    {
        var defenders = _global.GetAvailableDefenderList();
        for (int i = 0; i < defenders.Count; i++)
        {
            var defenderNode = (Defender)ObjectCreator.Create(defenders[i]);
            AddChild(defenderNode);
            defenderNode.GlobalPosition = GetNode<Position2D>($"{i}").GlobalPosition;
        }
    }
}
