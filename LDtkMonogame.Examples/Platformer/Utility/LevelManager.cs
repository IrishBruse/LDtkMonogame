using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDtk.Examples.Platformer
{
    public class LevelManager
    {
        public CustomizedLevel CurrentLevel { get; private set; }

        public Action<LDtkLevel> OnEnterNewLevel;
        private readonly List<string> levelsVisited = new List<string>();
        private readonly LDtkWorld world;
        private Vector2 center;

        public LevelManager(LDtkWorld world)
        {
            this.world = world;
        }

        public void Update()
        {
            // Handle leaving a level
            if (LevelContainsPoint(CurrentLevel, center) == false)
            {
                for (int i = 0; i < CurrentLevel.Neighbours.Length; i++)
                {
                    CustomizedLevel neighbour = world.GetLevel<CustomizedLevel>(CurrentLevel.Neighbours[i]);
                    if (LevelContainsPoint(neighbour, center))
                    {
                        CurrentLevel = neighbour;

                        EnterNewLevel();
                        for (int ii = 0; ii < CurrentLevel.Neighbours.Length; ii++)
                        {
                            world.LoadLevel(CurrentLevel.Neighbours[ii]);
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = CurrentLevel.Layers.Length - 1; i >= 0; i--)
            {
                spriteBatch.Draw(CurrentLevel.Layers[i], CurrentLevel.Position, Color.White);
            }

            for (int i = 0; i < CurrentLevel.Neighbours.Length; i++)
            {
                CustomizedLevel neighbour = world.GetLevel<CustomizedLevel>(CurrentLevel.Neighbours[i]);

                for (int j = CurrentLevel.Layers.Length - 1; j >= 0; j--)
                {
                    spriteBatch.Draw(neighbour.Layers[j], neighbour.Position, Color.White);
                }
            }
        }

        public void ChangeLevelTo(string identifier)
        {
            world.LoadLevel(identifier);
            CurrentLevel = world.GetLevel<CustomizedLevel>(identifier);
            EnterNewLevel();

            for (int i = 0; i < CurrentLevel.Neighbours.Length; i++)
            {
                world.LoadLevel(CurrentLevel.Neighbours[i]);
            }
        }

        public void SetCenterPoint(Vector2 center)
        {
            this.center = center;
        }

        internal void Clear(GraphicsDevice GraphicsDevice)
        {
            GraphicsDevice.Clear(CurrentLevel.BgColor);
        }

        private void EnterNewLevel()
        {
            if (levelsVisited.Contains(CurrentLevel.Identifier) == false)
            {
                levelsVisited.Add(CurrentLevel.Identifier);
                OnEnterNewLevel?.Invoke(CurrentLevel);
            }
        }

        private bool LevelContainsPoint(LDtkLevel level, Vector2 point)
        {
            return
                point.X >= level.Position.X &&
                point.Y >= level.Position.Y &&
                point.X < level.Position.X + level.Size.X &&
                point.Y <= level.Position.Y + level.Size.Y;
        }
    }
}