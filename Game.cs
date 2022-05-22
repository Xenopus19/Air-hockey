using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Aerohockey;
public class Game
{
	public const int WINDOW_X = 800;
	public const int WINDOW_Y = 600;

	private Player Player1;
	private Player Player2;

	private Orb orb;

	private RenderWindow window;

	private List<Shape> drawableShapes;


	public Game()
	{
		window = new (new VideoMode(WINDOW_X, WINDOW_Y), "Air Hockey");

		orb = new(new(WINDOW_X/2, WINDOW_Y / 2));

		Controls player1Controls = new Controls(Keyboard.Key.W, Keyboard.Key.S);
		Player1 = new(new(50, WINDOW_Y / 2), player1Controls);

		Controls player2Controls = new Controls(Keyboard.Key.I, Keyboard.Key.K);
		Player2 = new(new(WINDOW_X-75, WINDOW_Y / 2), player2Controls);

		InitDrawableShapes();
	}

	private void InitDrawableShapes()
	{
		drawableShapes = new List<Shape>();

		drawableShapes.Add(orb);
		drawableShapes.Add(Player1);
		drawableShapes.Add(Player2);
	}

	public void StartGame()
    {
		while(true)
        {
			Time.UpdateDeltaTime();

			GetInput();
			DrawObjects();
        }
    }

	private void GetInput()
    {
		Player1.GetInput();

		Player2.GetInput();
    }

	private void DrawObjects()
    {
		window.Clear();

		foreach(Shape shape in drawableShapes)
        {
			shape.Draw(window, RenderStates.Default);
        }

		window.Display();
    }

	


}
