#define UseContentPipeline

namespace LDtkMonogameExample;

using System;
using System.Collections.Generic;
using LDtk;
using LDtk.Renderer;
using LDtkMonogameExample.Entities;
using LDtkTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class LDtkMonogameGame : Game
{
    // LDtk stuff
    LDtkFile file;
    LDtkWorld world;
    LDtkRenderer renderer;
    readonly List<EnemyEntity> enemies = new();
    readonly List<BulletEntity> bullets = new();
    PlayerEntity player;
    GunEntity gun;

    Camera camera;
    Texture2D spriteSheet;

    // Monogame Stuff
    SpriteBatch spriteBatch;
    readonly GraphicsDeviceManager graphics;
    float pixelScale = 1f;
    public static Texture2D Pixel { get; set; }

    public static bool DebugF1 { get; set; }
    public static bool DebugF2 { get; set; }
    public static bool DebugF3 { get; set; }

    KeyboardState oldKeyboard;

    public LDtkMonogameGame()
    {
        graphics = new GraphicsDeviceManager(this);

#if UseContentPipeline
        Content.RootDirectory = "Content";
#endif
    }

    void MonogameInitialize()
    {
        Window.Title = "LDtkMonogame - Shooter";

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

        camera = new Camera(GraphicsDevice);

#if UseContentPipeline
        renderer = new LDtkRenderer(spriteBatch, Content);
        file = LDtkFile.FromFile("World", Content);
        spriteSheet = Content.Load<Texture2D>("Characters");
#else
        renderer = new LDtkRenderer(spriteBatch);
        file = LDtkFile.FromFile("Content/World.ldtk");
        spriteSheet = Texture2D.FromFile(GraphicsDevice, Path.Combine(Path.GetDirectoryName(file.Path), "Characters.png"));
#endif

        world = file.LoadWorld(Guid.Parse("2c81d720-b4d0-11ec-9871-056972512958"));

        foreach (LDtkLevel level in world.Levels)
        {
            foreach (Enemy enemy in level.GetEntities<Enemy>())
            {
                enemies.Add(new EnemyEntity(enemy, spriteSheet, renderer));
            }

            renderer.PrerenderLevel(level);
        }

        Gun_Pickup gunData = world.GetEntity<Gun_Pickup>();
        gun = new GunEntity(gunData, spriteSheet, renderer);

        Player playerData = world.GetEntity<Player>();
        player = new PlayerEntity(playerData, spriteSheet, renderer, gun);

        foreach (ILDtkEntity item in world.Levels[1].GetAllEntities())
        {
            switch (item)
            {
                case Enemy:
                Console.WriteLine("Enemy Type");
                break;
                case Player:
                Console.WriteLine("Player Type");
                break;
                case Gun_Pickup:
                Console.WriteLine("Gun_Pickup Type");
                break;
                default:
                Console.WriteLine("Unhandled Entity of type " + item.GetType());
                break;
            }
        }

        player.onShoot += () =>
        {
            BulletEntity b = new(spriteSheet, renderer)
            {
                Position = player.Position + new Vector2(player.flip ? -23 : 7, -5.5f),
                flip = player.flip,
            };
            bullets.Add(b);
        };
    }

    protected override void Update(GameTime gameTime)
    {
        DebugInput(oldKeyboard);

        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        float totalTime = (float)gameTime.TotalGameTime.TotalSeconds;

        camera.Update();
        camera.Position = new Vector2(player.Position.X, 120);
        camera.Zoom = pixelScale;

        gun.Update(totalTime);

        foreach (LDtkLevel level in world.Levels)
        {
            if (level.Contains(player.Position))
            {
                player.level = level;
                break;
            }
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Update(deltaTime);
        }

        for (int i = bullets.Count - 1; i >= 0; i--)
        {
            bullets[i].Update(deltaTime);

            for (int j = enemies.Count - 1; j >= 0; j--)
            {
                if (bullets[i].collider.Contains(enemies[j].collider))
                {
                    bullets.RemoveAt(i);
                    enemies[j].Kill(deltaTime);
                    break;
                }
            }
        }

        player.Update(deltaTime, totalTime);

        oldKeyboard = Keyboard.GetState();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        float totalTime = (float)gameTime.TotalGameTime.TotalSeconds;

        GraphicsDevice.Clear(file.BgColor);

        spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, transformMatrix: camera.Transform);
        {
            // Draw Levels layers
            foreach (LDtkLevel level in world.Levels)
            {
                renderer.RenderPrerenderedLevel(level);
            }

            player.Draw(totalTime);

            // Draw Entities
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(totalTime);
            }

            // Draw bullets
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Draw();
            }
        }

        gun.Draw();

        spriteBatch.End();

        base.Draw(gameTime);
    }

    static void DebugInput(KeyboardState old)
    {
        if (old.IsKeyUp(Keys.F1) && Keyboard.GetState().IsKeyDown(Keys.F1))
        {
            DebugF1 = !DebugF1;
        }

        if (old.IsKeyUp(Keys.F2) && Keyboard.GetState().IsKeyDown(Keys.F2))
        {
            DebugF2 = !DebugF2;
        }

        if (old.IsKeyUp(Keys.F3) && Keyboard.GetState().IsKeyDown(Keys.F3))
        {
            DebugF3 = !DebugF3;
        }
    }
}
