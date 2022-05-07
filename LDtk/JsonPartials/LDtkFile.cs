namespace LDtk;

using System;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework.Content;

public partial class LDtkFile
{
    /// <summary> The absolute path to the ldtkFile </summary>
    public string Path { get; set; }

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
        file.Path = System.IO.Path.GetFullPath(filePath);
        return file;
    }

    /// <summary> Loads the ldtk world file from disk directly </summary>
    /// <param name="filePath">Path to the .ldtk file excluding file extension</param>
    /// <param name="content">The optional content manager if you are using the content pipeline</param>
    public static LDtkFile FromFile(string filePath, ContentManager content)
    {
        LDtkFile file;
        file = content.Load<LDtkFile>(filePath);
        file.Content = content;
        file.Path = filePath;
        return file;
    }

    /// <summary> Loads the ldtkl world file from disk directly or from the embeded one depending on if the file uses externalworlds </summary>
    public LDtkWorld LoadWorld(Guid iid)
    {
        foreach (LDtkWorld world in Worlds)
        {
            if (world.Iid == iid)
            {
                world.Path = Path;
                return world;
            }
        }
        return null;
    }
}
