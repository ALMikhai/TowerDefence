using Godot;
using System.Collections.Generic;

public class ObjectCreator
{
    public enum Objects : int {DEFENDER = 0, ENEMYNEAR = 1, BARRIER = 2}
    private static string[] _objectsPaths =
    {
        "res://Scens/Defender.tscn",
        "res://Scens/EnemyNear.tscn",
        "res://Scens/Barrier.tscn"
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
