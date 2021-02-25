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

            world = new World(spriteBatch, LDTK_FILE, Content);

            levelManager = new LevelManager(world);
            levelManager.ChangeLevelTo("Level1");

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
                int currentFrame = (int)((gameTime.TotalGameTime.TotalSeconds * 10) % 10) * (int)diamonds[i].Size.X;
                diamonds[i].Tile = new Rectangle(currentFrame, 0, (int)diamonds[i].Size.X, (int)diamonds[i].Size.Y);
                diamonds[i].Position += new Vector2(0, -MathF.Sin((float)gameTime.TotalGameTime.TotalSeconds * 2) * 0.1f);
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
                    spriteBatch.Draw(entities[i].Texture,
                        entities[i].Position,
                        entities[i].Tile.Width > 0 ? entities[i].Tile : new Rectangle(0, 0, (int)entities[i].Size.X, (int)entities[i].Size.Y),
                        Color.White,
                        0,
                        entities[i].Pivot * entities[i].Size,
                        1,
                        SpriteEffects.None,
                        0);
                }

                // Wiggle
                player.fliped = gameTime.TotalGameTime.TotalSeconds % 2 < 1;
                player.Tile = new Rectangle(0, 0, (int)player.Size.X, (int)player.Size.Y);

                // Custom entity rendering
                spriteBatch.Draw(player.Texture,
                    player.Position,
                    player.Tile,
                    Color.White, 0,
                    (player.Pivot * player.Size) + new Vector2(player.fliped ? -8 : 8, -14), 1,
                    player.fliped ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}