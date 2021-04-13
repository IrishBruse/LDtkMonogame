using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LDtk.Examples.Platformer
{
    public class Player : LDtkEntity
    {
        private const float Gavity = 175f;

        public Animator animator;
        public bool fliped = true;
        public Rect collider;
        public Rect attack;
        public Vector2 velocity;
        public List<(Rect rect, long type)> tiles;
        public Door door;

        private float gravityMultiplier;
        private bool grounded;
        private bool noClip;
        private bool onPlatfrom;
        internal bool attacking;

        public Player()
        {
            collider = new Rect(-10, -25, 20, 25);
            attack = new Rect(20, -30, 20, 40);
            animator = new Animator(this);
        }

        public void Update(KeyboardState keyboard, KeyboardState oldKeyboard, MouseState mouse, MouseState oldMouse, LDtkLevel level, float deltaTime)
        {
            if (keyboard.IsKeyDown(Keys.F3) && oldKeyboard.IsKeyDown(Keys.F3) == false)
            {
                noClip = !noClip;
            }

            attack.ParentPosition = collider.ParentPosition = Position;

            attack.Origin = new Vector2(fliped ? 20 : -20 - attack.Size.X, attack.Origin.Y);

            Movement(keyboard, oldKeyboard, mouse, oldMouse, deltaTime);
            if (noClip == false)
            {
                CollisionDetection(level, deltaTime);
            }
            animator.SetData(velocity, grounded);
            animator.Animate(deltaTime);

            Position += velocity * deltaTime;
        }

        private void Movement(KeyboardState keyboard, KeyboardState oldKeyboard, MouseState mouse, MouseState oldMouse, float deltaTime)
        {
            float h = (keyboard.IsKeyDown(Keys.A) ? -1 : 0) + (keyboard.IsKeyDown(Keys.D) ? 1 : 0);
            float v = (keyboard.IsKeyDown(Keys.W) ? -1 : 0) + (keyboard.IsKeyDown(Keys.S) ? 1 : 0);

            attacking = false;
            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                attacking = true;
                animator.SetState(Animator.Animation.Attack);
            }

            if (keyboard.IsKeyDown(Keys.E) && oldKeyboard.IsKeyDown(Keys.E) == false && door != null)
            {
                animator.SetState(Animator.Animation.EnterDoor);
            }

            if (noClip == false)
            {
                if (animator.CanMove())
                {
                    h = 0;
                }

                velocity = new Vector2(h * 90, velocity.Y);
            }
            else
            {
                velocity = new Vector2(h * 90, v * 90);
            }

            if (keyboard.IsKeyDown(Keys.Space) && oldKeyboard.IsKeyDown(Keys.Space) == false && grounded)
            {
                if (onPlatfrom && keyboard.IsKeyDown(Keys.S))
                {
                    Position += new Vector2(0, 1);
                }
                else
                {
                    grounded = false;
                    velocity = new Vector2(velocity.X, -130);
                    animator.SetState(Animator.Animation.Jump);
                }
            }

            // falling
            gravityMultiplier = velocity.Y > 0 ? 3f : velocity.Y < 0 && !keyboard.IsKeyDown(Keys.Space) ? 1.5f : 1;

            if (noClip == false)
            {
                velocity += new Vector2(0, Gavity) * gravityMultiplier * deltaTime;
            }

            if (h != 0)
            {
                fliped = h > 0;
            }
        }

        private void CollisionDetection(LDtkLevel level, float deltaTime)
        {
            grounded = false;

            LDtkIntGrid collisions = level.GetIntGrid("Collisions");
            Vector2 topleft = Vector2.Min(collider.WorldPosition, collider.WorldPosition + (velocity * deltaTime)) - level.Position;
            Vector2 bottomRight = Vector2.Max(collider.WorldPosition + collider.Size, collider.WorldPosition + collider.Size + (velocity * deltaTime)) - level.Position;

            Point topLeftGrid = collisions.FromWorldToGridSpace(topleft);
            Point bottomRightGrid = collisions.FromWorldToGridSpace(bottomRight + (Vector2.One * collisions.TileSize));

            tiles = new List<(Rect rect, long type)>();

            for (int x = topLeftGrid.X; x < bottomRightGrid.X; x++)
            {
                for (int y = topLeftGrid.Y; y < bottomRightGrid.Y; y++)
                {
                    long intGridValue = collisions.GetValueAt(x, y);
                    if (intGridValue > 0)
                    {
                        tiles.Add((new Rect(level.Position.X + (x * collisions.TileSize), level.Position.Y + (y * collisions.TileSize), collisions.TileSize, collisions.TileSize), intGridValue));
                    }
                }
            }

            List<KeyValuePair<int, float>> z = new List<KeyValuePair<int, float>>();
            // get values to be sorted
            for (int i = 0; i < tiles.Count; i++)
            {
                if (collider.Cast(velocity, tiles[i].rect, out Vector2 cp, out Vector2 cn, out float ct, deltaTime))
                {
                    z.Add(new KeyValuePair<int, float>(i, ct));
                }
            }

            // Sort to stop jitter
            z.Sort((a, b) =>
            {
                return a.Value.CompareTo(b.Value);
            });

            onPlatfrom = false;
            // Perform collision resolution
            for (int i = 0; i < z.Count; i++)
            {
                (Rect rect, long type) = tiles[z[i].Key];

                if (collider.Cast(velocity, rect, out Vector2 cp, out Vector2 cn, out float ct, deltaTime))
                {
                    long val = type;

                    if (cn == new Vector2(0, -1))
                    {
                        grounded = true;
                    }

                    switch (val)
                    {
                        case 1:
                            velocity += cn * new Vector2(MathF.Abs(velocity.X), MathF.Abs(velocity.Y)) * (1 - ct);
                            break;

                        case 2:
                        case 3:
                            if (cn == new Vector2(0, -1))
                            {
                                onPlatfrom = true;
                                velocity += cn * new Vector2(MathF.Abs(velocity.X), MathF.Abs(velocity.Y)) * (1 - ct);
                            }
                            break;

                        default:
                            throw new Exception(val + " is an unhandled intgrid type");
                    }
                }
            }
        }
    }
}