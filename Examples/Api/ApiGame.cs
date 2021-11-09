using System;
using System.IO;
using System.Text.Json;
using Comora;
using LDtk;
using LDtk.Generator;
using LDtk.Renderer;
using LDtkTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

            // Load world you can pass content here if you plan on using it
            world = LDtkWorld.LoadWorld("Test_file_for_API_showing_all_features", Content);

            level = world.LoadLevel("Level_0", Content);

            // Prerender the level to speed up rendering
            renderer.PrerenderLevel(level);

            intGrid8px = level.GetIntGrid("IntGrid_8px_grid");
            intGridClassic = level.GetIntGrid("IntGrid_classic");

            MyLevelClass levelFields = level.GetCustomFields<MyLevelClass>();
            PrintEntities(levelFields);




            LdtkGeneratorContext ctx = new LdtkGeneratorContext();
            ctx.LevelClassName = "MyLevelClass";
            ctx.TypeConverter = new LdtkTypeConverter();
            ctx.CodeSettings.Namespace = "LDtkTypes";

            SourceGeneratorOutput fOut = new SourceGeneratorOutput();

            LdtkCodeGenerator cg = new LdtkCodeGenerator();

            LDtkWorld ldtkWorld = JsonSerializer.Deserialize<LDtkWorld>(File.ReadAllText("C:\\Users\\Econn\\Desktop\\Kenney Shooter\\World.ldtk"), LDtkWorld.SerializeOptions);

            cg.GenerateCode(ldtkWorld, ctx, fOut);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(fOut.TextOutput);
        }

        private void PrintEntities(MyLevelClass levelFields)
        {
            Console.WriteLine(level.Identifier + " desc :\n" + levelFields.desc);

            EntityFieldsTest[] entityFieldsTests = level.GetEntities<EntityFieldsTest>();
            Console.WriteLine("EntityFieldsTests:");

            for (int i = 0; i < entityFieldsTests.Length; i++)
            {
                if (entityFieldsTests[i].Array_Enum.Length > 0)
                    Console.WriteLine(entityFieldsTests[i].Array_Enum[0]);
                if (entityFieldsTests[i].Array_Enum.Length > 0)
                    Console.WriteLine(entityFieldsTests[i].Array_Integer[0]);
                if (entityFieldsTests[i].Array_Enum.Length > 0)
                    Console.WriteLine(entityFieldsTests[i].Array_multilines[0]);
                if (entityFieldsTests[i].Array_Enum.Length > 0)
                    Console.WriteLine(entityFieldsTests[i].Array_points[0]);
                Console.WriteLine(entityFieldsTests[i].Boolean);
                Console.WriteLine(entityFieldsTests[i].EditorVisualColor);
                Console.WriteLine(entityFieldsTests[i].Enum);
                Console.WriteLine(entityFieldsTests[i].FilePath);
                Console.WriteLine(entityFieldsTests[i].Float);
                Console.WriteLine(entityFieldsTests[i].Identifier);
                Console.WriteLine(entityFieldsTests[i].Integer);
                Console.WriteLine(entityFieldsTests[i].Pivot);
                Console.WriteLine(entityFieldsTests[i].Point);
                Console.WriteLine(entityFieldsTests[i].Position);
                Console.WriteLine(entityFieldsTests[i].Size);
                Console.WriteLine("`" + entityFieldsTests[i].String_multiLines + "`");
                Console.WriteLine(entityFieldsTests[i].String_singleLine);
                Console.WriteLine(entityFieldsTests[i].Uid);
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
            camera.Zoom = 2;
            camera.Position = level.Position.ToVector2() + (level.Size.ToVector2() / 2f);
            camera.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(level._BgColor);

            spriteBatch.Begin(camera, SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            {
                // Draw Levels layers
                renderer.RenderPrerenderedLevel(level);

                // Rendering int grid
                renderer.RenderIntGrid(intGrid8px);

                // Rendering int grid
                renderer.RenderIntGrid(intGridClassic, 3);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}