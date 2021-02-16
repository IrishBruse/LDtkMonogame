using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDtk
{
    /// <summary>
    /// Entity base
    /// Use this if you just want to draw a bunch of entities
    /// You can make an array of this class and draw them all together easily
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// World position of the entity
        /// </summary>
        /// <value>Pixel position in the world</value>
        public Vector2 position;

        /// <summary>
        /// The pivot of the texture attached to the entity
        /// </summary>        
        /// <value>Pixel position relative to <see cref="position"/></value>
        public Vector2 pivot;

        /// <summary>
        /// The texture if specified for the sprite
        /// </summary>
        /// <value>The optional texture for the entity</value>
        public Texture2D texture;

        /// <summary>
        /// This is the width and height of the entity in ldtk
        /// </summary>
        /// <value>The scale of the entity in pixels</value>
        public Vector2 size;

        /// <summary>
        /// Renders the entity how you see it in LDtk
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch used to render it</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,
                position,
                new Rectangle(0, 0, (int)size.X, (int)size.Y),
                Color.White,
                0, pivot * size, 1,
                SpriteEffects.None, 0);
        }
    }
}
