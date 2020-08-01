using Godot;
using System;

public class MoneyNode : Node2D
{
    [Signal]
    public delegate void MoneyChange(int current);

    private Label _valueLabel;
    private int _money = 0;
    private AudioStreamPlayer _goodTransactionSound;
    private AudioStreamPlayer _badTransactionSound;

    public override void _Ready()
    {
        _goodTransactionSound = GetNode<AudioStreamPlayer>("GoodTransaction");
        _badTransactionSound = GetNode<AudioStreamPlayer>("BadTransaction");
    }

    public int Get() => _money;

    public void Add(int money)
    {
        _money += money;
        UpdateLabel();
    }

    public void ViewAdd(int money, Vector2 position)
    {
        var moneyView = (Money)GD.Load<PackedScene>("res://Scens/Characteristics/Money.tscn").Instance();
        AddChild(moneyView);
        moneyView.GlobalPosition = position;
        moneyView.SetMoneyNode(this);
        moneyView.SetCost(money);
    }

    public bool TrySpend(int money)
    {
        if (money <= _money)
        {
            _money -= money;
            _goodTransactionSound.Play();
            UpdateLabel();
            return true;
        }
        else
        {
            _badTransactionSound.Play();
            return false;
        }
    }

    public void UpdateLabel()
    {
        if (_valueLabel == null)
            _valueLabel = GetNode<Label>("Coin/Value");

        _valueLabel.Text = _money.ToString();
        EmitSignal(nameof(MoneyChange));
    }
}
