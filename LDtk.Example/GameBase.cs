namespace LDtkMonogameExample;

using System;

using LDtkMonogameExample.Platformer;
using LDtkMonogameExample.Shooter;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class GameBase : Game
{
    protected readonly GraphicsDeviceManager graphics;

    Texture2D platformerScreenshot;
    Texture2D shooterScreenshot;
    Rectangle box1Rect;
    Rectangle box2Rect;
    Color box1Color;
    Color box2Color;

    IMonogame currentGame;

    public GameBase()
    {
        Content.RootDirectory = "Content";

        graphics = new GraphicsDeviceManager(this);
        IsFixedTimeStep = false;
    }

    protected override void Initialize()
    {
        Window.Title = "LDtkMonogame - ";

        Globals.Pixel = new Texture2D(GraphicsDevice, 1, 1);
        Globals.Pixel.SetData(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

        Globals.Window = Window;
        Globals.GraphicsDevice = GraphicsDevice;
        Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);

        Window.AllowUserResizing = true;
        IsMouseVisible = true;
        IsFixedTimeStep = false;

        graphics.PreferredBackBufferWidth = 1280;
        graphics.PreferredBackBufferHeight = 720;
        graphics.ApplyChanges();

        platformerScreenshot = Texture2D.FromFile(GraphicsDevice, "Images/Platformer.png");
        shooterScreenshot = Texture2D.FromFile(GraphicsDevice, "Images/Shooter.png");

        Window.ClientSizeChanged += (e, o) => OnWindowResize();
        OnWindowResize();

        Globals.Content = Content;
    }

    void OnWindowResize()
    {
        Globals.PixelScale = Math.Max(GraphicsDevice.Viewport.Height / 180, 1);

        int screenWidth = GraphicsDevice.Viewport.Width;
        int screenHeight = GraphicsDevice.Viewport.Height;

        Console.WriteLine(Globals.PixelScale);

        int boxWidth = (int)(platformerScreenshot.Width / Math.Max(2, 7 - Globals.PixelScale));
        int boxHeight = (int)(platformerScreenshot.Height / Math.Max(2, 7 - Globals.PixelScale));

        int spacing = boxWidth / 2;
        box1Rect = new Rectangle((screenWidth / 2) - boxWidth - (spacing / 2), (screenHeight / 2) - (boxHeight / 2), boxWidth, boxHeight);
        box2Rect = new Rectangle((screenWidth / 2) + (spacing / 2), (screenHeight / 2) - (boxHeight / 2), boxWidth, boxHeight);
    }

    MouseState previousMouseState;
    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            currentGame = null;
        }

        if (currentGame != null)
        {
            currentGame.Update(gameTime);
            base.Update(gameTime);
            return;
        }


        MouseState currentMouseState = Mouse.GetState();

        if (currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed && box1Rect.Contains(currentMouseState.Position))
        {
            currentGame = new PlatformerGame();
            currentGame.Initialize();
        }
        else if (box1Rect.Contains(currentMouseState.Position))
        {
            box1Color = Color.Gray;
        }
        else
        {
            box1Color = Color.White;
        }

        if (currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed && box2Rect.Contains(currentMouseState.Position))
        {
            currentGame = new ShooterGame();
            currentGame.Initialize();

        }
        else if (box2Rect.Contains(currentMouseState.Position))
        {
            box2Color = Color.Gray;
        }
        else
        {
            box2Color = Color.White;
        }

        previousMouseState = currentMouseState;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        if (currentGame != null)
        {
            currentGame.Draw(gameTime);
            base.Draw(gameTime);
            return;
        }

        GraphicsDevice.Clear(Color.CornflowerBlue);

        SpriteBatch spriteBatch = Globals.SpriteBatch;

        spriteBatch.Begin();
        {
            spriteBatch.Draw(platformerScreenshot, box1Rect, box1Color);
            spriteBatch.Draw(shooterScreenshot, box2Rect, box2Color);
        }
        spriteBatch.End();

        base.Draw(gameTime);
    }
}
