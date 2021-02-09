using System;
using Microsoft.Xna.Framework;

namespace LDtk
{
    /// <summary>
    /// LDtk IntGrid
    /// </summary>
    public struct IntGrid
    {
        /// <summary>
        /// Size of 
        /// </summary>
        /// <value></value>
        public int TileSize { get => tileSize; }

        internal string identifier;
        internal long[,] grid;
        internal int tileSize;

        /// <summary>
        /// Gets the int value at location
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Int at grid position otherwise -1 if out of bounds</returns>
        public long GetValueAt(int x, int y)
        {
            if (x > 0 && y > 0 && x < grid.GetLength(0) && y < grid.GetLength(1))
            {
                return grid[x, y];
            }
            return -1;
        }

        /// <summary>
        /// Convert from world pixel space to int grid space
        /// </summary>
        /// <param name="position">World pixel coordinates</param>
        /// <returns>Grid position</returns>
        public Vector2 FromWorldToGridSpace(Vector2 position)
        {
            int x = (int)MathF.Floor(position.X / tileSize);
            int y = (int)MathF.Floor(position.Y / tileSize);

            return new Vector2(x, y);
        }
    }
}