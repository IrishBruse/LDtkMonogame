namespace LDtk;

using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

using Microsoft.Xna.Framework.Content;

[DebuggerDisplay("ExternalFiles: {ExternalLevels} Path: {FilePath}")]
public partial class LDtkFile
{
    /// <summary> Gets or sets the absolute path to the ldtkFile. </summary>
    public string FilePath { get; set; }

    /// <summary> Gets or sets the content manager used if you are using the contentpipeline. </summary>
    public ContentManager Content { get; set; }

    /// <summary> Initializes a new instance of the <see cref="LDtkFile"/> class. Used by json deserializer not for use by user. </summary>
    public LDtkFile() { }

    /// <summary> Loads the ldtk world file from disk directly using json source generator. </summary>
    /// <param name="filePath"> Path to the .ldtk file. </param>
    /// <returns> Returns the file loaded from the path. </returns>
    public static LDtkFile FromFile(string filePath)
    {
        LDtkFile file = JsonSerializer.Deserialize(File.ReadAllText(filePath), Constants.JsonSourceGenerator.LDtkFile);
        file.FilePath = Path.GetFullPath(filePath);
        ValidateFile(file);
        return file;
    }

    /// <summary> Loads the ldtk world file from disk directly. </summary>
    /// <param name="filePath"> Path to the .ldtk file. </param>
    /// <returns> Returns the file loaded from the path. </returns>
    public static LDtkFile FromFileReflection(string filePath)
    {
        LDtkFile file = JsonSerializer.Deserialize<LDtkFile>(File.ReadAllText(filePath), Constants.SerializeOptions);
        file.FilePath = Path.GetFullPath(filePath);
        ValidateFile(file);
        return file;
    }

    /// <summary> Loads the ldtk world file from disk directly. </summary>
    /// <param name="filePath">Path to the .ldtk file excluding file extension.</param>
    /// <param name="content">The optional content manager if you are using the content pipeline.</param>
    /// <returns> Returns the file loaded from the path. </returns>
    public static LDtkFile FromFile(string filePath, ContentManager content)
    {
        LDtkFile file = content.Load<LDtkFile>(filePath);
        file.FilePath = filePath;
        file.Content = content;
        ValidateFile(file);
        return file;
    }

    /// <summary> Loads the ldtkl world file from disk directly or from the embeded one depending on if the file uses externalworlds. </summary>
    /// <param name="iid" > The iid of the world to load. </param>
    /// <returns> Returns the world from the iid. </returns>
    public LDtkWorld LoadWorld(Guid iid)
    {
        foreach (LDtkWorld world in Worlds)
        {
            if (world.Iid != iid)
            {
                continue;
            }

            world.FilePath = FilePath;
            foreach (LDtkLevel level in world.Levels)
            {
                if (level.ExternalRelPath != null)
                {
                    level.FilePath = Path.Join(Path.GetDirectoryName(FilePath), level.ExternalRelPath);
                }

                level.WorldFilePath = world.FilePath;
            }

            world.Content = Content;
            return world;
        }

        return null;
    }

    /// <summary> Gets an entity from an <paramref name="entityRef"/> converted to <typeparamref name="T"/>. </summary>
    /// <typeparam name="T"> The type to convert the entity to. </typeparam>
    /// <param name="entityRef"> The entityRef to convert. </param>
    /// <returns> The converted entity. </returns>
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

    private static void ValidateFile(LDtkFile file)
    {
        if (Version.Parse(file.JsonVersion).Minor > Version.Parse(Constants.SupportedLDtkVersion).Minor)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            string value = $"LDtkMonogame supports {Constants.SupportedLDtkVersion} your file is on {file.JsonVersion} it\n";
            value += "is probably supported but new features may be missing please make an issue on github to remind me to update it :)";

            Console.WriteLine(value);
            Console.ResetColor();
        }
    }
}
