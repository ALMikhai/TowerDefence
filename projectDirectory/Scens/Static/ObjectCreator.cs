using Godot;
using System.Collections.Generic;

public static class ObjectCreator
{
    public enum Objects : int { DEFENDER = 0, ENEMYNEAR = 1, CRYSTAL = 2, DEFENDERGINO = 3 }
    private static string[] _objectsPaths =
    {
        "res://Scens/Characters/Defenders/Defender.tscn",
        "res://Scens/Characters/Enemies/EnemyNear.tscn",
        "res://Scens/Crystal.tscn",
        "res://Scens/Characters/Defenders/DefenderGino.tscn"
    };

    public static Node Create(Objects obj)
    {
        var newObject = GD.Load<PackedScene>(_objectsPaths[(int)obj]);
        return newObject.Instance();
    }

    public static Queue<Node> Create(Objects obj, int num)
    {
        var result = new Queue<Node>();
        for (int i = 0; i < num; i++)
        {
            result.Enqueue(Create(obj));
        }
        return result;
    }
}
