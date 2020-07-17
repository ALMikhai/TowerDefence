using Godot;
using System;
using System.Collections.Generic;

public class CharacterStats
{
    public int Hp { get; set; }
    public int Damage { get; set; }
    public CharacterStats(int hp, int damage)
    {
        Hp = hp;
        Damage = damage;
    }
}

public class Global : Node
{
    public enum Stats { HP, DAMAGE }

    [Signal]
    public delegate void Update();

    private MoneyNode _moneyNode;
    private Dictionary<ObjectCreator.Objects, bool> _defenderAvailable;
    private Dictionary<ObjectCreator.Objects, CharacterStats> _characterStats;
    private int _hpPrice = 100;
    private int _damagePrice = 200;

    public override void _Ready()
    {
        _defenderAvailable = new Dictionary<ObjectCreator.Objects, bool>
        {
            { ObjectCreator.Objects.DEFENDERGINO, true },
            { ObjectCreator.Objects.DEFENDER, true }
        };

        _characterStats = new Dictionary<ObjectCreator.Objects, CharacterStats>
        {
            { ObjectCreator.Objects.DEFENDERGINO, new CharacterStats(100, 25) },
            { ObjectCreator.Objects.DEFENDER, new CharacterStats(100, 25) },
            { ObjectCreator.Objects.CRYSTAL, new CharacterStats(100, 15) },
            { ObjectCreator.Objects.ENEMYNEAR, new CharacterStats(100, 25) }
        };

        _moneyNode = (MoneyNode)(GD.Load<PackedScene>("res://Scens/Сharacteristics/MoneyNode.tscn").Instance());
        AddChild(_moneyNode);
        Load();
    }

    public void TryBuy(ObjectCreator.Objects obj, Stats stat)
    {
        var cost = 0;

        if (stat == Stats.HP)
        {
            cost = _hpPrice;
        }
        else
        {
            cost = _damagePrice;
        }

        if (_moneyNode.TrySpend(cost))
        {
            if (stat == Stats.HP)
            {
                _characterStats[obj].Hp += 25;
            }
            else
            {
                _characterStats[obj].Damage += 25;
            }

            EmitSignal(nameof(Update));
        }
    }

    public CharacterStats GetCharacterStats(ObjectCreator.Objects obj)
    {
        try 
        {
            return _characterStats[obj];
        }
        catch
        {
            throw new Exception("Bad object name.");
        }
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

    public int GetMoney()
    {
        return _moneyNode.Get();
    }

    public void AddMoney(int money)
    {
        _moneyNode.Add(money);
    }

    public void Save()
    {
        var saveFile = new File();
        saveFile.Open("user://savegame.save", File.ModeFlags.Write);

        var saveData = new Godot.Collections.Dictionary<string, object>
        {
            { "DEFENDERGINO_HP", _characterStats[ObjectCreator.Objects.DEFENDERGINO].Hp },
            { "DEFENDERGINO_DAMAGE", _characterStats[ObjectCreator.Objects.DEFENDERGINO].Damage },
            { "DEFENDER_HP", _characterStats[ObjectCreator.Objects.DEFENDER].Hp },
            { "DEFENDER_DAMAGE", _characterStats[ObjectCreator.Objects.DEFENDER].Damage },
            { "CRYSTAL_HP", _characterStats[ObjectCreator.Objects.CRYSTAL].Hp },
            { "CRYSTAL_DAMAGE", _characterStats[ObjectCreator.Objects.CRYSTAL].Damage },
            { "ENEMYNEAR_HP", _characterStats[ObjectCreator.Objects.ENEMYNEAR].Hp },
            { "ENEMYNEAR_DAMAGE", _characterStats[ObjectCreator.Objects.ENEMYNEAR].Damage },
            { "DEFENDERGINO_OPEN", _defenderAvailable[ObjectCreator.Objects.DEFENDERGINO] },
            { "DEFENDER_OPEN", _defenderAvailable[ObjectCreator.Objects.DEFENDER] },
            { "MONEY", _moneyNode.Get() }
        };

        saveFile.StoreLine(JSON.Print(saveData));
        saveFile.Close();
    }

    private void Load()
    {
        var saveGame = new File();
        if (!saveGame.FileExists("user://savegame.save"))
            return;

        saveGame.Open("user://savegame.save", File.ModeFlags.Read);

        var saveData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);

        _characterStats[ObjectCreator.Objects.DEFENDERGINO] = new CharacterStats(saveData["DEFENDERGINO_HP"].ToString().ToInt(), (saveData["DEFENDERGINO_DAMAGE"]).ToString().ToInt());
        _characterStats[ObjectCreator.Objects.DEFENDER] = new CharacterStats(saveData["DEFENDER_HP"].ToString().ToInt(), saveData["DEFENDER_DAMAGE"].ToString().ToInt());
        _characterStats[ObjectCreator.Objects.CRYSTAL] = new CharacterStats(saveData["CRYSTAL_HP"].ToString().ToInt(), saveData["CRYSTAL_DAMAGE"].ToString().ToInt());
        _characterStats[ObjectCreator.Objects.ENEMYNEAR] = new CharacterStats(saveData["ENEMYNEAR_HP"].ToString().ToInt(), saveData["ENEMYNEAR_DAMAGE"].ToString().ToInt());

        _defenderAvailable[ObjectCreator.Objects.DEFENDERGINO] = (bool)saveData["DEFENDERGINO_OPEN"];
        _defenderAvailable[ObjectCreator.Objects.DEFENDER] = (bool)saveData["DEFENDER_OPEN"];

        _moneyNode.Add(saveData["MONEY"].ToString().ToInt());

        saveGame.Close();
    }
}
