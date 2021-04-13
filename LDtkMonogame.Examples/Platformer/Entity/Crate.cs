
using Microsoft.Xna.Framework;

namespace LDtk.Examples.Platformer
{
    public class Crate : LDtkEntity
    {
        // LDtk entity fields
        public int integer;
        public float dec;
        public bool boolean;
        public string name;
        public string multilines;
        public Vector2 point;
        public Color color;
        public Alphabet alphabet;
        public Rect collider;
        private bool damaged;
        private float timer;

        public enum Alphabet
        {
            A, B, C
        }

        public void Update(float deltaTime)
        {
            if (damaged)
            {
                timer += deltaTime;

                if (timer >= .2f)
                {
                    timer -= .2f;
                    Tile = new Rectangle(0 * (int)Size.X, 0, (int)Size.X, (int)Size.Y);
                }
            }
        }
        public void Damage()
        {
            damaged = true;
            Tile = new Rectangle(1 * (int)Size.X, 0, (int)Size.X, (int)Size.Y);
        }
    }
}