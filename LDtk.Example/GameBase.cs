namespace LDtkMonogameExample;

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameBase : Game
{

    protected float pixelScale = 1f;

    // Framework
    protected readonly GraphicsDeviceManager graphics;
    protected SpriteBatch spriteBatch;

    public static Texture2D Pixel { get; set; }


    public GameBase()
    {
        Content.RootDirectory = "Content";

        graphics = new GraphicsDeviceManager(this);
        IsFixedTimeStep = false;
    }

    protected override void Initialize()
    {
        Window.Title = "LDtkMonogame - ";

        Pixel = new Texture2D(GraphicsDevice, 1, 1);
        Pixel.SetData(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });


        spriteBatch = new SpriteBatch(GraphicsDevice);

        Window.AllowUserResizing = true;
        IsMouseVisible = true;
        IsFixedTimeStep = false;

        graphics.PreferredBackBufferWidth = 1280;
        graphics.PreferredBackBufferHeight = 720;
        graphics.ApplyChanges();

        Window.ClientSizeChanged += (o, e) => pixelScale = Math.Max(GraphicsDevice.Viewport.Height / 180, 1);

        pixelScale = Math.Max(GraphicsDevice.Viewport.Height / 180, 1);

        Pixel = new Texture2D(GraphicsDevice, 1, 1);
        Pixel.SetData(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });
    }

}
