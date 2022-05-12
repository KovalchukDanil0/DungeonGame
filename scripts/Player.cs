using Godot;

public class Player : Character
{
    public static Player instance;

    public int score;

    public override void _Ready()
    {
        Singelton();
        Init();
    }

    public override void _Process(float delta)
    {
        MoveInit();
        CollisionInit();
    }

    private void Singelton()
    {
        if (instance == this)
            GD.PrintErr("Singelton is not valid");
        else
            instance = this;
    }

    private void CollisionInit()
    {
        int slideCount = GetSlideCount();
        for (int i = 0; i < slideCount; i++)
        {
            Godot.Collections.Array groups = ((Node)GetSlideCollision(i).Collider).GetGroups();

            switch (groups[0])
            {
                case "enemy":
                    PlayerTakeDamage(damage);
                    break;
                case "coin":
                    GD.Print("gg");
                    break;
            }
        }
    }

    private void MoveInit()
    {
        Vector2 move = new Vector2()
        {
            x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left"),
            y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up")
        };

        Move(move);
    }

    public void PlayerTakeDamage(int amount)
    {
        TakeDamage(amount);

        GUI.instance.UpdateHearts();
    }
}
