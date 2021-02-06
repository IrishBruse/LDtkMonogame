using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDtk
{
    /// <summary>
    /// Sprite Interface
    /// </summary>
    public interface ISprite
    {
        /// <summary>
        /// World position of the sprite
        /// </summary>
        /// <value></value>
        public Vector2 Position { get; set; }

        /// <summary>
        /// The pivot of the sprite
        /// </summary>
        /// <value></value>
        public Vector2 Pivot { get; set; }

        /// <summary>
        /// The texture if specified for the sprite
        /// </summary>
        /// <value></value>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// This is the width and height of the entity in ldtk
        /// </summary>
        public Vector2 FrameSize { get; set; }
    }
}
