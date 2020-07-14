using Godot;
using System;
using Static = projectDirectory.Scens.Static;

public class DefenderSpawnPoint : Node2D
{
    public override void _Ready()
    {
        AddDefenders();
    }

    private void AddDefenders()
    {
        var defenders = Static.SelectedDefenders.GetDefenders();
        for (int i = 0; i < defenders.Count; i++)
        {
            var defenderNode = (Defender)ObjectCreator.Create(defenders[i]);
            AddChild(defenderNode);
            defenderNode.GlobalPosition = GetNode<Position2D>($"{i}").GlobalPosition;
        }
    }
}
