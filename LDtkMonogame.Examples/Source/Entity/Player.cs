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

        const float Gavity = 175f;

        internal bool inDoor;
        float frameTime;
        float gravityMultiplier;
        bool grounded;
        bool oldGrounded;
        int animationFrame;
        int animationType;
        bool doorInteraction;
        bool enteringDoor;


        public Player()
        {
            collider = new Rect(-10, -25, 20, 25);
        }

        public void Update(KeyboardState keyboard, KeyboardState oldKeyboard, Level level, float deltaTime)
        {
            if (keyboard.IsKeyDown(Keys.E) && oldKeyboard.IsKeyDown(Keys.E) == false)
            {
                doorInteraction = true;
                enteringDoor = true;
                animationFrame = 0;
            }

            Movement(keyboard, oldKeyboard, deltaTime);
            CollisionDetection(level, deltaTime);
            Animate(deltaTime);

            position += velocity * deltaTime;
        }

        void Movement(KeyboardState keyboard, KeyboardState oldKeyboard, float deltaTime)
        {
            float h = (keyboard.IsKeyDown(Keys.A) ? -1 : 0) + (keyboard.IsKeyDown(Keys.D) ? 1 : 0);
            float v = (keyboard.IsKeyDown(Keys.W) ? -1 : 0) + (keyboard.IsKeyDown(Keys.S) ? 1 : 0);

            velocity = new Vector2(h * 90, velocity.Y);

            if ((keyboard.IsKeyDown(Keys.Space) && oldKeyboard.IsKeyDown(Keys.Space) == false) && grounded == true)
            {
                grounded = false;
                velocity = new Vector2(velocity.X, -130);
            }

            // falling
            if (velocity.Y > 0)
            {
                gravityMultiplier = 3f;
            }// fast fall
            else if (velocity.Y < 0 && keyboard.IsKeyDown(Keys.S))
            {
                gravityMultiplier = 23f;
            }// high jump
            else if (velocity.Y < 0 && !keyboard.IsKeyDown(Keys.Space))
            {
                gravityMultiplier = 1.5f;
            }
            else
            {
                gravityMultiplier = 1;
            }

            velocity += new Vector2(0, Gavity) * gravityMultiplier * deltaTime;

            if (h != 0)
            {
                fliped = h > 0;
            }
        }

        void CollisionDetection(Level level, float deltaTime)
        {
            collider.ParentPosition = position;

            IntGrid collisions = level.GetIntGrid("Collisions");

            Vector2 topleft = Vector2.Min(collider.WorldPosition, collider.WorldPosition + (velocity * deltaTime));
            Vector2 bottomRight = Vector2.Max(collider.WorldPosition + collider.Size,
            collider.WorldPosition + collider.Size + (velocity * deltaTime));

            Vector2 topLeftGrid = collisions.FromWorldToGridSpace(topleft);
            Vector2 bottomRightGrid = collisions.FromWorldToGridSpace(bottomRight + (Vector2.One * collisions.TileSize));

            List<(Rect rect, long gridValue)> tiles = new List<(Rect rect, long gridValue)>();

            for (int x = (int)topLeftGrid.X; x < (int)bottomRightGrid.X; x++)
            {
                for (int y = (int)topLeftGrid.Y; y < (int)bottomRightGrid.Y; y++)
                {
                    int index = x + ((int)topLeftGrid.X - (int)bottomRightGrid.X) * y;
                    long intGridValue = collisions.GetValueAt(x, y);
                    if (intGridValue >= 0)
                    {
                        tiles.Add((new Rect(x * collisions.TileSize, y * collisions.TileSize, collisions.TileSize, collisions.TileSize), intGridValue));
                    }
                }
            }

            List<KeyValuePair<int, float>> z = new List<KeyValuePair<int, float>>();
            grounded = false;
            // get values to be sorted
            for (int i = 0; i < tiles.Count; i++)
            {
                if (collider.Cast(velocity, tiles[i].rect, out Vector2 cp, out Vector2 cn, out float ct, deltaTime))
                {
                    if (cn == new Vector2(0, -1))
                    {
                        grounded = true;
                    }

                    z.Add(new KeyValuePair<int, float>(i, ct));
                }
            }

            // Sort to stop jitter
            z.Sort((a, b) =>
            {
                return a.Value.CompareTo(b.Value);
            });

            // Perform collision resolution
            for (int i = 0; i < z.Count; i++)
            {
                if (collider.Cast(velocity, tiles[z[i].Key].rect, out Vector2 cp, out Vector2 cn, out float ct, deltaTime))
                {
                    long val = tiles[z[i].Key].gridValue;
                    if (val == 0)
                    {
                        velocity += cn * new Vector2(MathF.Abs(velocity.X), MathF.Abs(velocity.Y)) * (1 - ct);
                    }
                    else if (val == 1)
                    {
                        if (cn == new Vector2(0, -1))
                        {
                            velocity += cn * new Vector2(MathF.Abs(velocity.X), MathF.Abs(velocity.Y)) * (1 - ct);
                        }
                    }
                    else if (val == 2)
                    {
                        inDoor = true;
                    }
                }
            }


        }

        void Animate(float deltaTime)
        {
            frameTime += deltaTime;

            // This is awful but its only a demo so it doesnt really matter
            if (frameTime >= .1f)
            {
                frameTime -= .1f;
                if (doorInteraction)
                {
                    if (enteringDoor)
                    {
                        // Enter door
                        if (animationFrame < 7)
                        {
                            animationFrame++;
                        }
                        else
                        {
                            enteringDoor = false;
                            animationFrame = -1;
                        }
                        animationType = 2;
                    }
                    else
                    {
                        // Exit door
                        if (animationFrame < 6)
                        {
                            animationFrame++;
                        }
                        else
                        {
                            doorInteraction = false;
                        }
                        animationType = 3;
                    }
                }
                else if (velocity.Y == 0)
                {
                    if (grounded == true && oldGrounded == false)
                    {
                        animationFrame = 2;
                        animationType = 5;
                    }
                    else if (velocity.X == 0)
                    {
                        // Idling
                        animationType = 0;
                        if (animationFrame < 10)
                        {
                            animationFrame++;
                        }
                        else
                        {
                            animationFrame = 0;
                        }
                    }
                    else
                    {
                        // Walk
                        animationType = 1;
                        if (animationFrame < 7)
                        {
                            animationFrame++;
                        }
                        else
                        {
                            animationFrame = 0;
                        }
                    }

                }
                else if (velocity.Y > 0)
                {
                    animationFrame = 1;
                    animationType = 5;
                }
                else if (velocity.Y < 0)
                {
                    animationFrame = 0;
                    animationType = 5;
                }

                oldGrounded = grounded;

                frame = new Rectangle(animationFrame * (int)size.X, animationType * (int)size.Y, (int)size.X, (int)size.Y);
            }

        }
    }
}