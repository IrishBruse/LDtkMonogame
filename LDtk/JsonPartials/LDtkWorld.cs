namespace LDtk;

using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

[DebuggerDisplay("WorldLayout: {WorldLayout} Size: {WorldGridSize} Path: {FilePath}")]
public partial class LDtkWorld
{
    /// <summary> Gets or sets the absolute filepath to the world. </summary>
    [JsonIgnore]
    public string FilePath { get; set; } = "";

    /// <summary> Gets size of the world grid in pixels. </summary>
    [JsonIgnore]
    public Point WorldGridSize => new(WorldGridWidth, WorldGridHeight);

    /// <summary> Initializes a new instance of the <see cref="LDtkWorld"/> class. Used by json deserializer not for use by user!. </summary>
    public LDtkWorld() { }

    /// <summary> Gets or sets the content manager used if you are using the contentpipeline. </summary>
    public ContentManager? Content { get; set; }

    /// <summary> Goes through all the loaded levels looking for the entity. </summary>
    public T GetEntity<T>()
        where T : new()
    {
        T entity = new();

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

    /// <summary> Get the level with an identifier. </summary>
    public LDtkLevel LoadLevel(string identifier)
    {
        foreach (LDtkLevel level in Levels)
        {
            if (level.Identifier != identifier)
            {
                continue;
            }

            return LoadLevel(level);
        }

        throw new LDtkException($"No level with identifier {identifier} found in this world");
    }

    /// <summary> Get the level with an iid. </summary>
    public LDtkLevel LoadLevel(Guid iid)
    {
        foreach (LDtkLevel level in Levels)
        {
            if (level.Iid != iid)
            {
                continue;
            }

            return LoadLevel(level);
        }

        throw new LDtkException($"No level with iid {iid} found in this world");
    }

    /// <summary> Get the level with an index. </summary>
    public LDtkLevel LoadLevel(int index)
    {
        if (index >= 0 && index <= Levels.Length)
        {
            return LoadLevel(Levels[index]);
        }

        throw new LDtkException($"No level with index {index} found in this world");
    }

    /// <summary> Get the level with an index. </summary>
    public LDtkLevel LoadLevel(LDtkLevel rawLevel)
    {
        if (rawLevel.ExternalRelPath == null)
        {
            rawLevel.FilePath = FilePath;
            return rawLevel;
        }
        else
        {
            LDtkLevel? level;

            if (Content != null)
            {
                level = LDtkLevel.FromFile(Path.Join(Path.GetDirectoryName(FilePath), rawLevel.ExternalRelPath), Content);
            }
            else
            {
                level = LDtkLevel.FromFile(Path.Join(Path.GetDirectoryName(FilePath), rawLevel.ExternalRelPath));
            }

            if (level == null)
            {
                throw new LDtkException($"No level with identifier {rawLevel.Identifier} found in this world");
            }

            level.ExternalRelPath = rawLevel.ExternalRelPath;
            level.WorldFilePath = FilePath;
            level.FilePath = level.ExternalRelPath;
            level.Loaded = true;

            return level;
        }
    }

    /// <summary> Gets an entity from an <paramref name="reference"/> converted to <typeparamref name="T"/>. </summary>
    public T GetEntityRef<T>(EntityReference reference)
        where T : new()
    {
        foreach (LDtkLevel level in Levels)
        {
            if (level.Iid != reference.LevelIid)
            {
                continue;
            }

            return level.GetEntityRef<T>(reference);
        }

        throw new LDtkException($"No EntityRef of type {typeof(T).Name} found in world {Identifier}");
    }
}
