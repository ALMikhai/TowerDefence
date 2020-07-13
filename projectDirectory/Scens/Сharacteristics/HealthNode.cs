using Godot;
using System;

public class HealthNode : Node2D
{
    private int _current = 100;
    private int _max = 100;

    [Signal]
    public delegate void Death();
    [Signal]
    public delegate void ValueUpdate(int max, int current);

    public void SetHp(int hp)
    {
        _current = hp;
    }

    public void GetDamage(int damage)
    {
        _current -= damage;
        EmitSignal(nameof(ValueUpdate), _max, _current);
        if (_current <= 0)
        {
            EmitSignal(nameof(Death));
        }
    }
}
