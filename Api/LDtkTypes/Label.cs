using Microsoft.Xna.Framework;

namespace Examples.Api
{
    public class Label// : LDtkEntity
    {
        public string text;
        public Color color;

        public override string ToString()
        {
            return text + "\n" + color.ToString();
        }
    }
}