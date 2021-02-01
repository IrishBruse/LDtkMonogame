using System;
using System.Linq;

using LDtk;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Examples
{
    public class LDtkExample : BaseExample
    {
        // Camera
        private Vector3 cameraPosition;
        private Vector3 cameraOrigin;
        private float cameraZoom = 1f;

        // LDtk stuff
        private int currentLevel = 0;
        private Project projectFile;
        private const string LDTK_FILE = "samples/LDtkMonogameExample.ldtk";

        Level startLevel;
        Level[] neighbours;

        public LDtkExample() : base()
        {
            IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            base.Initialize();

            projectFile = new Project(spriteBatch, LDTK_FILE);
            projectFile.Load(currentLevel);

            startLevel = projectFile.GetLevel("Level1");
            neighbours = (from neighbour in startLevel.Neighbours select projectFile.GetLevel(neighbour)).ToArray();
        }

        public override void OnWindowResized()
        {
            cameraOrigin = new Vector3(GraphicsDevice.Viewport.Width / 2f, GraphicsDevice.Viewport.Height / 2f, 0);
            cameraZoom = 2;
        }

        protected override void Update(GameTime gameTime)
        {
            double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if(mouse.MiddleButton == ButtonState.Pressed)
            {
                Point pos = mouse.Position - oldMouse.Position;
                cameraPosition += new Vector3(pos.X, pos.Y, 0) * 30 * (float)deltaTime;
            }

            oldKeyboard = keyboard;
            oldMouse = mouse;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(startLevel.BgColor);

            spriteBatch.Begin(SpriteSortMode.Texture, samplerState: SamplerState.PointClamp, transformMatrix: Matrix.CreateTranslation(cameraPosition) * Matrix.CreateScale(cameraZoom) * Matrix.CreateTranslation(cameraOrigin));
            {
                for(int i = 0; i < startLevel.Layers.Length; i++)
                {
                    spriteBatch.Draw(startLevel.Layers[i], startLevel.WorldPosition, Color.White);
                }

                for(int i = 0; i < neighbours.Length; i++)
                {
                    for(int j = 0; j < neighbours[i].Layers.Length; j++)
                    {
                        spriteBatch.Draw(neighbours[i].Layers[j], neighbours[i].WorldPosition, Color.White);
                    }
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}