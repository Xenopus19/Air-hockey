using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Aerohockey;

public class Coin : IDrawable
{
	private CircleShape circle;

	private bool IsActive;

	private List<Player> PlayerList;
	public Coin(Player player1, Player player2)
	{
		IsActive = true;
		InitGraphics();
		PlayerList = new();
		PlayerList.Add(player1);
		PlayerList.Add(player2);
		SetCoinPosition();
	}

	private void SetCoinPosition()
    {
		Random random = new Random();
		int PlayerIndex = random.Next(0, PlayerList.Count);
		circle.Position = new(PlayerList[PlayerIndex].GetPosition().X, random.Next(0, Game.WINDOW_Y));
    }

	public void CheckCollisionWithPlayer()
    {
		if (!IsActive) return;
		foreach (Player player in PlayerList)
        {
			if(circle.GetGlobalBounds().Intersects(player.GetGlobalBounds()))
            {
				player.CoinBonus = true;
				IsActive = false;
            }
        }
    }

	public void Draw(RenderWindow renderWindow)
    {
		if(!IsActive) return;
		circle.Draw(renderWindow, RenderStates.Default);
    }

	private void InitGraphics()
    {
		circle = new(15);
		circle.Texture = Content.OrbTexture;
		circle.FillColor = Color.Yellow;
    }

}
