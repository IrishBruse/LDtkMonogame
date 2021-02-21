using System;
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
        private World world;

        Player player;
        List<Entity> entities = new List<Entity>();
        Entity[] diamonds;

        LevelManager levelManager;

        public ApiExample() : base() { }

        protected override void Initialize()
        {
            base.Initialize();

            world = new World(spriteBatch, LDTK_FILE);

            levelManager = new LevelManager(world);
            levelManager.SetStarterLevel("Level1");

            diamonds = levelManager.CurrentLevel.GetEntities<Entity>("Diamond");
            entities.AddRange(diamonds);

            entities.AddRange(levelManager.CurrentLevel.GetEntities<Door>());
            entities.AddRange(levelManager.CurrentLevel.GetEntities<Crate>());
            player = levelManager.CurrentLevel.GetEntity<Player>();
        }

        protected override void Update(GameTime gameTime)
        {
            // Animate all diamonds
            for (int i = 0; i < diamonds.Length; i++)
            {
                int currentFrame = (int)((gameTime.TotalGameTime.TotalSeconds * 10) % 10) * (int)diamonds[i].size.X;
                diamonds[i].tile = new Rectangle(currentFrame, 0, (int)diamonds[i].size.X, (int)diamonds[i].size.Y);
                diamonds[i].position += new Vector2(0, -MathF.Sin((float)gameTime.TotalGameTime.TotalSeconds * 2) * 0.1f);
            }

            levelManager.SetCenterPoint(-cameraPosition);
            levelManager.Update(gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            levelManager.Clear(GraphicsDevice);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Matrix.CreateTranslation(cameraPosition.X, cameraPosition.Y, 0) * Matrix.CreateScale(pixelScale) * Matrix.CreateTranslation(cameraOrigin.X, cameraOrigin.Y, 0));
            {
                // Level rendering
                levelManager.Draw(spriteBatch);

                // Bulk entity rendering
                for (int i = 0; i < entities.Count; i++)
                {
                    spriteBatch.Draw(entities[i].texture,
                        entities[i].position,
                        entities[i].tile.Width > 0 ? entities[i].tile : new Rectangle(0, 0, (int)entities[i].size.X, (int)entities[i].size.Y),
                        Color.White,
                        0,
                        entities[i].pivot * entities[i].size,
                        1,
                        SpriteEffects.None,
                        0);
                }

                // Wiggle
                player.fliped = gameTime.TotalGameTime.TotalSeconds % 2 < 1;
                player.tile = new Rectangle(0, 0, (int)player.size.X, (int)player.size.Y);

                // Custom entity rendering
                spriteBatch.Draw(player.texture,
                    player.position,
                    player.tile,
                    Color.White, 0,
                    (player.pivot * player.size) + new Vector2(player.fliped ? -8 : 8, -14), 1,
                    player.fliped ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}