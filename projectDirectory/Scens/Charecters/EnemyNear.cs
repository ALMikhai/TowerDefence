    using Godot;
using System;
using projectDirectory.Static;

public class EnemyNear : Enemy
{
    protected override void Attack()
    {
        _damageNode.ApplyDamage();
    }

    private void _OnEnemyNearBodyEntered(Node body)
    {
        if (body is Character character && character == _target)
        {
            SetAnimation(projectDirectory.Static.Character.ATACK);
        }
    }

    private void _OnEnemyNearBodyExited(Node body)
    {
        GoToTarget();
    }
}
