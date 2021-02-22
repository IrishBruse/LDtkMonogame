using System;
using System.Collections.Generic;
using LDtk;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Examples
{
    public class Player : Entity
    {
        // LDtk entity fields
        public bool fliped = true;
        public Rect collider;
        public Vector2 velocity;

        public List<(Rect rect, long type)> tiles;

        const float Gavity = 175f;

        internal bool inDoor;

        public Animation state;
        private Animation newState;
        int animationFrame;

        float animationTimer;
        float gravityMultiplier;
        bool grounded;
        public bool doorInteraction;
        private bool noClip;
        private bool onPlatfrom;
        Vector2 input;
        internal bool doorTransition;

        public Player()
        {
            collider = new Rect(-10, -25, 20, 25);
        }

        public void Update(KeyboardState keyboard, KeyboardState oldKeyboard, MouseState mouse, MouseState oldMouse, Level level, float deltaTime)
        {
            doorInteraction = false;

            if (keyboard.IsKeyDown(Keys.F3) && oldKeyboard.IsKeyDown(Keys.F3) == false)
            {
                noClip = !noClip;
            }

            Movement(keyboard, oldKeyboard, mouse, oldMouse, deltaTime);
            CollisionDetection(level, deltaTime);
            Animate(deltaTime);

            position += velocity * deltaTime;
        }

        void Movement(KeyboardState keyboard, KeyboardState oldKeyboard, MouseState mouse, MouseState oldMouse, float deltaTime)
        {
            float h = (keyboard.IsKeyDown(Keys.A) ? -1 : 0) + (keyboard.IsKeyDown(Keys.D) ? 1 : 0);
            float v = (keyboard.IsKeyDown(Keys.W) ? -1 : 0) + (keyboard.IsKeyDown(Keys.S) ? 1 : 0);

            input = new Vector2(h, v);

            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                SetState(Animation.Attack);
            }

            if (keyboard.IsKeyDown(Keys.E) && oldKeyboard.IsKeyDown(Keys.E) == false && inDoor == true)
            {
                ChangeState(Animation.EnterDoor);
            }

            if (noClip == false)
            {
                if (state == Animation.EnterDoor || state == Animation.ExitDoor)
                {
                    h = 0;
                }

                velocity = new Vector2(h * 90, velocity.Y);
            }
            else
            {
                velocity = new Vector2(h * 90, v * 90);
            }

            if ((keyboard.IsKeyDown(Keys.Space) && oldKeyboard.IsKeyDown(Keys.Space) == false) && grounded == true)
            {
                if (onPlatfrom && keyboard.IsKeyDown(Keys.S))
                {
                    position += new Vector2(0, 1);
                }
                else
                {
                    grounded = false;
                    velocity = new Vector2(velocity.X, -130);
                    SetState(Animation.Jump);
                }
            }

            // falling
            if (velocity.Y > 0)
            {
                gravityMultiplier = 3f;
            }// high jump
            else if (velocity.Y < 0 && !keyboard.IsKeyDown(Keys.Space))
            {
                gravityMultiplier = 1.5f;
            }
            else
            {
                gravityMultiplier = 1;
            }

            if (noClip == false)
            {
                velocity += new Vector2(0, Gavity) * gravityMultiplier * deltaTime;
            }

            if (h != 0)
            {
                fliped = h > 0;
            }
        }

        void CollisionDetection(Level level, float deltaTime)
        {
            grounded = false;

            collider.ParentPosition = position;

            IntGrid collisions = level.GetIntGrid("Collisions");
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
                    if (intGridValue >= 0)
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
                (Rect rect, long type) cell = tiles[z[i].Key];

                if (collider.Cast(velocity, cell.rect, out Vector2 cp, out Vector2 cn, out float ct, deltaTime))
                {
                    long val = cell.type;

                    if (cn == new Vector2(0, -1))
                    {
                        grounded = true;
                    }

                    switch (val)
                    {
                        case 0:
                            velocity += cn * new Vector2(MathF.Abs(velocity.X), MathF.Abs(velocity.Y)) * (1 - ct);
                            break;

                        case 1:
                            if (cn == new Vector2(0, -1))
                            {
                                onPlatfrom = true;
                                velocity += cn * new Vector2(MathF.Abs(velocity.X), MathF.Abs(velocity.Y)) * (1 - ct);
                            }
                            break;

                        default:
                            Console.WriteLine(val + " is an unhandled intgrid type");
                            break;
                    }
                }
            }
        }

        void Animate(float deltaTime)
        {
            animationTimer += deltaTime;

            if (animationTimer >= .1f)
            {
                animationTimer -= .1f;

                tile = new Rectangle(animationFrame * (int)size.X, (int)state * (int)size.Y, (int)size.X, (int)size.Y);
                state = newState;

                switch (state)
                {
                    case Animation.Idle:
                        if (animationFrame < 10)
                        {
                            animationFrame++;
                        }
                        else
                        {
                            animationFrame = 0;
                        }

                        if (input.X != 0)
                        {
                            SetState(Animation.Walk);
                        }
                        break;

                    case Animation.Walk:
                        if (animationFrame < 7)
                        {
                            animationFrame++;
                        }
                        else
                        {
                            animationFrame = 0;
                        }

                        if (input.X == 0)
                        {
                            SetState(Animation.Idle);
                        }
                        break;

                    case Animation.EnterDoor:
                        doorTransition = true;

                        if (animationFrame < 7)
                        {
                            animationFrame++;
                        }
                        else
                        {
                            ChangeState(Animation.ExitDoor);
                        }
                        break;

                    case Animation.ExitDoor:
                        if (animationFrame < 6)
                        {
                            animationFrame++;
                        }
                        else
                        {
                            SetState(Animation.Idle);
                        }
                        break;

                    case Animation.Attack:

                        if (animationFrame < 2)
                        {
                            animationFrame++;
                        }
                        else
                        {
                            SetState(Animation.Idle);
                        }
                        break;

                    case Animation.Jump:
                        if (grounded == true)
                        {
                            ChangeState(Animation.Idle);
                            animationFrame = 2;
                        }
                        else if (velocity.Y > 0)
                        {
                            animationFrame = 1;
                        }
                        else if (velocity.Y < 0)
                        {
                            animationFrame = 0;
                        }
                        break;
                }
            }
        }

        void ChangeState(Animation state)
        {
            this.newState = state;
            animationFrame = 0;
        }

        void SetState(Animation state)
        {
            this.newState = this.state = state;
            animationFrame = 0;
        }

        public enum Animation
        {
            Idle,
            Walk,
            EnterDoor,
            ExitDoor,
            Attack,
            Jump,
            Die,
            Hurt,
        }
    }
}