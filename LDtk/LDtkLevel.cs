using System;
using System.Collections.Generic;
using System.Linq;
using LDtk.Exceptions;
using Microsoft.Xna.Framework;
using Vector2Int = Microsoft.Xna.Framework.Point;

namespace LDtk
{
    public partial class LDtkLevel
    {
        /// <summary>
        /// The parent world of this level
        /// </summary>
        public LDtkWorld parent;

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

                var intgridValues = parent.GetIntgridValueDefinitions(layer._Identifier);
                Dictionary<int, Color> colors = new Dictionary<int, Color>();
                for (int j = 0; j < intgridValues.Length; j++)
                {
                    colors.Add(intgridValues[j].Value, intgridValues[j].Color);
                }

                LDtkIntGrid intGrid = new LDtkIntGrid()
                {
                    grid = new int[layer._CWid, layer._CHei],
                    tileSize = layer._GridSize,
                    colors = colors,
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
        /// Gets the first entity it finds of type T in the current level
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T GetEntity<T>() where T : new()
        {
            var entities = ParseEntities<T>(typeof(T).Name);

            if (entities.Length == 1)
            {
                return entities[0];
            }
            else
            {
                throw new EntityNotFoundException($"Could not find one entity with identifier {typeof(T).Name}");
            }
        }

        /// <summary>
        /// Gets a collection of entities of type T in the current level
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T[] GetEntities<T>() where T : new()
        {
            return ParseEntities<T>(typeof(T).Name);
        }

        /// <summary>
        /// Gets a collection of entities of type T in the current level
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T[] GetEntities<T>(string identifier) where T : new()
        {
            return ParseEntities<T>(identifier);
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

        private T[] ParseEntities<T>(string identifier) where T : new()
        {
            List<T> parsedEntities = new List<T>();

            for (int i = 0; i < LayerInstances.Length; i++)
            {
                if (LayerInstances[i]._Type == LayerType.Entities)
                {
                    for (int entityIndex = 0; entityIndex < LayerInstances[i].EntityInstances.Length; entityIndex++)
                    {
                        if (LayerInstances[i].EntityInstances[entityIndex]._Identifier == identifier)
                        {
                            T entity = new T();
                            EntityInstance entityInstance = LayerInstances[i].EntityInstances[entityIndex];

                            LDtkFieldParser.ParseBaseField(entity, "Position", entityInstance.Px + Position);
                            // LDtkFieldParser.ParseBaseField(entity, "levelPosition", entityInstance.Px);

                            LDtkFieldParser.ParseBaseField(entity, "Pivot", entityInstance._Pivot);

                            if (entityInstance._Tile != null)
                            {
                                // LDtkFieldParser.ParseBaseField(entity, "texture", owner.GetTilesetTextureFromUid(entityInstance._Tile.TilesetUid));
                            }

                            LDtkFieldParser.ParseBaseField(entity, "Size", new Vector2(entityInstance.Width, entityInstance.Height));
#if DEBUG
                            // LDtkFieldParser.ParseBaseField(entity, "editorVisualColor", entityDefinition.Color);
#endif
                            if (entityInstance._Tile != null)
                            {
                                EntityInstanceTile tileDefinition = entityInstance._Tile;
                                Rectangle rect = tileDefinition.SrcRect;
                                LDtkFieldParser.ParseBaseField(entity, "tile", rect);
                            }

                            for (int fieldIndex = 0; fieldIndex < entityInstance.FieldInstances.Length; fieldIndex++)
                            {
                                LDtkFieldParser.Parse(entity, entityInstance.FieldInstances[fieldIndex]);
                            }

                            parsedEntities.Add(entity);
                        }
                    }

                    return parsedEntities.ToArray();
                }

            }

            return parsedEntities.ToArray();
        }
    }
}
