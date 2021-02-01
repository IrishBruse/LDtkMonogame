using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Examples
{
    public class BaseExample : Game
    {
        // Framework
        internal readonly GraphicsDeviceManager graphics;
        internal SpriteBatch spriteBatch;
        internal KeyboardState oldKeyboard;
        internal MouseState oldMouse;

        public BaseExample()
        {
            graphics = new GraphicsDeviceManager(this);
            IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            MonogameInitialize();

            base.Initialize();
        }

        private void MonogameInitialize()
        {
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Window.Title = "LDtk Game Example";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d);

            graphics.ApplyChanges();

            Window.ClientSizeChanged += (o, e) => OnWindowResized();
            OnWindowResized();
        }

        public virtual void OnWindowResized()
        {
        }
    }
}