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

public class Player : CircleShape
{
	public int Loses;

	private float Speed = 3f;
	private Controls controls;

	private Text ScoreText;
	public Player(Vector2f pos, Controls controls)
	{
		this.controls = controls;
		Position = pos;
		InitGraphics();
	}

	private void InitGraphics()
    {
		Texture = Textures.PlayerTexture;
		Radius = 25;
		OutlineThickness = 0.2f;
		FillColor = Color.White;
    }

	public void GetInput()
    {
		if(Keyboard.IsKeyPressed(controls.DownKey) && Position.Y < Game.WINDOW_Y - Radius*2)
        {
			Move(new Vector2f(0, 1) * Speed);
        }
		else if (Keyboard.IsKeyPressed(controls.UpKey) && Position.Y > 0)
		{
			Move(new Vector2f(0, -1) * Speed);
		}
	}

	private void Move(Vector2f Direction)
    {
		Position += Direction * Time.DeltaTime;
    }
}
