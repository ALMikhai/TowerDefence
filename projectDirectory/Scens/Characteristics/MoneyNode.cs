using Godot;
using System;

public class MoneyNode : Node2D
{
    [Signal]
    public delegate void BadTransaction(); // Звуковой сигнал.
    [Signal]
    public delegate void GoodTransaction(); // Звуковой сигнал.
    [Signal]
    public delegate void MoneyChange(int current);

    private Label _valueLabel;
    private int _money = 0;

    public int Get() => _money;

    public void Add(int money)
    {
        _money += money;
        UpdateLabel();
    }

    public bool TrySpend(int money)
    {
        if (money <= _money)
        {
            _money -= money;
            EmitSignal(nameof(GoodTransaction));
            UpdateLabel();
            return true;
        }
        else
        {
            EmitSignal(nameof(BadTransaction));
            return false;
        }
    }

    private void UpdateLabel()
    {
        if (_valueLabel == null)
            _valueLabel = GetNode<Label>("Coin/Value");

        _valueLabel.Text = _money.ToString();
        EmitSignal(nameof(MoneyChange));
    }
}
