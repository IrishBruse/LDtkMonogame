namespace LDtk;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

[DebuggerDisplay("Identifier: {Identifier} Pos: {Position} Size: {Size} Path: {FilePath}")]
public partial class LDtkLevel
{
    /// <summary> Gets or sets the absolute filepath to the level. </summary>
    [JsonIgnore]
    public string FilePath { get; set; }

    /// <summary> Gets or sets the absolute filepath to the world. </summary>
    [JsonIgnore]
    public string WorldFilePath { get; set; }

    /// <summary> Gets world Position of the level in pixels. </summary>
    [JsonIgnore]
    public Point Position => new(WorldX, WorldY);

    /// <summary> Gets world size of the level in pixels. </summary>
    [JsonIgnore]
    public Point Size => new(PxWid, PxHei);

    /// <summary> Gets a value indicating whether the file been loaded externaly. </summary>
    [JsonIgnore]
    public bool Loaded { get; internal set; }

    /// <summary> Initializes a new instance of the <see cref="LDtkLevel"/> class. Used by json deserializer not for use by user!. </summary>
#pragma warning disable CS8618
    public LDtkLevel()
    {
        layersLookup = new(LayerInstances?.Length ?? 0);
    }
#pragma warning restore

    /// <summary> Loads the ldtk world file from disk directly using json source generator. </summary>
    /// <param name="filePath"> Path to the .ldtk file. </param>
    public static LDtkLevel? FromFile(string filePath)
    {
        LDtkLevel? file = JsonSerializer.Deserialize(File.ReadAllText(filePath), Constants.JsonSourceGenerator.LDtkLevel);
        if (file != null)
        {
            file.FilePath = Path.GetFullPath(filePath);
        }
        return file;
    }

    /// <summary> Loads the ldtk world file from disk directly. </summary>
    /// <param name="filePath"> Path to the .ldtk file. </param>
    public static LDtkLevel? FromFileReflection(string filePath)
    {
        LDtkLevel? file = JsonSerializer.Deserialize<LDtkLevel>(File.ReadAllText(filePath), Constants.SerializeOptions);
        if (file != null)
        {
            file.FilePath = Path.GetFullPath(filePath);
        }
        return file;
    }

    /// <summary> Loads the ldtk world file from disk directly. </summary>
    /// <param name="filePath">Path to the .ldtk file excluding file extension.</param>
    /// <param name="content">The optional content manager if you are using the content pipeline.</param>
    public static LDtkLevel? FromFile(string filePath, ContentManager content)
    {
        LDtkLevel? file = content.Load<LDtkLevel>(filePath.Replace(".ldtkl", string.Empty));
        if (file != null)
        {
            file.FilePath = Path.GetFullPath(filePath);
        }
        return file;
    }

    /// <summary> Get the level with an iid. </summary>
    /// <param name="content"> Content pipeline </param>
    /// <exception cref="LDtkException">No level with identifier  found in this world</exception>
    public void Load(ContentManager? content = null)
    {
        if (ExternalRelPath != null)
        {
            LDtkLevel? level;

            if (content != null)
            {
                level = FromFile(FilePath, content);
            }
            else
            {
                level = FromFile(FilePath);
            }

            if (level == null)
            {
                throw new LDtkException("External level not found");
            }

            _BgColor = level._BgColor;
            _BgPos = level._BgPos;
            BgRelPath = level.BgRelPath;
            ExternalRelPath = level.ExternalRelPath;
            FieldInstances = level.FieldInstances;
            Identifier = level.Identifier;
            Iid = level.Iid;
            LayerInstances = level.LayerInstances;
            _Neighbours = level._Neighbours;
            PxHei = level.PxHei;
            PxWid = level.PxWid;
            Uid = level.Uid;
            WorldDepth = level.WorldDepth;
            WorldX = level.WorldX;
            WorldY = level.WorldY;

            Loaded = true;
        }
    }

    /// <summary> Gets an intgrid with the <paramref name="identifier"/> in a <see cref="LDtkLevel"/>. </summary>
    /// <param name="identifier"></param>
    /// <exception cref="LDtkException">Throws when identifier is not valid</exception>
    public LDtkIntGrid GetIntGrid(string identifier)
    {
        foreach (LayerInstance layer in LayerInstances ?? Array.Empty<LayerInstance>())
        {
            if (layer._Identifier != identifier)
            {
                continue;
            }

            if (layer._Type != LayerType.IntGrid)
            {
                continue;
            }

            if (layer.IntGridCsv == null)
            {
                continue;
            }

            return new()
            {
                Values = layer.IntGridCsv,
                WorldPosition = Position,
                GridSize = new(layer._CWid, layer._CHei),
                TileSize = layer._GridSize,
            };
        }

        throw new LDtkException($"{identifier} is not a valid intgrid identifier");
    }

