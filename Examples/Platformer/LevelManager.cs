using System;
using System.Collections.Generic;
using LDtk;
using LDtk.Renderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer;

public class LevelManager
{
    public LDtkLevel CurrentLevel { get; private set; }

    public Action<LDtkLevel> onEnterNewLevel;
    private readonly List<string> levelsVisited = new List<string>();
    private readonly LDtkWorld world;

    public ContentManager content;

    private Vector2 center;
    private readonly LDtkRenderer renderer;

    public LevelManager(LDtkWorld world, SpriteBatch spriteBatch)
    {
        this.world = world;
        renderer = new LDtkRenderer(spriteBatch);
    }

    public LevelManager(LDtkWorld world, SpriteBatch spriteBatch, ContentManager content)
    {
        this.world = world;
        this.content = content;
        renderer = new LDtkRenderer(spriteBatch, content);
    }

    public void Update()
    {
        // Handle leaving a level
        if (CurrentLevel.Contains(center) == false)
        {
            for (int i = 0; i < CurrentLevel._Neighbours.Length; i++)
            {
                LDtkLevel neighbour = world.LoadLevel(CurrentLevel._Neighbours[i].LevelUid, content);
                renderer.PrerenderLevel(neighbour);

                if (neighbour.Contains(center))
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
            LDtkLevel neighbour = world.LoadLevel(CurrentLevel._Neighbours[i].LevelUid, content);
            renderer.RenderPrerenderedLevel(neighbour);
        }
    }

    public void ChangeLevelTo(string identifier)
    {
        CurrentLevel = content != null ? world.LoadLevel(identifier, content) : world.LoadLevel(identifier);

        renderer.PrerenderLevel(CurrentLevel);

        for (int ii = 0; ii < CurrentLevel._Neighbours.Length; ii++)
        {
            LDtkLevel neighbourLevel = world.LoadLevel(CurrentLevel._Neighbours[ii].LevelUid, content);
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
            onEnterNewLevel?.Invoke(CurrentLevel);
        }
    }
}
