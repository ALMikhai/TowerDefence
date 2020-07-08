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
    private Queue<Enemy> _enemies = new Queue<Enemy>();
    private Random _random = new Random();
    private Barrier _barrier;
    private Position2D _defenderSpawnPoint;
    private PathFollow2D _enemySpawnLocation;

    public override void _Ready()
    {
        _barrier = GetNode<Barrier>("Barrier");
        _barrier.Connect(nameof(Barrier.Broken), this, nameof(OnLose));

        _defenderSpawnPoint = GetNode<Position2D>("DefendersSpawnPoint");
        _enemySpawnLocation = GetNode<PathFollow2D>("EnemyPath/EnemySpawnLocation");

        AddDefender();
    }

    public void _OnEnemySpawnTimerTimeout()
    {
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
        enemy.SetTarget((HealthNode)_barrier.GetNode<HealthNode>("HealthNode"));

        _enemies.Enqueue(enemy);
        enemy.Connect(nameof(Enemy.Death), this, nameof(OnEnemyDeath));
        UpdateDefendersTarget();
    }

    private void UpdateDefendersTarget()
    {
        if (_enemies.Count > 0)
        {
            GetTree().CallGroup("Defender", "SetTarget", _enemies.Peek().GetNode<HealthNode>("HealthNode"));
        }
        else
        {
            GetTree().CallGroup("Defender", "Wait");
        }
       
    }

    private void OnEnemyDeath()
    {
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
