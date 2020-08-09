using Godot;
using System;

public class HealEffect : Effect
{
	protected override void Action()
	{
		var healthNode = _battleGround.GetNode<HealthNode>("Crystal/HealthNode");
		healthNode.ResetHealth();
	}
}
