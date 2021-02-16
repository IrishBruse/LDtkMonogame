using System.Collections.Generic;
using LDtk;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Examples
{
    public class ApiExample : BaseExample
    {
        private const string LDTK_FILE = "Assets/LDtkMonogameExample.ldtk";

        // LDtk stuff
        private World projectFile;

        Level level;
        Player player;
        List<Entity> entities = new List<Entity>();

        public ApiExample() : base() { }

        protected override void Initialize()
        {
            base.Initialize();

            projectFile = new World(spriteBatch, LDTK_FILE);
            level = projectFile.GetLevel("Level1");

            entities.AddRange(level.GetEntities<Door>());
            entities.AddRange(level.GetEntities<Crate>());
            player = level.GetEntity<Player>();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(level.BgColor);

            spriteBatch.Begin(SpriteSortMode.Texture, samplerState: SamplerState.PointClamp, transformMatrix: Matrix.CreateTranslation(cameraPosition) * Matrix.CreateScale(cameraZoom) * Matrix.CreateTranslation(cameraOrigin));
            {
                // Level rendering
                for (int i = 0; i < level.Layers.Length; i++)
                {
                    spriteBatch.Draw(level.Layers[i], Vector2.Zero, Color.White);
                }

                // Bulk entity rendering
                for (int i = 0; i < entities.Count; i++)
                {
                    entities[i].Draw(spriteBatch);
                }

                // Wiggle
                player.fliped = gameTime.TotalGameTime.TotalSeconds % 2 < 1;
                player.frame = new Rectangle(0, 0, (int)player.size.X, (int)player.size.Y);

                // Custom entity rendering
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