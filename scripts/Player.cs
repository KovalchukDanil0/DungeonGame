using Godot;

public class Player : Character
{
    public static Player instance;

    public int score;

    private AnimationPlayer animationPlayer;
    private Weapon weapon;

    public override void _Ready()
    {
        Singelton();
        Init();

        weapon = GetNode<Weapon>("AnimatedSprite/WeaponPosition/Area2D");
        GD.Print(weapon.damage);
    }

    public override void _Process(float delta)
    {
        MoveInit();
        Collision();
        Weapon();
    }

    private void Singelton()
    {
        if (instance == this)
            GD.PrintErr("Singelton is not valid");
        else
            instance = this;
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

    private void Collision()
    {
        int slideCount = GetSlideCount();
        for (int i = 0; i < slideCount; i++)
        {
            Node collider = (Node)GetSlideCollision(i).Collider;

            if (collider.IsInGroup("enemy"))
            {
                PlayerTakeDamage(damage);
                return;
            }

            if (collider.IsInGroup("coin"))
            {
                collider.QueueFree();
                score++;
                GUI.instance.UpdateScore();
                return;
            }
        }
    }

    private void Weapon()
    {
        if (Input.IsActionJustPressed("mouse_left"))
        {
            animationPlayer = GetNode<AnimationPlayer>("AnimatedSprite/WeaponPosition/Area2D/AnimationPlayer");
            animationPlayer.CurrentAnimation = "baton";
            animationPlayer.Play();
        }
    }

    public void PlayerTakeDamage(int amount)
    {
        TakeDamage(amount);

        GUI.instance.UpdateHearts();
    }
}
