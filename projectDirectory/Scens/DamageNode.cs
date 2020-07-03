using Godot;
using System;

public class DamageNode : Node2D
{
    [Export]
    public int Damage = 25;
    private HealthNode _target;

    public void SetTarget(HealthNode healthNode)
    {
        _target = healthNode;
    }

    public HealthNode GetTarget()
    {
        return _target;
    }

    public void TakeDamage()
    {
        _target.Hit(Damage);
    }
}
