using System;
using System.Collections.Generic;
using System.Linq;

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
        private Level startLevel;
        private Level[] neighbours;
        private readonly List<Entity> drawableEntities = new List<Entity>();
        private KeyboardState oldKeyboard;
        private MouseState oldMouse;

        // Entities
        Texture2D pixelTexture;
        Door[] doors;
        Crate[] crates;
        Player player;

        public LDtkExample() : base()
        {
            IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            base.Initialize();

            world = new World(spriteBatch, LDTK_FILE);
            startLevel = world.GetLevel("Level1");

            neighbours = (from neighbour in startLevel.Neighbours select world.GetLevel(neighbour)).ToArray();

            doors = startLevel.GetEntities<Door>();

            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].trigger = new Rect(doors[i].position.X - 16, doors[i].position.Y - 32, 32, 32);
            }

            drawableEntities.AddRange(doors);

            crates = startLevel.GetEntities<Crate>();

            player = startLevel.GetEntity<Player>();

            pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
            pixelTexture.SetData(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });
        }

        public override void OnWindowResized()
        {
            cameraOrigin = new Vector2(GraphicsDevice.Viewport.Width / 2f, GraphicsDevice.Viewport.Height / 2f);
            cameraZoom = Math.Max(GraphicsDevice.Viewport.Height / 250, 1);
        }

        protected override void Update(GameTime gameTime)
        {
            double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (freeCam)
            {
                cameraPosition = -new Vector2(player.position.X, player.position.Y - 30);
            }

            for (int i = 0; i < doors.Length; i++)
            {
                if (doors[i].trigger.Contains(player.collider))
                {
                    player.inDoor = true;
                    doors[i].opening = true;
                    break;
                }
            }

            oldKeyboard = keyboard;
            oldMouse = mouse;


            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            GraphicsDevice.Clear(startLevel.BgColor);

            Texture2D texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            spriteBatch.Begin(SpriteSortMode.Texture, blendState: BlendState.NonPremultiplied, samplerState: SamplerState.PointClamp, transformMatrix: Matrix.CreateTranslation(cameraPosition.X, cameraPosition.Y, 0) * Matrix.CreateScale(cameraZoom) * Matrix.CreateTranslation(cameraOrigin.X, cameraOrigin.Y, 0));
            {
                for (int i = 0; i < startLevel.Layers.Length; i++)
                {
                    spriteBatch.Draw(startLevel.Layers[i], startLevel.Position, Color.White);
                }

                for (int i = 0; i < neighbours.Length; i++)
                {
                    for (int j = 0; j < neighbours[i].Layers.Length; j++)
                    {
                        spriteBatch.Draw(neighbours[i].Layers[j], neighbours[i].Position, Color.White);
                    }
                }

                for (int i = 0; i < drawableEntities.Count; i++)
                {
                    drawableEntities[i].Draw(spriteBatch);
                }

                spriteBatch.Draw(player.texture,
                    player.position,
                    player.frame,
                    Color.White, 0,
                    (player.pivot * player.size) + new Vector2(player.fliped ? -8 : 8, -14), 1,
                    player.fliped ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}