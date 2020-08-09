using Godot;
using System;

public class Effect : Control
{
	[Export] 
	public int Cost = 100;

	private bool _isInit = false;
	protected BattleGround _battleGround;
	private AudioStreamPlayer _audioStreamPlayer;
	private MoneyNode _moneyNode;

	public override void _Ready()
	{
		_audioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		GetNode<Label>("Cost").Text = Cost.ToString();
	}

	public void Init(BattleGround battleGround)
	{
		_battleGround = battleGround;
		_moneyNode = _battleGround.GetNode<MoneyNode>("Hud/Money");
		_isInit = true;
	}

	private void _PreAction()
	{
		if (_isInit == false)
		{
			throw new NullReferenceException("BattleGround is not initialized.");
		}

		if (_moneyNode.TrySpend(Cost))
		{
			_audioStreamPlayer.Play();
			Action();
		}
	}
	
	protected virtual void Action() { }
}
