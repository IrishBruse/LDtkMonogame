using System;
using System.Collections.Generic;
using Comora;
using LDtk;
using LDtk.Renderer;
using LDtkTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Api;

public class ApiGame : BaseExample
{
    // LDtk stuff
    private LDtkWorld world;
    private LDtkLevel[] levels;
    private LDtkRenderer renderer;
    private readonly List<RectRegion> rects = new List<RectRegion>();
    private Camera camera;

    public ApiGame()
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

        levels = new LDtkLevel[world.Levels.Length];
        for (int i = 0; i < world.Levels.Length; i++)
        {
            levels[i] = world.LoadLevel("Level_" + i);

            rects.AddRange(levels[i].GetEntities<RectRegion>());

            // Prerender the level to speed up rendering
            renderer.PrerenderLevel(levels[i]);

            PrintEntities(levels[i]);
        }
    }

    private void PrintEntities(LDtkLevel level)
    {
        LDtkLevelData levelFields = level.GetCustomFields<LDtkLevelData>();
        Console.WriteLine(level.Identifier + " desc :\n" + levelFields.desc);

        EntityFieldsTest[] entityFieldsTests = level.GetEntities<EntityFieldsTest>();
        Console.WriteLine("EntityFieldsTests:");

        for (int i = 0; i < entityFieldsTests.Length; i++)
        {
            if (entityFieldsTests[i].Array_Enum.Length > 0)
            {
                Console.WriteLine(entityFieldsTests[i].Array_Enum[0]);
            }

            if (entityFieldsTests[i].Array_Integer.Length > 0)
            {
                Console.WriteLine(entityFieldsTests[i].Array_Integer[0]);
            }

            if (entityFieldsTests[i].Array_multilines.Length > 0)
            {
                Console.WriteLine(entityFieldsTests[i].Array_multilines[0]);
            }

            if (entityFieldsTests[i].Array_points.Length > 0)
            {
                Console.WriteLine(entityFieldsTests[i].Array_points[0]);
            }

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
        camera.Position = Mouse.GetState().Position.ToVector2() + new Vector2(0, 300);
        camera.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(world.BgColor);

        for (int i = 0; i < world.Levels.Length; i++)
        {
            spriteBatch.Begin(camera, SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            {
                for (int j = 0; j < rects.Count; j++)
                {
                    spriteBatch.Draw(pixel, rects[j].Position, null, rects[j].EditorVisualColor, 0, rects[j].Pivot, rects[j].Size, SpriteEffects.None, 0);
                }

                // a good idea would be to cache these intgrids
                // Rendering int grid
                renderer.RenderIntGrid(levels[i].GetIntGrid("IntGrid_8px_grid"));

                // Rendering int grid
                renderer.RenderIntGrid(levels[i].GetIntGrid("IntGrid_classic"));

                // Draw Levels layers
                renderer.RenderPrerenderedLevel(levels[i]);
            }

            spriteBatch.End();

        }

        base.Draw(gameTime);
    }
}
