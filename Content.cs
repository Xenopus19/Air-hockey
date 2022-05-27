using SFML.Graphics;
using System;

namespace Aerohockey;
public class Content
{
    public static Texture PlayerTexture;
    public static Texture OrbTexture;
    public static Texture BG;

    public static Font font;

    public static void Load()
    {
        font = new("font.otf");
        PlayerTexture = new("Player.png");
        OrbTexture = new("Orb.jpg");
        BG = new("BG.jpg");
    }
}
