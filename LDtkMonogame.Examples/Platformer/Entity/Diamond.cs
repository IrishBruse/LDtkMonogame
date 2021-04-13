using System;
using Microsoft.Xna.Framework;

namespace LDtk.Examples.Platformer
{
    public class Diamond : LDtkEntity
    {
        public Rect collider;

        public bool collected;
        public bool delete;
        private float timer;

        public void Update(float deltaTime, float totalTime)
        {
            if (collected)
            {
                if ((9 + (int)(timer / 0.1f)) < 12)
                {
                    timer += deltaTime;
                    Tile = new Rectangle((9 + (int)(timer / 0.1f)) * (int)Size.X, 0, (int)Size.X, (int)Size.Y);
                }
                else
                {
                    delete = true;
                }
            }
            else
            {
                int currentFrame = (int)(totalTime * 10 % 10) * (int)Size.X;
                Tile = new Rectangle(currentFrame, 0, (int)Size.X, (int)Size.Y);
                Position += new Vector2(0, -MathF.Sin((float)totalTime * 2) * 0.1f);
            }
        }
    }
}