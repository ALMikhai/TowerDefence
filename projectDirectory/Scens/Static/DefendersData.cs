using Godot;
using System;
using System.Collections.Generic;

public class DefendersData : Node
{
    private Dictionary<ObjectCreator.Objects, bool> _defenderAvailable;
    private Dictionary<ObjectCreator.Objects, int> _defenderLevels;
    private Dictionary<ObjectCreator.Objects, string> _defenderNames;
    private Global _global;

    public override void _Ready()
    {
        _global = GetTree().Root.GetNode<Global>("Global");

        _defenderAvailable = new Dictionary<ObjectCreator.Objects, bool>
        {
            { ObjectCreator.Objects.DEFENDERGINO, true },
            { ObjectCreator.Objects.DEFENDERFROST, true },
            { ObjectCreator.Objects.ROBOT, true },
        };

        _defenderLevels = new Dictionary<ObjectCreator.Objects, int>
        {
            { ObjectCreator.Objects.PLAYER, 1 },
            { ObjectCreator.Objects.DEFENDERGINO, 1 },
            { ObjectCreator.Objects.DEFENDERFROST, 1 },
            { ObjectCreator.Objects.ROBOT, 1 }
        };

        _defenderNames = new Dictionary<ObjectCreator.Objects, string>
        {
            { ObjectCreator.Objects.PLAYER, "PLAYER" },
            { ObjectCreator.Objects.DEFENDERGINO, "DEFENDERGINO" },
            { ObjectCreator.Objects.DEFENDERFROST, "DEFENDERFROST" },
            { ObjectCreator.Objects.ROBOT, "DEFENDERROBOT" }
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
        var saveData = new Godot.Collections.Dictionary<string, object>();

        foreach (var valuePair in _defenderLevels)
        {
            saveData.Add($"{_defenderNames[valuePair.Key]}_Level", valuePair.Value);
        }
        
        foreach (var valuePair in _defenderAvailable)
        {
            saveData.Add($"{_defenderNames[valuePair.Key]}_Open", valuePair.Value);
        }

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

        var defenderLevels = new Dictionary<ObjectCreator.Objects, int>();
        foreach (var valuePair in _defenderLevels)
        {
            defenderLevels[valuePair.Key] = saveData[$"{_defenderNames[valuePair.Key]}_Level"].ToString().ToInt();
        }
        _defenderLevels = defenderLevels;
        
        var defenderAvailable = new Dictionary<ObjectCreator.Objects, bool>();
        foreach (var valuePair in _defenderAvailable)
        {
            defenderAvailable[valuePair.Key] = (bool)saveData[$"{_defenderNames[valuePair.Key]}_Open"];
        }
        _defenderAvailable = defenderAvailable;

        saveGame.Close();
    }
}
