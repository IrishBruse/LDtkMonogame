namespace LDtkMonogameExample;

using System;

using LDtk;
using LDtk.Renderer;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Entry : Game
{
    // LDtk stuff

    LDtkFile file;
    LDtkWorld world;
    ExampleRenderer renderer;

    // Monogame Stuff

    Camera camera;
    SpriteBatch spriteBatch;
    readonly GraphicsDeviceManager graphics;
    float pixelScale = 1f;

    public static Texture2D Pixel { get; set; }

    public Entry()
    {
        graphics = new GraphicsDeviceManager(this);

        Content.RootDirectory = "Content";
    }

    void MonogameInitialize()
    {
        Window.Title = "LDtkMonogame - Level Viewer";

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

    protected override void Initialize()
    {
        MonogameInitialize();

        renderer = new ExampleRenderer(spriteBatch);
        file = LDtkFile.FromFile("./Typical_2D_platformer_example.ldtk");

        world = file.LoadSingleWorld();

        foreach (LDtkLevel level in world.Levels)
        {
            _ = renderer.PrerenderLevel(world.LoadLevel(level.Iid));
        }

        camera = new Camera(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        camera.Update();
        camera.Zoom = pixelScale;

        float speed = 60 * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (Keyboard.GetState().IsKeyDown(Keys.W))
        {
            camera.Position += new Vector2(0, -1) * speed;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.S))
        {
            camera.Position += new Vector2(0, 1) * speed;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            camera.Position += new Vector2(-1, 0) * speed;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D))
        {
            camera.Position += new Vector2(1, 0) * speed;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(file.BgColor);

        spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp, transformMatrix: camera.Transform);
        {
            // Draw Levels layers
            foreach (LDtkLevel level in world.Levels)
            {
                renderer.RenderPrerenderedLevel(level);
            }
        }
        spriteBatch.End();

        base.Draw(gameTime);
    }
}
