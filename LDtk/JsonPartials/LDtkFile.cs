namespace LDtk;

using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.Xna.Framework.Content;

[DebuggerDisplay("ExternalFiles: {ExternalLevels} Path: {FilePath}")]
public partial class LDtkFile
{
    /// <inheritdoc cref="Definitions.Entities"/>
    public EntityDefinition[] Entities => Defs.Entities;
    /// <inheritdoc cref="Definitions.Enums"/>
    public EnumDefinition[] Enums => Defs.Enums;
    /// <inheritdoc cref="Definitions.ExternalEnums"/>
    public EnumDefinition[] ExternalEnums => Defs.ExternalEnums;
    /// <inheritdoc cref="Definitions.Layers"/>
    public LayerDefinition[] Layers => Defs.Layers;
    /// <inheritdoc cref="Definitions.Tilesets"/>
    public TilesetDefinition[] Tilesets => Defs.Tilesets;

    /// <summary> Initializes a new instance of the <see cref="LDtkFile"/> class. Used by json deserializer not for use by user. </summary>
    public LDtkFile() { }

    /// <summary> Gets or sets the absolute path to the ldtkFile. </summary>
    [JsonIgnore]
    public string FilePath { get; set; } = string.Empty;

    /// <summary> Gets or sets the content manager used if you are using the contentpipeline. </summary>
    [JsonIgnore]
    public ContentManager? Content { get; set; }

    /// <summary> Loads the ldtk world file from disk directly using json source generator. </summary>
    /// <param name="filePath"> Path to the .ldtk file. </param>
    /// <returns> Returns the file loaded from the path. </returns>
    /// <exception cref="LDtkException">Failed to Deserialize ldtk file from</exception>
    public static LDtkFile FromFile(string filePath)
    {
        LDtkFile? file = JsonSerializer.Deserialize(File.ReadAllText(filePath), Constants.JsonSourceGenerator.LDtkFile);
        if (file == null)
        {
            throw new LDtkException($"Failed to Deserialize ldtk file from {filePath}");
        }
        file.FilePath = Path.GetFullPath(filePath);
        return file;
    }

    /// <summary> Loads the ldtk world file from disk directly. </summary>
    /// <param name="filePath"> Path to the .ldtk file. </param>
    /// <returns> Returns the file loaded from the path. </returns>
    /// <exception cref="LDtkException"> Thrown when the file failed to deserialize. </exception>
    public static LDtkFile FromFileReflection(string filePath)
    {
        LDtkFile? file = JsonSerializer.Deserialize<LDtkFile>(File.ReadAllText(filePath), Constants.SerializeOptions);
        if (file == null)
        {
            throw new LDtkException($"Failed to Deserialize ldtk file from {filePath}");
        }
        file.FilePath = Path.GetFullPath(filePath);
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
        return file;
    }

    /// <summary> Loads the ldtkl world file from disk directly or from the embeded one depending on if the file uses externalworlds. </summary>
    /// <param name="iid" > The iid of the world to load. </param>
    /// <returns> Returns the world from the iid. </returns>
    /// <exception cref="LDtkException"> Throws if no world with the <paramref name="iid"/> is found. </exception>
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

        throw new LDtkException($"No World({iid}) found in this project");
    }

    /// <summary> Gets an entity from an <paramref name="reference"/> converted to <typeparamref name="T"/>. </summary>
    /// <typeparam name="T"> The type to convert the entity to. </typeparam>
    /// <param name="reference"> The entityRef to convert. </param>
    /// <returns> The converted entity. </returns>
    /// <exception cref="LDtkException"> Throws if no entity with the <paramref name="reference"/> is found. </exception>
    public T GetEntityRef<T>(EntityReference reference)
        where T : new()
    {
        foreach (LDtkWorld world in Worlds)
        {
            if (world.Iid != reference.WorldIid)
            {
                continue;
            }

            return world.GetEntityRef<T>(reference);
        }

        throw new LDtkException($"No EntityRef of type {typeof(T).Name} found in this level");
    }

    /// <summary> ToString </summary>
    public override string ToString()
    {
        return $"""
        LDtkFile: {JsonVersion}
            BgColor: {BgColor}
            Defs: {Defs}
            ExternalLevels: {ExternalLevels}
            Iid: {Iid}
            Levels: {Levels}
            Toc: {Toc}
            Worlds: {Worlds}
        """;
    }
}
