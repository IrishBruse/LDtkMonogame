namespace LDtkMonogameExample.Platformer;

using System;
using System.Collections.Generic;

using LDtk;
using LDtk.Renderer;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


public class LevelManager
{
    public LDtkLevel CurrentLevel { get; private set; }

    public Action<LDtkLevel> OnEnterNewLevel;
    readonly List<string> levelsVisited = new();
    readonly LDtkWorld world;

    public ContentManager Content;

    Vector2 center;
    readonly ExampleRenderer renderer;

    public LevelManager(LDtkWorld world, SpriteBatch spriteBatch)
    {
        this.world = world;
        renderer = new ExampleRenderer(spriteBatch);
    }

    public LevelManager(LDtkWorld world, SpriteBatch spriteBatch, ContentManager content)
    {
        this.world = world;
        this.Content = content;
        renderer = new ExampleRenderer(spriteBatch, content);
    }

    public void Update()
    {
        // Handle leaving a level
        if (CurrentLevel.Contains(center) == false)
        {
            for (int i = 0; i < CurrentLevel._Neighbours.Length; i++)
            {
                LDtkLevel neighbour = world.LoadLevel(CurrentLevel._Neighbours[i].LevelIid);
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
            LDtkLevel neighbour = world.LoadLevel(CurrentLevel._Neighbours[i].LevelIid);
            renderer.RenderPrerenderedLevel(neighbour);
        }
    }

    public void ChangeLevelTo(string identifier)
    {
        CurrentLevel = world.LoadLevel(identifier);

        renderer.PrerenderLevel(CurrentLevel);

        for (int ii = 0; ii < CurrentLevel._Neighbours.Length; ii++)
        {
            LDtkLevel neighbourLevel = world.LoadLevel(CurrentLevel._Neighbours[ii].LevelIid);
            renderer.PrerenderLevel(neighbourLevel);
        }

        EnterNewLevel();
    }

    public void MoveTo(Vector2 center)
    {
        this.center = center;
    }

    void EnterNewLevel()
    {
        if (levelsVisited.Contains(CurrentLevel.Identifier) == false)
        {
            levelsVisited.Add(CurrentLevel.Identifier);
            OnEnterNewLevel?.Invoke(CurrentLevel);
        }
    }
}
