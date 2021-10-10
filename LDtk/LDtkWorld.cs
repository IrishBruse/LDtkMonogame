// 0.9.3

#pragma warning disable 1591, 1570, IDE1006
namespace LDtk
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using Color = Microsoft.Xna.Framework.Color;
    using Rect = Microsoft.Xna.Framework.Rectangle;
    using Vector2 = Microsoft.Xna.Framework.Vector2;
    using Vector2Int = Microsoft.Xna.Framework.Point;

    /// <summary>
    /// The main class that contains all the project related info
    /// </summary>
    public partial class LDtkWorld
    {
        /// <summary>
        /// Project background color
        /// </summary>
        [JsonPropertyName("bgColor")]
        public Color BgColor { get; set; }

        /// <summary>
        /// A structure containing all the definitions of this project
        /// </summary>
        [JsonPropertyName("defs")]
        public Definitions Defs { get; set; }

        /// <summary>
        /// If TRUE, one file will be saved for the project (incl. all its definitions) and one file
        /// in a sub-folder for each level.
        /// </summary>
        [JsonPropertyName("externalLevels")]
        public bool ExternalLevels { get; set; }

        /// <summary>
        /// File format version
        /// </summary>
        [JsonPropertyName("jsonVersion")]
        public string JsonVersion { get; set; }

        /// <summary>
        /// All levels. The order of this array is only relevant in `LinearHorizontal` and
        /// `linearVertical` world layouts (see `worldLayout` value). Otherwise, you should refer to
        /// the `worldX`,`worldY` coordinates of each Level.
        /// </summary>
        [JsonPropertyName("levels")]
        public LDtkLevel[] Levels { get; set; }

        /// <summary>
        /// Height of the world grid in pixels.
        /// </summary>
        [JsonPropertyName("worldGridHeight")]
        public long WorldGridHeight { get; set; }

        /// <summary>
        /// Width of the world grid in pixels.
        /// </summary>
        [JsonPropertyName("worldGridWidth")]
        public long WorldGridWidth { get; set; }

        /// <summary>
        /// An enum that describes how levels are organized in this project (ie. linearly or in a 2D
        /// space). Possible values: `Free`, `GridVania`, `LinearHorizontal`, `LinearVertical`
        /// </summary>
        [JsonPropertyName("worldLayout")]
        public WorldLayout WorldLayout { get; set; }


        public static readonly JsonSerializerOptions SerializeOptions = new JsonSerializerOptions
        {
            Converters ={
                new JsonStringEnumConverter(),
                new ColorConverter(),
                new RectConverter(),
                new Vector2Converter(),
                new Vector2IntConverter(),
            }
        };
    }

    /// <summary>
    /// A structure containing all the definitions of this project
    ///
    /// If you're writing your own LDtk importer, you should probably just ignore *most* stuff in
    /// the `defs` section, as it contains data that are mostly important to the editor. To keep
    /// you away from the `defs` section and avoid some unnecessary JSON parsing, important data
    /// from definitions is often duplicated in fields prefixed with a double underscore (eg.
    /// `__identifier` or `__type`).  The 2 only definition types you might need here are
    /// **Tilesets** and **Enums**.
    /// </summary>
    public partial class Definitions
    {
        /// <summary>
        /// All entities definitions, including their custom fields
        /// </summary>
        [JsonPropertyName("entities")]
        public EntityDefinition[] Entities { get; set; }

        /// <summary>
        /// All internal enums
        /// </summary>
        [JsonPropertyName("enums")]
        public EnumDefinition[] Enums { get; set; }

        /// <summary>
        /// Note: external enums are exactly the same as `enums`, except they have a `relPath` to
        /// point to an external source file.
        /// </summary>
        [JsonPropertyName("externalEnums")]
        public EnumDefinition[] ExternalEnums { get; set; }

        /// <summary>
        /// All layer definitions
        /// </summary>
        [JsonPropertyName("layers")]
        public LayerDefinition[] Layers { get; set; }

        /// <summary>
        /// All tilesets
        /// </summary>
        [JsonPropertyName("tilesets")]
        public TilesetDefinition[] Tilesets { get; set; }
    }

    public partial class EntityDefinition
    {
        /// <summary>
        /// Base entity color
        /// </summary>
        [JsonPropertyName("color")]
        public Color Color { get; set; }

        /// <summary>
        /// Pixel height
        /// </summary>
        [JsonPropertyName("height")]
        public long Height { get; set; }

        /// <summary>
        /// Unique String identifier
        /// </summary>
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// Pivot X coordinate (from 0 to 1.0)
        /// </summary>
        [JsonPropertyName("pivotX")]
        public double PivotX { get; set; }

        /// <summary>
        /// Pivot Y coordinate (from 0 to 1.0)
        /// </summary>
        [JsonPropertyName("pivotY")]
        public double PivotY { get; set; }

        /// <summary>
        /// Tile ID used for optional tile display
        /// </summary>
        [JsonPropertyName("tileId")]
        public long? TileId { get; set; }

        /// <summary>
        /// Tileset ID used for optional tile display
        /// </summary>
        [JsonPropertyName("tilesetId")]
        public long? TilesetId { get; set; }

        /// <summary>
        /// Unique Int identifier
        /// </summary>
        [JsonPropertyName("uid")]
        public long Uid { get; set; }

        /// <summary>
        /// Pixel width
        /// </summary>
        [JsonPropertyName("width")]
        public long Width { get; set; }
    }

    public partial class EnumDefinition
    {
        /// <summary>
        /// Relative path to the external file providing this Enum
        /// </summary>
        [JsonPropertyName("externalRelPath")]
        public string ExternalRelPath { get; set; }

        /// <summary>
        /// Tileset UID if provided
        /// </summary>
        [JsonPropertyName("iconTilesetUid")]
        public long? IconTilesetUid { get; set; }

        /// <summary>
        /// Unique String identifier
        /// </summary>
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// Unique Int identifier
        /// </summary>
        [JsonPropertyName("uid")]
        public long Uid { get; set; }

        /// <summary>
        /// All possible enum values, with their optional Tile infos.
        /// </summary>
        [JsonPropertyName("values")]
        public EnumValueDefinition[] Values { get; set; }
    }

    public partial class EnumValueDefinition
    {
        /// <summary>
        /// Optional color
        /// </summary>
        [JsonPropertyName("color")]
        public long Color { get; set; }

        /// <summary>
        /// Enum value
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The optional ID of the tile
        /// </summary>
        [JsonPropertyName("tileId")]
        public long? TileId { get; set; }

        /// <summary>
        /// An array of 4 Int values that refers to the tile in the tileset image: `[ x, y, width,
        /// height ]`
        /// </summary>
        [JsonPropertyName("__tileSrcRect")]
        public Rect __TileSrcRect { get; set; }
    }

    public partial class LayerDefinition
    {
        [JsonPropertyName("autoSourceLayerDefUid")]
        public long? AutoSourceLayerDefUid { get; set; }

        /// <summary>
        /// Reference to the Tileset UID being used by this auto-layer rules. WARNING: some layer
        /// *instances* might use a different tileset. So most of the time, you should probably use
        /// the `__tilesetDefUid` value from layer instances.
        /// </summary>
        [JsonPropertyName("autoTilesetDefUid")]
        public long? AutoTilesetDefUid { get; set; }

        /// <summary>
        /// Opacity of the layer (0 to 1.0)
        /// </summary>
        [JsonPropertyName("displayOpacity")]
        public double DisplayOpacity { get; set; }

        /// <summary>
        /// Width and height of the grid in pixels
        /// </summary>
        [JsonPropertyName("gridSize")]
        public long GridSize { get; set; }

        /// <summary>
        /// Unique String identifier
        /// </summary>
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// An array that defines extra optional info for each IntGrid value. The array is sorted
        /// using value (ascending).
        /// </summary>
        [JsonPropertyName("intGridValues")]
        public IntGridValueDefinition[] IntGridValues { get; set; }

        /// <summary>
        /// X offset of the layer, in pixels (IMPORTANT: this should be added to the `LayerInstance`
        /// optional offset)
        /// </summary>
        [JsonPropertyName("pxOffsetX")]
        public long PxOffsetX { get; set; }

        /// <summary>
        /// Y offset of the layer, in pixels (IMPORTANT: this should be added to the `LayerInstance`
        /// optional offset)
        /// </summary>
        [JsonPropertyName("pxOffsetY")]
        public long PxOffsetY { get; set; }

        /// <summary>
        /// Reference to the Tileset UID being used by this Tile layer. WARNING: some layer
        /// *instances* might use a different tileset. So most of the time, you should probably use
        /// the `__tilesetDefUid` value from layer instances.
        /// </summary>
        [JsonPropertyName("tilesetDefUid")]
        public long? TilesetDefUid { get; set; }

        /// <summary>
        /// Type of the layer (*IntGrid, Entities, Tiles or AutoLayer*)
        /// </summary>
        [JsonPropertyName("__type")]
        public string __Type { get; set; }

        /// <summary>
        /// Unique Int identifier
        /// </summary>
        [JsonPropertyName("uid")]
        public long Uid { get; set; }
    }

    /// <summary>
    /// IntGrid value definition
    /// </summary>
    public partial class IntGridValueDefinition
    {
        [JsonPropertyName("color")]
        public Color Color { get; set; }

        /// <summary>
        /// Unique String identifier
        /// </summary>
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// The IntGrid value itself
        /// </summary>
        [JsonPropertyName("value")]
        public long Value { get; set; }
    }

    /// <summary>
    /// The `Tileset` definition is the most important part among project definitions. It
    /// contains some extra informations about each integrated tileset. If you only had to parse
    /// one definition section, that would be the one.
    /// </summary>
    public partial class TilesetDefinition
    {
        /// <summary>
        /// Grid-based height
        /// </summary>
        [JsonPropertyName("__cHei")]
        public long __CHei { get; set; }

        /// <summary>
        /// Grid-based width
        /// </summary>
        [JsonPropertyName("__cWid")]
        public long __CWid { get; set; }

        /// <summary>
        /// An array of custom tile metadata
        /// </summary>
        [JsonPropertyName("customData")]
        public Dictionary<string, object>[] CustomData { get; set; }

        /// <summary>
        /// Tileset tags using Enum values specified by `tagsSourceEnumId`. This array contains 1
        /// element per Enum value, which contains an array of all Tile IDs that are tagged with it.
        /// </summary>
        [JsonPropertyName("enumTags")]
        public Dictionary<string, object>[] EnumTags { get; set; }

        /// <summary>
        /// Unique String identifier
        /// </summary>
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// Distance in pixels from image borders
        /// </summary>
        [JsonPropertyName("padding")]
        public long Padding { get; set; }

        /// <summary>
        /// Image height in pixels
        /// </summary>
        [JsonPropertyName("pxHei")]
        public long PxHei { get; set; }

        /// <summary>
        /// Image width in pixels
        /// </summary>
        [JsonPropertyName("pxWid")]
        public long PxWid { get; set; }

        /// <summary>
        /// Path to the source file, relative to the current project JSON file
        /// </summary>
        [JsonPropertyName("relPath")]
        public string RelPath { get; set; }

        /// <summary>
        /// Space in pixels between all tiles
        /// </summary>
        [JsonPropertyName("spacing")]
        public long Spacing { get; set; }

        /// <summary>
        /// Optional Enum definition UID used for this tileset meta-data
        /// </summary>
        [JsonPropertyName("tagsSourceEnumUid")]
        public long? TagsSourceEnumUid { get; set; }

        [JsonPropertyName("tileGridSize")]
        public long TileGridSize { get; set; }

        /// <summary>
        /// Unique Intidentifier
        /// </summary>
        [JsonPropertyName("uid")]
        public long Uid { get; set; }
    }

    /// <summary>
    /// This section contains all the level data. It can be found in 2 distinct forms, depending
    /// on Project current settings:  - If "*Separate level files*" is **disabled** (default):
    /// full level data is *embedded* inside the main Project JSON file, - If "*Separate level
    /// files*" is **enabled**: level data is stored in *separate* standalone `.ldtkl` files (one
    /// per level). In this case, the main Project JSON file will still contain most level data,
    /// except heavy sections, like the `layerInstances` array (which will be null). The
    /// `externalRelPath` string points to the `ldtkl` file.  A `ldtkl` file is just a JSON file
    /// containing exactly what is described below.
    /// </summary>
    public partial class LDtkLevel
    {
        /// <summary>
        /// Background color of the level (same as `bgColor`, except the default value is
        /// automatically used here if its value is `null`)
        /// </summary>
        [JsonPropertyName("__bgColor")]
        public Color __BgColor { get; set; }

        /// <summary>
        /// The *optional* relative path to the level background image.
        /// </summary>
        [JsonPropertyName("bgRelPath")]
        public string BgRelPath { get; set; }

        /// <summary>
        /// This value is not null if the project option "*Save levels separately*" is enabled. In
        /// this case, this **relative** path points to the level Json file.
        /// </summary>
        [JsonPropertyName("externalRelPath")]
        public string ExternalRelPath { get; set; }

        /// <summary>
        /// An array containing this level custom field values.
        /// </summary>
        [JsonPropertyName("fieldInstances")]
        public FieldInstance[] FieldInstances { get; set; }

        /// <summary>
        /// Unique String identifier
        /// </summary>
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// An array containing all Layer instances. **IMPORTANT**: if the project option "*Save
        /// levels separately*" is enabled, this field will be `null`.<br/>  This array is **sorted
        /// in display order**: the 1st layer is the top-most and the last is behind.
        /// </summary>
        [JsonPropertyName("layerInstances")]
        public LayerInstance[] LayerInstances { get; set; }

        /// <summary>
        /// An enum defining the way the background image (if any) is positioned on the level. See
        /// `__bgPos` for resulting position info. Possible values: &lt;`null`&gt;, `Unscaled`,
        /// `Contain`, `Cover`, `CoverDirty`
        /// </summary>
        [JsonPropertyName("bgPos")]
        public BgPos? LevelBgPos { get; set; }

        /// <summary>
        /// An array listing all other levels touching this one on the world map. In "linear" world
        /// layouts, this array is populated with previous/next levels in array, and `dir` depends on
        /// the linear horizontal/vertical layout.
        /// </summary>
        [JsonPropertyName("__neighbours")]
        public NeighbourLevel[] __Neighbours { get; set; }

        /// <summary>
        /// Height of the level in pixels
        /// </summary>
        [JsonPropertyName("pxHei")]
        public long PxHei { get; set; }

        /// <summary>
        /// Width of the level in pixels
        /// </summary>
        [JsonPropertyName("pxWid")]
        public long PxWid { get; set; }

        /// <summary>
        /// Unique Int identifier
        /// </summary>
        [JsonPropertyName("uid")]
        public long Uid { get; set; }

        /// <summary>
        /// World X coordinate in pixels
        /// </summary>
        [JsonPropertyName("worldX")]
        public long WorldX { get; set; }

        /// <summary>
        /// World Y coordinate in pixels
        /// </summary>
        [JsonPropertyName("worldY")]
        public long WorldY { get; set; }
    }

    public partial class FieldInstance
    {
        /// <summary>
        /// Reference of the **Field definition** UID
        /// </summary>
        [JsonPropertyName("defUid")]
        public long DefUid { get; set; }

        /// <summary>
        /// Field definition identifier
        /// </summary>
        [JsonPropertyName("__identifier")]
        public string __Identifier { get; set; }

        /// <summary>
        /// Type of the field, such as `Int`, `Float`, `Enum(my_enum_name)`, `Bool`, etc.
        /// </summary>
        [JsonPropertyName("__type")]
        public string __Type { get; set; }

        /// <summary>
        /// Actual value of the field instance. The value type may vary, depending on `__type`
        /// (Integer, Boolean, String etc.)<br/>  It can also be an `Array` of those same types.
        /// </summary>
        [JsonPropertyName("__value")]
        public object __Value { get; set; }
    }

    public partial class LayerInstance
    {
        /// <summary>
        /// An array containing all tiles generated by Auto-layer rules. The array is already sorted
        /// in display order (ie. 1st tile is beneath 2nd, which is beneath 3rd etc.).<br/><br/>
        /// Note: if multiple tiles are stacked in the same cell as the result of different rules,
        /// all tiles behind opaque ones will be discarded.
        /// </summary>
        [JsonPropertyName("autoLayerTiles")]
        public TileInstance[] AutoLayerTiles { get; set; }

        /// <summary>
        /// Grid-based height
        /// </summary>
        [JsonPropertyName("__cHei")]
        public long __CHei { get; set; }

        /// <summary>
        /// Grid-based width
        /// </summary>
        [JsonPropertyName("__cWid")]
        public long __CWid { get; set; }

        [JsonPropertyName("entityInstances")]
        public EntityInstance[] EntityInstances { get; set; }

        /// <summary>
        /// Grid size
        /// </summary>
        [JsonPropertyName("__gridSize")]
        public long __GridSize { get; set; }

        [JsonPropertyName("gridTiles")]
        public TileInstance[] GridTiles { get; set; }

        /// <summary>
        /// Layer definition identifier
        /// </summary>
        [JsonPropertyName("__identifier")]
        public string __Identifier { get; set; }

        /// <summary>
        /// **WARNING**: this deprecated value will be *removed* completely on version 0.10.0+
        /// Replaced by: `intGridCsv`
        /// </summary>
        [JsonPropertyName("intGrid")]
        public IntGridValueInstance[] IntGrid { get; set; }

        /// <summary>
        /// A list of all values in the IntGrid layer, stored from left to right, and top to bottom
        /// (ie. first row from left to right, followed by second row, etc). `0` means "empty cell"
        /// and IntGrid values start at 1. This array size is `__cWid` x `__cHei` cells.
        /// </summary>
        [JsonPropertyName("intGridCsv")]
        public Vector2Int IntGridCsv { get; set; }

        /// <summary>
        /// Reference the Layer definition UID
        /// </summary>
        [JsonPropertyName("layerDefUid")]
        public long LayerDefUid { get; set; }

        /// <summary>
        /// Reference to the UID of the level containing this layer instance
        /// </summary>
        [JsonPropertyName("levelId")]
        public long LevelId { get; set; }

        /// <summary>
        /// Layer opacity as Float [0-1]
        /// </summary>
        [JsonPropertyName("__opacity")]
        public double __Opacity { get; set; }

        /// <summary>
        /// This layer can use another tileset by overriding the tileset UID here.
        /// </summary>
        [JsonPropertyName("overrideTilesetUid")]
        public long? OverrideTilesetUid { get; set; }

        /// <summary>
        /// X offset in pixels to render this layer, usually 0 (IMPORTANT: this should be added to
        /// the `LayerDef` optional offset, see `__pxTotalOffsetX`)
        /// </summary>
        [JsonPropertyName("pxOffsetX")]
        public long PxOffsetX { get; set; }

        /// <summary>
        /// Y offset in pixels to render this layer, usually 0 (IMPORTANT: this should be added to
        /// the `LayerDef` optional offset, see `__pxTotalOffsetY`)
        /// </summary>
        [JsonPropertyName("pxOffsetY")]
        public long PxOffsetY { get; set; }

        /// <summary>
        /// Total layer X pixel offset, including both instance and definition offsets.
        /// </summary>
        [JsonPropertyName("__pxTotalOffsetX")]
        public long __PxTotalOffsetX { get; set; }

        /// <summary>
        /// Total layer Y pixel offset, including both instance and definition offsets.
        /// </summary>
        [JsonPropertyName("__pxTotalOffsetY")]
        public long __PxTotalOffsetY { get; set; }

        /// <summary>
        /// The definition UID of corresponding Tileset, if any.
        /// </summary>
        [JsonPropertyName("__tilesetDefUid")]
        public long? __TilesetDefUid { get; set; }

        /// <summary>
        /// The relative path to corresponding Tileset, if any.
        /// </summary>
        [JsonPropertyName("__tilesetRelPath")]
        public string __TilesetRelPath { get; set; }

        /// <summary>
        /// Layer type (possible values: IntGrid, Entities, Tiles or AutoLayer)
        /// </summary>
        [JsonPropertyName("__type")]
        public string __Type { get; set; }

        /// <summary>
        /// Layer instance visibility
        /// </summary>
        [JsonPropertyName("visible")]
        public bool Visible { get; set; }
    }

    /// <summary>
    /// This structure represents a single tile from a given Tileset.
    /// </summary>
    public partial class TileInstance
    {
        /// <summary>
        /// "Flip bits", a 2-bits integer to represent the mirror transformations of the tile.<br/>
        /// - Bit 0 = X flip<br/>   - Bit 1 = Y flip<br/>   Examples: f=0 (no flip), f=1 (X flip
        /// only), f=2 (Y flip only), f=3 (both flips)
        /// </summary>
        [JsonPropertyName("f")]
        public long F { get; set; }

        /// <summary>
        /// Pixel coordinates of the tile in the **layer** (`[x,y]` format). Don't forget optional
        /// layer offsets, if they exist!
        /// </summary>
        [JsonPropertyName("px")]
        public Vector2Int Px { get; set; }

        /// <summary>
        /// Pixel coordinates of the tile in the **tileset** (`[x,y]` format)
        /// </summary>
        [JsonPropertyName("src")]
        public Vector2Int Src { get; set; }

        /// <summary>
        /// The *Tile ID* in the corresponding tileset.
        /// </summary>
        [JsonPropertyName("t")]
        public long T { get; set; }
    }

    public partial class EntityInstance
    {
        /// <summary>
        /// Reference of the **Entity definition** UID
        /// </summary>
        [JsonPropertyName("defUid")]
        public long DefUid { get; set; }

        /// <summary>
        /// An array of all custom fields and their values.
        /// </summary>
        [JsonPropertyName("fieldInstances")]
        public FieldInstance[] FieldInstances { get; set; }

        /// <summary>
        /// Grid-based coordinates (`[x,y]` format)
        /// </summary>
        [JsonPropertyName("__grid")]
        public Vector2Int __Grid { get; set; }

        /// <summary>
        /// Entity height in pixels. For non-resizable entities, it will be the same as Entity
        /// definition.
        /// </summary>
        [JsonPropertyName("height")]
        public long Height { get; set; }

        /// <summary>
        /// Entity definition identifier
        /// </summary>
        [JsonPropertyName("__identifier")]
        public string __Identifier { get; set; }

        /// <summary>
        /// Pivot coordinates  (`[x,y]` format, values are from 0 to 1) of the Entity
        /// </summary>
        [JsonPropertyName("__pivot")]
        public Vector2 __Pivot { get; set; }

        /// <summary>
        /// Pixel coordinates (`[x,y]` format) in current level coordinate space. Don't forget
        /// optional layer offsets, if they exist!
        /// </summary>
        [JsonPropertyName("px")]
        public Vector2Int Px { get; set; }

        /// <summary>
        /// Optional Tile used to display this entity (it could either be the default Entity tile, or
        /// some tile provided by a field value, like an Enum).
        /// </summary>
        [JsonPropertyName("__tile")]
        public EntityInstanceTile __Tile { get; set; }

        /// <summary>
        /// Entity width in pixels. For non-resizable entities, it will be the same as Entity
        /// definition.
        /// </summary>
        [JsonPropertyName("width")]
        public long Width { get; set; }
    }

    /// <summary>
    /// Tile data in an Entity instance
    /// </summary>
    public partial class EntityInstanceTile
    {
        /// <summary>
        /// An array of 4 Int values that refers to the tile in the tileset image: `[ x, y, width,
        /// height ]`
        /// </summary>
        [JsonPropertyName("srcRect")]
        public Rect SrcRect { get; set; }

        /// <summary>
        /// Tileset ID
        /// </summary>
        [JsonPropertyName("tilesetUid")]
        public long TilesetUid { get; set; }
    }

    /// <summary>
    /// IntGrid value instance
    /// </summary>
    public partial class IntGridValueInstance
    {
        /// <summary>
        /// Coordinate ID in the layer grid
        /// </summary>
        [JsonPropertyName("coordId")]
        public long CoordId { get; set; }

        /// <summary>
        /// IntGrid value
        /// </summary>
        [JsonPropertyName("v")]
        public long V { get; set; }
    }

    /// <summary>
    /// Nearby level info
    /// </summary>
    public partial class NeighbourLevel
    {
        /// <summary>
        /// A single lowercase character tipping on the level location (`n`orth, `s`outh, `w`est,
        /// `e`ast).
        /// </summary>
        [JsonPropertyName("dir")]
        public string Dir { get; set; }

        [JsonPropertyName("levelUid")]
        public long LevelUid { get; set; }
    }

    class ColorConverter : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string str = reader.GetString();
            if (str.StartsWith('#'))
            {
                uint col = Convert.ToUInt32(str[1..], 16);
                return new Color(col);
            }

            throw new Exception(str);
        }

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            string str = "#" + value.R.ToString("X") + value.G.ToString("X") + value.B.ToString("X");
            writer.WriteStringValue(str);
        }
    }

    class RectConverter : JsonConverter<Rect>
    {
        public override Rect Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }

            var value = new List<int>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    return new Rect(value[0], value[1], value[2], value[3]);
                }

                if (reader.TokenType != JsonTokenType.Number)
                {
                    throw new JsonException();
                }

                int element = reader.GetInt32();
                value.Add(element);
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Rect val, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            writer.WriteNumberValue(val.X);
            writer.WriteNumberValue(val.Y);
            writer.WriteNumberValue(val.Width);
            writer.WriteNumberValue(val.Height);
            writer.WriteEndArray();
        }
    }

    class Vector2Converter : JsonConverter<Vector2>
    {
        public override Vector2 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }

            var value = new List<float>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    return new Vector2(value[0], value[1]);
                }

                if (reader.TokenType != JsonTokenType.Number)
                {
                    throw new JsonException();
                }

                float element = reader.GetSingle();
                value.Add(element);
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Vector2 val, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            writer.WriteNumberValue(val.X);
            writer.WriteNumberValue(val.Y);
            writer.WriteEndArray();
        }
    }

    class Vector2IntConverter : JsonConverter<Vector2Int>
    {
        public override Vector2Int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }

            var value = new List<int>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    if (value.Count > 0)
                    {
                        return new Vector2Int(value[0], value[1]);
                    }
                    else
                    {
                        return new Vector2Int();
                    }
                }

                if (reader.TokenType != JsonTokenType.Number)
                {
                    throw new JsonException();
                }

                int element = reader.GetInt32();
                value.Add(element);
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Vector2Int val, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            writer.WriteNumberValue(val.X);
            writer.WriteNumberValue(val.Y);
            writer.WriteEndArray();
        }
    }

    public enum BgPos { Contain, Cover, CoverDirty, Unscaled };

    /// <summary>
    /// An enum that describes how levels are organized in this project (ie. linearly or in a 2D
    /// space). Possible values: `Free`, `GridVania`, `LinearHorizontal`, `LinearVertical`
    /// </summary>
    public enum WorldLayout { Free, GridVania, LinearHorizontal, LinearVertical };

    // Converters for the enums

}
#pragma warning restore 1591, 1570, IDE1006

