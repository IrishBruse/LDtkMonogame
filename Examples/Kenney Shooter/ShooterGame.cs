using System;
using System.Collections.Generic;
using System.IO;
using Comora;
using LDtk;
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
        LDtkWorld world;
        LDtkLevel[] levels;
        LDtkRenderer renderer;
        readonly List<Bee> yellow_bees = new List<Bee>();
        readonly List<Blue_Bee> blue_bees = new List<Blue_Bee>();
        readonly List<Slug> slugs = new List<Slug>();
        readonly List<Gun_Pickup> guns = new List<Gun_Pickup>();

        Camera camera;

        Texture2D spriteSheet;

        public ApiGame() : base()
        {
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            Window.Title = "LDtkMonogame - Kenney Shooter";

            camera = new Camera(GraphicsDevice);
            renderer = new LDtkRenderer(spriteBatch);

            world = LDtkWorld.LoadWorld("Kenney Shooter\\World.ldtk");
            levels = new LDtkLevel[world.Levels.Length];
            for (int i = 0; i < world.Levels.Length; i++)
            {
                levels[i] = world.LoadLevel(world.Levels[i].Identifier);

                yellow_bees.AddRange(levels[i].GetEntities<Bee>());
                blue_bees.AddRange(levels[i].GetEntities<Blue_Bee>());
                slugs.AddRange(levels[i].GetEntities<Slug>());
                guns.AddRange(levels[i].GetEntities<Gun_Pickup>());

                renderer.PrerenderLevel(levels[i]);
            }

            spriteSheet = Texture2D.FromFile(GraphicsDevice, Path.Combine(world.RootFolder, "Characters.png"));
        }

        protected override void Update(GameTime gameTime)
        {
            camera.Update(gameTime);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float totalTime = (float)gameTime.TotalGameTime.TotalSeconds;

            var center = levels[0].Position.ToVector2() + (levels[0].Size.ToVector2() / 2f);
            camera.Position = Mouse.GetState().Position.ToVector2() + center;
            camera.Zoom = 4;

            for (int i = 0; i < guns.Count; i++)
            {
                guns[i].Position += new Vector2(0, -MathF.Sin(totalTime * 1.5f) * .1f);
            }

            for (int i = 0; i < yellow_bees.Count; i++)
            {
                yellow_bees[i].Position += new Vector2(0, -MathF.Sin(totalTime * 1f) * .13f);
            }

            for (int i = 0; i < blue_bees.Count; i++)
            {
                blue_bees[i].Position = MoveTowards(blue_bees[i].Position, Vector2.Zero, deltaTime * 20);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(world.BgColor);

            spriteBatch.Begin(camera, SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            {
                // Draw Levels layers
                for (int i = 0; i < levels.Length; i++)
                {
                    renderer.RenderPrerenderedLevel(levels[i]);
                }

                // Draw Entities
                for (int i = 0; i < slugs.Count; i++)
                {
                    renderer.RenderEntity(slugs[i], spriteSheet, slugs[i].Flip ? 1 : 0);
                }

                for (int i = 0; i < yellow_bees.Count; i++)
                {
                    renderer.RenderEntity(yellow_bees[i], spriteSheet, yellow_bees[i].Flip ? 1 : 0);
                }

                for (int i = 0; i < blue_bees.Count; i++)
                {
                    renderer.RenderEntity(blue_bees[i], spriteSheet, (SpriteEffects)(blue_bees[i].Flip ? 1 : 0), (int)(gameTime.TotalGameTime.TotalSeconds % .5f / .25f));
                }

                for (int i = 0; i < guns.Count; i++)
                {
                    renderer.RenderEntity(guns[i], spriteSheet);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public static Vector2 MoveTowards(Vector2 start, Vector2 end, float maxDistanceDelta)
        {
            float diffX = end.X - start.X;
            float diffY = end.Y - start.Y;

            float sqDist = diffX * diffX + diffY * diffY;

            if (sqDist == 0 || (maxDistanceDelta >= 0 && sqDist <= maxDistanceDelta * maxDistanceDelta))
                return end;

            float dist = MathF.Sqrt(sqDist);

            return new Vector2(start.X + diffX / dist * maxDistanceDelta, start.Y + diffY / dist * maxDistanceDelta);
        }
    }
}
