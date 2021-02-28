using System;
using System.Collections.Generic;
using LDtk;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Examples
{
    public class LDtkExample : BaseExample
    {
        private const string LDTK_FILE = "Assets/LDtkMonogameExample.ldtk";

        // LDtk stuff
        private World world;
        private LevelManager levelManager;
        private KeyboardState oldKeyboard;
        private MouseState oldMouse;

        // Entities
        private Texture2D pixelTexture;
        private List<Door> doors = new List<Door>();
        private List<Crate> crates = new List<Crate>();
        private List<Diamond> diamonds = new List<Diamond>();
        private Player player;

        // UI
        Texture2D diamondTexture;
        Texture2D fontTexture;
        int diamondsCollected;

        // Debug
        private bool showTileColliders = false;
        private bool showEntityColliders;
        Door destinationDoor;

        public LDtkExample() : base()
        {
            freeCam = false;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            world = new World(spriteBatch, LDTK_FILE, Content);
            levelManager = new LevelManager(world);

            levelManager.OnEnterNewLevel += (level) =>
            {
                doors.AddRange(level.GetEntities<Door>());
                for (int i = 0; i < doors.Count; i++)
                {
                    doors[i].collider = new Rect(doors[i].Position.X - 4, doors[i].Position.Y - 2, 8, 4);
                }

                crates.AddRange(level.GetEntities<Crate>());
                for (int i = 0; i < crates.Count; i++)
                {
                    crates[i].collider = new Rect(crates[i].Position.X - 8, crates[i].Position.Y - 16, 16, 16);
                }

                diamonds.AddRange(level.GetEntities<Diamond>());
                for (int i = 0; i < diamonds.Count; i++)
                {
                    diamonds[i].collider = new Rect(diamonds[i].Position.X - 6, diamonds[i].Position.Y - 16, 12, 16);
                }
            };

            levelManager.ChangeLevelTo("Level1");

            Entity startLocation = levelManager.CurrentLevel.GetEntity<Entity>("PlayerSpawn");

            player = new Player();
            player.Texture = Content.Load<Texture2D>("Art/Characters/KingHuman");
            player.Position = startLocation.Position;
            player.Pivot = startLocation.Pivot;
#if DEBUG
            player.EditorVisualColor = startLocation.EditorVisualColor;
#endif
            player.Tile = new Rectangle(0, 0, 78, 58);
            player.Size = new Vector2(78, 58);

            player.animator.OnEnteredDoor += () =>
            {
                player.animator.SetState(Animator.Animation.ExitDoor);
                levelManager.ChangeLevelTo(player.door.levelIdentifier);
                destinationDoor = levelManager.CurrentLevel.GetEntity<Door>();
                player.Position = destinationDoor.Position;
            };

            pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
            pixelTexture.SetData(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            fontTexture = Content.Load<Texture2D>("Art/Font");
            diamondTexture = Content.Load<Texture2D>("Art/Diamond");
        }

        public override void OnWindowResized()
        {
            cameraOrigin = new Vector2(GraphicsDevice.Viewport.Width / 2f, GraphicsDevice.Viewport.Height / 2f);
            pixelScale = Math.Max(GraphicsDevice.Viewport.Height / 250, 1);
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

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

            if (keyboard.IsKeyDown(Keys.F5) && oldKeyboard.IsKeyDown(Keys.F5) == false)
            {

            }

            levelManager.SetCenterPoint(player.Position);
            levelManager.Update(deltaTime);
            player.Update(keyboard, oldKeyboard, mouse, oldMouse, levelManager.CurrentLevel, deltaTime);

            player.door = null;
            for (int i = 0; i < doors.Count; i++)
            {
                doors[i].Update(deltaTime);

                if (player.collider.Contains(doors[i].collider))
                {
                    player.door = doors[i];

                    if (player.animator.EnteredDoor() == true)
                    {
                        doors[i].opening = true;
                    }
                    break;
                }
            }

            for (int i = 0; i < crates.Count; i++)
            {
                crates[i].Update(deltaTime);

                if (player.attack.Contains(crates[i].collider) && player.attacking == true)
                {
                    crates[i].Damage();
                }
            }

            for (int i = diamonds.Count - 1; i >= 0; i--)
            {
                diamonds[i].Update(deltaTime, (float)gameTime.TotalGameTime.TotalSeconds);

                if (diamonds[i].delete == true)
                {
                    diamonds.Remove(diamonds[i]);
                }
                else if (diamonds[i].collected == false)
                {
                    if (diamonds[i].collider.Contains(player.collider))
                    {
                        diamondsCollected++;
                        diamonds[i].collected = true;
                    }
                }
            }

            // Follow Player
            if (freeCam == false)
            {
                cameraPosition = -new Vector2(player.Position.X, player.Position.Y - 30);
            }

            oldKeyboard = keyboard;
            oldMouse = mouse;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            levelManager.Clear(GraphicsDevice);

            Matrix camera = Matrix.CreateTranslation(cameraPosition.X, cameraPosition.Y, 0) * Matrix.CreateScale(pixelScale) * Matrix.CreateTranslation(cameraOrigin.X, cameraOrigin.Y, 0);

            spriteBatch.Begin(blendState: BlendState.NonPremultiplied, samplerState: SamplerState.PointClamp, transformMatrix: camera);
            {
                levelManager.Draw(spriteBatch);
                EntityRendering();
                DebugRendering();
            }
            spriteBatch.End();

            // UI
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

                int units = (int)(diamondsCollected % 10);
                int tens = (int)((diamondsCollected / 10) % 10);
                int hundreds = (int)((diamondsCollected / 100) % 10);

                spriteBatch.Draw(fontTexture,
                    new Vector2(12, 1) * pixelScale * 2,
                    new Rectangle(7 * hundreds, 0, 7, 9),
                    new Color(97, 152, 204, 255),
                    0,
                    Vector2.Zero,
                    pixelScale * 2,
                    SpriteEffects.None,
                    0);

                // Digit tens
                spriteBatch.Draw(fontTexture,
                    new Vector2(12 + 7, 1) * pixelScale * 2,
                    new Rectangle(7 * tens, 0, 7, 9),
                    new Color(97, 152, 204, 255),
                    0,
                    Vector2.Zero,
                    pixelScale * 2,
                    SpriteEffects.None,
                    0);

                // Digit units
                spriteBatch.Draw(fontTexture,
                    new Vector2(12 + 7 + 7, 1) * pixelScale * 2,
                    new Rectangle(7 * units, 0, 7, 9),
                    new Color(97, 152, 204, 255),
                    0,
                    Vector2.Zero,
                    pixelScale * 2,
                    SpriteEffects.None,
                    0);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void EntityRendering()
        {
            for (int i = 0; i < doors.Count; i++)
            {
                spriteBatch.Draw(doors[i].Texture, doors[i].Position, doors[i].Tile, Color.White, 0, doors[i].Pivot * doors[i].Size, 1, SpriteEffects.None, 0);
            }

            for (int i = 0; i < crates.Count; i++)
            {
                spriteBatch.Draw(crates[i].Texture, crates[i].Position, crates[i].Tile, Color.White, 0, crates[i].Pivot * crates[i].Size, 1, SpriteEffects.None, 0);
            }

            spriteBatch.Draw(
                player.Texture,
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
                spriteBatch.Draw(diamonds[i].Texture, diamonds[i].Position, diamonds[i].Tile, Color.White, 0, diamonds[i].Pivot * diamonds[i].Size, 1, SpriteEffects.None, 0);
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

#if DEBUG
            if (showEntityColliders)
            {
                for (int i = 0; i < doors.Count; i++)
                {
                    spriteBatch.DrawRect(doors[i].collider, doors[i].EditorVisualColor);
                }

                for (int i = 0; i < crates.Count; i++)
                {
                    spriteBatch.DrawRect(crates[i].collider, crates[i].EditorVisualColor);
                }

                for (int i = 0; i < diamonds.Count; i++)
                {
                    spriteBatch.DrawRect(diamonds[i].collider, diamonds[i].EditorVisualColor);
                }

                spriteBatch.DrawRect(player.collider, player.EditorVisualColor);
                spriteBatch.DrawRect(player.attack, player.EditorVisualColor);
            }
#endif
        }
    }
}