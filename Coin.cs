using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Aerohockey;

public class Coin : IDrawable
{
	private CircleShape sprite;

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
		sprite.Position = new(PlayerList[PlayerIndex].GetPosition().X, random.Next(0, Game.WINDOW_Y));
    }

	public void CheckCollisionWithPlayer()
    {
		if (!IsActive) return;
		foreach (Player player in PlayerList)
        {
			if(sprite.GetGlobalBounds().Intersects(player.GetGlobalBounds()))
            {
				player.CoinBonus = true;
				IsActive = false;
            }
        }
    }

	public void Draw(RenderWindow renderWindow)
    {
		if(!IsActive) return;
		sprite.Draw(renderWindow, RenderStates.Default);
    }

	private void InitGraphics()
    {
		sprite = new(15);
		sprite.Texture = Content.OrbTexture;
		sprite.FillColor = Color.Yellow;
    }

}
