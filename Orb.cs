using System;
using SFML.Graphics;
using SFML.System;

public class Orb : CircleShape
{
	private Vector2f DefaultPosition;
	public Orb(Vector2f defPosition)
	{
		DefaultPosition = defPosition;
		ReturnToDefaultPosition();
		InitVisuals();
	}

	public void ReturnToDefaultPosition()
    {
		Position = DefaultPosition;
    }

	private void InitVisuals()
    {
		Radius = 20;
		FillColor = Color.White;
    }
}
