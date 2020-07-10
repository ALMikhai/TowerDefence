using Godot;
using System;

public class MoneyNode : Node2D
{
    [Signal]
    public delegate void BadTransaction();
    [Signal]
    public delegate void GoodTransaction();
    [Signal]
    public delegate void MoneyChange();

    private int _money = 0;

    public int GetMoney() => _money;

    public void AddMoney(int money)
    {
        _money += money;
        EmitSignal(nameof(MoneyChange));
    }

    public bool SpendMoney(int money)
    {
        if (money <= _money)
        {
            _money -= money;
            EmitSignal(nameof(GoodTransaction));
            EmitSignal(nameof(MoneyChange));
            return true;
        }
        else
        {
            EmitSignal(nameof(BadTransaction));
            return false;
        }
    }
}
