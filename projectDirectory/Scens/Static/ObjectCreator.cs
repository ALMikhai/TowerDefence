using Godot;
using System.Collections.Generic;

public static class ObjectCreator
{
    public enum Objects : int 
    { 
        DEFENDERFROST = 0, SKELETON = 1,
        PLAYER = 2, DEFENDERGINO = 3,
        ROBOT = 4, ZOMBIE = 5,
    }
    
    private static readonly string[] _objectsPaths =
    {
        "res://Scens/Characters/Defenders/DefenderFrost.tscn",
        "res://Scens/Characters/Enemies/Skeleton.tscn",
        "res://Scens/Characters/Crystal.tscn",
        "res://Scens/Characters/Defenders/DefenderGino.tscn",
        "res://Scens/Characters/Defenders/Robot.tscn",
        "res://Scens/Characters/Enemies/Zombie.tscn"
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
