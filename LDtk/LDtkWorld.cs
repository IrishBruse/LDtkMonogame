using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using LDtk.Exceptions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace LDtk;

public partial class LDtkWorld
{
    /// <summary>
    /// Size of the world grid in pixels.
    /// </summary>
    [JsonIgnore]
    public Point WorldGridSize => new(WorldGridWidth, WorldGridHeight);

    /// <summary>
    /// The absolute folder that the world is located in.
    /// Used to absolute relative addresses of textures
    /// </summary>
    public string RootFolder { get; set; }

    /// <summary>
    /// Loads the ldtk world file from disk directly
    /// </summary>
    /// <param name="filePath">Path to the .ldtk file excludeing file extension</param>
    /// <returns>LDtkWorld</returns>
    public static LDtkWorld LoadWorld(string filePath)
    {
        LDtkWorld world = JsonSerializer.Deserialize<LDtkWorld>(File.ReadAllText(filePath), SerializeOptions);

        world.RootFolder = Path.GetFullPath(Path.GetDirectoryName(filePath));
        return world;
    }

    /// <summary>
    /// Loads the ldtk world file from disk directly
    /// </summary>
    /// <param name="filePath">Path to the .ldtk file excludeing file extension</param>
    /// <param name="content">The optional XNA content manager if you are using the content pipeline</param>
    /// <returns>LDtkWorld</returns>
    public static LDtkWorld LoadWorld(string filePath, ContentManager content)
    {
        if (content != null)
        {
            LDtkWorld world;
            world = content.Load<LDtkWorld>(filePath);
            world.RootFolder = Path.GetDirectoryName(filePath);

            return world;
        }

        throw new ContentLoadException($"Could not load ldtk world at {filePath}.");
    }

    /// <summary>
    /// Loads the ldtkl world file from disk directly or from the embeded one depending on if externalLevels is set
    /// </summary>
    /// <param name="identifier">The Level identifier</param>
    /// <returns><see cref="LDtkLevel"/></returns>
    /// <exception cref="LevelNotFoundException"></exception>
    public LDtkLevel LoadLevel(string identifier)
    {
        LDtkLevel level = null;

        for (int i = 0; i < Levels.Length; i++)
        {
            if (Levels[i].Identifier != identifier)
            {
                continue;
            }

            if (ExternalLevels == false)
            {
                level = Levels[i];
                break;
            }

            string path = Path.Join(RootFolder, Levels[i].ExternalRelPath);

            level = JsonSerializer.Deserialize<LDtkLevel>(File.ReadAllText(path), SerializeOptions);
            break;
        }

        if (level != null)
        {
            level.Parent = this;
            return level;
        }

        throw new LevelNotFoundException($"Could not find {identifier} Level in {this}.");
    }

    /// <summary>
    /// Gets a collection of entities of type <typeparamref name="T"/> in the current level
    /// </summary>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException"></exception>
    public T GetEntity<T>() where T : new()
    {
        List<T> entities = new List<T>();
        for (int i = 0; i < Levels.Length; i++)
        {
            entities.AddRange(Levels[i].GetEntities<T>());
        }

        if (entities.Count == 1)
        {
            return entities[0];
        }
        else
        {
            throw new EntityNotFoundException($"Could not find entity with identifier {typeof(T).Name}");
        }
    }

    /// <summary>
    /// Loads the ldtkl world file from disk directly or from the embeded one depending on if externalLevels is set
    /// </summary>
    /// <param name="uid">The Levels uid</param>
    /// <returns><see cref="LDtkLevel"/></returns>
    /// <exception cref="LevelNotFoundException"></exception>
    public LDtkLevel LoadLevel(int uid)
    {
        LDtkLevel level = null;

        for (int i = 0; i < Levels.Length; i++)
        {
            if (Levels[i].Uid != uid)
            {
                continue;
            }

            if (ExternalLevels == false)
            {
                level = Levels[i];
                break;
            }

            string path = Path.Join(RootFolder, Levels[i].ExternalRelPath);

            level = JsonSerializer.Deserialize<LDtkLevel>(File.ReadAllText(path), SerializeOptions);
            break;
        }

        if (level != null)
        {
            level.Parent = this;
            return level;
        }

        throw new LevelNotFoundException($"Could not find {uid} Level in {this}.");
    }

    /// <summary>
    /// Loads the ldtkl world file from disk directly or from the embeded one depending on if externalLevels is set
    /// </summary>
    /// <param name="uid">The Levels uid</param>
    /// <param name="content">Content Pipeline</param>
    /// <returns><see cref="LDtkLevel"/></returns>
    /// <exception cref="LevelNotFoundException"></exception>
    public LDtkLevel LoadLevel(int uid, ContentManager content)
    {
        LDtkLevel level = null;

        for (int i = 0; i < Levels.Length; i++)
        {
            if (Levels[i].Uid != uid)
            {
                continue;
            }

            if (ExternalLevels == false)
            {
                level = Levels[i];
                break;
            }

            string path = Path.Join(RootFolder, Levels[i].ExternalRelPath);
            level = content.Load<LDtkLevel>(path.Replace(".ldtkl", ""));
            break;
        }

        if (level != null)
        {
            level.Parent = this;
            return level;
        }

        throw new LevelNotFoundException($"Could not find {uid} Level in {this}.");
    }

    /// <summary>
    /// Gets the entity definition form a uid
    /// </summary>
    /// <param name="uid"></param>
    /// <returns>EntityDefinition</returns>
    /// <exception cref="NotImplementedException"></exception>
    public EntityDefinition GetEntityDefinitionFromUid(int uid)
    {
        for (int i = 0; i < Defs.Entities.Length; i++)
        {
            if (Defs.Entities[i].Uid == uid)
            {
                return Defs.Entities[i];
            }
        }

        return null;
    }

    /// <summary>
    /// Loads the ldtkl world file from disk directly or from the embeded one depending on if externalLevels is set
    /// </summary>
    /// <param name="identifier">The Level identifier</param>
    /// <param name="content">The optional XNA content manager if you are using the content pipeline</param>
    /// <returns>LDtkWorld</returns>
    public LDtkLevel LoadLevel(string identifier, ContentManager content)
    {
        LDtkLevel level = null;

        for (int i = 0; i < Levels.Length; i++)
        {
            if (Levels[i].Identifier != identifier)
            {
                continue;
            }

            if (ExternalLevels == false)
            {
                level = Levels[i];
                break;
            }

            string path = Path.Join(RootFolder, Levels[i].ExternalRelPath);
            level = content.Load<LDtkLevel>(path.Replace(".ldtkl", ""));
            break;
        }

        if (level != null)
        {
            level.Parent = this;
            return level;
        }

        throw new LevelNotFoundException($"Could not Content.Load `{identifier}` in {this}.");
    }

    /// <summary>
    /// Gets the intgrid value definitions
    /// </summary>
    /// <param name="identifier">leyer identifier</param>
    /// <returns></returns>
    /// <exception cref="FieldNotFoundException"></exception>
    public IntGridValueDefinition[] GetIntgridValueDefinitions(string identifier)
    {
        for (int i = 0; i < Defs.Layers.Length; i++)
        {
            if (Defs.Layers[i].Identifier != identifier)
            {
                continue;
            }

            if (Defs.Layers[i]._Type == LayerType.IntGrid)
            {
                return Defs.Layers[i].IntGridValues;
            }
        }

        throw new FieldNotFoundException();
    }
}
