using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using LDtk.Exceptions;
using Microsoft.Xna.Framework;

namespace LDtk
{
    public partial class LDtkLevel
    {
        /// <summary>
        /// The parent world of this level
        /// </summary>
        public LDtkWorld Parent { get; set; }

        /// <summary>
        /// World Position of the level in pixels
        /// </summary>
        [JsonIgnore]
        public Point Position => new(WorldX, WorldY);

        /// <summary>
        /// World size of the level in pixels
        /// </summary>
        [JsonIgnore]
        public Point Size => new(PxWid, PxHei);

        /// <summary>
        /// Gets an intgrid with the <paramref name="identifier"/> in a <see cref="LDtkLevel"/>
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

                IntGridValueDefinition[] intgridValues = Parent.GetIntgridValueDefinitions(layer._Identifier);
                Dictionary<int, Color> colors = new Dictionary<int, Color>();
                for (int j = 0; j < intgridValues.Length; j++)
                {
                    colors.Add(intgridValues[j].Value, intgridValues[j].Color);
                }

                LDtkIntGrid intGrid = new()
                {
                    Values = new int[layer._CWid, layer._CHei],
                    WorldPosition = Position,
                    TileSize = layer._GridSize,
                    colors = colors,
                };

                if (layer.IntGridCsv != null)
                {
                    for (int j = 0; j < layer.IntGridCsv.Length; j++)
                    {
                        int y = j / layer._CWid;
                        int x = j - (y * layer._CWid);
                        intGrid.Values[x, y] = layer.IntGridCsv[j];
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
        /// <exception cref="EntityNotFoundException"></exception>
        public T GetEntity<T>() where T : new()
        {
            T[] entities = ParseEntities<T>(typeof(T).Name);

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
        /// Gets a collection of entities of type <typeparamref name="T"/> in the current level
        /// </summary>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"></exception>
        public T[] GetEntities<T>() where T : new()
        {
            return ParseEntities<T>(typeof(T).Name);
        }

        /// <summary>
        /// Gets a collection of entities of type <typeparamref name="T"/> with <paramref name="identifier"/> in the current level
        /// </summary>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"></exception>
        public T[] GetEntities<T>(string identifier) where T : new()
        {
            return ParseEntities<T>(identifier);
        }

        /// <summary>
        /// Gets the custom fields of the level
        /// </summary>
        /// <typeparam name="T">The custom level type generated from compiling the level</typeparam>
        /// <exception cref="FieldNotFoundException"></exception>
        /// <returns>Custom Fields for this level</returns>
        public T GetCustomFields<T>() where T : new()
        {
            T levelFields = new T();

            LDtkFieldParser.ParseCustomLevelFields(levelFields, FieldInstances);

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

                            LDtkFieldParser.ParseBaseEntityFields(entity, entityInstance, this);
                            LDtkFieldParser.ParseCustomEntityFields(entity, entityInstance.FieldInstances, this);

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
