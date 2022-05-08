namespace LDtk;

using System;
using System.IO;
using System.Text.Json.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

public partial class LDtkWorld
{
    /// <summary> The Real LDtk Levels Json data Use indexer directly on the world eg world[0] instead as that will load external files if that setting is enabled. </summary>
    public LDtkLevel[] Levels { get; set; }

    /// <summary>
    /// This field will call LoadLevel if the file uses external levels.<br/>
    /// All levels from this world. The order of this array is only relevant in LinearHorizontal and linearVertical
    /// world layouts (see worldLayout value). Otherwise, you should refer to the worldX,worldY coordinates of each Level.
    /// </summary>
    public LDtkLevel this[int i]
    {
        get
        {
            if (Levels[i].ExternalRelPath != null)
            {
                LDtkLevel level = LDtkLevel.FromFile(Path.Join(Path.GetDirectoryName(FilePath), Levels[i].ExternalRelPath));
                level.ExternalRelPath = Levels[i].ExternalRelPath;
                level.Loaded = true;
                Levels[i] = level;
            }
            return Levels[i];
        }
    }

    /// <summary> The number of levels in the world </summary>
    public int Length => Levels.Length;

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

    /// <summary> Goes through all the loaded levels looking for the entities </summary>
    public static T[] GetEntities<T>() where T : new()
    {
        return default;
    }
}
