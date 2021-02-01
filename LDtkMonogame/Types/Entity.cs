using Microsoft.Xna.Framework;

namespace LDtk
{
    /// <summary>
    /// Abstracted version of ldtk's Entites instance/def
    /// </summary>
    public struct Entity
    {
        /// <summary>
        /// World position of the <see cref="Entity"/>
        /// </summary>
        public Vector2 WorldPosition { get; internal set; }
    }
}