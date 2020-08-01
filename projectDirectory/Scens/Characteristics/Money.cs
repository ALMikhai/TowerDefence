using Godot;
using System;

public class Money : Node2D
{
    private MoneyNode _moneyNode;
    private int _speed = 700;
    private int _cost = 0;

    public void SetMoneyNode(MoneyNode node)
    {
        _moneyNode = node;
    }

    public void SetCost(int cost)
    {
        _cost = cost;
    }

    public override void _PhysicsProcess(float delta)
    {
        if (_moneyNode == null)
            return;

        GlobalPosition += (_moneyNode.GlobalPosition - GlobalPosition).Normalized() * delta * _speed;
        if (GlobalPosition.DistanceTo(_moneyNode.GlobalPosition) <= 10)
        {
            _moneyNode.Add(_cost);
            _moneyNode.UpdateLabel();
            QueueFree();
        }
    }
}
