using Godot;

public class GUI : CanvasLayer
{
	public static GUI instance;

	AnimatedSprite[] hearts;
	RichTextLabel scoreLabel;

	public override void _Ready()
	{
		Singelton();
		Init();
	}

	void Singelton()
	{
		if (instance == this)
			GD.PrintErr("Singelton is not valid");
		else
			instance = this;
	}
	
	void Init()
	{
		Godot.Collections.Array list = GetNode<Node2D>("Hearts").GetChildren();
		hearts = new AnimatedSprite[list.Count];

		for (int i = 0; i < list.Count; i++)
		{
			AnimatedSprite heart = (AnimatedSprite)list[i];
			hearts[i] = heart;
		}

		scoreLabel = GetNode<RichTextLabel>("Score");
	}

	public void UpdateHearts()
	{
		for (int i = 0; i < hearts.Length; i++)
		{
			if (i < Player.instance.health)
				hearts[i].Frame = 0;
			else
				hearts[i].Frame = 2;
		}
	}

	public void UpdateScore()
	{
		scoreLabel.Text = Player.instance.score.ToString();
	}
}
