namespace LDtkMonogameExample.Platformer.Player;

using System;
using System.Collections.Generic;

using AABB;

using LDtk;

using LDtkTypes.Platformer;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class PlayerController
{
    const float Gavity = 175f;
    public Animator Animator;
    public bool Fliped = true;
    public Box Collider;
    public Box Attack;
    public Vector2 Velocity;
    public List<(Box rect, long type)> Tiles;
    public Door Door;

    float gravityMultiplier;
    bool grounded;
    bool noClip;
    bool onPlatfrom;
    internal bool Attacking;

    public Vector2 Size { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Pivot { get; set; }
    public Rectangle Tile { get; set; }
    public Color SmartColor { get; set; }

    public PlayerController(PlayerSpawn spawn)
    {
        Position = spawn.Position;
        Pivot = spawn.Pivot;
        SmartColor = spawn.SmartColor;
        Tile = new Rectangle(0, 0, 78, 58);
        Size = new Vector2(78, 58);

        Collider = new Box(Vector2.Zero, new Vector2(20, 30), Pivot);
        Attack = new Box(Vector2.Zero, new Vector2(20, 40), Vector2.One * .5f);
        Animator = new Animator(this);
    }

    public void Update(KeyboardState keyboard, KeyboardState oldKeyboard, MouseState mouse, MouseState oldMouse, LDtkLevel level, float deltaTime)
    {
        if (keyboard.IsKeyDown(Keys.F3) && !oldKeyboard.IsKeyDown(Keys.F3))
        {
            noClip = !noClip;
        }

        Attack.Position = Collider.Position = Position;
        Attack.Position = Position + new Vector2(Fliped ? 30 : -30, 0);

        Movement(keyboard, oldKeyboard, mouse, oldMouse, deltaTime);
        if (!noClip)
        {
            CollisionDetection(level, deltaTime);
        }

        Animator.SetData(Velocity, grounded);
        Animator.Animate(deltaTime);

        Position += Velocity * deltaTime;
    }

    void Movement(KeyboardState keyboard, KeyboardState oldKeyboard, MouseState mouse, MouseState oldMouse, float deltaTime)
    {
        float h = (keyboard.IsKeyDown(Keys.A) ? -1 : 0) + (keyboard.IsKeyDown(Keys.D) ? 1 : 0);
        float v = (keyboard.IsKeyDown(Keys.W) ? -1 : 0) + (keyboard.IsKeyDown(Keys.S) ? 1 : 0);

        Attacking = false;
        if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
        {
            Attacking = true;
            Animator.SetState(Animator.Animation.Attack);
        }

        if (keyboard.IsKeyDown(Keys.E) && !oldKeyboard.IsKeyDown(Keys.E) && Door != null)
        {
            Animator.SetState(Animator.Animation.EnterDoor);
        }

        if (!noClip)
        {
            if (Animator.CanMove())
            {
                h = 0;
            }

            Velocity = new Vector2(h * 90, Velocity.Y);
        }
        else
        {
            Velocity = new Vector2(h * 90, v * 90);
        }

        if (keyboard.IsKeyDown(Keys.Space) && !oldKeyboard.IsKeyDown(Keys.Space) && grounded)
        {
            if (onPlatfrom && keyboard.IsKeyDown(Keys.S))
            {
                Position += new Vector2(0, 1);
            }
            else
            {
                grounded = false;
                Velocity = new Vector2(Velocity.X, -130);
                Animator.SetState(Animator.Animation.Jump);
            }
        }

        // falling
        gravityMultiplier = Velocity.Y > 0 ? 3f : Velocity.Y < 0 && !keyboard.IsKeyDown(Keys.Space) ? 1.5f : 1;

        if (!noClip)
        {
            Velocity += new Vector2(0, Gavity) * gravityMultiplier * deltaTime;
        }

        if (h != 0)
        {
            Fliped = h > 0;
        }
    }

    void CollisionDetection(LDtkLevel level, float deltaTime)
    {
        grounded = false;

        LDtkIntGrid collisions = level.GetIntGrid("Level");
        Vector2 topleft = Vector2.Min(Collider.TopLeft, Collider.TopLeft + (Velocity * deltaTime));
        Vector2 bottomRight = Vector2.Max(Collider.BottomRight, Collider.BottomRight + (Velocity * deltaTime));

        Point topLeftGrid = collisions.FromWorldToGridSpace(topleft);
        Point bottomRightGrid = collisions.FromWorldToGridSpace(bottomRight + (Vector2.One * collisions.TileSize));

        Tiles = new List<(Box rect, long type)>();

        for (int x = topLeftGrid.X; x < bottomRightGrid.X; x++)
        {
            for (int y = topLeftGrid.Y; y < bottomRightGrid.Y; y++)
            {
                long intGridValue = collisions.GetValueAt(x, y);
                if (intGridValue > 0)
                {
                    Tiles.Add((new Box(level.Position.ToVector2() + new Vector2(x * collisions.TileSize, y * collisions.TileSize), new Vector2(collisions.TileSize), Vector2.Zero), intGridValue));
                }
            }
        }

        List<KeyValuePair<int, float>> z = new();
        // get values to be sorted
        for (int i = 0; i < Tiles.Count; i++)
        {
            if (Collider.Cast(Velocity, Tiles[i].rect, out Vector2 cp, out Vector2 cn, out float ct, deltaTime))
            {
                z.Add(new KeyValuePair<int, float>(i, ct));
            }
        }

        // Sort to stop jitter
        z.Sort(static (a, b) => a.Value.CompareTo(b.Value));

        onPlatfrom = false;
        // Perform collision resolution
        for (int i = 0; i < z.Count; i++)
        {
            (Box rect, long type) = Tiles[z[i].Key];

            if (Collider.Cast(Velocity, rect, out Vector2 cp, out Vector2 cn, out float ct, deltaTime))
            {
                long val = type;

                if (cn == new Vector2(0, -1))
                {
                    grounded = true;
                }

                switch (val)
                {
                    case 1:
                    Velocity += cn * new Vector2(MathF.Abs(Velocity.X), MathF.Abs(Velocity.Y)) * (1 - ct);
                    break;

                    case 2:
                    case 3:
                    if (cn == new Vector2(0, -1))
                    {
                        onPlatfrom = true;
                        Velocity += cn * new Vector2(MathF.Abs(Velocity.X), MathF.Abs(Velocity.Y)) * (1 - ct);
                    }

                    break;

                    default:
                    throw new Exception(val + " is an unhandled intgrid type");
                }
            }
        }
    }
}
