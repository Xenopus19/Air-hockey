using System;
using SFML.Graphics;
using SFML.System;

namespace Aerohockey;
public class Orb : CircleShape
{
	private Vector2f DefaultPosition;

	private float Speed = 2f;
	private Vector2f Direction;

	private List<Player> PlayerList;
	public Orb(Vector2f defPosition, Player player1, Player player2)
	{
		PlayerList = new List<Player>();
		PlayerList.Add(player1);
		PlayerList.Add(player2);

		DefaultPosition = defPosition;
		ReturnToDefaultPosition();
		SetRandomDirection();
		InitVisuals();
	}

	public void Move()
    {
		Position += Direction * Speed * Time.DeltaTime;
		CheckCollision();
    }

	private void CheckCollision()
    {
		bool CollidesWithScreenBorders = Position.Y <= 0 || Position.Y >= Game.WINDOW_Y - Radius * 2;
		bool CollidesWithGates = CheckCollisionWithGates();
		bool CollidesWithPlayers = false;

		foreach(Player player in PlayerList)
        {
			if(GetGlobalBounds().Intersects(player.GetGlobalBounds()))
				CollidesWithPlayers = true;
        }

		if (CollidesWithPlayers || CollidesWithScreenBorders || CollidesWithGates)
			Bounce();
		if (CollidesWithGates)
			ReturnToDefaultPosition();

    }
	
	private bool CheckCollisionWithGates()
    {
		if (Position.X <= 0)
        {
			PlayerList[0].Loses++;
			return true;
		}
			
		else if(Position.X >= Game.WINDOW_X - Radius * 2)
        {
			PlayerList[1].Loses++;
			return true;
		}
		return false;	
    }

	private void Bounce()
    {
		Direction *= -1;
		Direction += new Vector2f(0.003f, 0.07f);
		FillColor = Color.Blue;
    }

	private void SetRandomDirection()
    {
		Random r = new();

		Direction = new((float)r.NextDouble(), 0);
    }

	private void ReturnToDefaultPosition()
    {
		Position = DefaultPosition;
    }

	private void InitVisuals()
    {
		Radius = 20;
		FillColor = Color.White;
    }
}
