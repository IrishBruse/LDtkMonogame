using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDtk
{
    /// <summary>
    /// Entity base
    /// Use this if you just want to draw a bunch of entities
    /// You can make an array of this class and draw them all together easily
    /// </summary>
    public class LDtkEntity
    {
        /// <summary>
        /// World position of the entity
        /// </summary>
        /// <value>Pixel position in the world</value>
        public Vector2 Position { get => position; set => position = value; }

        private Vector2 position;

        /// <summary>
        /// Local level position
        /// </summary>
        /// <value>Pixel position in the world</value>
        public Vector2 LevelPosition { get => levelPosition; set => levelPosition = value; }

        private Vector2 levelPosition;

        /// <summary>
        /// The pivot of the texture attached to the entity
        /// in the range 0-1
        /// </summary>        
        /// <value>Pixel position relative to <see cref="Position"/></value>
        public Vector2 Pivot { get => pivot; set => pivot = value; }
        private Vector2 pivot;

        /// <summary>
        /// The texture if specified for the sprite
        /// </summary>
        /// <value>The optional texture for the entity</value>
        public Texture2D Texture { get => texture; set => texture = value; }
        private Texture2D texture;

        /// <summary>
        /// This is the width and height of the entity in ldtk
        /// </summary>
        /// <value>The scale of the entity in pixels</value>
        public Vector2 Size { get => size; set => size = value; }
        private Vector2 size;

#if DEBUG
        /// <summary>
        /// This is the editor debug color of the entity in ldtk
        /// </summary>
        /// <value>The Debug only color</value>
        public Color EditorVisualColor { get => editorVisualColor; set => editorVisualColor = value; }
        private Color editorVisualColor;
#endif

        /// <summary>
        /// Optional Tile used to display this entity
        /// </summary>
        /// <value>The scale of the entity in pixels</value>
        public Rectangle Tile { get => tile; set => tile = value; }
        private Rectangle tile;
    }
}
