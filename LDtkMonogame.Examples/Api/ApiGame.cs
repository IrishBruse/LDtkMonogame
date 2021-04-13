using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDtk.Examples.Api
{
    public class ApiGame : BaseExample
    {
        // LDtk stuff
        private LDtkWorld world;
        private CustomLevel myLevel;
        private LDtkIntGrid intGrid8px;
        private LDtkIntGrid intGridClassic;

        public ApiGame() : base()
        {
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            world = Content.Load<LDtkWorld>("Test_file_for_API_showing_all_features");
            world.spriteBatch = spriteBatch;
            world.GraphicsDevice = GraphicsDevice;

            world.LoadLevel("Level1");
            myLevel = world.GetLevel<CustomLevel>("Level1");
            intGrid8px = myLevel.GetIntGrid("IntGrid_8px_grid");
            intGridClassic = myLevel.GetIntGrid("IntGrid_classic");
            Console.WriteLine(myLevel.Identifier + " desc :\n" + myLevel.desc);

            EntityFieldsTest[] entityFieldsTests = myLevel.GetEntities<EntityFieldsTest>();
            Console.WriteLine("EntityFieldsTests:");
            for (int i = 0; i < entityFieldsTests.Length; i++)
            {
                Console.WriteLine(entityFieldsTests[i].ToString());
                Console.WriteLine();
            }

            Label[] labels = myLevel.GetEntities<Label>("Labels");
            Console.WriteLine("Labels:");
            for (int i = 0; i < labels.Length; i++)
            {
                Console.WriteLine(labels[i].ToString());
                Console.WriteLine();
            }

            RectRegion[] rectRegions = myLevel.GetEntities<RectRegion>();
            Console.WriteLine("RectRegions:");
            for (int i = 0; i < rectRegions.Length; i++)
            {
                Console.WriteLine(rectRegions[i].ToString());
                Console.WriteLine();
            }
        }

        protected override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(myLevel.BgColor);

            Matrix mat = Matrix.CreateTranslation(cameraPosition.X, cameraPosition.Y, 0) * Matrix.CreateScale(pixelScale) * Matrix.CreateTranslation(cameraOrigin.X, cameraOrigin.Y, 0);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: mat);
            {
                // Draw Levels layers
                for (int i = 0; i < myLevel.Layers.Length; i++)
                {
                    spriteBatch.Draw(myLevel.Layers[i], myLevel.Position, Color.White);
                }

                // Rendering int grid 
                for (int x = 0; x < intGrid8px.Values.GetLength(0); x++)
                {
                    for (int y = 0; y < intGrid8px.Values.GetLength(1); y++)
                    {
                        if (intGrid8px.Values[x, y] != 0)
                        {
                            spriteBatch.Draw(texture, new Rectangle(new Point(x * intGrid8px.TileSize, y * intGrid8px.TileSize), new Point(1 * intGrid8px.TileSize, 1 * intGrid8px.TileSize)), Color.White);
                        }
                    }
                }

                // Rendering int grid 
                for (int x = 0; x < intGridClassic.Values.GetLength(0); x++)
                {
                    for (int y = 0; y < intGridClassic.Values.GetLength(1); y++)
                    {
                        if (intGridClassic.Values[x, y] != 0 && intGridClassic.Values[x, y] != 3)
                        {
                            Color col = intGridClassic.Values[x, y] == 1 ? Color.Black : Color.CornflowerBlue;
                            spriteBatch.Draw(texture, new Rectangle(new Point(x * intGridClassic.TileSize, y * intGridClassic.TileSize), new Point(1 * intGridClassic.TileSize, 1 * intGridClassic.TileSize)), col);
                        }
                    }
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}