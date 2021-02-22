using System;
using System.Collections.Generic;
using System.IO;
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
        private Door[] doors;
        private Crate[] crates;
        private List<Diamond> diamonds;
        private Player player;

        // UI
        Texture2D diamondTexture;
        Texture2D fontTexture;
        int diamondsCollected;

        // Debug
        private bool showTileColliders = false;
        private bool showEntityColliders;

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
            levelManager.ChangeLevelTo("Level1");

            doors = levelManager.CurrentLevel.GetEntities<Door>();
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].collider = new Rect(doors[i].position.X - 16, doors[i].position.Y - 32, 32, 32);
            }

            crates = levelManager.CurrentLevel.GetEntities<Crate>();
            for (int i = 0; i < crates.Length; i++)
            {
                crates[i].collider = new Rect(crates[i].position.X - 8, crates[i].position.Y - 16, 16, 16);
            }

            diamonds = new List<Diamond>(levelManager.CurrentLevel.GetEntities<Diamond>("Diamond"));
            for (int i = 0; i < diamonds.Count; i++)
            {
                diamonds[i].collider = new Rect(diamonds[i].position.X - 6, diamonds[i].position.Y - 16, 12, 16);
            }

            Entity startLocation = levelManager.CurrentLevel.GetEntity<Entity>("PlayerSpawn");

            player = new Player();
            player.texture = Content.Load<Texture2D>("Art/Characters/KingHuman");
            player.position = startLocation.position;
            player.pivot = startLocation.pivot;
#if DEBUG
            player.editorVisualColor = startLocation.editorVisualColor;
#endif
            player.tile = new Rectangle(0, 0, 78, 58);
            player.size = new Vector2(78, 58);

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

            levelManager.SetCenterPoint(player.position);
            levelManager.Update(deltaTime);

            player.Update(keyboard, oldKeyboard, mouse, oldMouse, levelManager.CurrentLevel, deltaTime);
            player.inDoor = false;
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].Update(deltaTime);

                if (player.collider.Contains(doors[i].collider))
                {
                    player.inDoor = true;
                    if (player.doorTransition == true)
                    {
                        doors[i].opening = true;
                    }
                    break;
                }
            }

            // Animate all diamonds
            for (int i = 0; i < diamonds.Count; i++)
            {
                int currentFrame = (int)((gameTime.TotalGameTime.TotalSeconds * 10) % 10) * (int)diamonds[i].size.X;
                diamonds[i].tile = new Rectangle(currentFrame, 0, (int)diamonds[i].size.X, (int)diamonds[i].size.Y);
                diamonds[i].position += new Vector2(0, -MathF.Sin((float)gameTime.TotalGameTime.TotalSeconds * 2) * 0.1f);
            }

            for (int i = 0; i < diamonds.Count; i++)
            {
                if (diamonds[i].collider.Contains(player.collider))
                {
                    diamondsCollected++;
                    diamonds.Remove(diamonds[i]);
                }
            }

            if (player.inDoor == false && player.doorInteraction)
            {
                player.doorInteraction = false;
            }

            if (keyboard.IsKeyDown(Keys.F1) && oldKeyboard.IsKeyDown(Keys.F1) == false)
            {
                showTileColliders = !showTileColliders;
            }

            if (keyboard.IsKeyDown(Keys.F4) && oldKeyboard.IsKeyDown(Keys.F4) == false)
            {
                diamondsCollected++;
            }

            if (keyboard.IsKeyDown(Keys.F2) && oldKeyboard.IsKeyDown(Keys.F2) == false)
            {
                showEntityColliders = !showEntityColliders;
            }

            if (freeCam == false)
            {
                cameraPosition = -new Vector2(player.position.X, player.position.Y - 30);
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
            for (int i = 0; i < doors.Length; i++)
            {
                spriteBatch.Draw(doors[i].texture,
                                        doors[i].position,
                                        doors[i].tile,
                                        Color.White,
                                        0,
                                        doors[i].pivot * doors[i].size,
                                        1,
                                        SpriteEffects.None,
                                        0);
            }

            for (int i = 0; i < crates.Length; i++)
            {
                spriteBatch.Draw(crates[i].texture,
                                        crates[i].position,
                                        crates[i].tile,
                                        Color.White,
                                        0,
                                        crates[i].pivot * crates[i].size,
                                        1,
                                        SpriteEffects.None,
                                        0);
            }

            for (int i = 0; i < diamonds.Count; i++)
            {
                spriteBatch.Draw(diamonds[i].texture,
                                        diamonds[i].position,
                                        diamonds[i].tile,
                                        Color.White,
                                        0,
                                        diamonds[i].pivot * diamonds[i].size,
                                        1,
                                        SpriteEffects.None,
                                        0);
            }

            spriteBatch.Draw(player.texture,
                player.position,
                player.tile,
                Color.White, 0,
                (player.pivot * player.size) + new Vector2(player.fliped ? -8 : 8, -14), 1,
                player.fliped ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0.1f);
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
                for (int i = 0; i < doors.Length; i++)
                {
                    spriteBatch.DrawRect(doors[i].collider, doors[i].editorVisualColor);
                }

                for (int i = 0; i < crates.Length; i++)
                {
                    spriteBatch.DrawRect(crates[i].collider, crates[i].editorVisualColor);
                }

                for (int i = 0; i < diamonds.Count; i++)
                {
                    spriteBatch.DrawRect(diamonds[i].collider, diamonds[i].editorVisualColor);
                }

                spriteBatch.DrawRect(player.collider, player.editorVisualColor);
            }
        }
    }
}