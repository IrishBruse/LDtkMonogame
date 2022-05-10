namespace LDtk;

using System;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework.Content;

public partial class LDtkFile
{
    /// <summary> The absolute path to the ldtkFile </summary>
    public string FilePath { get; set; }

    /// <summary> The content manager used if you are using the contentpipeline </summary>
    public ContentManager Content { get; set; }

    /// <summary> Used by json deserializer not for use by user! </summary>
    [Obsolete("Used by json deserializer not for use by user!", true)]
    public LDtkFile() { }

    /// <summary> Loads the ldtk world file from disk directly </summary>
    /// <param name="filePath"> Path to the .ldtk file </param>
    public static LDtkFile FromFile(string filePath)
    {
        LDtkFile file = JsonSerializer.Deserialize<LDtkFile>(File.ReadAllText(filePath), Constants.SerializeOptions);
        file.FilePath = Path.GetFullPath(filePath);
        return file;
    }

    /// <summary> Loads the ldtk world file from disk directly </summary>
    /// <param name="filePath">Path to the .ldtk file excluding file extension</param>
    /// <param name="content">The optional content manager if you are using the content pipeline</param>
    public static LDtkFile FromFile(string filePath, ContentManager content)
    {
        LDtkFile file = content.Load<LDtkFile>(filePath);
        file.FilePath = filePath;
        file.Content = content;
        return file;
    }

    /// <summary> Loads the ldtkl world file from disk directly or from the embeded one depending on if the file uses externalworlds </summary>
    public LDtkWorld LoadWorld(Guid iid)
    {
        foreach (LDtkWorld world in Worlds)
        {
            if (world.Iid == iid)
            {
                world.FilePath = FilePath;
                world.Content = Content;
                return world;
            }
        }
        return null;
    }

    /// <summary> Gets an entity from an <paramref name="entityRef"/> converted to <typeparamref name="T"/> </summary>
    public T GetEntityRef<T>(EntityRef entityRef) where T : new()
    {
        foreach (LDtkWorld world in Worlds)
        {
            if (world.Iid != entityRef.WorldIid)
            {
                continue;
            }

            return world.GetEntityRef<T>(entityRef);
        }

        throw new LDtkException($"No EntityRef of type {typeof(T).Name} found in this level");
    }
}
