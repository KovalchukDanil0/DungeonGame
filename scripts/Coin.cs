using Godot;

public class Coin : Area2D
{
    public void _OnCoinBodyEntered(PhysicsBody2D body)
    {
        if (!body.IsInGroup("player"))
            return;

        Player.instance.score++;
        GUI.instance.UpdateScore();

        QueueFree();
    }
}
