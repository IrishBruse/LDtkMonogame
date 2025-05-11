namespace LDtkMonogameExample;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public static class Globals
{
    public static float PixelScale { get; set; } = 1f;
    public static SpriteBatch SpriteBatch { get; set; }
    public static GraphicsDevice GraphicsDevice { get; set; }
    public static ContentManager Content { get; set; }
    public static GameWindow Window { get; set; }

    public static Texture2D Pixel { get; set; }
}
