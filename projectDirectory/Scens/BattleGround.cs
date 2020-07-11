using Godot;
using System;
using System.Collections.Generic;
using Static = projectDirectory.Scens.Static;

public class BattleGround : Node2D
{
    // Этот класс управляет поялением персонажей и их 
    // сражением + оповещает о проигрыще или выигрыше.

    [Signal]
    public delegate void Win();
    [Signal]
    public delegate void Lose();
    [Signal]
    public delegate void OnWaveEnd();
    [Signal]
    public delegate void EnemyDeath(int moneyDrop); // При использовании MoneyNode, может не понадобиться.

    private int _wavesNum = 3;
    private int _enemyOnWave = 3;
    private List<Enemy> _enemies = new List<Enemy>();
    private Random _random = new Random();
    private Character _crystal;
    private PathFollow2D _enemySpawnLocation;
    private Timer _waveSpawnTimer;
    private MoneyNode _moneyNode;
    private Label _moneyLabel; // Test !!!

    public override void _Ready()
    {
        _crystal = GetNode<Character>("Crystal");
        _enemySpawnLocation = GetNode<PathFollow2D>("EnemyPath/EnemySpawnLocation");
        _waveSpawnTimer = GetNode<Timer>("WaveSpawnTimer");
        _moneyLabel = GetNode<Label>("TestMoneyLable/Money"); // Test !!!
        _moneyNode = GetNode<MoneyNode>("MoneyNode");

        _crystal.Connect(nameof(Character.Death), this, nameof(OnLose));
        AddDefenders();
    }

    public void _OnWaveSpawnTimerTimeout()
    {
        for (int i = 0; i < _enemyOnWave; i++)
            AddEnemy();
    }

    public void _OnMoneyChange() // Test !!!
    {
        _moneyLabel.Text = _moneyNode.Get().ToString();
    }

    public void Start(int waveNum, int enemyOnWave)
    {
        _wavesNum = waveNum;
        _enemyOnWave = enemyOnWave;
    }

    private void AddDefenders()
    {
        var defenders = Static.SelectedDefenders.GetDefenders();
        for (int i = 0; i < defenders.Count; i++)
        {
            var defenderNode = (Defender)ObjectCreator.Create(defenders[i]);
            AddChild(defenderNode);
            defenderNode.Position = GetNode<Position2D>($"DefendersSpawnPoint{i}").Position;
        }
    }

    private void AddEnemy()
    {
        var enemy = (Enemy)ObjectCreator.Create(ObjectCreator.Objects.ENEMYNEAR);
        AddChild(enemy);
        _enemySpawnLocation.Offset = _random.Next();
        enemy.Position = _enemySpawnLocation.Position;
        enemy.SetTarget(_crystal);

        _enemies.Add(enemy);
        enemy.Connect(nameof(Enemy.Death), this, nameof(OnEnemyDeath));
        UpdateDefendersTarget();
    }

    private void UpdateDefendersTarget()
    {
        if (_enemies.Count > 0)
        {
            GetTree().CallGroup("Defender", "SetTarget", _enemies[0]);
        }
        else
        {
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

    private void OnEnemyDeath(Enemy enemy)
    {
        EmitSignal(nameof(EnemyDeath), 0); // При использовании MoneyNode, может не понадобиться.
        _enemies.Remove(enemy);
        _moneyNode.Add(enemy.GetCost());
        enemy.Disconnect(nameof(Enemy.Death), this, nameof(OnEnemyDeath));
        UpdateDefendersTarget();
    }

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
