using Godot;

public class Weapon : Node2D
{
    [Export] public int damage = 0;

    public override void _Ready()
    {
        Connect("body_entered", this, "_OnArea2DBodyEntered");
    }

    public void _OnArea2DBodyEntered(Node body)
    {
        if (!body.IsInGroup("enemy"))
            return;

        Enemy enemy = body as Enemy;

        enemy.TakeDamage(damage + Player.instance.damage);
        GD.Print(enemy.health);
    }
}
