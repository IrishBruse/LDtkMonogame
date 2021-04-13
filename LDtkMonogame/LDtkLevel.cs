using System.Collections.Generic;
using LDtk.Exceptions;
using LDtk.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDtk
{
    /// <summary>
    /// Abstracted version of ldtk's level
    /// </summary>
    public class LDtkLevel
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
        /// Prerendered layer textures created from <see cref="LDtkWorld.GetLevel(string)"/>
        /// </summary>
        public RenderTarget2D[] Layers { get; internal set; }

        /// <summary>
        /// The neighbours uids of the level
        /// </summary>
        public long[] Neighbours { get; internal set; }

        internal LDtkWorld owner;
        internal EntityInstance[] entities;
        internal LDtkIntGrid[] intGrids;

        /// <summary>
        /// Gets the parsed entity from the ldtk json
        /// If fields are missing they will be logged to the console in debug mode only
        /// </summary>
        /// <param name="identifier">The name of the entity to parse this to</param>
        /// <returns>The entity cast to the <see cref="LDtkEntity"/> class</returns>
        public LDtkEntity GetEntity(string identifier)
        {
            return ParseEntities<LDtkEntity>(identifier, true)[0];
        }

        /// <summary>
        /// Gets the first found parsed entity from the ldtk json
        /// If fields are missing they will be logged to the console in debug mode only
        /// </summary>
        /// <param name="identifier">The name of the entity to parse this to</param>
        /// <typeparam name="T">The class/struct you will use to parse the ldtk entity</typeparam>
        /// <returns>The entity cast to the class</returns>
        public T GetEntity<T>(string identifier) where T : LDtkEntity, new()
        {
            return ParseEntities<T>(identifier, false)[0];
        }

        /// <summary>
        /// Gets the first found parsed entity from the ldtk json
        /// If fields are missing they will be logged to the console in debug mode only
        /// </summary>
        /// <typeparam name="T">The class/struct you will use to parse the ldtk entity</typeparam>
        /// <returns>The entity cast to the class</returns>
        public T GetEntity<T>() where T : new()
        {
            return ParseEntities<T>(typeof(T).Name, true)[0];
        }


        /// <summary>
        /// Gets the parsed entities from the ldtk json
        /// If fields are missing they will be logged to the console in debug mode only
        /// </summary>
        /// <param name="identifier">The name of the entity to parse this to</param>
        /// <returns>The entities cast to the class</returns>
        public LDtkEntity[] GetEntities(string identifier)
        {
            return ParseEntities<LDtkEntity>(identifier, false);
        }

        /// <summary>
        /// Gets the parsed entities from the ldtk json
        /// If fields are missing they will be logged to the console in debug mode only
        /// </summary>
        /// <param name="identifier">The name of the entity to parse this to</param>
        /// <typeparam name="T">The class/struct you will use to parse the ldtk entities</typeparam>
        /// <returns>The entities cast to the class</returns>
        public T[] GetEntities<T>(string identifier) where T : new()
        {
            return ParseEntities<T>(identifier, false);
        }

        /// <summary>
        /// Gets the parsed entities from the ldtk json
        /// If fields are missing they will be logged to the console in debug mode only
        /// </summary>
        /// <typeparam name="T">The class/struct you will use to parse the ldtk entities</typeparam>
        /// <returns>The entities cast to the class</returns>
        public T[] GetEntities<T>() where T : new()
        {
            return ParseEntities<T>(typeof(T).Name, false);
        }

        /// <summary>
        /// Gets an <see cref="LDtkIntGrid"/> from an identifier
        /// </summary>
        /// <param name="identifier">Identifier of an intgrid</param>
        public LDtkIntGrid GetIntGrid(string identifier)
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

        private T[] ParseEntities<T>(string identifier, bool breakOnMatch) where T : new()
        {
            List<T> parsedEntities = new List<T>();

            for (int entityIndex = 0; entityIndex < entities.Length; entityIndex++)
            {
                if (entities[entityIndex].Identifier == identifier)
                {
                    T entity = new T();
                    EntityInstance entityInstance = entities[entityIndex];

                    EntityDefinition entityDefinition = owner.GetEntityDefinitionFromUid(entityInstance.DefUid);

                    Parser.ParseBaseField(entity, "position", new Vector2(entityInstance.Px[0], entityInstance.Px[1]) + Position);
                    Parser.ParseBaseField(entity, "levelPosition", new Vector2(entityInstance.Px[0], entityInstance.Px[1]));

                    Parser.ParseBaseField(entity, "pivot", new Vector2((float)entityInstance.Pivot[0], (float)entityInstance.Pivot[1]));

                    if (entityInstance.Tile != null)
                    {
                        Parser.ParseBaseField(entity, "texture", owner.GetTilesetTextureFromUid(entityInstance.Tile.TilesetUid));
                    }

                    Parser.ParseBaseField(entity, "size", new Vector2(entityInstance.Width, entityInstance.Height));
#if DEBUG
                    Parser.ParseBaseField(entity, "editorVisualColor", Parser.ParseStringToColor(entityDefinition.Color, 128));
#endif
                    if (entityDefinition.TilesetId.HasValue)
                    {
                        EntityInstanceTile tileDefinition = entityInstance.Tile;
                        Rectangle rect = new Rectangle((int)tileDefinition.SrcRect[0], (int)tileDefinition.SrcRect[1], (int)tileDefinition.SrcRect[2], (int)tileDefinition.SrcRect[3]);
                        Parser.ParseBaseField(entity, "tile", rect);
                    }

                    for (int fieldIndex = 0; fieldIndex < entityInstance.FieldInstances.Length; fieldIndex++)
                    {
                        Parser.ParseField(entity, entityInstance.FieldInstances[fieldIndex]);
                    }

                    parsedEntities.Add(entity);

                    if (breakOnMatch)
                    {
                        break;
                    }
                }
            }

            return parsedEntities.ToArray();
        }
    }
}