using Godot;
using System.Collections.Generic;

public class TestScene : Node
{
    [Export]
    public PackedScene Enemy;
    [Export]
    public PackedScene Barrier;
    private Queue<Barrier> _barriers = new Queue<Barrier>();
    private Barrier _barrierNow;

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
            GetTree().CallGroup("Enemy", "SetTarget", (HealthNode)_barrierNow.GetNode<HealthNode>("Hp"));
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
            var newEnemy = (EnemyNear)Enemy.Instance();
            AddChild(newEnemy);
            newEnemy.Position = eventScreenTouch.Position;
            newEnemy.GetNode<DamageNode>("DamageNode").SetTarget((HealthNode)_barrierNow.GetNode<HealthNode>("Hp"));
        }
    }

    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
