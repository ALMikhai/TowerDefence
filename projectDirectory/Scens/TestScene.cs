using Godot;
using System;

public class TestScene : Node
{
    [Export]
    public PackedScene Enemy;
    protected Position2D _target;

    public override void _Ready()
    {
        _target = GetNode<Position2D>("Target");
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventScreenTouch eventScreenTouch && eventScreenTouch.Pressed)
        {
            var newEnemy = (EnemyNear)Enemy.Instance();
            newEnemy.Position = eventScreenTouch.Position;
            newEnemy.SetTarget(_target.Position);
            AddChild(newEnemy);
        }
    }

    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
