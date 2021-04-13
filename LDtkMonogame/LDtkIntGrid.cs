using System;
using Microsoft.Xna.Framework;

namespace LDtk
{
    /// <summary>
    /// LDtk IntGrid
    /// </summary>
    public class LDtkIntGrid
    {
        /// <summary>
        /// Size of a tile in pixels
        /// </summary>
        /// <value>Pixels</value>
        public int TileSize => tileSize;

        /// <summary>
        /// The underlying values of the int grid
        /// </summary>
        /// <value>Integer</value>
        public long[,] Values => grid;

        internal string identifier;
        internal long[,] grid;
        internal int tileSize;

        /// <summary>
        /// Gets the int value at location
        /// </summary>
        /// <param name="x">X index</param>
        /// <param name="y">Y index</param>
        /// <returns>Value at position its -1 if out of bounds</returns>
        public long GetValueAt(int x, int y)
        {
            return x >= 0 && y >= 0 && x < grid.GetLength(0) && y < grid.GetLength(1) ? grid[x, y] : -1;
        }

        /// <summary>
        /// Convert from world pixel space to int grid space
        /// Floors the value based on <see cref="TileSize"/> to an Integer
        /// </summary>
        /// <param name="position">World pixel coordinates</param>
        /// <returns>Grid position</returns>
        public Point FromWorldToGridSpace(Vector2 position)
        {
            int x = (int)MathF.Floor(position.X / tileSize);
            int y = (int)MathF.Floor(position.Y / tileSize);

            return new Point(x, y);
        }
    }
}