/*

    /// <summary>
    /// Possible values: `Hidden`, `ValueOnly`, `NameAndValue`, `EntityTile`, `Points`,
    /// `PointStar`, `PointPath`, `PointPathLoop`, `RadiusPx`, `RadiusGrid`
    /// </summary>
    public enum EditorDisplayMode { EntityTile, Hidden, NameAndValue, PointPath, PointPathLoop, PointStar, Points, RadiusGrid, RadiusPx, ValueOnly };

    /// <summary>
    /// Possible values: `Above`, `Center`, `Beneath`
    /// </summary>
    public enum EditorDisplayPos { Above, Beneath, Center };

    public enum TextLanguageMode { LangC, LangHaxe, LangJs, LangJson, LangLua, LangMarkdown, LangPython, LangRuby, LangXml };

    /// <summary>
    /// Possible values: `DiscardOldOnes`, `PreventAdding`, `MoveLastOne`
    /// </summary>
    public enum LimitBehavior { DiscardOldOnes, MoveLastOne, PreventAdding };

    /// <summary>
    /// If TRUE, the maxCount is a "per world" limit, if FALSE, it's a "per level". Possible
    /// values: `PerLayer`, `PerLevel`, `PerWorld`
    /// </summary>
    public enum LimitScope { PerLayer, PerLevel, PerWorld };

    /// <summary>
    /// Possible values: `Rectangle`, `Ellipse`, `Tile`, `Cross`
    /// </summary>
    public enum RenderMode { Cross, Ellipse, Rectangle, Tile };

    /// <summary>
    /// Possible values: `Cover`, `FitInside`, `Repeat`, `Stretch`
    /// </summary>
    public enum TileRenderMode { Cover, FitInside, Repeat, Stretch };

    /// <summary>
    /// Checker mode Possible values: `None`, `Horizontal`, `Vertical`
    /// </summary>
    public enum Checker { Horizontal, None, Vertical };

    /// <summary>
    /// Defines how tileIds array is used Possible values: `Single`, `Stamp`
    /// </summary>
    public enum TileMode { Single, Stamp };

    /// <summary>
    /// Type of the layer as Haxe Enum Possible values: `IntGrid`, `Entities`, `Tiles`,
    /// `AutoLayer`
    /// </summary>
    public enum TypeEnum { AutoLayer, Entities, IntGrid, Tiles };

    public enum Flag { DiscardPreCsvIntGrid, IgnoreBackupSuggest };

    /// <summary>
    /// "Image export" option when saving project. Possible values: `None`, `OneImagePerLayer`,
    /// `OneImagePerLevel`
    /// </summary>
    public enum ImageExportMode { None, OneImagePerLayer, OneImagePerLevel };
*/