    /// <summary> Gets the custom fields of the level. </summary>
    /// <typeparam name="T"></typeparam>
    public T GetCustomFields<T>() where T : new()
    {
        T levelFields = new();

        LDtkFieldParser.ParseCustomLevelFields(levelFields, FieldInstances);

        return levelFields;
    }

    /// <summary> Gets one entity of type T in the current level best used with 1 per level constraint. </summary>
    /// <typeparam name="T"></typeparam>
    /// <exception cref="LDtkException"></exception>
    public T GetEntity<T>()
        where T : new()
    {
        T[] entities = GetEntities<T>();

        if (entities.Length == 1)
        {
            return entities[0];
        }
        else if (entities.Length > 1)
        {
            throw new LDtkException($"More than one entity of type {typeof(T).Name} found in this level");
        }

        throw new LDtkException($"No entity of type {typeof(T).Name} found in this level");
    }

    /// <summary> Gets an entity from an <paramref name="reference"/> converted to <typeparamref name="T"/>. </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="reference"></param>
    /// <exception cref="LDtkException"></exception>
    public T GetEntityRef<T>(EntityReference reference)
        where T : new()
    {
        foreach (LayerInstance layer in LayerInstances ?? Array.Empty<LayerInstance>())
        {
            if (layer.Iid != reference.LayerIid)
            {
                continue;
            }

            foreach (EntityInstance entity in layer.EntityInstances)
            {
                if (entity.Iid != reference.EntityIid)
                {
                    continue;
                }

                return GetEntityFromInstance<T>(entity);
            }

            throw new LDtkException($"No EntityRef of type {typeof(T).Name} found in layer {layer._Identifier}");
        }

        throw new LDtkException($"No EntityRef of type {typeof(T).Name} found in level {Identifier}");
    }

    /// <summary> Gets an array of entities of type <typeparamref name="T"/> in the current level. </summary>
    /// <typeparam name="T"></typeparam>
    public T[] GetEntities<T>()
        where T : new()
    {
        List<T> entities = [];

        foreach (LayerInstance layer in LayerInstances ?? Array.Empty<LayerInstance>())
        {
            if (layer._Type == LayerType.Entities)
            {
                foreach (EntityInstance entityInstance in layer.EntityInstances)
                {
                    if (entityInstance._Identifier != typeof(T).Name)
                    {
                        continue;
                    }

                    T entity = GetEntityFromInstance<T>(entityInstance);
                    entities.Add(entity);
                }
            }
        }

        return entities.ToArray();
    }

    T GetEntityFromInstance<T>(EntityInstance entityInstance)
        where T : new()
    {
        T entity = new();
        LDtkFieldParser.ParseBaseEntityFields(entity, entityInstance, this);
        LDtkFieldParser.ParseCustomEntityFields(entity, entityInstance.FieldInstances, this);
        return entity;
    }

    /// <summary> Check if point is inside of a level. </summary>
    /// <param name="point"></param>
    /// <returns> True if point is inside level. </returns>
    public bool Contains(Vector2 point)
    {
        return point.X >= Position.X && point.Y >= Position.Y && point.X <= Position.X + Size.X && point.Y <= Position.Y + Size.Y;
    }

    /// <summary> Check if point is inside of a level. </summary>
    /// <param name="point"></param>
    /// <returns> True if point is inside level. </returns>
    public bool Contains(Point point)
    {
        return point.X >= Position.X && point.Y >= Position.Y && point.X <= Position.X + Size.X && point.Y <= Position.Y + Size.Y;
    }

    Dictionary<string, LayerInstance> layersLookup;

    /// <summary> Gets the layer instance with the given <paramref name="layerIdentifier"/>. </summary>
    /// <param name="layerIdentifier">Layer Identifier</param>
    /// <exception cref="LDtkException"></exception>
    public LayerInstance this[string layerIdentifier]
    {
        get
        {
            if (layersLookup.TryGetValue(layerIdentifier, out LayerInstance? layerLookup))
            {
                return layerLookup;
            }

            LayerInstance? layer = LayerInstances!.FirstOrDefault(x => x?._Identifier == layerIdentifier, null);
            if (layer != null)
            {
                layersLookup.Add(layerIdentifier, layer);
                return layer;
            }

            throw new LDtkException($"Layer {layerIdentifier} not found in level {Identifier}");
        }
    }
}
