using Godot;
using System;

public class DefenderSpawnPoint : Node2D
{
    private DefendersData _defendersData;

    public override void _Ready()
    {
        _defendersData = GetTree().Root.GetNode<DefendersData>("DefendersData");
        AddDefenders();
    }

    private void AddDefenders()
    {
        var defenders = _defendersData.GetAvailableDefenderList();
        for (var i = 0; i < defenders.Count; i++)
        {
            var defenderNode = (Defender)ObjectCreator.Create(defenders[i]);
            AddChild(defenderNode);
            defenderNode.GlobalPosition = GetNode<Position2D>($"{i}").GlobalPosition;
        }
    }
}
