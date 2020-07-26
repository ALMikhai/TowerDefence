using Godot;
using System;

public class DefenderCell : Control
{
    [Export]
    public ObjectCreator.Objects Type;

    private Defender _defenderNode;
    private Position2D _defenderPosition;
    private Label _damage;
    private Label _level;
    private Button _levelUpButton;
    private DefendersData _defendersData;

    public override void _Ready()
    {
        _defenderPosition = GetNode<Position2D>("DefenderPosition");
        _damage = GetNode<Label>("Damage/Value");
        _level = GetNode<Label>("Level/Value");
        _levelUpButton = GetNode<Button>("LevelUp");
        _defendersData = GetTree().Root.GetNode<DefendersData>("DefendersData");
        _defenderNode = (Defender)ObjectCreator.Create(Type);

        AddChild(_defenderNode);
        _defenderNode.Position = _defenderPosition.Position;

        UpdateView();
    }

    private void UpdateView()
    {
        _defenderNode._Ready();
        _level.Text = _defendersData.GetDefenderLevel(Type).ToString();
        _damage.Text = ((int)(_defenderNode.Damage / _defenderNode.ReloadTime)).ToString();
        _levelUpButton.Text = _defendersData.GetNextLevelCost(Type).ToString();
    }

    private void _OnLevelUpPressed()
    {
        _defendersData.TryBuyLevel(Type);
        UpdateView();
    }
}
