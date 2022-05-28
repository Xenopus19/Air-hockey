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

	private CircleShape sprite;
	public Player(Vector2f pos, Controls controls)
	{
		sprite = new();
		ScoreText = new();
		this.controls = controls;
		sprite.Position = pos;
		InitGraphics();
	}
	public void AddLoses()
    {
		Loses++;
		ScoreText.DisplayedString = Loses.ToString();
    }

	public void Draw(RenderWindow renderWindow)
    {
		sprite.Draw(renderWindow, RenderStates.Default);
		ScoreText.Draw(renderWindow, RenderStates.Default);
    }

	public FloatRect GetGlobalBounds() => sprite.GetGlobalBounds();

	public Vector2f GetPosition() => sprite.Position;

	private void InitGraphics()
    {
		ScoreText.Position = sprite.Position;
		ScoreText.Color = Color.Red;
		ScoreText.Font = Content.font;
		ScoreText.CharacterSize = 40;
		ScoreText.DisplayedString = Loses.ToString();

		sprite.Texture = Content.PlayerTexture;
		sprite.Radius = 25;
		sprite.OutlineThickness = 0.2f;
		sprite.FillColor = Color.White;
    }

	public void GetInput()
    {
		Move(GetEnteredDirection());
	}

	private Vector2f GetEnteredDirection()
    {
		if (Keyboard.IsKeyPressed(controls.DownKey) && sprite.Position.Y < Game.WINDOW_Y - sprite.Radius * 2)
		{
			return new Vector2f(0, 1);
		}
		else if (Keyboard.IsKeyPressed(controls.UpKey) && sprite.Position.Y > 0)
		{
			return new Vector2f(0, -1);
		}
		return new Vector2f(0, 0);
	}

	private void Move(Vector2f Direction)
    {
		sprite.Position += Direction * Time.DeltaTime;
		ScoreText.Position = sprite.Position;
    }
}
