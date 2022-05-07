namespace LDtk;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

public partial class LDtkLevel
{
    /// <summary> The absolute filepath to the level </summary>
    [JsonIgnore] public string Path { get; set; }

    /// <summary> The parent world of this level </summary>
    [JsonIgnore] public LDtkFile Parent { get; set; }

    /// <summary> World Position of the level in pixels </summary>
    [JsonIgnore] public Point Position => new(WorldX, WorldY);

    /// <summary> World size of the level in pixels </summary>
    [JsonIgnore] public Point Size => new(PxWid, PxHei);

    /// <summary> Has the file been loaded if the level is external </summary>
    [JsonIgnore] public bool Loaded { get; internal set; }

    /// <summary> Used by json deserializer not for use by user! </summary>
    [Obsolete("Used by json deserializer not for use by user!", true)]
    public LDtkLevel() { }

    /// <summary> Loads the ldtk world file from disk directly </summary>
    /// <param name="filePath"> Path to the .ldtk file </param>
    public static LDtkLevel FromFile(string filePath)
    {
        LDtkLevel file = JsonSerializer.Deserialize<LDtkLevel>(File.ReadAllText(filePath), Constants.SerializeOptions);
        file.Path = System.IO.Path.GetFullPath(filePath);
        return file;
    }

    /// <summary> Loads the ldtk world file from disk directly </summary>
    /// <param name="filePath">Path to the .ldtk file excluding file extension</param>
    /// <param name="content">The optional content manager if you are using the content pipeline</param>
    public static LDtkLevel FromFile(string filePath, ContentManager content)
    {
        LDtkLevel file;
        file = content.Load<LDtkLevel>(filePath);
        file.Path = filePath;
        return file;
    }

    /// <summary> Gets an intgrid with the <paramref name="identifier"/> in a <see cref="LDtkLevel"/> </summary>
    public LDtkIntGrid GetIntGrid(string identifier)
    {
        foreach (LayerInstance layer in LayerInstances)
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

    /// <summary> Gets one entity of type T in the current level best used with 1 per level constraint </summary>
    public T GetEntity<T>() where T : new()
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

    /// <summary> Gets an array of entities of type <typeparamref name="T"/> in the current level </summary>
    public T[] GetEntities<T>() where T : new()
    {
        List<T> entities = new();

        foreach (LayerInstance layer in LayerInstances)
        {
            if (layer._Type == LayerType.Entities)
            {
                foreach (EntityInstance entityInstance in layer.EntityInstances)
                {
                    if (entityInstance._Identifier != typeof(T).Name)
                    {
                        continue;
                    }

                    T entity = new();

                    LDtkFieldParser.ParseBaseEntityFields(entity, entityInstance, this);
                    LDtkFieldParser.ParseCustomEntityFields(entity, entityInstance.FieldInstances, this);

                    entities.Add(entity);
                }
            }
        }

        return entities.ToArray();
    }

    /// <summary> Check if point is inside of a level </summary>
    /// <returns> True if point is inside level </returns>
    public bool Contains(Vector2 point)
    {
        return point.X >= Position.X && point.Y >= Position.Y && point.X <= Position.X + Size.X && point.Y <= Position.Y + Size.Y;
    }

    /// <summary> Check if point is inside of a level </summary>
    /// <returns> True if point is inside level </returns>
    public bool Contains(Point point)
    {
        return point.X >= Position.X && point.Y >= Position.Y && point.X <= Position.X + Size.X && point.Y <= Position.Y + Size.Y;
    }

    public ILDtkEntity[] GetAllEntities()
    {
        return Array.Empty<ILDtkEntity>();
    }
}
