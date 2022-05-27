using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Aerohockey;

public struct Controls
{
	public Keyboard.Key UpKey;
	public Keyboard.Key DownKey;

	public Controls(Keyboard.Key up, Keyboard.Key down)
    {
		UpKey = up;
		DownKey = down;
    }
}

public class Player : IDrawable
{
	public int Loses { get; private set; }

	public bool CoinBonus;

	private float Speed = 1f;
	private Controls controls;

	private Text ScoreText;

	private CircleShape shape;
	public Player(Vector2f pos, Controls controls)
	{
		shape = new();
		ScoreText = new();
		this.controls = controls;
		shape.Position = pos;
		InitGraphics();
	}
	public void AddLoses()
    {
		Loses++;
		ScoreText.DisplayedString = Loses.ToString();
    }

	public void Draw(RenderWindow renderWindow)
    {
		shape.Draw(renderWindow, RenderStates.Default);
		ScoreText.Draw(renderWindow, RenderStates.Default);
    }

	public FloatRect GetGlobalBounds() => shape.GetGlobalBounds();

	public Vector2f GetPosition() => shape.Position;

	private void InitGraphics()
    {
		ScoreText.Position = shape.Position;
		ScoreText.Color = Color.Red;
		ScoreText.Font = Content.font;
		ScoreText.CharacterSize = 40;
		ScoreText.DisplayedString = Loses.ToString();

		shape.Texture = Content.PlayerTexture;
		shape.Radius = 25;
		shape.OutlineThickness = 0.2f;
		shape.FillColor = Color.White;
    }

	public void GetInput()
    {
		if(Keyboard.IsKeyPressed(controls.DownKey) && shape.Position.Y < Game.WINDOW_Y - shape.Radius *2)
        {
			Move(new Vector2f(0, 1) * Speed);
        }
		else if (Keyboard.IsKeyPressed(controls.UpKey) && shape.Position.Y > 0)
		{
			Move(new Vector2f(0, -1) * Speed);
		}
	}

	private void Move(Vector2f Direction)
    {
		shape.Position += Direction * Time.DeltaTime;
		ScoreText.Position = shape.Position;
    }
}
