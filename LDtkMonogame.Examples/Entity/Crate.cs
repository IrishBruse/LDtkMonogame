using LDtk;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Examples
{
    public class Crate : ISprite
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