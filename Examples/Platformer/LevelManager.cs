using System;
using System.Collections.Generic;
using LDtk.Renderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LDtk.Examples.Platformer
{
    public class LevelManager
    {
        public LDtkLevel CurrentLevel { get; private set; }

        public Action<LDtkLevel> OnEnterNewLevel;
        private readonly List<string> levelsVisited = new List<string>();
        private readonly LDtkWorld world;

        public ContentManager Content;

        private Vector2 center;
        private readonly LDtkRenderer renderer;

        public LevelManager(LDtkWorld world, SpriteBatch spriteBatch)
        {
            this.world = world;
            renderer = new LDtkRenderer(spriteBatch);
        }

        public LevelManager(LDtkWorld world, SpriteBatch spriteBatch, ContentManager Content)
        {
            this.world = world;
            this.Content = Content;
            renderer = new LDtkRenderer(spriteBatch, Content);
        }

        public void Update()
        {
            // Handle leaving a level
            if (LevelContainsPoint(CurrentLevel, center) == false)
            {
                for (int i = 0; i < CurrentLevel._Neighbours.Length; i++)
                {
                    LDtkLevel neighbour = world.LoadLevel(CurrentLevel._Neighbours[i].LevelUid, Content);
                    renderer.PrerenderLevel(neighbour);

                    if (LevelContainsPoint(neighbour, center))
                    {
                        ChangeLevelTo(neighbour.Identifier);
                    }
                }
            }
        }

        public void Draw()
        {
            renderer.RenderPrerenderedLevel(CurrentLevel);

            for (int i = 0; i < CurrentLevel._Neighbours.Length; i++)
            {
                LDtkLevel neighbour = world.LoadLevel(CurrentLevel._Neighbours[i].LevelUid, Content);
                renderer.RenderPrerenderedLevel(neighbour);
            }
        }

        public void ChangeLevelTo(string identifier)
        {
            if (Content != null)
            {
                CurrentLevel = world.LoadLevel(identifier, Content);
            }
            else
            {
                CurrentLevel = world.LoadLevel(identifier);
            }

            renderer.PrerenderLevel(CurrentLevel);

            for (int ii = 0; ii < CurrentLevel._Neighbours.Length; ii++)
            {
                LDtkLevel neighbourLevel = world.LoadLevel(CurrentLevel._Neighbours[ii].LevelUid, Content);
                renderer.PrerenderLevel(neighbourLevel);
            }

            EnterNewLevel();
        }

        public void MoveTo(Vector2 center)
        {
            this.center = center;
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
                point.X <= level.Position.X + level.Size.X &&
                point.Y <= level.Position.Y + level.Size.Y;
        }
    }
}