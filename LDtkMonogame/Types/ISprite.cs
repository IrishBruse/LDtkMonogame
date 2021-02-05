using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDtk
{
    public interface ISprite
    {
        public Vector2 Position { get; set; }
        public Vector2 Pivot { get; set; }
        public Texture2D Texture { get; set; }

        /// <summary>
        /// This is the width and height of the entity in ldtk
        /// </summary>
        public Vector2 FrameSize { get; set; }
    }
}
