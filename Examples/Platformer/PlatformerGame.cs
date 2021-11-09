using System;
using System.Collections.Generic;
using Comora;
using LDtk;
using LDtk.Examples.Platformer;
using LDtkTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Examples.Platformer
{
    public class PlatformerGame : BaseExample
    {
        private LDtkWorld world;
        private LevelManager levelManager;

        private KeyboardState oldKeyboard;
        private MouseState oldMouse;

        private Player player;
        private bool showTileColliders = false;
        private bool showEntityColliders = false;

        private readonly List<Door> doors = new List<Door>();
        private readonly List<Crate> crates = new List<Crate>();
        private readonly List<Diamond> diamonds = new List<Diamond>();

        int diamondsCollected;

        Camera camera;

        Texture2D doorTexture;
        Texture2D boxTexture;
        Texture2D diamondTexture;
        Texture2D fontTexture;
        Texture2D playerTexture;

        public PlatformerGame() : base()
        {
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            Window.Title = "LDtkMonogame - Api";

            camera = new Camera(GraphicsDevice);

            world = LDtkWorld.LoadWorld("LDtkMonogameExample", Content);

            levelManager = new LevelManager(world, spriteBatch, Content);

            levelManager.OnEnterNewLevel += (level) =>
            {
                doors.AddRange(level.GetEntities<Door>());
                crates.AddRange(level.GetEntities<Crate>());
                diamonds.AddRange(level.GetEntities<Diamond>());
            };

            Door destinationDoor;

            levelManager.ChangeLevelTo("Level1");

            var spawnPoint = levelManager.CurrentLevel.GetEntity<PlayerSpawn>();

            player = new Player
            {
                Position = spawnPoint.Position,
                Pivot = spawnPoint.Pivot,
                EditorVisualColor = spawnPoint.EditorVisualColor,
                Tile = new Rectangle(0, 0, 78, 58),
                Size = new Vector2(78, 58)
            };

            player.animator.OnEnteredDoor += () =>
            {
                player.animator.SetState(Animator.Animation.ExitDoor);
                levelManager.ChangeLevelTo(player.door.LevelIdentifier);
                destinationDoor = levelManager.CurrentLevel.GetEntity<Door>();
                player.Position = destinationDoor.Position;
            };

            doorTexture = Content.Load<Texture2D>("Art/Door");
            playerTexture = Content.Load<Texture2D>("Art/Characters/KingHuman");
            boxTexture = Content.Load<Texture2D>("Art/Box/Box");
            diamondTexture = Content.Load<Texture2D>("Art/Diamond");
            fontTexture = Content.Load<Texture2D>("Art/Gui/Font");
        }

        protected override void Update(GameTime gameTime)
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
            camera.Zoom = 2;
            camera.Update(gameTime);

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

        private void UpdateDiamonds(float deltaTime, float totalTime)
        {
            for (int i = diamonds.Count - 1; i >= 0; i--)
            {
                if (player.collider.Contains(diamonds[i].Position))
                {
                    diamondsCollected++;

                    if ((9 + (int)(diamonds[i].Timer / 0.1f)) < 12)
                    {
                        diamonds[i].Timer += deltaTime;
                        diamonds[i].Tile = new Rectangle((9 + (int)(diamonds[i].Timer / 0.1f)) * (int)diamonds[i].Size.X, 0, (int)diamonds[i].Size.X, (int)diamonds[i].Size.Y);
                    }
                    else
                    {
                        _ = diamonds.Remove(diamonds[i]);
                        return;
                    }
                }
                else
                {
                    int currentFrame = (int)(totalTime * 10 % 10) * (int)diamonds[i].Size.X;
                    diamonds[i].Tile = new Rectangle(currentFrame, 0, (int)diamonds[i].Size.X, (int)diamonds[i].Size.Y);
                    diamonds[i].Position += new Vector2(0, -MathF.Sin((float)totalTime * 2) * 0.1f);
                }
            }
        }

        private void UpdateCrates(float deltaTime)
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

                if (player.attack.Contains(crates[i].Position) && player.attacking)
                {
                    crates[i].Damaged = true;
                    crates[i].Tile = new Rectangle(1 * (int)crates[i].Size.X, 0, (int)crates[i].Size.X, (int)crates[i].Size.Y);
                }
            }
        }

        private void UpdateDoors(float deltaTime)
        {
            player.door = null;

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

                if (player.collider.Contains(doors[i].Position))
                {
                    player.door = doors[i];

                    if (player.animator.EnteredDoor())
                    {
                        doors[i].Opening = true;
                    }
                    break;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(levelManager.CurrentLevel._BgColor);

            spriteBatch.Begin(camera, SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            {
                levelManager.Draw();

                EntityRendering();
                DebugRendering();

                spriteBatch.Draw(playerTexture, player.Position, player.Tile, Color.White, 0,
                (player.Pivot * player.Size) + new Vector2(player.fliped ? -8 : 8, -14),
                1,
                player.fliped ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                0.1f);
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

                // Digit hundreds

                int units = diamondsCollected % 10;
                int tens = diamondsCollected / 10 % 10;
                int hundreds = diamondsCollected / 100 % 10;

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

        private void EntityRendering()
        {
            for (int i = 0; i < doors.Count; i++)
            {
                spriteBatch.Draw(doorTexture, doors[i].Position, doors[i].Tile, Color.White, 0, doors[i].Pivot * doors[i].Size, 1, SpriteEffects.None, 0);
            }

            for (int i = 0; i < crates.Count; i++)
            {
                spriteBatch.Draw(boxTexture, crates[i].Position, crates[i].Tile, Color.White, 0, crates[i].Pivot * crates[i].Size, 1, SpriteEffects.None, 0);
            }

            spriteBatch.Draw(
                playerTexture,
                player.Position,
                player.Tile,
                Color.White,
                0,
                (player.Pivot * player.Size) + new Vector2(player.fliped ? -8 : 8, -14),
                1,
                player.fliped ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                0.1f);

            for (int i = 0; i < diamonds.Count; i++)
            {
                spriteBatch.Draw(diamondTexture, diamonds[i].Position, diamonds[i].Tile, Color.White, 0, diamonds[i].Pivot * diamonds[i].Size, 1, SpriteEffects.None, 0);
            }
        }

        private void DebugRendering()
        {
            // Debugging
            if (showTileColliders)
            {
                for (int i = 0; i < player.tiles.Count; i++)
                {
                    spriteBatch.DrawRect(player.tiles[i].rect, new Color(128, 255, 0, 128));
                }
            }

            if (showEntityColliders)
            {
                for (int i = 0; i < doors.Count; i++)
                {
                    spriteBatch.DrawRect(new Rect(doors[i].Position - (doors[i].Size / 2f), doors[i].Size), doors[i].EditorVisualColor);
                }

                for (int i = 0; i < crates.Count; i++)
                {
                    spriteBatch.DrawRect(new Rect(crates[i].Position - (crates[i].Size / 2f), crates[i].Size), crates[i].EditorVisualColor);
                }

                for (int i = 0; i < diamonds.Count; i++)
                {
                    spriteBatch.DrawRect(new Rect(diamonds[i].Position - (diamonds[i].Size / 2f), diamonds[i].Size), diamonds[i].EditorVisualColor);
                }

                spriteBatch.DrawRect(player.collider, player.EditorVisualColor);
                spriteBatch.DrawRect(player.attack, player.EditorVisualColor);
            }
        }
    }
}
