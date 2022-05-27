using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Aerohockey;
public class Game
{
	public const int WINDOW_X = 800;
	public const int WINDOW_Y = 600;

	private const int LosesToLose = 5;

	private RectangleShape BG;

	private Player Player1;
	private Player Player2;

	private Orb orb;

	private RenderWindow window;

	private List<IDrawable> drawableShapes;
	private List<Coin> coins;

	private float CoinCooldown;
	private float PassedCoinCooldown;


	public Game()
	{
		window = new (new VideoMode(WINDOW_X, WINDOW_Y), "Air Hockey");
		window.SetFramerateLimit(60);
		Content.Load();
		BG = new RectangleShape(new Vector2f(WINDOW_X, WINDOW_Y));
		BG.Texture = Content.BG;

		Controls player1Controls = new Controls(Keyboard.Key.W, Keyboard.Key.S);
		Player1 = new(new(50, WINDOW_Y / 2), player1Controls);

		Controls player2Controls = new Controls(Keyboard.Key.I, Keyboard.Key.K);
		Player2 = new(new(WINDOW_X-75, WINDOW_Y / 2), player2Controls);

		orb = new(new(WINDOW_X / 2, WINDOW_Y / 2), Player1, Player2);
		orb.OnTouchedGates += HandleGateCollision;
		coins = new();
		CoinCooldown = 2000;
		PassedCoinCooldown = 0;

		InitDrawableShapes();
	}

	private void InitDrawableShapes()
	{
		drawableShapes = new List<IDrawable>();

		drawableShapes.Add(orb);
		drawableShapes.Add(Player1);
		drawableShapes.Add(Player2);
	}

	public void StartGame()
    {
		while(!GameEnded())
        {
			Time.UpdateDeltaTime();

			orb.Move();
			CheckCoinSpawnTime();
			UpdateCoins();
			GetInput();
			DrawObjects();
        }

		FinishGame();
    }

	private void UpdateCoins()
    {
		foreach(Coin coin in coins)
        {
			coin.CheckCollisionWithPlayer();
        }
    }

	private void CheckCoinSpawnTime()
    {
		PassedCoinCooldown += Time.DeltaTime;
		if(PassedCoinCooldown>=CoinCooldown)
        {
			SpawnCoin();
			PassedCoinCooldown = 0;
        }
    }

	private void SpawnCoin()
    {
		Coin coin = new(Player1, Player2);
		drawableShapes.Add(coin);
		coins.Add(coin);
    }

	private bool GameEnded()
    {
		return Player1.Loses >= LosesToLose || Player2.Loses >= LosesToLose;
    }

	private void GetInput()
    {
		Player1.GetInput();

		Player2.GetInput();
    }

	private void DrawObjects()
    {
		window.Clear();
		BG.Draw(window, RenderStates.Default);
		foreach(IDrawable shape in drawableShapes)
        {
			shape.Draw(window);
        }

		window.Display();
    }

	private void HandleGateCollision(Player attacked)
    {
		if (attacked == Player1)
			AddLoseToPlayer(Player1, Player2);
		else
			AddLoseToPlayer(Player2, Player1);
    }

	private void AddLoseToPlayer(Player attacked, Player other)
    {
		attacked.AddLoses();

		if(other.CoinBonus)
        {
			other.CoinBonus = false;
			attacked.AddLoses();
        }
    }

	private void FinishGame()
    {
		string Winner;

		if (Player1.Loses >= LosesToLose)
			Winner = "Second Player";
		else
			Winner = "First Player";

		window.Close();
		Console.WriteLine(Winner + " won, congratulations.");
    }
}
