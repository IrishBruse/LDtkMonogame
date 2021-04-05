using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDtk.Examples.Api
{
    public class RectRegion
    {
        public Vector2 position;
        public Vector2 levelPosition;
        public Vector2 pivot;
        public Texture2D texture;
        public Vector2 size;
#if DEBUG
        public Color editorVisualColor;
#endif
        public Rectangle tile;
        public SomeEnum someEnum;
        public int integer;


        public override string ToString()
        {
            return
            position + "\n" +
            levelPosition + "\n" +
            pivot + "\n" +
            texture?.Name + "\n" +
            size + "\n" +
            editorVisualColor + "\n" +
            tile + "\n" +
            someEnum + "\n" +
            integer + "\n";
        }
    }
}