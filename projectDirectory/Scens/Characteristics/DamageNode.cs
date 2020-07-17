using Godot;
using System;

public class DamageNode : Node2D
{
    private int _damage = 25;
    private HealthNode _target;

    public void SetTarget(HealthNode target)
    {
        if (target == null)
            throw new NullReferenceException();
        _target = target;
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    public int GetDamage()
    {
        return _damage;
    }

    public HealthNode GetTarget()
    {
        return _target;
    }

    public void ApplyDamage()
    {
        _target.GetDamage(_damage);
    }
}
