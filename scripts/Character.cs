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

    private AnimatedSprite animatedSprite;
    private Timer timer;

    public void Init()
    {
        health = maxHealth;

        animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");

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

        animatedSprite.FlipH = FlipH(move.x);

        velocity = move * speed * 10;
        MoveAndSlide(velocity);

        if (!isTakingDamage)
            return;

        if (move.x == 0 && move.y == 0)
            animatedSprite.Animation = "idle";
        else
            animatedSprite.Animation = "run";
    }

    private bool FlipH(float x)
    {
        if (x < 0)
            return true;

        if (x > 0)
            return false;

        return animatedSprite.FlipH;
    }

    public void _OnTimerTimeout()
    {
        GD.Print("timeout");
        isTakingDamage = true;
    }

    public void TakeDamage(int amount)
    {
        if (!isTakingDamage)
            return;

        health -= amount;

        if (health <= 0)
            QueueFree();

        isTakingDamage = false;
        timer.Start();

        animatedSprite.Animation = "hit";
    }
}
