using LDtk;

using Microsoft.Xna.Framework;

namespace Examples
{
    public class Crate : Entity
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

        public enum Alphabet
        {
            A, B, C
        }
    }
}