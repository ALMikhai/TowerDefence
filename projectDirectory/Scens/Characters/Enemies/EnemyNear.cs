using Godot;
using System;
using projectDirectory.Static;

public class EnemyNear : Enemy
{
    public override void Attack()
    {
        _damageNode.ApplyDamage();
    }
}
