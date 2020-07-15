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
    private Dictionary<ObjectCreator.Objects, bool> _defenderPackedPaths = new Dictionary<ObjectCreator.Objects, bool>
    {
        { ObjectCreator.Objects.DEFENDERGINO, true },
        { ObjectCreator.Objects.DEFENDER, false }
    };

    private Dictionary<ObjectCreator.Objects, CharacterStats> _characterStats = new Dictionary<ObjectCreator.Objects, CharacterStats>
    {
            { ObjectCreator.Objects.DEFENDERGINO, new CharacterStats(100, 10) },
            { ObjectCreator.Objects.DEFENDER, new CharacterStats(100, 10) },
            { ObjectCreator.Objects.CRYSTAL, new CharacterStats(100, 10) },
            { ObjectCreator.Objects.ENEMYNEAR, new CharacterStats(100, 25) }
    };

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
        foreach (var defender in _defenderPackedPaths)
        {
            if (defender.Value == true)
                result.Add(defender.Key);
        }
        return result;
    }

    
}
