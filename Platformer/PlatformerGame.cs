using System.Collections.Generic;
using Comora;
using LDtk;
using LDtk.Examples.Platformer;
using LDtkTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Examples.Platformer
{
    public class PlatformerGame : BaseExample
    {
        private LDtkWorld world;
        private LevelManager levelManager;

        private KeyboardState oldKeyboard;
        private MouseState oldMouse;

        private Player player;
        private bool showTileColliders = false;
        private bool showEntityColliders = false;

        private readonly List<Door> doors = new List<Door>();
        private readonly List<Crate> crates = new List<Crate>();
        private readonly List<Diamond> diamonds = new List<Diamond>();

        Camera camera;

        public PlatformerGame() : base()
        {
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            Window.Title = "LDtkMonogame - Api";

            camera = new Camera(GraphicsDevice);

            world = LDtkWorld.LoadWorld("LDtkMonogameExample", Content);

            levelManager = new LevelManager(world, spriteBatch, Content);
            levelManager.ChangeLevelTo("Level1");

            var spawnPoint = levelManager.CurrentLevel.GetEntity<PlayerSpawn>();

            player = new Player
            {
                texture = Content.Load<Texture2D>("Art/Characters/KingHuman"),
                Position = spawnPoint.Position,
                Pivot = spawnPoint.Pivot,
#if DEBUG
                // EditorVisualColor = startLocation.EditorVisualColor,
#endif
                Tile = new Rectangle(0, 0, 78, 58),
                Size = new Vector2(78, 58)
            };
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState keyboard = Keyboard.GetState();

            // Debug/Cheats
            if (keyboard.IsKeyDown(Keys.F1) && oldKeyboard.IsKeyDown(Keys.F1) == false)
            {
                showTileColliders = !showTileColliders;
            }

            if (keyboard.IsKeyDown(Keys.F2) && oldKeyboard.IsKeyDown(Keys.F2) == false)
            {
                showEntityColliders = !showEntityColliders;
            }

            if (keyboard.IsKeyDown(Keys.F4) && oldKeyboard.IsKeyDown(Keys.F4) == false)
            {
                // diamondsCollected++;
            }

            camera.Position = Mouse.GetState().Position.ToVector2();
            camera.Update(gameTime);

            levelManager.MoveTo(player.Position);
            levelManager.Update();

            player.Update(Keyboard.GetState(), oldKeyboard, Mouse.GetState(), oldMouse, levelManager.CurrentLevel, deltaTime);

            oldKeyboard = Keyboard.GetState();
            oldMouse = Mouse.GetState();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(levelManager.CurrentLevel._BgColor);

            spriteBatch.Begin(camera);
            {
                levelManager.Draw();
                spriteBatch.Draw(player.texture, player.Position, player.Tile, Color.White, 0,
                (player.Pivot * player.Size) + new Vector2(player.fliped ? -8 : 8, -14),
                1,
                player.fliped ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                0.1f);
            }
            spriteBatch.End();
        }


        private void DebugRendering()
        {
            // Debugging
            if (showTileColliders)
            {
                for (int i = 0; i < player.tiles.Count; i++)
                {
                    spriteBatch.DrawRect(player.tiles[i].rect, new Color(128, 255, 0, 128));
                }
            }

#if DEBUG
            if (showEntityColliders)
            {
                for (int i = 0; i < doors.Count; i++)
                {
                    spriteBatch.DrawRect(doors[i].Tile, doors[i].colo);
                }

                for (int i = 0; i < crates.Count; i++)
                {
                    spriteBatch.DrawRect(crates[i].Tile, crates[i].EditorVisualColor);
                }

                for (int i = 0; i < diamonds.Count; i++)
                {
                    spriteBatch.DrawRect(diamonds[i].Tile, diamonds[i].EditorVisualColor);
                }

                spriteBatch.DrawRect(player.collider, player.EditorVisualColor);
                spriteBatch.DrawRect(player.attack, player.EditorVisualColor);
            }
#endif
        }


    }
}