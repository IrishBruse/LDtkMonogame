﻿namespace LDtkMonogameExample.Platformer;

using System.Collections.Generic;


using LDtk;

using LDtkMonogameExample.AABB;

using LDtkTypes.Platformer;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Platformer.Player;


public class PlatformerGame : IMonogame
{
    LDtkWorld world;
    LevelManager levelManager;

    KeyboardState oldKeyboard;
    MouseState oldMouse;

    PlayerController player;
    bool showTileColliders;
    bool showEntityColliders;

    readonly List<Door> doors = new();
    readonly List<Crate> crates = new();
    readonly List<Diamond> diamonds = new();
    int diamondsCollected;
    Camera camera;
    Texture2D doorTexture;
    Texture2D boxTexture;
    Texture2D diamondTexture;
    Texture2D fontTexture;
    Texture2D playerTexture;

    ContentManager content = Globals.Content;
    SpriteBatch spriteBatch = Globals.SpriteBatch;
    GameWindow window = Globals.Window;

    public void Initialize()
    {
        window.Title = "LDtkMonogame - Platformer";
        Globals.Content.RootDirectory = "Platformer";

        camera = new Camera(Globals.GraphicsDevice);

        LDtkFile worldFile = LDtkFile.FromFile("LDtkMonogameExample", content);

        world = worldFile.LoadWorld(Worlds.World.Iid);

        levelManager = new LevelManager(world, spriteBatch, content);

        levelManager.OnEnterNewLevel += (level) =>
        {
            doors.AddRange(level.GetEntities<Door>());
            crates.AddRange(level.GetEntities<Crate>());
            diamonds.AddRange(level.GetEntities<Diamond>());
        };

        Door destinationDoor;

        levelManager.ChangeLevelTo("Level1");

        PlayerSpawn spawnPoint = levelManager.CurrentLevel.GetEntity<PlayerSpawn>();

        player = new PlayerController(spawnPoint);

        player.Animator.OnEnteredDoor += () =>
        {
            player.Animator.SetState(Animator.Animation.ExitDoor);
            levelManager.ChangeLevelTo(player.Door.LevelIdentifier);
            destinationDoor = levelManager.CurrentLevel.GetEntity<Door>();
            player.Position = destinationDoor.Position - new Vector2(0, 15);
        };

        doorTexture = content.Load<Texture2D>("Art/Door");
        playerTexture = content.Load<Texture2D>("Art/Characters/KingHuman");
        boxTexture = content.Load<Texture2D>("Art/Box/Box");
        diamondTexture = content.Load<Texture2D>("Art/Diamond");
        fontTexture = content.Load<Texture2D>("Art/Gui/Font");
    }

    public void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        KeyboardState keyboard = Keyboard.GetState();

        // Debug/Cheats
        if (keyboard.IsKeyDown(Keys.F1) && oldKeyboard.IsKeyDown(Keys.F1) == false)
        {
            showTileColliders = !showTileColliders;
        }

        if (keyboard.IsKeyDown(Keys.F2) && oldKeyboard.IsKeyDown(Keys.F2) == false)
        {
            showEntityColliders = !showEntityColliders;
        }

        if (keyboard.IsKeyDown(Keys.F4) && oldKeyboard.IsKeyDown(Keys.F4) == false)
        {
            diamondsCollected++;
        }

        camera.Position = player.Position;
        camera.Zoom = Globals.PixelScale;
        camera.Update();

        levelManager.MoveTo(player.Position);
        levelManager.Update();

        player.Update(Keyboard.GetState(), oldKeyboard, Mouse.GetState(), oldMouse, levelManager.CurrentLevel, deltaTime);

        float totalTime = (float)gameTime.TotalGameTime.TotalSeconds;

        UpdateDiamonds(deltaTime, totalTime);
        UpdateDoors(deltaTime);
        UpdateCrates(deltaTime);

