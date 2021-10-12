using System;
using Comora;
using LDtk;
using LDtk.Renderer;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Examples.Api
{
    public class ApiGame : BaseExample
    {
        // LDtk stuff

        LDtkWorld world;
        LDtkLevel level1;
        LDtkRenderer renderer;
        // private LDtkIntGrid intGrid8px;
        // private LDtkIntGrid intGridClassic;

        Camera camera;

        public ApiGame() : base()
        {
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            Window.Title = "LDtkMonogame - Api";

            camera = new Camera(GraphicsDevice);
            renderer = new LDtkRenderer(spriteBatch);

            world = LDtkWorld.LoadWorld("Test_file_for_API_showing_all_features", Content);
            level1 = world.LoadLevel("Level1", Content);

            renderer.PrerenderLevel(level1);

            Console.WriteLine(level1.Position);

            // world.spriteBatch = spriteBatch;
            // world.GraphicsDevice = GraphicsDevice;

            // world.LoadLevel("Level1");
            // myLevel = world.GetLevel<CustomLevel>("Level1");
            // intGrid8px = myLevel.GetIntGrid("IntGrid_8px_grid");
            // intGridClassic = myLevel.GetIntGrid("IntGrid_classic");
            // Console.WriteLine(myLevel.Identifier + " desc :\n" + myLevel.desc);

            // EntityFieldsTest[] entityFieldsTests = myLevel.GetEntities<EntityFieldsTest>();
            // Console.WriteLine("EntityFieldsTests:");
            // for (int i = 0; i < entityFieldsTests.Length; i++)
            // {
            //     Console.WriteLine(entityFieldsTests[i].ToString());
            //     Console.WriteLine();
            // }

            // Label[] labels = myLevel.GetEntities<Label>("Labels");
            // Console.WriteLine("Labels:");
            // for (int i = 0; i < labels.Length; i++)
            // {
            //     Console.WriteLine(labels[i].ToString());
            //     Console.WriteLine();
            // }

            // RectRegion[] rectRegions = myLevel.GetEntities<RectRegion>();
            // Console.WriteLine("RectRegions:");
            // for (int i = 0; i < rectRegions.Length; i++)
            // {
            //     Console.WriteLine(rectRegions[i].ToString());
            //     Console.WriteLine();
            // }
        }

        protected override void Update(GameTime gameTime)
        {
            camera.Update(gameTime);

            camera.Position = Mouse.GetState().Position.ToVector2();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(level1._BgColor);

            spriteBatch.Begin(camera);
            {
                // Draw Levels layers
                renderer.RenderLevel(level1);

                // // Rendering int grid
                // for (int x = 0; x < intGrid8px.Values.GetLength(0); x++)
                // {
                //     for (int y = 0; y < intGrid8px.Values.GetLength(1); y++)
                //     {
                //         if (intGrid8px.Values[x, y] != 0)
                //         {
                //             spriteBatch.Draw(texture, new Rectangle(new Point(x * intGrid8px.TileSize, y * intGrid8px.TileSize), new Point(1 * intGrid8px.TileSize, 1 * intGrid8px.TileSize)), Color.White);
                //         }
                //     }
                // }

                // // Rendering int grid
                // for (int x = 0; x < intGridClassic.Values.GetLength(0); x++)
                // {
                //     for (int y = 0; y < intGridClassic.Values.GetLength(1); y++)
                //     {
                //         if (intGridClassic.Values[x, y] != 0 && intGridClassic.Values[x, y] != 3)
                //         {
                //             Color col = intGridClassic.Values[x, y] == 1 ? Color.Black : Color.CornflowerBlue;
                //             spriteBatch.Draw(texture, new Rectangle(new Point(x * intGridClassic.TileSize, y * intGridClassic.TileSize), new Point(1 * intGridClassic.TileSize, 1 * intGridClassic.TileSize)), col);
                //         }
                //     }
                // }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}