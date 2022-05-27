using System;
using SFML.Graphics;
using SFML.System;

namespace Aerohockey;
public interface IDrawable
{
    public void Draw(RenderWindow renderWindow);
}
