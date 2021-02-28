using System;
using System.Collections.Generic;
using LDtk.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LDtk.Exceptions;

namespace LDtk
{
    /// <summary>
    /// Abstracted version of ldtk's level
    /// </summary>
    public class Level
    {
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
        public Vector2 Position { get; internal set; }

        /// <summary>
        /// World size in pixels of the level
        /// </summary>
        public Vector2 Size { get; internal set; }

        /// <summary>
        /// The clear color for the level
        /// </summary>
        public Color BgColor { get; internal set; }

        /// <summary>
        /// Prerendered layer textures created from <see cref="World.GetLevel(string)"/>
        /// </summary>
        public RenderTarget2D[] Layers { get; internal set; }

        /// <summary>
        /// The neighbours uids of the level
        /// </summary>
        public long[] Neighbours { get; internal set; }

        internal World owner;
        internal EntityInstance[] entities;
        internal IntGrid[] intGrids;

        /// <summary>
        /// Gets the parsed entity from the ldtk json
        /// If fields are missing they will be logged to the console in debug mode only
        /// </summary>
        /// <param name="identifier">The name of the entity to parse this to</param>
        /// <returns>The entity cast to the <see cref="Entity"/> class</returns>
        public Entity GetEntity(string identifier)
        {
            return ParseEntities<Entity>(identifier, true)[0];
        }

        /// <summary>
        /// Gets the parsed entities from the ldtk json
        /// If fields are missing they will be logged to the console in debug mode only
        /// </summary>
        /// <param name="identifier">The name of the entity to parse this to</param>
        /// <returns>The entities cast to the class</returns>
        public Entity[] GetEntities(string identifier)
        {
            return ParseEntities<Entity>(typeof(Entity).Name, false);
        }

        /// <summary>
        /// Gets the first found parsed entity from the ldtk json
        /// If fields are missing they will be logged to the console in debug mode only
        /// </summary>
        /// <param name="identifier">The name of the entity to parse this to</param>
        /// <typeparam name="T">The class/struct you will use to parse the ldtk entity</typeparam>
        /// <returns>The entity cast to the class</returns>
        public T GetEntity<T>(string identifier) where T : Entity, new()
        {
            return ParseEntities<T>(identifier, false)[0];
        }

        /// <summary>
        /// Gets the first found parsed entity from the ldtk json
        /// If fields are missing they will be logged to the console in debug mode only
        /// </summary>
        /// <typeparam name="T">The class/struct you will use to parse the ldtk entity</typeparam>
        /// <returns>The entity cast to the class</returns>
        public T GetEntity<T>() where T : Entity, new()
        {
            return ParseEntities<T>(typeof(T).Name, true)[0];
        }

        /// <summary>
        /// Gets the parsed entities from the ldtk json
        /// If fields are missing they will be logged to the console in debug mode only
        /// </summary>
        /// <param name="identifier">The name of the entity to parse this to</param>
        /// <typeparam name="T">The class/struct you will use to parse the ldtk entities</typeparam>
        /// <returns>The entities cast to the class</returns>
        public T[] GetEntities<T>(string identifier) where T : Entity, new()
        {
            return ParseEntities<T>(identifier, false);
        }

        /// <summary>
        /// Gets the parsed entities from the ldtk json
        /// If fields are missing they will be logged to the console in debug mode only
        /// </summary>
        /// <typeparam name="T">The class/struct you will use to parse the ldtk entities</typeparam>
        /// <returns>The entities cast to the class</returns>
        public T[] GetEntities<T>() where T : Entity, new()
        {
            return ParseEntities<T>(typeof(T).Name, false);
        }


        private T[] ParseEntities<T>(string identifier, bool breakOnMatch) where T : Entity, new()
        {
            List<T> parsedEntities = new List<T>();

            for (int entityIndex = 0; entityIndex < entities.Length; entityIndex++)
            {
                if (entities[entityIndex].Identifier == identifier)
                {
                    T entity = new T();

                    ParseBaseEntityFields<T>(entity, entities[entityIndex]);
                    for (int fieldIndex = 0; fieldIndex < entities[entityIndex].FieldInstances.Length; fieldIndex++)
                    {
                        Utility.ParseField(entity, entities[entityIndex].FieldInstances[fieldIndex]);
                    }

                    parsedEntities.Add(entity);

                    if (breakOnMatch == true)
                    {
                        break;
                    }
                }
            }

            return parsedEntities.ToArray();
        }
        private void ParseBaseEntityFields<T>(T entity, EntityInstance entityInstance)
        {
            var entityDefinition = owner.GetEntityDefinitionFromUid(entityInstance.DefUid);

            ParseBaseField<T>(entity, "Position", new Vector2(entityInstance.Px[0], entityInstance.Px[1]) + Position);
            ParseBaseField<T>(entity, "LevelPosition", new Vector2(entityInstance.Px[0], entityInstance.Px[1]));

            ParseBaseField<T>(entity, "Pivot", new Vector2((float)entityInstance.Pivot[0], (float)entityInstance.Pivot[1]));

            if (entityInstance.Tile != null)
            {
                ParseBaseField<T>(entity, "Texture", owner.GetTilesetTextureFromUid(entityInstance.Tile.TilesetUid));
            }

            ParseBaseField<T>(entity, "Size", new Vector2(entityInstance.Width, entityInstance.Height));
#if DEBUG
            ParseBaseField<T>(entity, "EditorVisualColor", Utility.ConvertStringToColor(entityDefinition.Color, 128));
#endif
            if (entityDefinition.TilesetId.HasValue)
            {
                var tileDefinition = entityInstance.Tile;
                Rectangle rect = new Rectangle((int)tileDefinition.SrcRect[0], (int)tileDefinition.SrcRect[1], (int)tileDefinition.SrcRect[2], (int)tileDefinition.SrcRect[3]);
                ParseBaseField<T>(entity, "Tile", rect);
            }
        }

        void ParseBaseField<T>(T entity, string field, object value)
        {
            // WorldPosition
            var variable = typeof(T).GetProperty(field);
            if (variable != null)
            {
                variable.SetValue(entity, value);
            }
#if DEBUG
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: Field \"{field}\" not found add it to {typeof(T).FullName}");
                Console.ResetColor();
            }
#endif
        }

        /// <summary>
        /// Gets an <see cref="IntGrid"/> from an identifier
        /// </summary>
        /// <param name="identifier">Identifier of an intgrid</param>
        public IntGrid GetIntGrid(string identifier)
        {
            for (int i = 0; i < intGrids.Length; i++)
            {
                if (intGrids[i].identifier == identifier)
                {
                    return intGrids[i];
                }
            }

            throw new IntGridNotFoundException(identifier);
        }
    }
}