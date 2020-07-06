using Godot;
using System;

public class DamageNode : Node2D
{
    [Export]
    public int Damage = 25;
    private HealthNode _target;

    public void SetTarget(HealthNode target)
    {
        if (target == null)
            throw new NullReferenceException();
        _target = target;
    }

    public HealthNode GetTarget()
    {
        return _target;
    }

    public void ApplyDamage()
    {
        _target.GetDamage(Damage);
    }
}
