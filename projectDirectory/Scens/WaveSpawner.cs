using Godot;
using System;

public class WaveSpawner : Node2D
{
    [Signal]
    public delegate void WavesEnd();

    private EnemyContainer _enemyContainer;
    private Character _target;
    private PathFollow2D _spawnLocation;
    private Node2D _spawnNode;
    private Timer _waveSpawnTimer;
    private int _wavesNum = 3;
    private int _enemyOnWave = 3;
    private Random _random = new Random();

    public override void _Ready()
    {
        _enemyContainer = GetNode<EnemyContainer>("EnemyContainer");
        _waveSpawnTimer = GetNode<Timer>("WaveSpawnTimer");
    }

    public void _OnWaveSpawnTimerTimeout()
    {
        _wavesNum--;

        for (int i = 0; i < _enemyOnWave; i++)
        {
            var enemy = _enemyContainer.Add();
            _spawnNode.AddChild(enemy);
            _spawnLocation.Offset = _random.Next();
            enemy.GlobalPosition = _spawnLocation.GlobalPosition;
            enemy.SetTarget(_target);
        }
    }

    private void _StartNewWave()
    {
        if (_wavesNum <= 0)
        {
            EmitSignal(nameof(WavesEnd));
        }
        else
        {
            _waveSpawnTimer.Start();
        }
    }

    public void Start(int waveNum, int enemyOnWave, Node2D spawnNode, PathFollow2D spawnLocation, Character target)
    {
        _target = target;
        _spawnNode = spawnNode;
        _spawnLocation = spawnLocation;
        _wavesNum = waveNum;
        _enemyOnWave = enemyOnWave;
        _StartNewWave();
    }

    public EnemyContainer GetEnemyContainer() => _enemyContainer;
}
