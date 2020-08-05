using Godot;
using System;
using System.Collections.Generic;

public class EnemyContainer : Node
{
	[Signal]
	public delegate void Updated();
	[Signal]
	public delegate void EnemyDeath(Enemy enemy);
	[Signal]
	public delegate void AllEnemiesDeath();

	private List<Enemy> _enemies = new List<Enemy>();

	public bool IsEmpty() => _enemies.Count <= 0;

	public Enemy GetActual() => _enemies[0];

	private void _OnEnemyDeath(Enemy enemy)
	{
		enemy.Disconnect(nameof(Character.Death), this, nameof(_OnEnemyDeath));
		_enemies.Remove(enemy);
		EmitSignal(nameof(EnemyDeath), enemy);

		if (_enemies.Count <= 0)
			EmitSignal(nameof(AllEnemiesDeath));

		EmitSignal(nameof(Updated));
	}

	public Enemy Add()
	{
		var enemy = (Enemy)ObjectCreator.Create(ObjectCreator.Objects.ENEMYNEAR);
		_enemies.Add(enemy);
		enemy.Connect(nameof(Character.Death), this, nameof(_OnEnemyDeath));
		EmitSignal(nameof(Updated));
		return enemy;
	}
}
