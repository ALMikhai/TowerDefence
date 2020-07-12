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

    private Character _crystal;
    private WaveSpawner _waveSpawner;
    private EnemyContainer _enemyContainer;
    private MoneyNode _moneyNode;
    private Label _moneyLabel; // Test !!!

    public override void _Ready()
    {
        _crystal = GetNode<Character>("Crystal");
        _waveSpawner = GetNode<WaveSpawner>("WaveSpawner");
        _enemyContainer = _waveSpawner.GetEnemyContainer();
        _moneyLabel = GetNode<Label>("TestMoneyLable/Money"); // Test !!!
        _moneyNode = GetNode<MoneyNode>("MoneyNode");

        _enemyContainer.Connect(nameof(EnemyContainer.Updated), this, nameof(_OnEnemyContainerUpdated));
        _enemyContainer.Connect(nameof(EnemyContainer.EnemyDeath), this, nameof(_OnEnemyDeath));

        Start(3, 15);
    }

    public void _OnMoneyChange() // Test !!!
    {
        _moneyLabel.Text = _moneyNode.Get().ToString();
    }

    public void _OnCrystalBroke(Character character)
    {
        EmitSignal(nameof(Lose));
        QueueFree();
    }

    public void _OnWavesEnd()
    {
        EmitSignal(nameof(Win));
        QueueFree();
    }

    private void _OnEnemyContainerUpdated()
    {
        if (!_enemyContainer.IsEmpty())
        {
            GetTree().CallGroup("Defender", "SetTarget", _enemyContainer.GetActual());
        }
    }

    private void _OnEnemyDeath(Enemy enemy)
    {
        EmitSignal(nameof(EnemyDeath), 0); // При использовании MoneyNode, может не понадобиться.
        _moneyNode.Add(enemy.GetCost());
    }

    public void Start(int waveNum, int enemyOnWave)
    {
        AddDefenders();
        _waveSpawner.Start(waveNum, enemyOnWave, this, GetNode<PathFollow2D>("EnemyPath/EnemySpawnLocation"), _crystal);
    }

    private void AddDefenders()
    {
        var defenders = Static.SelectedDefenders.GetDefenders();
        for (int i = 0; i < defenders.Count; i++)
        {
            var defenderNode = (Defender)ObjectCreator.Create(defenders[i]);
            AddChild(defenderNode);
            defenderNode.Position = GetNode<Position2D>($"DefenderSpawnPoint/{i}").GlobalPosition;
        }
    }
}
