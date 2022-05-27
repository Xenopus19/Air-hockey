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

	private List<Player> PlayerList;

	private CircleShape OrbCircle;
	public Orb(Vector2f defPosition, Player player1, Player player2)
	{
		OrbCircle = new();
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
		OrbCircle.Draw(renderWindow, RenderStates.Default);
    }

	public void Move()
    {
		OrbCircle.Position += Direction * Speed * Time.DeltaTime;
		CheckCollision();
    }

	private void CheckCollision()
    {
		bool CollidesWithScreenBorders = OrbCircle.Position.Y <= 0 || OrbCircle.Position.Y >= Game.WINDOW_Y - OrbCircle.Radius * 2;
		bool CollidesWithGates = CheckCollisionWithGates();
		bool CollidesWithPlayers = false;

		foreach(Player player in PlayerList)
        {
			if(OrbCircle.GetGlobalBounds().Intersects(player.GetGlobalBounds()))
				CollidesWithPlayers = true;
        }

		if (CollidesWithPlayers || CollidesWithScreenBorders || CollidesWithGates)
			Bounce();

    }
	
	private bool CheckCollisionWithGates()
    {
		if (OrbCircle.Position.X <= 0)
        {
			InvokeTouchedGates(PlayerList[0]);
			ReturnToDefaultPosition();
			return true;
		}
			
		else if(OrbCircle.Position.X >= Game.WINDOW_X - OrbCircle.Radius * 2)
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
		Direction += new Vector2f(0.003f, 0.07f);
    }

	private void SetRandomDirection()
    {
		Random r = new();

		Direction = new((float)r.NextDouble(), 0);
    }

	private void ReturnToDefaultPosition()
    {
		OrbCircle.Position = DefaultPosition;
    }

	private void InitVisuals()
    {
		OrbCircle.Texture = Content.OrbTexture;
		OrbCircle.Radius = 20;
		OrbCircle.FillColor = Color.White;
    }
}
