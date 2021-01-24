using LDtk;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Example
{
    public class LDtkExample : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Project projectFile;

        Vector3 cameraPosition;
        Vector3 cameraOrigin;
        float cameraZoom = 1f;

        int currentLevel = 0;

        public LDtkExample()
        {
            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            MonogameStuff();

            projectFile = new Project("Data/AutoLayers_4_Advanced.ldtk");
            projectFile.Render(spriteBatch, 0);

            Window.ClientSizeChanged += (o, e) => cameraOrigin = new Vector3(GraphicsDevice.Viewport.Width / 2f, GraphicsDevice.Viewport.Height / 2f, 0);
            cameraOrigin = new Vector3(GraphicsDevice.Viewport.Width / 2f, GraphicsDevice.Viewport.Height / 2f, 0);

            base.Initialize();
        }

        void MonogameStuff()
        {
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Draw(GameTime gameTime)
        {
            Level level = projectFile.Levels[currentLevel];
            GraphicsDevice.Clear(level.BgColor);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Matrix.CreateTranslation(cameraPosition) * Matrix.CreateScale(cameraZoom) * Matrix.CreateTranslation(cameraOrigin));
            {
                for(int i = 0; i < level.layers.Length; i++)
                {
                    spriteBatch.Draw(level.layers[i], Vector2.Zero, Color.White);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();

            float h = (keyboard.IsKeyDown(Keys.D) ? -1f : 0f) + (keyboard.IsKeyDown(Keys.A) ? 1f : 0f);
            float v = (keyboard.IsKeyDown(Keys.S) ? -1f : 0f) + (keyboard.IsKeyDown(Keys.W) ? 1f : 0f);

            cameraPosition += new Vector3(h, v, 0) * 50f * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(keyboard.IsKeyDown(Keys.R))
            {
                cameraZoom = 1;
            }

            if(keyboard.IsKeyDown(Keys.E))
            {
                cameraZoom += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if(keyboard.IsKeyDown(Keys.Q))
            {
                cameraZoom -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            base.Update(gameTime);
        }
    }
}
