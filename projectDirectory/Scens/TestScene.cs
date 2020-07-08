using Godot;
using System.Collections.Generic;

public class TestScene : Node
{
    [Export]
    public PackedScene Barrier;

    private Queue<Barrier> _barriers = new Queue<Barrier>();
    private Barrier _barrierNow;
    private HealthNode _barrierHelthNode;

    private EnemySpawner _enemySpawner;
    private Queue<Enemy> _enemies;
    private Enemy _enemyNow;
    private HealthNode _enemyHelthNode;

    private int _numWaves = 3;

    public override void _Ready()
    {
        _enemySpawner = GetNode<EnemySpawner>("EnemySpawner");

        for (int i = 0; i < 4; i++)
        {
            var newBarrier = (Barrier)Barrier.Instance();
            newBarrier.Position = GetNode<Position2D>($"BarrierPos{i}").Position;
            AddChild(newBarrier);
            _barriers.Enqueue(newBarrier);
        }
        _enemies = _enemySpawner.CreateNewWave(_numWaves);
        SetEnemyTarget();
        SetDefenderTarget();
    }

    private void SetEnemyTarget()
    {
        if (_barriers.Count > 0)
        {
            _barrierNow = _barriers.Dequeue();
            _barrierNow.Connect("Broken", this, nameof(SetEnemyTarget));
            _barrierHelthNode = (HealthNode)_barrierNow.GetNode<HealthNode>("HealthNode");
            GetTree().CallGroup("Enemy", "SetTarget", _barrierHelthNode);
        }
        else
        {
            QueueFree();
        }
    }

    private void SetDefenderTarget()
    {
        if (_enemies.Count > 0)
        {
            if (_enemyNow != null)
                _enemyNow.Disconnect(nameof(Character.Death), this, nameof(SetDefenderTarget));

            _enemyNow = _enemies.Dequeue();
            _enemyNow.Connect(nameof(Character.Death), this, nameof(SetDefenderTarget));
            _enemyHelthNode = (HealthNode)_enemyNow.GetNode<HealthNode>("HealthNode");
            GetTree().CallGroup("Defender", "SetTarget", _enemyHelthNode);
        }
        else
        {
            //GetTree().CallGroup("Defender", "Wait");
            _enemies = _enemySpawner.CreateNewWave(_numWaves);
        }
    }
}
