using Godot;
using System;

public class HealthNode : Node2D
{
    private int _hp = 100;

    [Signal]
    public delegate void Death();

    public void SetHp(int hp)
    {
        _hp = hp;
    }

    public void GetDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            EmitSignal(nameof(Death));
        }
    }
}
