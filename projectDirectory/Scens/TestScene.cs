using Godot;
using System.Collections.Generic;

public class TestScene : Node
{
    [Export]
    public PackedScene Enemy;
    [Export]
    public PackedScene Barrier;
    [Signal]
    public delegate void EnemiesUpdeted();
    private Queue<Barrier> _barriers = new Queue<Barrier>();
    private Barrier _barrierNow;
    private HealthNode _barrierHelthNode;

    public override void _Ready()
    {
        for (int i = 0; i < 4; i++)
        {
            var newBarrier = (Barrier)Barrier.Instance();
            newBarrier.Position = GetNode<Position2D>($"BarrierPos{i}").Position;
            AddChild(newBarrier);
            _barriers.Enqueue(newBarrier);
        }
        _SetTarget();
    }

    private void _SetTarget()
    {
        if (_barriers.Count > 0)
        {
            _barrierNow = _barriers.Dequeue();
            _barrierNow.Connect("Broken", this, nameof(_SetTarget));
            _barrierHelthNode = (HealthNode)_barrierNow.GetNode<HealthNode>("HealthNode");
            GetTree().CallGroup("Enemy", "SetTarget", _barrierHelthNode);
        }
        else
        {
            QueueFree();
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventScreenTouch eventScreenTouch && eventScreenTouch.Pressed)
        {
            var newEnemy = (Character)Enemy.Instance();
            AddChild(newEnemy);
            newEnemy.Position = eventScreenTouch.Position;
            newEnemy.GetNode<DamageNode>("DamageNode").SetTarget(_barrierHelthNode);
            EmitEnemiesUpdeted();
            newEnemy.Connect(nameof(EnemyNear.Death), this, nameof(EmitEnemiesUpdeted));
        }
    }

    private void EmitEnemiesUpdeted()
    {
        EmitSignal(nameof(EnemiesUpdeted));
    }
}
