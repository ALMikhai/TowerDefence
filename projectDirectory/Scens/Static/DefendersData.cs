using Godot;
using System;
using System.Collections.Generic;

public class DefendersData : Node
{
    private Dictionary<ObjectCreator.Objects, bool> _defenderAvailable;
    private Dictionary<ObjectCreator.Objects, int> _defenderLevels;
    private Global _global;

    public override void _Ready()
    {
        _global = GetTree().Root.GetNode<Global>("Global");

        _defenderAvailable = new Dictionary<ObjectCreator.Objects, bool>
        {
            { ObjectCreator.Objects.DEFENDERGINO, true },
            { ObjectCreator.Objects.DEFENDER, true }
        };

        _defenderLevels = new Dictionary<ObjectCreator.Objects, int>
        {
            { ObjectCreator.Objects.DEFENDERGINO, 1 },
            { ObjectCreator.Objects.DEFENDER, 1 }
        };

        Load();
    }

    public List<ObjectCreator.Objects> GetAvailableDefenderList()
    {
        var result = new List<ObjectCreator.Objects>();
        foreach (var defender in _defenderAvailable)
        {
            if (defender.Value == true)
                result.Add(defender.Key);
        }
        return result;
    }

    public int GetDefenderLevel(ObjectCreator.Objects obj)
    {
        return _defenderLevels[obj];
    }

    public int GetDefenderDamage(ObjectCreator.Objects obj)
    {
        return (int)(25 + Math.Sqrt(350 * _defenderLevels[obj]));
    }

    public int GetNextLevelCost(ObjectCreator.Objects obj)
    {
        return 100 + 14 * _defenderLevels[obj];
    }

    public void TryBuyLevel(ObjectCreator.Objects obj)
    {
        var cost = GetNextLevelCost(obj);
        var money = _global.Money;

        if (money.TrySpend(cost))
        {
            _defenderLevels[obj]++;
        }
    }

    public void Save()
    {
        var saveFile = new File();
        saveFile.Open("user://DefendersData.save", File.ModeFlags.Write);

        var saveData = new Godot.Collections.Dictionary<string, object>
        {
            { "DEFENDERGINO_LEVEL", _defenderLevels[ObjectCreator.Objects.DEFENDERGINO]},
            { "DEFENDER_LEVEL", _defenderLevels[ObjectCreator.Objects.DEFENDER]},
            { "DEFENDERGINO_OPEN", _defenderAvailable[ObjectCreator.Objects.DEFENDERGINO] },
            { "DEFENDER_OPEN", _defenderAvailable[ObjectCreator.Objects.DEFENDER] }
        };

        saveFile.StoreLine(JSON.Print(saveData));
        saveFile.Close();
    }

    private void Load()
    {
        var saveGame = new File();
        if (!saveGame.FileExists("user://DefendersData.save"))
            return;

        saveGame.Open("user://DefendersData.save", File.ModeFlags.Read);

        var saveData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);

        _defenderLevels[ObjectCreator.Objects.DEFENDERGINO] = saveData["DEFENDERGINO_LEVEL"].ToString().ToInt();
        _defenderLevels[ObjectCreator.Objects.DEFENDER] = saveData["DEFENDER_LEVEL"].ToString().ToInt();

        _defenderAvailable[ObjectCreator.Objects.DEFENDERGINO] = (bool)saveData["DEFENDERGINO_OPEN"];
        _defenderAvailable[ObjectCreator.Objects.DEFENDER] = (bool)saveData["DEFENDER_OPEN"];

        saveGame.Close();
    }
}
