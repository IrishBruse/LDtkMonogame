using System;

using LDtk;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Example
{
    public class LDtkExample : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Project projectFile;

        // Camera
        private Vector3 cameraPosition;
        private Vector3 cameraOrigin;
        private float cameraZoom = 1f;

        private readonly int currentLevel = 1;
        private readonly bool[] activeLayers = { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, };

        KeyboardState oldKeyboardState;

        public LDtkExample()
        {
            graphics = new GraphicsDeviceManager(this);
            IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            MonogameInitialize();

            projectFile = new Project(spriteBatch, "samples/Test_file_for_API_showing_all_features.ldtk");
            projectFile.Render(currentLevel);

            base.Initialize();
        }

        private void OnWindowResized()
        {
            cameraOrigin = new Vector3(GraphicsDevice.Viewport.Width / 2f, GraphicsDevice.Viewport.Height / 2f, 0);
            cameraZoom = MathF.Max(1, GraphicsDevice.Viewport.Height / 240);
        }

        private void MonogameInitialize()
        {
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            IsFixedTimeStep = false;

            TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d);

            graphics.ApplyChanges();

            Window.ClientSizeChanged += (o, e) => OnWindowResized();
            OnWindowResized();
        }

        protected override void Update(GameTime gameTime)
        {
            double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState keyboard = Keyboard.GetState();

            float h = (keyboard.IsKeyDown(Keys.D) ? -1f : 0f) + (keyboard.IsKeyDown(Keys.A) ? 1f : 0f);
            float v = (keyboard.IsKeyDown(Keys.S) ? -1f : 0f) + (keyboard.IsKeyDown(Keys.W) ? 1f : 0f);

            cameraPosition += new Vector3(h, v, 0) * 100 * (float)deltaTime;

            if(keyboard.IsKeyDown(Keys.R))
            {
                cameraZoom = 1;
            }

            if(keyboard.IsKeyDown(Keys.E))
            {
                cameraZoom += (float)deltaTime;
            }

            if(keyboard.IsKeyDown(Keys.Q))
            {
                cameraZoom -= (float)deltaTime;
            }

            if(keyboard.IsKeyDown(Keys.D1) == false && oldKeyboardState.IsKeyDown(Keys.D1) == true)
            {
                activeLayers[0] = !activeLayers[0];
            }

            if(keyboard.IsKeyDown(Keys.D2) == false && oldKeyboardState.IsKeyDown(Keys.D2) == true)
            {
                activeLayers[1] = !activeLayers[1];
            }

            if(keyboard.IsKeyDown(Keys.D3) == false && oldKeyboardState.IsKeyDown(Keys.D3) == true)
            {
                activeLayers[2] = !activeLayers[2];
            }

            if(keyboard.IsKeyDown(Keys.D4) == false && oldKeyboardState.IsKeyDown(Keys.D4) == true)
            {
                activeLayers[3] = !activeLayers[3];
            }

            oldKeyboardState = keyboard;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Level level = projectFile.Levels[currentLevel];
            GraphicsDevice.Clear(level.BgColor);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Matrix.CreateTranslation(cameraPosition) * Matrix.CreateScale(cameraZoom) * Matrix.CreateTranslation(cameraOrigin));
            {
                for(int i = level.layers.Length - 1; i >= 0; i--)
                {
                    if(activeLayers[i] == true)
                    {
                        spriteBatch.Draw(level.layers[i], Vector2.Zero, Color.White);
                    }
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}