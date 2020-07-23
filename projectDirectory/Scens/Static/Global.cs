using Godot;
using System;
using System.Collections.Generic;

public class Global : Node
{
    public MoneyNode Money { get; private set; }
    private int _level = 1;

    public override void _Ready()
    {
        Money = (MoneyNode)(GD.Load<PackedScene>("res://Scens/Characteristics/MoneyNode.tscn").Instance());
        Load();
    }

    public int GetLevel()
    {
        return _level;
    }

    public void NextLevel()
    {
        _level++;
    }

    public void Save()
    {
        var saveFile = new File();
        saveFile.Open("user://Global.save", File.ModeFlags.Write);

        var saveData = new Godot.Collections.Dictionary<string, object>
        {
            { "MONEY", Money.Get() },
            { "LEVEL", _level }
        };

        saveFile.StoreLine(JSON.Print(saveData));
        saveFile.Close();
    }

    private void Load()
    {
        var saveGame = new File();
        if (!saveGame.FileExists("user://Global.save"))
            return;

        saveGame.Open("user://Global.save", File.ModeFlags.Read);

        var saveData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);

        Money.Add(saveData["MONEY"].ToString().ToInt());
        _level = saveData["LEVEL"].ToString().ToInt();

        saveGame.Close();
    }
}
