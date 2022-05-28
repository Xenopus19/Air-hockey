using System;
using SFML.Graphics;
using SFML.System;

namespace Aerohockey;
public class Orb : IDrawable
{
	public Action<Player> OnTouchedGates;

	private Vector2f DefaultPosition;

	private float Speed = 1f;
	private Vector2f Direction;
	private Random random;

	private List<Player> PlayerList;

	private CircleShape sprite;
	public Orb(Vector2f defPosition, Player player1, Player player2)
	{
		random = new();
		sprite = new();
		PlayerList = new List<Player>();
		PlayerList.Add(player1);
		PlayerList.Add(player2);

		DefaultPosition = defPosition;
		ReturnToDefaultPosition();
		SetRandomDirection();
		InitVisuals();
	}

	public void Draw(RenderWindow renderWindow)
    {
		sprite.Draw(renderWindow, RenderStates.Default);
    }

	public void Move()
    {
		sprite.Position += Direction * Speed * Time.DeltaTime;
		CheckCollision();
    }

	private void CheckCollision()
    {
		bool CollidesWithScreenBorders = sprite.Position.Y <= 0 || sprite.Position.Y >= Game.WINDOW_Y - sprite.Radius * 2;
		bool CollidesWithGates = CheckCollisionWithGates();
		bool CollidesWithPlayers = false;

		foreach(Player player in PlayerList)
        {
			if(sprite.GetGlobalBounds().Intersects(player.GetGlobalBounds()))
				CollidesWithPlayers = true;
        }

		if (CollidesWithPlayers || CollidesWithScreenBorders || CollidesWithGates)
			Bounce();

    }
	
	private bool CheckCollisionWithGates()
    {
		if (sprite.Position.X <= 0)
        {
			InvokeTouchedGates(PlayerList[0]);
			ReturnToDefaultPosition();
			return true;
		}
			
		else if(sprite.Position.X >= Game.WINDOW_X - sprite.Radius * 2)
        {
			InvokeTouchedGates(PlayerList[1]);
			ReturnToDefaultPosition();
			return true;
		}
		return false;	
    }

	private void InvokeTouchedGates(Player player)
    {
		if (OnTouchedGates != null)
			OnTouchedGates.Invoke(player);
	}

	private void Bounce()
    {
		Direction *= -1;
		Vector2f DeltaDir = new Vector2f(0.04f, 0.07f);
		if(random.Next(10)%2 == 0) DeltaDir *= -1;
		Direction += DeltaDir;
    }

	private void SetRandomDirection()
    {
		Direction = new((float)random.NextDouble(), 0);
    }

	private void ReturnToDefaultPosition()
    {
		sprite.Position = DefaultPosition;
    }

	private void InitVisuals()
    {
		sprite.Texture = Content.OrbTexture;
		sprite.Radius = 20;
		sprite.FillColor = Color.White;
    }
}
