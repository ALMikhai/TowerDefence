using Godot;
using System;

public class Fireball : Node2D
{
    private HealthNode _target;
    public void SetTarget(HealthNode healthNode) {
        _target = healthNode;
    }
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
