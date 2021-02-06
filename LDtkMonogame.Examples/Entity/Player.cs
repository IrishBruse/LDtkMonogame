using LDtk;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Examples
{
    public class Player : ISprite
    {
        Vector2 position;
        public Vector2 Position
        {
            get => position; set => position = value;
        }

        Vector2 pivot;
        public Vector2 Pivot
        {
            get => pivot; set => pivot = value;
        }

        Texture2D texture;
        public Texture2D Texture
        {
            get => texture; set => texture = value;
        }

        Vector2 frameSize;
        public Vector2 FrameSize
        {
            get => frameSize; set => frameSize = value;
        }

        Vector2 velocity;

        public void Update(KeyboardState keyboard, KeyboardState oldKeyboard, Level level, float deltaTime)
        {
            float h = (keyboard.IsKeyDown(Keys.A) ? -1 : 0) + (keyboard.IsKeyDown(Keys.D) ? 1 : 0);
            float v = (keyboard.IsKeyDown(Keys.W) ? -1 : 0) + (keyboard.IsKeyDown(Keys.S) ? 1 : 0);

            velocity = new Vector2(h, v) * 80 * deltaTime;

            IntGrid collisions = level.GetIntGrid("Walls");
            Point nextPosition = collisions.FromWorldToGridSpace(position + velocity);
            long val = collisions.GetValueAt(nextPosition.X, nextPosition.Y);

            if (val >= 0)
            {
                velocity = Vector2.Zero;
            }

            position += velocity;
        }
    }
}
