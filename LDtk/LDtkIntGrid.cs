using System;
using System.Collections.Generic;
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
        public int TileSize { get => tileSize; }

        /// <summary>
        /// The underlying values of the int grid
        /// </summary>
        /// <value>Integer</value>
        public int[,] Values { get => grid; }

        internal int[,] grid;
        internal int tileSize;
        internal Dictionary<int, Color> colors = new Dictionary<int, Color>();

        /// <summary>
        /// Gets the int value at location
        /// </summary>
        /// <param name="x">X index</param>
        /// <param name="y">Y index</param>
        /// <returns>Value at position</returns>
        public long GetValueAt(int x, int y)
        {
            // Inside bounds
            if (x >= 0 && y >= 0 && x < grid.GetLength(0) && y < grid.GetLength(1))
            {
                return grid[x, y];
            }
            return -1;
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

        /// <summary>
        /// Returns the color from the intgrid value set in ldtk
        /// </summary>
        /// <param name="value">Intgrid value</param>
        /// <returns>Color of intgrid cell value</returns>
        public Color GetColorFromValue(int value)
        {
            if (colors.TryGetValue(value, out Color col))
            {
                return col;
            }

            return Color.HotPink;
        }
    }
}