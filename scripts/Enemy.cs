using Godot;
using System.Linq;

public class Enemy : Character
{
    Navigation2D navigation2D;
    Vector2[] path;

    public override void _Ready()
    {
        Init();

        navigation2D = GetNode<Navigation2D>("/root/Node2D/Navigation2D");
    }

    public override void _Process(float delta)
    {
        Navigate();
    }

    Vector2[] GeneratePath()
    {
        return navigation2D.GetSimplePath(GlobalPosition, Player.instance.GlobalPosition, false);
    }

    void Navigate()
    {
        path = GeneratePath();

        if (path.Length > 0)
        {
            Vector2 move = GlobalPosition.DirectionTo(path[1]);
            Move(move);

            if (GlobalPosition == path[0])
                path.Skip(1).ToArray();
        }
    }

    public float RandomInRange()
    {
        RandomNumberGenerator random = new RandomNumberGenerator();
        random.Randomize();

        return random.RandfRange(-3, 3);
    }
}
