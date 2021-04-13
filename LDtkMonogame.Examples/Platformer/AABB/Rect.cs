using System;
using Microsoft.Xna.Framework;

namespace LDtk.Examples.Platformer
{
    public class Rect
    {
        public Vector2 ParentPosition { get; set; }
        public Vector2 Size { get; set; }

        public Vector2 Origin { get; set; }

        public Vector2 WorldPosition => Origin + ParentPosition;
        public Vector2 Center => WorldPosition + (Size / 2);

        public Rect(Vector2 topleftCorner, Vector2 size)
        {
            Origin = topleftCorner;
            Size = size;
        }

        public Rect(float left, float top, float width, float height)
        {
            Origin = new Vector2(left, top);
            Size = new Vector2(width, height);
        }

        public bool Contains(Vector2 point)
        {
            return point.X >= WorldPosition.X && point.Y >= WorldPosition.Y && point.X < WorldPosition.X + Size.X && point.Y <= WorldPosition.Y + Size.Y;
        }

        public bool Contains(Rect r2)
        {
            return WorldPosition.X < r2.WorldPosition.X + r2.Size.X && WorldPosition.X + Size.X > r2.WorldPosition.X &&
                WorldPosition.Y < r2.WorldPosition.Y + r2.Size.Y && WorldPosition.Y + Size.Y > r2.WorldPosition.Y;
        }


        public bool RayCast(Vector2 rayOrigin, Vector2 rayDirection, out Vector2 contactPoint, out Vector2 contactNormal, out float hitNear)
        {
            hitNear = 0;
            contactPoint = default;
            contactNormal = default;

            Vector2 invdir = new Vector2(1f / rayDirection.X, 1f / rayDirection.Y);

            Vector2 near = (WorldPosition - rayOrigin) * invdir;
            Vector2 far = (WorldPosition + Size - rayOrigin) * invdir;

            if (float.IsNaN(near.X) || float.IsNaN(near.Y) || float.IsNaN(far.X) || float.IsNaN(far.Y))
            {
                return false;
            }

            // Sort near and fars x and y
            if (near.X > far.X)
            {
                float temp = near.X;
                near.X = far.X;
                far.X = temp;
            }

            if (near.Y > far.Y)
            {
                float temp = near.Y;
                near.Y = far.Y;
                far.Y = temp;
            }

            if (near.X > far.Y || near.Y > far.X)
            {
                return false;
            }

            hitNear = MathF.Max(near.X, near.Y);
            float hitfar = MathF.Min(far.X, far.Y);

            if (hitfar < 0)
            {
                return false;
            }

            contactPoint = rayOrigin + (hitNear * rayDirection);

            contactNormal = near.X > near.Y
                ? invdir.X < 0 ? new Vector2(1, 0) : new Vector2(-1, 0)
                : invdir.Y < 0 ? new Vector2(0, 1) : new Vector2(0, -1);

            return true;
        }


        public bool Cast(Vector2 direction, Rect target, out Vector2 contactPoint, out Vector2 contactNormal, out float hitNear, float deltaTime)
        {
            contactPoint = default;
            contactNormal = default;
            hitNear = float.PositiveInfinity;

            if (direction.X == 0 && direction.Y == 0)
            {
                return false;
            }

            Rect expandedTarget = new Rect(target.WorldPosition - (Size / 2), target.Size + Size);

            return expandedTarget.RayCast(Center, direction * deltaTime, out contactPoint, out contactNormal, out hitNear) && hitNear >= 0f && hitNear < 1f;
        }
    }
}