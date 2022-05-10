namespace LDtk;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

public partial class LDtkWorld
{
    /// <summary> The Real LDtk Levels Json data Use indexer directly on the world eg world[0] instead as that will load external files if that setting is enabled. </summary>
    [Obsolete("The raw Levels json property. Use LoadLevel Instead or Levels."), JsonPropertyName("levels")]
    public LDtkLevel[] RawLevels { get; set; }

    /// <summary> The Levels iterator used in foreach will load external levels each time caching recommended </summary>
    [JsonIgnore]
    public IEnumerable<LDtkLevel> Levels
    {
        get
        {
            for (int i = 0; i < RawLevels.Length; i++)
            {
                yield return LoadLevel(RawLevels[i]);
            }
        }
    }

    /// <summary> The absolute filepath to the world </summary>
    [JsonIgnore] public string FilePath { get; set; }

    /// <summary> Size of the world grid in pixels. </summary>
    [JsonIgnore] public Point WorldGridSize => new(WorldGridWidth, WorldGridHeight);

    /// <summary> The absolute folder that the world is located in.Used to absolute relative addresses of textures </summary>
    [JsonIgnore] public string RootFolder { get; set; }

    /// <summary> Used by json deserializer not for use by user! </summary>
    [Obsolete("Used by json deserializer not for use by user!", true)]
    public LDtkWorld() { }

    /// <summary> The content manager used if you are using the contentpipeline </summary>
    public ContentManager Content { get; set; }

    /// <summary> Goes through all the loaded levels looking for the entity </summary>
    public T GetEntity<T>() where T : new()
    {
        T entity = default;

        foreach (LDtkLevel level in Levels)
        {
            T[] entities = level.GetEntities<T>();
            if (entities.Length == 1)
            {
                entity = entities[0];
            }
            else if (entities.Length > 1)
            {
                throw new LDtkException($"More than one entity of type {nameof(T)} found in this level");
            }
        }

        if (entity != null)
        {
            return entity;
        }

        throw new LDtkException($"No entity of type {nameof(T)} found in this level");
    }

    /// <summary> Get the level with an identifier </summary>
    public LDtkLevel LoadLevel(string identifier)
    {
        foreach (LDtkLevel level in RawLevels)
        {
            if (level.Identifier != identifier)
            {
                continue;
            }

            return LoadLevel(level);
        }

        throw new LDtkException($"No level with identifier {identifier} found in this world");
    }

    /// <summary> Get the level with an iid </summary>
    public LDtkLevel LoadLevel(Guid iid)
    {
        foreach (LDtkLevel level in RawLevels)
        {
            if (level.Iid != iid)
            {
                continue;
            }

            return LoadLevel(level);
        }

        throw new LDtkException($"No level with iid {iid} found in this world");
    }

    /// <summary> Get the level with an index </summary>
    public LDtkLevel LoadLevel(int index)
    {
        if (index >= 0 && index <= RawLevels.Length)
        {
            return LoadLevel(RawLevels[index]);
        }

        throw new LDtkException($"No level with index {index} found in this world");
    }

    LDtkLevel LoadLevel(LDtkLevel rawLevel)
    {
        LDtkLevel level = null;

        if (rawLevel.ExternalRelPath != null)
        {
            if (Content != null)
            {
                level = LDtkLevel.FromFile(Path.Join(Path.GetDirectoryName(FilePath), rawLevel.ExternalRelPath), Content);
            }
            else
            {
                level = LDtkLevel.FromFile(Path.Join(Path.GetDirectoryName(FilePath), rawLevel.ExternalRelPath));
            }

            level.ExternalRelPath = rawLevel.ExternalRelPath;
            level.Loaded = true;
        }

        return level;
    }

    /// <summary> Gets an entity from an <paramref name="entityRef"/> converted to <typeparamref name="T"/> </summary>
    public T GetEntityRef<T>(EntityRef entityRef) where T : new()
    {
        foreach (LDtkLevel level in RawLevels)
        {
            if (level.Iid != entityRef.LevelIid)
            {
                continue;
            }

            return level.GetEntityRef<T>(entityRef);
        }

        throw new LDtkException($"No EntityRef of type {typeof(T).Name} found in world {Identifier}");
    }
}
