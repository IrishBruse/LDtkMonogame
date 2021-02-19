using System;
using Microsoft.Xna.Framework.Graphics;
using LDtk;
using Microsoft.Xna.Framework;

public class LevelManager
{
    public Level CurrentLevel { get; private set; }

    World world;
    Vector2 center;

    public LevelManager(World world)
    {
        this.world = world;
    }

    public void Update(double deltaTime)
    {
        // Handle leaving a level
        if (LevelContainsPoint(CurrentLevel, center) == false)
        {
            for (int i = 0; i < CurrentLevel.Neighbours.Length; i++)
            {
                Level neighbour = world.GetLevel(CurrentLevel.Neighbours[i]);
                if (LevelContainsPoint(neighbour, center))
                {
                    CurrentLevel = neighbour;
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
        for (int i = 0; i < CurrentLevel.Layers.Length; i++)
        {
            spriteBatch.Draw(CurrentLevel.Layers[i], CurrentLevel.Position, Color.White);
        }

        for (int i = 0; i < CurrentLevel.Neighbours.Length; i++)
        {
            Level neighbour = world.GetLevel(CurrentLevel.Neighbours[i]);

            for (int j = 0; j < CurrentLevel.Layers.Length; j++)
            {
                spriteBatch.Draw(neighbour.Layers[j], neighbour.Position, Color.White);
            }
        }
    }

    public void SetStarterLevel(string identifier)
    {
        world.LoadLevel(identifier);
        CurrentLevel = world.GetLevel(identifier);

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

    private bool LevelContainsPoint(Level level, Vector2 point)
    {
        return
            point.X >= level.Position.X &&
            point.Y >= level.Position.Y &&
            point.X < level.Position.X + level.Size.X &&
            point.Y <= level.Position.Y + level.Size.Y;
    }
}