using Godot;
using System.Collections.Generic;

public class TestScene : Node
{
    [Export]
    public PackedScene Enemy;
    [Export]
    public PackedScene Barrier;
    private Queue<Barrier> _targets = new Queue<Barrier>();
    private Barrier _target;

    public override void _Ready()
    {
        for (int i = 0; i < 4; i++)
        {
            var newBarrier = (Barrier)Barrier.Instance();
            newBarrier.Position = GetNode<Position2D>($"BarrierPos{i}").Position;
            AddChild(newBarrier);
            _targets.Enqueue(newBarrier);
        }
        _SetTarget();
    }

    private void _SetTarget()
    {
        if (_targets.Count > 0)
        {
            _target = _targets.Dequeue();
            _target.Connect("Broken", this, nameof(_SetTarget));
            GetTree().CallGroup("Enemy", "SetTarget", _target);
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
            newEnemy.Position = eventScreenTouch.Position;
            newEnemy.SetTarget(_target);
            AddChild(newEnemy);
        }
    }

    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
