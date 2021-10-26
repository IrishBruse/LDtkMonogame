using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Examples.Api
{
    public class BaseExample : Game
    {
        // Camera
        protected Vector2 cameraPosition;
        protected Vector2 cameraOrigin;
        protected float pixelScale = 1f;
        protected bool freeCam = true;
        protected Texture2D pixel;

        // Framework
        protected readonly GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;
        private KeyboardState oldKeyboard;
        private MouseState oldMouse;

        public BaseExample()
        {
            graphics = new GraphicsDeviceManager(this);
            IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });
            MonogameInitialize();
        }

        private void MonogameInitialize()
        {
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            graphics.ApplyChanges();

            Window.ClientSizeChanged += (o, e) => OnWindowResized();
            OnWindowResized();
        }

        public virtual void OnWindowResized()
        {
            cameraOrigin = new Vector2(GraphicsDevice.Viewport.Width / 2f, GraphicsDevice.Viewport.Height / 2f);
            pixelScale = Math.Max(GraphicsDevice.Viewport.Height / 250, 1);
        }
    }
}