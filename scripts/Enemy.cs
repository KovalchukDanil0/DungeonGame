using Godot;

public class Enemy : Character
{
    public override void _Ready()
    {
        Init();
    }

    public override void _Process(float delta)
    {
        MoveInit();
    }

    private void MoveInit()
    {
        Vector2 move = new Vector2()
        {
            x = RandomInRange(),
            y = RandomInRange()
        };

        Move(move);
    }



    public float RandomInRange()
    {
        RandomNumberGenerator random = new RandomNumberGenerator();
        random.Randomize();

        return random.RandfRange(-3, 3);
    }
}
