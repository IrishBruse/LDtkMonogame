using System;
using Microsoft.Xna.Framework;

namespace LDtk.Examples.Platformer
{
    public class Animator
    {
        public Action OnEnteredDoor;

        private float animationTimer;
        private int animationFrame;
        private readonly LDtkEntity parent;
        private Animation state; private Vector2 velocity;
        private bool grounded;
        private Animation delayedState = Animation.None;
        private int delayedFrames = -1;

        public Animator(LDtkEntity parent)
        {
            this.parent = parent;
        }

        public void Animate(float deltaTime)
        {
            animationTimer += deltaTime;

            if (animationTimer >= .1f)
            {
                animationTimer -= .1f;

                parent.Tile = new Rectangle(animationFrame * (int)parent.Size.X, (int)state * (int)parent.Size.Y, (int)parent.Size.X, (int)parent.Size.Y);

                if (delayedFrames != -1)
                {
                    if (delayedFrames > 0)
                    {
                        delayedFrames--;
                    }
                    else
                    {
                        state = delayedState;
                        delayedState = Animation.None;
                        delayedFrames = -1;
                    }
                }

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

                        if (velocity.X != 0)
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

                        if (velocity.X == 0)
                        {
                            SetState(Animation.Idle);
                        }
                        break;

                    case Animation.EnterDoor:
                        if (animationFrame < 7)
                        {
                            animationFrame++;
                        }
                        else
                        {
                            OnEnteredDoor?.Invoke();
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
                        if (grounded)
                        {
                            SetStateDelayed(Animation.Idle);
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
                    case Animation.None:
                        break;
                    case Animation.Land:
                        break;
                    case Animation.Die:
                        break;
                    case Animation.Hurt:
                        break;
                    default:
                        break;
                }
            }
        }

        public void SetData(Vector2 velocity, bool grounded)
        {
            this.velocity = velocity;
            this.grounded = grounded;
        }


        public bool CanMove()
        {
            return EnteredDoor();
        }

        public bool EnteredDoor()
        {
            return state == Animation.EnterDoor || state == Animation.ExitDoor;
        }

        public void SetState(Animation state)
        {
            this.state = state;
            animationFrame = 0;
        }

        public void SetStateDelayed(Animation state)
        {
            if (delayedState != state)
            {
                delayedState = state;
                delayedFrames = 1;
            }
        }

        public enum Animation
        {
            None = -1,
            Idle,
            Walk,
            EnterDoor,
            ExitDoor,
            Attack,
            Jump,
            Land,
            Die,
            Hurt,
        }
    }
}