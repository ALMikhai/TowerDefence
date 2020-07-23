using Godot;
using System;

public class DefenderCell : Control
{
    [Export]
    public ObjectCreator.Objects Type;    

    private Position2D _defenderPosition;
    private Label _hp;
    private Label _damage;
    private Global _global;

    public override void _Ready()
    {
        _defenderPosition = GetNode<Position2D>("DefenderPosition");
        _hp = GetNode<Label>("Hp/Value");
        _damage = GetNode<Label>("Damage/Value");
        _global = GetTree().Root.GetNode<Global>("Global");

        var defenderNode = (Defender)ObjectCreator.Create(Type);
        AddChild(defenderNode);
        defenderNode.Position = _defenderPosition.Position;

        _global.Connect(nameof(Global.Update), this, nameof(_OnGlobalUpdate));

        _OnGlobalUpdate();
    }

    private void _OnGlobalUpdate()
    {
        var stats = _global.GetCharacterStats(Type);

        _hp.Text = stats.Hp.ToString();
        _damage.Text = stats.Damage.ToString();
    }

    private void _OnHpPlusPressed()
    {
        _global.TryBuy(Type, Global.Stats.HP);
    }

    private void _OnDamagePlusPressed()
    {
        _global.TryBuy(Type, Global.Stats.DAMAGE);
    }
}
