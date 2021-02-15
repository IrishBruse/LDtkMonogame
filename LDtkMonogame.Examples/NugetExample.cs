using System;

using LDtk;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Examples
{
    public class ApiExample : BaseExample
    {
        // LDtk stuff
        private World projectFile;
        private const string LDTK_FILE = "samples/LDtkMonogameExample.ldtk";

        public ApiExample() : base()
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            projectFile = new World(spriteBatch, LDTK_FILE);
            projectFile.Load(0);
        }

        protected override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Level level = projectFile.GetLevel("Level1");

            GraphicsDevice.Clear(level.BgColor);

            spriteBatch.Begin(SpriteSortMode.Texture, samplerState: SamplerState.PointClamp);
            {
                for (int i = 0; i < level.Layers.Length; i++)
                {
                    spriteBatch.Draw(level.Layers[i], Vector2.Zero, Color.White);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}