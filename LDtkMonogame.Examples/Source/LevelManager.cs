using System;
using Microsoft.Xna.Framework.Graphics;
using LDtk;
using Microsoft.Xna.Framework;

public class LevelManager
{
    World world;
    Level currentLevel;
    Vector2 center;

    public LevelManager(World world)
    {
        this.world = world;
    }

    public void Update(double deltaTime)
    {
        // Handle leaving a level
        if (LevelContainsPoint(currentLevel, center) == false)
        {
            for (int i = 0; i < currentLevel.Neighbours.Length; i++)
            {
                Level neighbour = world.GetLevel(currentLevel.Neighbours[i]);
                if (LevelContainsPoint(neighbour, center))
                {
                    currentLevel = neighbour;
                    for (int ii = 0; ii < currentLevel.Neighbours.Length; ii++)
                    {
                        world.LoadLevel(currentLevel.Neighbours[ii]);
                    }
                }
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < currentLevel.Layers.Length; i++)
        {
            spriteBatch.Draw(currentLevel.Layers[i], currentLevel.Position, Color.White);
        }

        for (int i = 0; i < currentLevel.Neighbours.Length; i++)
        {
            Level neighbour = world.GetLevel(currentLevel.Neighbours[i]);

            for (int j = 0; j < currentLevel.Layers.Length; j++)
            {
                spriteBatch.Draw(neighbour.Layers[j], neighbour.Position, Color.White);
            }
        }
    }

    public void SetStarterLevel(string identifier)
    {
        world.LoadLevel(identifier);
        currentLevel = world.GetLevel(identifier);

        for (int i = 0; i < currentLevel.Neighbours.Length; i++)
        {
            world.LoadLevel(currentLevel.Neighbours[i]);
        }
    }

    public void SetCenterPoint(Vector2 center)
    {
        this.center = center;
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