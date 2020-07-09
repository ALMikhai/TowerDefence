using Godot;
using System;
using System.Collections.Generic;

public class BattleGround : Node2D
{
    // Этот класс управляет поялением персонажей и их 
    // сражением + оповещает о проигрыще или выигрыше.

    [Signal]
    public delegate void Win();
    [Signal]
    public delegate void Lose();
    [Signal]
    public delegate void EnemyDeath(int moneyDrop);

    private int _wavesNum = 3;
    private int _enemyOnWave = 3;
    private Queue<Enemy> _enemies = new Queue<Enemy>();
    private Random _random = new Random();
    private Character _crystal;
    private Position2D _defenderSpawnPoint;
    private PathFollow2D _enemySpawnLocation;
    private Timer _waveSpawnTimer;

    public override void _Ready()
    {
        _crystal = GetNode<Character>("Crystal");
        _defenderSpawnPoint = GetNode<Position2D>("DefendersSpawnPoint");
        _enemySpawnLocation = GetNode<PathFollow2D>("EnemyPath/EnemySpawnLocation");
        _waveSpawnTimer = GetNode<Timer>("WaveSpawnTimer");

        _crystal.Connect(nameof(Character.Death), this, nameof(OnLose));
        AddDefender();
    }

    public void _OnWaveSpawnTimerTimeout()
    {
        for (int i = 0; i < _enemyOnWave; i++)
            AddEnemy();
    }

    private void AddDefender()
    {
        var defender = (Defender)ObjectCreator.Create(ObjectCreator.Objects.DEFENDER);
        AddChild(defender);
        defender.Position = _defenderSpawnPoint.Position;
    }

    private void AddEnemy()
    {
        var enemy = (Enemy)ObjectCreator.Create(ObjectCreator.Objects.ENEMYNEAR);
        AddChild(enemy);
        _enemySpawnLocation.Offset = _random.Next();
        enemy.Position = _enemySpawnLocation.Position;
        enemy.SetTarget(_crystal);

        _enemies.Enqueue(enemy);
        enemy.Connect(nameof(Enemy.Death), this, nameof(OnEnemyDeath));
        UpdateDefendersTarget();
    }

    private void UpdateDefendersTarget()
    {
        if (_enemies.Count > 0)
        {
            GetTree().CallGroup("Defender", "SetTarget", _enemies.Peek());
        }
        else
        {
            GetTree().CallGroup("Defender", "Wait");
            AddWave();
        }
    }

    private void AddWave()
    {
        if (_wavesNum > 0)
        {
            _wavesNum--;
            _waveSpawnTimer.Start();
        }
        else
        {
            OnWin();
        }
    }

    private void OnEnemyDeath()
    {
        EmitSignal(nameof(EnemyDeath), 0);
        var enemy = _enemies.Dequeue();
        enemy.Disconnect(nameof(Enemy.Death), this, nameof(OnEnemyDeath));
        UpdateDefendersTarget();
    }

    private void Start() { }

    private void OnLose()
    {
        EmitSignal(nameof(Lose));
        QueueFree();
    }

    private void OnWin()
    {
        EmitSignal(nameof(Win));
        QueueFree();
    }
}
