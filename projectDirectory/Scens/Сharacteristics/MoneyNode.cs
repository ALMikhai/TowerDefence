using Godot;
using System;

public class MoneyNode : Node2D
{
    [Signal]
    public delegate void BadTransaction(); // Звуковой сигнал.
    [Signal]
    public delegate void GoodTransaction(); // Звуковой сигнал.
    [Signal]
    public delegate void MoneyChange();

    private int _money = 0;

    public int Get() => _money;

    public void Add(int money)
    {
        _money += money;
        EmitSignal(nameof(MoneyChange));
    }

    public bool TrySpend(int money)
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
