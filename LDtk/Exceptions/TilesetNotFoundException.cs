using System;

namespace LDtk.Exceptions
{
    /// <summary>
    /// Tileset Not Found Exception
    /// </summary>
    public class TilesetNotFoundException : Exception
    {
        /// <summary>
        /// Tileset Not Found Exception
        /// </summary>
        public TilesetNotFoundException() : base() { }

        /// <summary>
        /// Tileset Not Found Exception
        /// </summary>
        /// <param name="message"></param>
        public TilesetNotFoundException(string message) : base(message) { }

        /// <summary>
        /// Tileset Not Found Exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public TilesetNotFoundException(string message, Exception innerException) : base(message, innerException) { }

    }
}
