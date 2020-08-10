using Godot;
using System;
using System.Collections.Generic;

public class Global : Node
{
    public MoneyNode Money { get; private set; }
    public int Level { get; private set; } = 1;

    public override void _Ready()
    {
        Money = (MoneyNode)(GD.Load<PackedScene>("res://Scens/Characteristics/MoneyNode.tscn").Instance());
        Load();
    }

    public void NextLevel()
    {
        Level++;
    }

    public void Save()
    {
        var saveFile = new File();
        saveFile.Open("user://Global.save", File.ModeFlags.Write);

        var saveData = new Godot.Collections.Dictionary<string, object>
        {
            { "MONEY", Money.Get() },
            { "LEVEL", Level }
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
        Level = saveData["LEVEL"].ToString().ToInt();

        saveGame.Close();
    }
}
