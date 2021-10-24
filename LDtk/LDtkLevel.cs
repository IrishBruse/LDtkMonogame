using System;
using LDtk.Exceptions;
using Microsoft.Xna.Framework.Graphics;
using Vector2Int = Microsoft.Xna.Framework.Point;

namespace LDtk
{
    public partial class LDtkLevel
    {
        /// <summary>
        /// World coordinate in pixels
        /// </summary>
        public Vector2Int Position => new Vector2Int(WorldX, WorldY);

        /// <summary>
        /// World coordinate in pixels
        /// </summary>
        public Vector2Int Size => new Vector2Int(PxWid, PxHei);

        /// <summary>
        /// Gets an intgrid in a <see cref="LDtkLevel"/>
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns><see cref="LDtkIntGrid"/></returns>
        /// <exception cref="NotImplementedException"></exception>
        public LDtkIntGrid GetIntGrid(string identifier)
        {
            // Render Tile, Auto and Int grid layers
            for (int i = 0; i < LayerInstances.Length; i++)
            {
                LayerInstance layer = LayerInstances[i];
                if (layer._Identifier != identifier)
                {
                    continue;
                }

                if (layer._Type != LayerType.IntGrid)
                {
                    continue;
                }

                LDtkIntGrid intGrid = new LDtkIntGrid()
                {
                    grid = new int[layer._CWid, layer._CHei],
                    identifier = layer._Identifier,
                    tileSize = layer._GridSize
                };

                if (layer.IntGridCsv != null)
                {
                    for (int j = 0; j < layer.IntGridCsv.Length; j++)
                    {
                        int y = j / layer._CWid;
                        int x = j - (y * layer._CWid);
                        intGrid.grid[x, y] = layer.IntGridCsv[j];
                    }
                }
                else
                {
                    throw new IntGridNotFoundException($"{identifier} not found.");
                }

                return intGrid;
            }

            throw new IntGridNotFoundException($"{identifier} not found.");
        }

        /// <summary>
        /// Gets the custom fields of the level
        /// </summary>
        /// <typeparam name="T">The custom level type generated from compiling the level</typeparam>
        public T GetCustomFields<T>() where T : new()
        {
            T levelFields = new T();

            for (int i = 0; i < FieldInstances.Length; i++)
            {
                LDtkFieldParser.Parse(levelFields, FieldInstances[i]);
            }

            return levelFields;
        }
    }
}
