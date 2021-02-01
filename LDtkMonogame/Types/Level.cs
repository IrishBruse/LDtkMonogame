using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDtk
{
    /// <summary>
    /// Abstracted version of ldtk's level
    /// </summary>
    public struct Level
    {
        /// <summary>
        /// Wether or not the level has been loaded
        /// </summary>
        internal bool Loaded;

        /// <summary>
        /// The identifier of the level set in ldtk
        /// </summary>
        public string Identifier { get; internal set; }

        /// <summary>
        /// The Uid of the level set in ldtk
        /// </summary>
        public long Uid { get; internal set; }

        /// <summary>
        /// World position of the level
        /// </summary>
        public Vector2 WorldPosition { get; internal set; }

        /// <summary>
        /// The clear color for the level
        /// </summary>
        public Color BgColor { get; internal set; }

        /// <summary>
        /// Prerendered layer textures created from <see cref="Project.Load(int)"/>
        /// </summary>
        public RenderTarget2D[] Layers { get; internal set; }

        /// <summary>
        /// The neighbours of the level
        /// </summary>
        public long[] Neighbours { get; internal set; }

        /// <summary>
        /// The entities in the level
        /// </summary>
        public Entity[] Entities { get; internal set; }
    }
}