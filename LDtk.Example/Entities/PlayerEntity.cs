namespace LDtkMonogameExample.Entities;

using System;
using System.Collections.Generic;

using LDtk;
using LDtk.JsonPartials;
using LDtk.Renderer;

using LDtkMonogameExample;
using LDtkMonogameExample.AABB;

using LDtkTypes.World;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class PlayerEntity
{
    public Vector2 Position
    {
        get => data.Position;
        set => data.Position = value;
    }

    public LDtkLevel Level { get; set; }
    public bool Flip { get; set; }

    public Action OnShoot { get; set; }
    private readonly Box collider;
    private readonly Player data;
    private readonly Texture2D texture;
    private readonly LDtkRenderer renderer;
    private Vector2 velocity;
    private List<Box> tiles;
    private bool grounded;
    private readonly GunEntity gun;
    private bool hasGun;
    private KeyboardState oldKeyboard;
    private Vector2 startPosition;
    private float startTime;
    private bool shoot;

    public PlayerEntity(Player player, Texture2D texture, LDtkRenderer renderer, GunEntity gun)
    {
        data = player;
        this.texture = texture;
        this.renderer = renderer;
        this.gun = gun;

        startPosition = data.Position;

        collider = new Box(Vector2.Zero, new Vector2(10, 16), data.Pivot);
    }

    public void Update(float deltaTime, float totalTime)
    {
        collider.Position = Position;
        KeyboardState keyboard = Keyboard.GetState();

        int h = (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left) ? -1 : 0) + (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right) ? +1 : 0);

        if ((keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up)) && grounded)
        {
            velocity -= new Vector2(0, 90);
        }

        if (keyboard.IsKeyDown(Keys.Space) && oldKeyboard.IsKeyUp(Keys.Space) && hasGun)
        {
            OnShoot?.Invoke();
            startTime = totalTime;
            shoot = true;
        }

        if (totalTime - startTime > 0.15f)
        {
            shoot = false;
        }

        if (gun.Collider.Contains(collider))
        {
            gun.Taken = true;
            hasGun = true;
        }

        if (Position.Y > 240)
        {
            Position = startPosition;
        }

        if (h != 0)
        {
            Flip = h < 0;
        }

        float gravityMultiplier = 1;

        if (velocity.Y > 0)
        {
            gravityMultiplier = 1.8f;
        }

        velocity = new Vector2(h * 60, velocity.Y);
        velocity += new Vector2(0, 200) * gravityMultiplier * deltaTime;

        CollisionDetection(Level, deltaTime);
        Position += velocity * deltaTime;

        oldKeyboard = keyboard;
    }

    public void Draw(float totalTime)
    {
        int frame = 0;
        if (velocity.X != 0)
        {
            frame = (int)(totalTime * 10) % 2;
        }

        renderer.RenderEntity(data, texture, (SpriteEffects)(Flip ? 1 : 0), frame - (hasGun ? 5 : 0));

        if (shoot)
        {
            renderer.SpriteBatch.Draw(texture, Position + new Vector2(Flip ? -23 : 7, -5.5f), new Rectangle(48, 0, 16, 16), Color.White, 0, Vector2.Zero, Vector2.One, (SpriteEffects)(Flip ? 1 : 0), 0);
        }

        if (LDtkMonogameGame.DebugF2)
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                renderer.SpriteBatch.DrawRect(tiles[i], new Color(128, 255, 0, 128));
            }

            renderer.SpriteBatch.DrawRect(collider, new Color(128, 255, 0, 128));
        }
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

        List<KeyValuePair<int, float>> tilesDistance = new();
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
        tilesDistance.Sort((a, b) => a.Value.CompareTo(b.Value));

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
