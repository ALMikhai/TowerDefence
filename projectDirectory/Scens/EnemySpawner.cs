using Godot;
using System.Collections.Generic;

public class EnemySpawner : Node2D
{
    [Export]
    public PackedScene Enemy;

    public Queue<Enemy> CreateNewWave(int enemiesNumber)
    {
        var enemies = new Queue<Enemy>();
        for (int i = 0; i < enemiesNumber; i++)
        {
            var newEnemy = (Enemy)Enemy.Instance();
            AddChild(newEnemy);
            var position = Vector2.Zero;
            position.x = 1280;
            position.y = 360;
            newEnemy.Position = position;
            enemies.Enqueue(newEnemy);
        }

        return enemies;
    }
}
