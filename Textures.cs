using SFML.Graphics;
using System;

namespace Aerohockey;
public class Textures
{
    public static Texture PlayerTexture;
    public static Texture OrbTexture;
    public static Texture BG;

    public static void Load()
    {
        PlayerTexture = new("Player.png");
        OrbTexture = new("Orb.jpg");
        BG = new("BG.jpg");
    }
}
