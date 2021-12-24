using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer;

public class BaseExample : Game
{
    protected Texture2D pixel;

    // Framework
    protected readonly GraphicsDeviceManager graphics;
    protected SpriteBatch spriteBatch;

    public BaseExample()
    {
        graphics = new GraphicsDeviceManager(this);
        IsFixedTimeStep = false;
    }

    protected override void Initialize()
    {
        pixel = new Texture2D(GraphicsDevice, 1, 1);
        pixel.SetData(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });
        MonogameInitialize();
    }

    private void MonogameInitialize()
    {
        Window.AllowUserResizing = true;
        IsMouseVisible = true;
        spriteBatch = new SpriteBatch(GraphicsDevice);

        graphics.PreferredBackBufferWidth = 1280;
        graphics.PreferredBackBufferHeight = 720;

        graphics.ApplyChanges();
    }
}
