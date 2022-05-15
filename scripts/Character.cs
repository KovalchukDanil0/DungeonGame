using Godot;

public class Character : KinematicBody2D
{
    [Export] public int maxHealth = 6;
    public int health;

    [Export] public int damage = 1;

    [Export] public float speed = 1;

    [Export] private float countdownTime = 2;
    [Export] private bool isTakingDamage = true;

    private Vector2 velocity;

    private Node2D animatedSprite;

    private AnimationPlayer animationPlayer;

    private Timer timer;

    public void Init()
    {
        health = maxHealth;

        animatedSprite = GetNode<Node2D>("AnimatedSprite");
        animationPlayer = GetNode<AnimationPlayer>("AnimatedSprite/AnimationPlayer");

        if (isTakingDamage)
        {
            timer = GetNode<Timer>("Timer");
            timer.Connect("timeout", this, "_OnTimerTimeout");
            timer.WaitTime = countdownTime;
        }
    }

    public void Move(Vector2 move)
    {
        if (move.x != 0 && move.y != 0)
            move *= 0.75f;

        animatedSprite.Scale = new Vector2(FlipH(move.x), 1);

        velocity = move * speed * 10;
        MoveAndSlide(velocity);

        if (!isTakingDamage)
            return;

        if (move.x == 0 && move.y == 0)
            animationPlayer.CurrentAnimation = "idle";
        else
            animationPlayer.CurrentAnimation = "run";
    }

    private float FlipH(float x)
    {
        if (x < 0)
            return -1;

        if (x > 0)
            return 1;

        return animatedSprite.Scale.x;
    }

    public void _OnTimerTimeout()
    {
        GD.Print("timeout");
        isTakingDamage = true;
    }

    // !!!
    public void TakeDamage(int amount)
    {
        if (!isTakingDamage)
            return;

        health -= amount;

        if (health <= 0)
            QueueFree();

        isTakingDamage = false;
        timer.Start();

        animationPlayer.CurrentAnimation = "hit";
    }
}
