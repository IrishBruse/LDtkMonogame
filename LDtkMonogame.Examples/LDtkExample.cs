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
        private const string LDTK_FILE = "samples/LDtkMonogameExample.ldtk";

        // Camera
        private Vector3 cameraPosition;
        private Vector3 cameraOrigin;
        private float cameraZoom = 1f;

        // LDtk stuff
        private readonly int currentLevel = 0;
        private World world;
        private Level startLevel;
        private Level[] neighbours;
        private readonly List<ISprite> drawableEntities = new List<ISprite>();
        Player player;
        private bool followPlayer = true;

        Texture2D pixelTexture;
        Door[] doors;
        Crate[] crates;

        public LDtkExample() : base()
        {
            IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            base.Initialize();

            world = new World(spriteBatch, LDTK_FILE);
            world.Load(currentLevel);

            startLevel = world.GetLevel("Level1");
            neighbours = (from neighbour in startLevel.Neighbours select world.GetLevel(neighbour)).ToArray();

            doors = startLevel.GetEntities<Door>();

            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].trigger = new Rect(doors[i].Position.X - 16, doors[i].Position.Y - 32, 32, 32);
            }

            drawableEntities.AddRange(doors);

            crates = startLevel.GetEntities<Crate>();

            player = startLevel.GetEntity<Player>();

            pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
            pixelTexture.SetData(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });
        }

        public override void OnWindowResized()
        {
            cameraOrigin = new Vector3(GraphicsDevice.Viewport.Width / 2f, GraphicsDevice.Viewport.Height / 2f, 0);
            cameraZoom = Math.Max(GraphicsDevice.Viewport.Height / 250, 1);
        }

        protected override void Update(GameTime gameTime)
        {
            double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (keyboard.IsKeyDown(Keys.Tab) && oldKeyboard.IsKeyDown(Keys.Tab) == false)
            {
                followPlayer = !followPlayer;
            }

            if (followPlayer)
            {
                cameraPosition = -new Vector3(player.Position.X, player.Position.Y - 30, 0);
            }
            else
            {
                if (mouse.MiddleButton == ButtonState.Pressed)
                {
                    Point pos = mouse.Position - oldMouse.Position;
                    cameraPosition += new Vector3(pos.X, pos.Y, 0) * 30 * (float)deltaTime;
                }
            }
            player.Update(keyboard, oldKeyboard, startLevel, (float)deltaTime);

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

            spriteBatch.Begin(SpriteSortMode.Texture, blendState: BlendState.NonPremultiplied, samplerState: SamplerState.PointClamp, transformMatrix: Matrix.CreateTranslation(cameraPosition) * Matrix.CreateScale(cameraZoom) * Matrix.CreateTranslation(cameraOrigin));
            {
                for (int i = 0; i < startLevel.Layers.Length; i++)
                {
                    spriteBatch.Draw(startLevel.Layers[i], startLevel.WorldPosition, Color.White);
                }

                for (int i = 0; i < neighbours.Length; i++)
                {
                    for (int j = 0; j < neighbours[i].Layers.Length; j++)
                    {
                        spriteBatch.Draw(neighbours[i].Layers[j], neighbours[i].WorldPosition, Color.White);
                    }
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

                for (int i = 0; i < drawableEntities.Count; i++)
                {
                    spriteBatch.Draw(drawableEntities[i].Texture,
                        drawableEntities[i].Position,
                        new Rectangle(0, 0, (int)drawableEntities[i].FrameSize.X, (int)drawableEntities[i].FrameSize.Y),
                        Color.White,
                        0, drawableEntities[i].Pivot * drawableEntities[i].FrameSize, 1,
                        SpriteEffects.None, 0);
                }

                spriteBatch.Draw(player.Texture,
                    player.Position,
                    player.Frame,
                    Color.White, 0,
                                (player.Pivot * player.FrameSize) + new Vector2(player.fliped ? -8 : 8, -14), 1,
                                player.fliped ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}