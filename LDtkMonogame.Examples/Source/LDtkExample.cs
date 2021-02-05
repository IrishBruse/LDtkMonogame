using System;
using System.Collections.Generic;
using System.Linq;

using LDtk;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Examples
{
    public class LDtkExample : BaseExample
    {
        // Camera
        private Vector3 cameraPosition;
        private Vector3 cameraOrigin;
        private float cameraZoom = 1f;

        // LDtk stuff
        private readonly int currentLevel = 0;
        private World world;
        private const string LDTK_FILE = "samples/LDtkMonogameExample.ldtk";
        private Level startLevel;
        private Level[] neighbours;
        private readonly List<ISprite> drawableEntities = new List<ISprite>();

        public LDtkExample() : base()
        {
            IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            base.Initialize();

            world = new World(spriteBatch, LDTK_FILE);
            world.Load(currentLevel);

            startLevel = world.GetLevel("Level1");
            neighbours = (from neighbour in startLevel.Neighbours select world.GetLevel(neighbour)).ToArray();

            Door[] doors = startLevel.GetEntities<Door>();

            drawableEntities.AddRange(doors);

            Crate[] crates = startLevel.GetEntities<Crate>();

            drawableEntities.AddRange(crates);

            Console.WriteLine("crates     | " + crates.Length);

            for(int i = 0; i < crates.Length; i++)
            {
                Console.WriteLine("Position   | " + crates[i].Position);
                Console.WriteLine("Pivot      | " + crates[i].Pivot);
                Console.WriteLine("Texture    | " + crates[i].Texture.Name);
                Console.WriteLine("FrameSize  | " + crates[i].FrameSize);
                Console.WriteLine("integer    | " + crates[i].integer);
                Console.WriteLine("decimal    | " + crates[i].dec);
                Console.WriteLine("boolean    | " + crates[i].boolean);
                Console.WriteLine("name       | " + crates[i].name);
                Console.WriteLine("multilines | " + crates[i].multilines);
                Console.WriteLine("color      | " + crates[i].color);
                Console.WriteLine("alphabet   | " + crates[i].alphabet);
                Console.WriteLine();
            }
        }

        public override void OnWindowResized()
        {
            cameraOrigin = new Vector3(GraphicsDevice.Viewport.Width / 2f, GraphicsDevice.Viewport.Height / 2f, 0);
            cameraZoom = 2;
        }

        protected override void Update(GameTime gameTime)
        {
            double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if(mouse.MiddleButton == ButtonState.Pressed)
            {
                Point pos = mouse.Position - oldMouse.Position;
                cameraPosition += new Vector3(pos.X, pos.Y, 0) * 30 * (float)deltaTime;
            }

            oldKeyboard = keyboard;
            oldMouse = mouse;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(startLevel.BgColor);

            Texture2D texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            spriteBatch.Begin(SpriteSortMode.Texture, samplerState: SamplerState.PointClamp, transformMatrix: Matrix.CreateTranslation(cameraPosition) * Matrix.CreateScale(cameraZoom) * Matrix.CreateTranslation(cameraOrigin));
            {
                for(int i = 0; i < startLevel.Layers.Length; i++)
                {
                    spriteBatch.Draw(startLevel.Layers[i], startLevel.WorldPosition, Color.White);
                }

                for(int i = 0; i < neighbours.Length; i++)
                {
                    for(int j = 0; j < neighbours[i].Layers.Length; j++)
                    {
                        spriteBatch.Draw(neighbours[i].Layers[j], neighbours[i].WorldPosition, Color.White);
                    }
                }

                for(int i = 0; i < drawableEntities.Count; i++)
                {
                    Vector2 size = new Vector2(drawableEntities[i].Texture.Width, drawableEntities[i].Texture.Height);
                    spriteBatch.Draw(drawableEntities[i].Texture,
                        drawableEntities[i].Position,
                        new Rectangle(0, 0, (int)drawableEntities[i].FrameSize.X, (int)drawableEntities[i].FrameSize.Y),
                        Color.White,
                        0, drawableEntities[i].Pivot * drawableEntities[i].FrameSize, 1,
                        SpriteEffects.None, 0);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}