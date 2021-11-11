using System.IO;
using Comora;
using LDtk;
using LDtk.Renderer;
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
        // readonly List<Bee> bees = new List<Bee>();
        // readonly List<Blue_Bee> blue_bees = new List<Blue_Bee>();
        // readonly List<Slug> slugs = new List<Slug>();
        // readonly List<Gun_Pickup> guns = new List<Gun_Pickup>();

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

                // bees.AddRange(levels[i].GetEntities<Bee>());
                // blue_bees.AddRange(levels[i].GetEntities<Blue_Bee>());
                // slugs.AddRange(levels[i].GetEntities<Slug>());
                // guns.AddRange(levels[i].GetEntities<Gun_Pickup>());

                renderer.PrerenderLevel(levels[i]);
            }

            spriteSheet = Texture2D.FromFile(GraphicsDevice, Path.Combine(world.RootFolder, "Characters.png"));
        }

        protected override void Update(GameTime gameTime)
        {
            camera.Update(gameTime);

            var center = levels[0].Position.ToVector2() + (levels[0].Size.ToVector2() / 2f);
            camera.Zoom = 4;
            camera.Position = Mouse.GetState().Position.ToVector2() + center;

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

                // for (int i = 0; i < slugs.Count; i++)
                // {
                //     renderer.RenderEntity(slugs[i], spriteSheet);
                // }
            }
            spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);

            base.Draw(gameTime);
        }
    }
}