        oldKeyboard = Keyboard.GetState();
        oldMouse = Mouse.GetState();
    }

    void UpdateDiamonds(float deltaTime, float totalTime)
    {
        for (int i = diamonds.Count - 1; i >= 0; i--)
        {
            if (diamonds[i].Collected)
            {
                if (9 + (int)(diamonds[i].Timer / 0.1f) < 12)
                {
                    diamonds[i].Timer += deltaTime;
                    diamonds[i].Tile = new Rectangle((9 + (int)(diamonds[i].Timer / 0.1f)) * (int)diamonds[i].Size.X, 0, (int)diamonds[i].Size.X, (int)diamonds[i].Size.Y);
                }
                else
                {
                    diamonds.Remove(diamonds[i]);
                    return;
                }
            }
            else if (player.Collider.Contains(new Box(diamonds[i].Position, diamonds[i].Size * 2, diamonds[i].Pivot)))
            {
                diamondsCollected++;
                diamonds[i].Collected = true;
            }
            else
            {
                int currentFrame = (int)(totalTime * 10 % 10) * (int)diamonds[i].Size.X;
                diamonds[i].Tile = new Rectangle(currentFrame, 0, (int)diamonds[i].Size.X, (int)diamonds[i].Size.Y);
            }
        }
    }

    void UpdateCrates(float deltaTime)
    {
        for (int i = 0; i < crates.Count; i++)
        {
            if (crates[i].Damaged)
            {
                crates[i].Timer += deltaTime;

                if (crates[i].Timer >= .2f)
                {
                    crates[i].Timer -= .2f;
                    crates[i].Tile = new Rectangle(0 * (int)crates[i].Size.X, 0, (int)crates[i].Size.X, (int)crates[i].Size.Y);
                }
            }

            if (player.Attack.Contains(new Box(crates[i].Position, crates[i].Size, crates[i].Pivot)) && player.Attacking)
            {
                crates[i].Damaged = true;
                crates[i].Tile = new Rectangle(1 * (int)crates[i].Size.X, 0, (int)crates[i].Size.X, (int)crates[i].Size.Y);
            }
        }
    }

    void UpdateDoors(float deltaTime)
    {
        player.Door = null;

        for (int i = 0; i < doors.Count; i++)
        {
            if (doors[i].Opening)
            {
                if (doors[i].Tile.Location.X < 166)
                {
                    doors[i].AnimationTimer += deltaTime;

                    if (doors[i].AnimationTimer >= .1f)
                    {
                        doors[i].AnimationTimer -= .1f;
                        doors[i].Tile.Offset(56, 0);
                    }
                }
                else
                {
                    doors[i].Opening = false;
                }
            }
            else
            {
                Rectangle tile = doors[i].Tile;
                tile.Location = Point.Zero;
                doors[i].Tile = tile;
            }

            if (new Box(doors[i].Position, doors[i].Size, doors[i].Pivot).Contains(player.Collider))
            {
                player.Door = doors[i];

                if (player.Animator.EnteredDoor())
                {
                    doors[i].Opening = true;
                }

                break;
            }
        }
    }

    public void Draw(GameTime gameTime)
    {
        Globals.GraphicsDevice.Clear(levelManager.CurrentLevel._BgColor);


        spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, transformMatrix: camera.Transform);
        {
            levelManager.Draw();

            for (int i = 0; i < doors.Count; i++)
            {
                spriteBatch.Draw(doorTexture, doors[i].Position, doors[i].Tile, Color.White, 0, doors[i].Pivot * doors[i].Size, 1, SpriteEffects.None, 0.1f);
                spriteBatch.DrawPoint(new Box(doors[i].Position, doors[i].Size, doors[i].Pivot).TopLeft, Color.Black);
                spriteBatch.DrawPoint(new Box(doors[i].Position, doors[i].Size, doors[i].Pivot).BottomRight, Color.Black);
            }

            spriteBatch.Draw(playerTexture, player.Position, player.Tile, Color.White, 0, (player.Pivot * player.Size) + new Vector2(player.Fliped ? -8 : 8, 0), 1, player.Fliped ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0.2f);

            EntityRendering();

            DebugRendering();
        }

        spriteBatch.End();

        // UI

        float pixelScale = 1f;
        spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        {
            spriteBatch.Draw(diamondTexture,
                Vector2.One * pixelScale * 2,
                new Rectangle(0, 0, 12, 12),
                Color.White,
                0,
                Vector2.Zero,
                pixelScale * 2,
                SpriteEffects.None,
                0);

            int units = diamondsCollected % 10;
            int tens = diamondsCollected / 10 % 10;
            int hundreds = diamondsCollected / 100 % 10;

            // Digit hundreds
            spriteBatch.Draw(fontTexture,
                new Vector2(14, 2) * pixelScale * 2,
                new Rectangle(6 * hundreds, 0, 6, 8),
                new Color(97, 152, 204, 255),
                0,
                Vector2.Zero,
                pixelScale * 2,
                SpriteEffects.None,
                0);

            // Digit tens
            spriteBatch.Draw(fontTexture,
                new Vector2(14 + 6, 2) * pixelScale * 2,
                new Rectangle(6 * tens, 0, 6, 8),
                new Color(97, 152, 204, 255),
                0,
                Vector2.Zero,
                pixelScale * 2,
                SpriteEffects.None,
                0);

            // Digit units
            spriteBatch.Draw(fontTexture,
                new Vector2(14 + 6 + 6, 2) * pixelScale * 2,
                new Rectangle(6 * units, 0, 6, 8),
                new Color(97, 152, 204, 255),
                0,
                Vector2.Zero,
                pixelScale * 2,
                SpriteEffects.None,
                0);
        }

        spriteBatch.End();
    }

    void EntityRendering()
    {


        for (int i = 0; i < crates.Count; i++)
        {
            spriteBatch.Draw(boxTexture, crates[i].Position, crates[i].Tile, Color.White, 0, crates[i].Pivot * crates[i].Size, 1, SpriteEffects.None, 0.1f);
        }

        for (int i = 0; i < diamonds.Count; i++)
        {
            spriteBatch.Draw(diamondTexture, diamonds[i].Position, diamonds[i].Tile, Color.White, 0, diamonds[i].Pivot * diamonds[i].Size, 1, SpriteEffects.None, 0);
        }
    }

    void DebugRendering()
    {
        // Debugging
        if (showTileColliders)
        {
            for (int i = 0; i < player.Tiles.Count; i++)
            {
                spriteBatch.DrawRect(player.Tiles[i].rect, new Color(128, 255, 0, 128));
            }
        }

        if (showEntityColliders)
        {
            for (int i = 0; i < doors.Count; i++)
            {
                spriteBatch.DrawRect(new Box(doors[i].Position, doors[i].Size, doors[i].Pivot), doors[i].SmartColor.Alpha(128));
                spriteBatch.DrawPoint(doors[i].Position, Color.Black);
            }

            for (int i = 0; i < crates.Count; i++)
            {
                spriteBatch.DrawRect(new Box(crates[i].Position, crates[i].Size, crates[i].Pivot), crates[i].SmartColor.Alpha(128));
                spriteBatch.DrawPoint(crates[i].Position, Color.Black);
            }

            for (int i = 0; i < diamonds.Count; i++)
            {
                spriteBatch.DrawRect(new Box(diamonds[i].Position, diamonds[i].Size, diamonds[i].Pivot), diamonds[i].SmartColor.Alpha(128));
                spriteBatch.DrawPoint(diamonds[i].Position, Color.Black);
            }

            spriteBatch.DrawRect(player.Collider, player.SmartColor.Alpha(128));
            spriteBatch.DrawPoint(player.Collider.TopLeft, Color.Black);
            spriteBatch.DrawPoint(player.Collider.BottomRight, Color.Black);

            spriteBatch.DrawRect(player.Attack, player.SmartColor.Alpha(128));
            spriteBatch.DrawPoint(player.Position, Color.Black);
            spriteBatch.DrawPoint(player.Attack.Position, Color.Black);
        }
    }

}
