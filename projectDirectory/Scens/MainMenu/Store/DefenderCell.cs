using Godot;
using System;

public class DefenderCell : Control
{
	[Export]
	public ObjectCreator.Objects Type;

	private Defender _defenderNode;
	private Position2D _defenderPosition;
	private Label _dps;
	private Label _level;
	private Label _levelUpCostLabel;
	private DefendersData _defendersData;

	public override void _Ready()
	{
		_defenderPosition = GetNode<Position2D>("DefenderPosition");
		_dps = GetNode<Label>("Dps/Value");
		_level = GetNode<Label>("Level");
		_levelUpCostLabel = GetNode<Label>("LevelUp/Cost");
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
		_dps.Text = ((int)(_defenderNode.Damage / _defenderNode.ReloadTime)).ToString();
		_levelUpCostLabel.Text = _defendersData.GetNextLevelCost(Type).ToString();
	}

	private void _OnLevelUpPressed()
	{
		_defendersData.TryBuyLevel(Type);
		UpdateView();
	}
}
