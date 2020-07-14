using Godot;
using System;
using System.Collections.Generic;
using Static = projectDirectory.Scens.Static;
using projectDirectory.Scens.GameSM;

public class BattleGround : Node2D
{
    [Signal]
    public delegate void Win();
    [Signal]
    public delegate void Lose();
    [Signal]
    public delegate void OnWaveEnd();

    private Character _crystal;
    private WaveSpawner _waveSpawner;
    private EnemyContainer _enemyContainer;
    private MoneyNode _moneyNode;

    private StateMachine _stateMachine;
    public PauseState _pauseState;
    public PlayerAttackState _playerAttackState;

    public override void _Ready()
    {
        _crystal = GetNode<Character>("Crystal");
        _waveSpawner = GetNode<WaveSpawner>("WaveSpawner");
        _enemyContainer = _waveSpawner.GetEnemyContainer();
        _moneyNode = GetNode<MoneyNode>("MoneyNode");

        _enemyContainer.Connect(nameof(EnemyContainer.Updated), this, nameof(_OnEnemyContainerUpdated));
        _enemyContainer.Connect(nameof(EnemyContainer.EnemyDeath), this, nameof(_OnEnemyDeath));

        _stateMachine = new StateMachine();
        _pauseState = new PauseState(this, _stateMachine);
        _playerAttackState = new PlayerAttackState(this, _stateMachine);

        _stateMachine.Initialize(_playerAttackState);

        Start(3, 15);
    }

    public override void _Input(InputEvent @event)
    {
        _stateMachine.CurrentState.HandleInput(@event);
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
        _moneyNode.Add(enemy.GetCost());
    }

    public void Start(int waveNum, int enemyOnWave)
    {
        _waveSpawner.Start(waveNum, enemyOnWave, this, GetNode<PathFollow2D>("EnemyPath/EnemySpawnLocation"), _crystal);
    }
}
