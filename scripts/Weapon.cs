using Godot;

public class Weapon : Node2D
{
    [Export] public int damage = 1;

    public override void _Ready()
    {
        Connect("body_entered", this, "_OnArea2DBodyEntered");
    }

    public void _OnArea2DBodyEntered(Node body)
    {

    }
}
