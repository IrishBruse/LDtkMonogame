using System;
using System.Collections.Generic;
using System.IO;
using Comora;
using LDtk;
using LDtk.Renderer;
using LDtkTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shooter.Entities;

namespace Shooter;
public class ShooterGame : Game
{
    // LDtk stuff
    private LDtkWorld world;
    private LDtkLevel[] levels;
    private LDtkRenderer renderer;
    private readonly List<EnemyEntity> enemies = new List<EnemyEntity>();
    private readonly List<BulletEntity> bullets = new List<BulletEntity>();
    private PlayerEntity player;
    private GunEntity gun;

    private Camera camera;
    private Texture2D spriteSheet;

    public static bool DebugF1 = false;
    public static bool DebugF2 = false;
    public static bool DebugF3 = false;

    // Monogame Stuff
    private SpriteBatch spriteBatch;
    private readonly GraphicsDeviceManager graphics;
    private float pixelScale = 1f;
    public static Texture2D Pixel { get; set; }

    private KeyboardState oldKeyboard;

    public ShooterGame()
    {
        graphics = new GraphicsDeviceManager(this);
    }

    private void MonogameInitialize()
    {
        Window.Title = "LDtkMonogame - Shooter";

        spriteBatch = new SpriteBatch(GraphicsDevice);

        Window.AllowUserResizing = true;
        IsMouseVisible = true;
        IsFixedTimeStep = false;

        graphics.PreferredBackBufferWidth = 1280;
        graphics.PreferredBackBufferHeight = 720;
        graphics.ApplyChanges();

        Window.ClientSizeChanged += (o, e) =>
        {
            pixelScale = Math.Max(GraphicsDevice.Viewport.Height / 180, 1);
        };
        pixelScale = Math.Max(GraphicsDevice.Viewport.Height / 180, 1);

        Pixel = new Texture2D(GraphicsDevice, 1, 1);
        Pixel.SetData(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });
    }

    protected override void Initialize()
    {
        MonogameInitialize();

        camera = new Camera(GraphicsDevice);
        renderer = new LDtkRenderer(spriteBatch);

        world = LDtkWorld.LoadWorld("Data\\World.ldtk");
        levels = new LDtkLevel[world.Levels.Length];

        spriteSheet = Texture2D.FromFile(GraphicsDevice, Path.Combine(world.RootFolder, "Characters.png"));

        for (int i = 0; i < world.Levels.Length; i++)
        {
            levels[i] = world.LoadLevel(world.Levels[i].Identifier);

            foreach (Enemy enemy in levels[i].GetEntities<Enemy>())
            {
                enemies.Add(new EnemyEntity(enemy, spriteSheet, renderer));
            }

            renderer.PrerenderLevel(levels[i]);
        }

        Gun_Pickup gunData = levels[1].GetEntity<Gun_Pickup>();
        gun = new GunEntity(gunData, spriteSheet, renderer);

        Player playerData = world.Levels[1].GetEntity<Player>();// TODO: get entity in levels
        player = new PlayerEntity(playerData, spriteSheet, renderer, gun);// TODO: levels

        player.onShoot += () =>
        {
            BulletEntity b = new BulletEntity(spriteSheet, renderer)
            {
                Position = player.Position + new Vector2(player.flip ? -21 : 5, -6),
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

        camera.Update(gameTime);
        camera.Position = new Vector2(player.Position.X, -170);
        camera.Zoom = pixelScale;

        gun.Update(totalTime);

        for (int i = 0; i < world.Levels.Length; i++)
        {
            if (world.Levels[i].Contains(player.Position))
            {
                player.level = world.Levels[i];
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

        player.Update(deltaTime);

        oldKeyboard = Keyboard.GetState();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        float totalTime = (float)gameTime.TotalGameTime.TotalSeconds;

        GraphicsDevice.Clear(world.BgColor);

        spriteBatch.Begin(camera, SpriteSortMode.Deferred, null, SamplerState.PointClamp);
        {
            // Draw Levels layers
            for (int i = 0; i < levels.Length; i++)
            {
                renderer.RenderPrerenderedLevel(levels[i]);
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

    private void DebugInput(KeyboardState old)
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
