namespace LDtkMonogameExample.Shooter;

using System;
using System.Collections.Generic;

using LDtk;
using LDtk.Renderer;

using LDtkMonogameExample.Shooter.Entities;

using LDtkTypes.Shooter;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class ShooterGame : IMonogame
{
    // LDtk stuff

    LDtkFile file;
    LDtkWorld world;
    ExampleRenderer renderer;
    readonly List<EnemyEntity> enemies = [];
    readonly List<BulletEntity> bullets = [];
    PlayerEntity player;
    GunEntity gun;
    Camera camera;
    Texture2D spriteSheet;

    // Monogame Stuff
    SpriteBatch spriteBatch = Globals.SpriteBatch;
    GraphicsDevice graphicsDevice = Globals.GraphicsDevice;
    GameWindow window = Globals.Window;

    public static bool DebugF1 { get; set; }
    public static bool DebugF2 { get; set; }
    public static bool DebugF3 { get; set; }

    KeyboardState oldKeyboard;

    public ShooterGame() : base() { }

    public void Initialize()
    {
        window.Title = "LDtkMonogame - Shooter";

        camera = new Camera(graphicsDevice);

        // renderer = new ExampleRenderer(spriteBatch, Content);
        // file = LDtkFile.FromFile("World", Content);
        // spriteSheet = Content.Load<Texture2D>("Characters");

        // None ContentManager version
        renderer = new ExampleRenderer(spriteBatch);
        file = LDtkFile.FromFile("Shooter/Content/World.ldtk");
        spriteSheet = Texture2D.FromFile(graphicsDevice, System.IO.Path.Combine(System.IO.Path.GetDirectoryName(file.FilePath), "Characters.png"));

        world = file.LoadWorld(Worlds.World.Iid);

        LDtkLevel level0 = world.LoadLevel("Level_0");
        LDtkLevel level1 = world.LoadLevel(Worlds.World.Level_1);

        ShooterLevelDataName levelData = level1.GetCustomFields<ShooterLevelDataName>();

        Console.WriteLine(levelData.Float);
        Console.WriteLine(levelData.Multilines);

        foreach (TilesetRectangle item in levelData.Tile)
        {
            Console.WriteLine(item.X);
        }

        RefTest[] entities = level0.GetEntities<RefTest>();

        RefTest test = level0.GetEntityRef<RefTest>(entities[0].Test);

        for (int i = 0; i < world.Levels.Length; i++)
        {
            world.Levels[i] = world.LoadLevel(world.Levels[i].Iid);

            foreach (Enemy enemy in world.Levels[i].GetEntities<Enemy>())
            {
                enemies.Add(new EnemyEntity(enemy, spriteSheet, renderer));
            }

            _ = renderer.PrerenderLevel(world.Levels[i]);
        }

        // Default value instance example
        var prefab = Enemy.Default();
        prefab.Wander = new Vector2[] { new(200, 160), new(200, 100) };
        prefab.Position = prefab.Wander[0];
        enemies.Add(new EnemyEntity(prefab, spriteSheet, renderer));

        var prefab2 = Enemy.Default();
        prefab2.Wander = new Vector2[] { new(250, 100), new(230, 160) };
        prefab2.Position = prefab2.Wander[0];
        enemies.Add(new EnemyEntity(prefab2, spriteSheet, renderer));

        Gun_Pickup gunData = world.GetEntity<Gun_Pickup>();
        gun = new GunEntity(gunData, spriteSheet, renderer);

        Player playerData = world.GetEntity<Player>();
        player = new PlayerEntity(playerData, spriteSheet, renderer, gun);

        player.OnShoot += () =>
        {
            BulletEntity b = new(spriteSheet, renderer)
            {
                Position = player.Position + new Vector2(player.Flip ? -23 : 7, -5.5f),
                Flip = player.Flip,
            };
            bullets.Add(b);
        };
    }

    public void Update(GameTime gameTime)
    {
        DebugInput(oldKeyboard);

        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        float totalTime = (float)gameTime.TotalGameTime.TotalSeconds;

        camera.Update();
        camera.Position = new Vector2(player.Position.X, 120);
        camera.Zoom = Globals.PixelScale;

        gun.Update(totalTime);

        foreach (LDtkLevel level in world.Levels)
        {
            if (level.Contains(player.Position))
            {
                player.Level = level;
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
                if (bullets[i].Collider.Contains(enemies[j].Collider))
                {
                    bullets.RemoveAt(i);
                    enemies[j].Kill(deltaTime);
                    break;
                }
            }
        }

        player.Update(deltaTime, totalTime);

        oldKeyboard = Keyboard.GetState();
    }

    public void Draw(GameTime gameTime)
    {
        float totalTime = (float)gameTime.TotalGameTime.TotalSeconds;

        graphicsDevice.Clear(file.BgColor);

        spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, transformMatrix: camera.Transform);
        {
            // Draw Levels layers
            foreach (LDtkLevel level in world.Levels)
            {
                renderer.RenderPrerenderedLevel(level);
            }

            player.Draw(totalTime);
            gun.Draw();

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
        spriteBatch.End();
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
