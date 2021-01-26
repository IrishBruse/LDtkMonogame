using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDtk
{
    public struct Level
    {
        /// <summary>
        /// The clear color for the level
        /// </summary>
        public Color BgColor { get; internal set; }

        /// <summary>
        /// The background image related fields
        /// </summary>
        public Background Background { get; internal set; }

        /// <summary>
        /// Prerendered layer textures created from <see cref="Project.Render(int)"/>
        /// </summary>
        public RenderTarget2D[] Layers { get; internal set; }
    }

    /// <summary>
    /// The background image class
    /// </summary>
    public class Background
    {
        /// <summary>
        /// The croped texture if your settings caused it to
        /// clip out side the level
        /// </summary>
        public Vector2 TopLeft { get; internal set; }

        /// <summary>
        /// The croped texture if your settings caused it to
        /// clip out side the level
        /// </summary>
        public Vector2 Scale { get; internal set; }

        /// <summary>
        /// Levels Background Image
        /// </summary>
        public Texture2D Image { get; internal set; }

        /// <summary>
        /// The croped texture if your settings caused it to
        /// clip out side the level
        /// </summary>
        public Rectangle CropRect { get; internal set; }
    }
}