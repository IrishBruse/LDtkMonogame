using System;
using Comora;
using LDtk;
using LDtk.Renderer;
using LDtkTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Examples.Api
{
    public class ApiGame : BaseExample
    {
        // LDtk stuff

        private LDtkWorld world;
        private LDtkLevel level;
        private LDtkRenderer renderer;
        private LDtkIntGrid intGrid8px;
        private LDtkIntGrid intGridClassic;

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
            renderer = new LDtkRenderer(spriteBatch, Content);

            world = LDtkWorld.LoadWorld("Test_file_for_API_showing_all_features", Content);

            level = world.LoadLevel("Level_0", Content);

            MyLevelClass levelFields = level.GetCustomFields<MyLevelClass>();

            renderer.PrerenderLevel(level);

            intGrid8px = level.GetIntGrid("IntGrid_8px_grid");
            intGridClassic = level.GetIntGrid("IntGrid_classic");

            Console.WriteLine(level.Identifier + " desc :\n" + levelFields.desc);

            EntityFieldsTest[] entityFieldsTests = level.GetEntities<EntityFieldsTest>();
            Console.WriteLine("EntityFieldsTests:");

            for (int i = 0; i < entityFieldsTests.Length; i++)
            {
                Console.WriteLine("    " + entityFieldsTests[i].Array_Enum[0]);
                Console.WriteLine("    " + entityFieldsTests[i].Array_Integer[0]);
                Console.WriteLine("    " + entityFieldsTests[i].Array_multilines[0]);
                Console.WriteLine("    " + entityFieldsTests[i].Array_points[0]);
                Console.WriteLine("    " + entityFieldsTests[i].Boolean);
                Console.WriteLine("    " + entityFieldsTests[i].Color);
                Console.WriteLine("    " + entityFieldsTests[i].Enum);
                Console.WriteLine("    " + entityFieldsTests[i].FilePath);
                Console.WriteLine("    " + entityFieldsTests[i].Float);
                Console.WriteLine("    " + entityFieldsTests[i].Identifier);
                Console.WriteLine("    " + entityFieldsTests[i].Integer);
                Console.WriteLine("    " + entityFieldsTests[i].Pivot);
                Console.WriteLine("    " + entityFieldsTests[i].Point);
                Console.WriteLine("    " + entityFieldsTests[i].Position);
                Console.WriteLine("    " + entityFieldsTests[i].Size);
                Console.WriteLine("    \"" + entityFieldsTests[i].String_multiLines + "\"");
                Console.WriteLine("    " + entityFieldsTests[i].String_singleLine);
                Console.WriteLine("    " + entityFieldsTests[i].Uid);
                Console.WriteLine();
            }

            Labels[] labels = level.GetEntities<Labels>("Labels");
            Console.WriteLine("Labels:");
            for (int i = 0; i < labels.Length; i++)
            {
                Console.WriteLine(labels[i].text);
                Console.WriteLine(labels[i].color);
                Console.WriteLine();
            }

            RectRegion[] rectRegions = level.GetEntities<RectRegion>();
            Console.WriteLine("RectRegions:");
            for (int i = 0; i < rectRegions.Length; i++)
            {
                Console.WriteLine(rectRegions[i].Size);
                Console.WriteLine(rectRegions[i].Position);
                Console.WriteLine(rectRegions[i].SomeEnum);
                Console.WriteLine();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            camera.Update(gameTime);

            camera.Position = Mouse.GetState().Position.ToVector2();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(level._BgColor);

            spriteBatch.Begin(camera);
            {
                // Draw Levels layers
                renderer.RenderLevel(level);

                // Rendering int grid
                for (int x = 0; x < intGrid8px.Values.GetLength(0); x++)
                {
                    for (int y = 0; y < intGrid8px.Values.GetLength(1); y++)
                    {
                        if (intGrid8px.Values[x, y] != 0)
                        {
                            spriteBatch.Draw(pixel, new Rectangle(x * intGrid8px.TileSize, y * intGrid8px.TileSize, intGrid8px.TileSize, intGrid8px.TileSize), Color.LightGreen);
                        }
                    }
                }

                // Rendering int grid
                for (int x = 0; x < intGridClassic.Values.GetLength(0); x++)
                {
                    for (int y = 0; y < intGridClassic.Values.GetLength(1); y++)
                    {
                        // 3 is used in the autotile layer
                        if (intGridClassic.Values[x, y] != 0 && intGridClassic.Values[x, y] != 3)
                        {
                            Color col = intGridClassic.Values[x, y] == 1 ? Color.Black : Color.SkyBlue;
                            spriteBatch.Draw(pixel, new Rectangle(x * intGridClassic.TileSize, y * intGridClassic.TileSize, intGridClassic.TileSize, intGridClassic.TileSize), col);
                        }
                    }
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}