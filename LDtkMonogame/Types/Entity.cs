using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDtk
{
    /// <summary>
    /// Entity base
    /// Use this if you just want to draw a bunch of entities
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// World position of the entity
        /// </summary>
        public Vector2 position;

        /// <summary>
        /// The pivot of the texture attached to the entity
        /// </summary>
        public Vector2 pivot;

        /// <summary>
        /// The texture if specified for the sprite
        /// </summary>
        public Texture2D texture;

        /// <summary>
        /// This is the width and height of the entity in ldtk
        /// </summary>
        public Vector2 size;
    }
}
