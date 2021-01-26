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

        private int currentLevel = 0;
        private KeyboardState oldKeyboard;
        private readonly string filename = "SeparateLevelFiles";

        public LDtkExample()
        {
            graphics = new GraphicsDeviceManager(this);
            IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            MonogameInitialize();

            projectFile = new Project(spriteBatch, "samples/" + filename + ".ldtk");
            projectFile.Render(currentLevel);

            base.Initialize();
        }

        private void OnWindowResized()
        {
            cameraOrigin = new Vector3(GraphicsDevice.Viewport.Width / 2f, GraphicsDevice.Viewport.Height / 2f, 0);
            cameraZoom = 2;
        }

        private void MonogameInitialize()
        {
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            IsFixedTimeStep = false;

            Window.Title = filename;

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

            if(keyboard.IsKeyDown(Keys.E) == false && oldKeyboard.IsKeyDown(Keys.E))
            {
                currentLevel++;
                if(currentLevel >= projectFile.Levels.Length)
                {
                    currentLevel = 0;
                }
                projectFile.Render(currentLevel);
            }

            if(keyboard.IsKeyDown(Keys.Q) == false && oldKeyboard.IsKeyDown(Keys.Q))
            {
                currentLevel--;
                if(currentLevel < 0)
                {
                    currentLevel = projectFile.Levels.Length - 1;
                }
                projectFile.Render(currentLevel);
            }

            oldKeyboard = keyboard;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Level level = projectFile.Levels[currentLevel];
            GraphicsDevice.Clear(level.BgColor);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Matrix.CreateTranslation(cameraPosition) * Matrix.CreateScale(cameraZoom) * Matrix.CreateTranslation(cameraOrigin));
            {
                if(level.Background.Image != null)
                {
                    spriteBatch.Draw(level.Background.Image, level.Background.TopLeft, level.Background.CropRect, Color.White, 0, Vector2.Zero, level.Background.Scale, SpriteEffects.None, 0);
                }

                for(int i = level.Layers.Length - 1; i >= 0; i--)
                {
                    spriteBatch.Draw(level.Layers[i], Vector2.Zero, Color.White);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}