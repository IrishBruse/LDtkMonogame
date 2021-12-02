using System;
using System.Collections.Generic;
using AABB;
using LDtk;
using LDtk.Renderer;
using LDtkTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Shooter.Entities;
public class PlayerEntity
{
    public Vector2 Position { get => data.Position; set => data.Position = value; }

    private readonly Box collider;
    private readonly Player data;
    private readonly Texture2D texture;
    private readonly LDtkRenderer renderer;
    private readonly LDtkLevel level;
    private Vector2 velocity;
    private float direction;
    private List<Box> tiles;
    private bool grounded;

    public PlayerEntity(Player player, Texture2D texture, LDtkRenderer renderer, LDtkLevel level)
    {
        data = player;
        this.texture = texture;
        this.renderer = renderer;
        this.level = level;

        collider = new Box(Vector2.Zero, new Vector2(10, 16), data.Pivot);
    }

    public void Update(float deltaTime)
    {
        collider.Position = Position;
        KeyboardState keyboard = Keyboard.GetState();

        int h = (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left) ? -1 : 0) + (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right) ? +1 : 0);

        if (keyboard.IsKeyDown(Keys.Space) && grounded)
        {
            velocity -= new Vector2(0, 90);
        }

        if (h != 0)
        {
            direction = h;
        }

        float gravityMultiplier = 1;

        if (velocity.Y > 0)
        {
            gravityMultiplier = 1.8f;
        }

        velocity = new Vector2(h * 40, velocity.Y);
        velocity += new Vector2(0, 200) * gravityMultiplier * deltaTime;

        CollisionDetection(level, deltaTime);
        Position += velocity * deltaTime;
    }

    public void Draw(float totalTime)
    {
        int frame = 0;
        if (velocity.X != 0)
        {
            frame = (int)(totalTime * 10) % 2;
        }

        renderer.RenderEntity(data, texture, (SpriteEffects)(direction < 0 ? 1 : 0), frame);

        for (int i = 0; i < tiles.Count; i++)
        {
            renderer.SpriteBatch.DrawRect(tiles[i], new Color(128, 255, 0, 128));
        }

        renderer.SpriteBatch.DrawRect(collider, new Color(128, 255, 0, 128));
    }

    private void CollisionDetection(LDtkLevel level, float deltaTime)
    {
        grounded = false;
        LDtkIntGrid collisions = level.GetIntGrid("Tiles");
        Vector2 topleft = Vector2.Min(collider.TopLeft, collider.TopLeft + (velocity * deltaTime)) - level.Position.ToVector2();
        Vector2 bottomRight = Vector2.Max(collider.BottomRight, collider.BottomRight + (velocity * deltaTime)) - level.Position.ToVector2();

        Point topLeftGrid = collisions.FromWorldToGridSpace(topleft);
        Point bottomRightGrid = collisions.FromWorldToGridSpace(bottomRight + (Vector2.One * collisions.TileSize));

        tiles = new List<Box>();

        for (int x = topLeftGrid.X; x < bottomRightGrid.X; x++)
        {
            for (int y = topLeftGrid.Y; y < bottomRightGrid.Y; y++)
            {
                long intGridValue = collisions.GetValueAt(x, y);
                if (intGridValue is 6 or 7)
                {
                    tiles.Add(new Box(level.Position.ToVector2() + new Vector2(x * collisions.TileSize, y * collisions.TileSize), new Vector2(collisions.TileSize), Vector2.Zero));
                }
            }
        }

        List<KeyValuePair<int, float>> tilesDistance = new List<KeyValuePair<int, float>>();
        // get values to be sorted
        for (int i = 0; i < tiles.Count; i++)
        {
            if (collider.Cast(velocity, tiles[i], out Vector2 cp, out Vector2 cn, out float hitNear, deltaTime))
            {
                if (cn == new Vector2(0, -1))
                {
                    grounded = true;
                }

                tilesDistance.Add(new KeyValuePair<int, float>(i, hitNear));
            }
        }

        // Sort to stop jitter
        tilesDistance.Sort((a, b) =>
        {
            return a.Value.CompareTo(b.Value);
        });

        // Perform collision resolution
        for (int i = 0; i < tilesDistance.Count; i++)
        {
            Box rect = tiles[tilesDistance[i].Key];

            if (collider.Cast(velocity, rect, out Vector2 cp, out Vector2 cn, out float ct, deltaTime))
            {
                velocity += cn * new Vector2(MathF.Abs(velocity.X), MathF.Abs(velocity.Y)) * (1 - ct);
            }
        }
    }
}
