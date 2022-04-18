namespace LDtk;

using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable 1591, 1570, IDE1006
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
    /// <para>
    /// This holds the same levels that are returned from the LoadLevel Method but behaviour may be weird if external levels is on
    /// and the parent field will not be populated if used before LoadLevel is called.
    /// </para>
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
    public int WorldGridHeight { get; set; }

    /// <summary>
    /// Width of the world grid in pixels.
    /// </summary>
    [JsonPropertyName("worldGridWidth")]
    public int WorldGridWidth { get; set; }

    /// <summary>
    /// An enum that describes how levels are organized in this project (ie. linearly or in a 2D
    /// space). Possible values: `Free`, `GridVania`, `LinearHorizontal`, `LinearVertical`
    /// </summary>
    [JsonPropertyName("worldLayout")]
    public WorldLayout WorldLayout { get; set; }


    public static readonly JsonSerializerOptions SerializeOptions = new()
    {
        Converters ={
                new JsonStringEnumConverter(),
                new ColorConverter(),
                new RectConverter(),
                new Vector2Converter(),
                new PointConverter(),
            }
    };
}

/// <summary>
/// A structure containing all the definitions of this project
///
/// If you're writing your own LDtk importer, you should probably just ignore *most* stuff in
/// the `defs` section, as it contains data that are mostly important to the editor. To keep
/// you away from the `defs` section and avoid some unnecessary JSON parsing, important data
/// from definitions is often duplicated in fields prefixed with a float underscore (eg.
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
    /// An array containing all custom fields available to all levels.
    /// </summary>
    [JsonPropertyName("levelFields")]
    public FieldDefinition[] LevelFields { get; set; }

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
    /// Array of field definitions
    /// </summary>
    [JsonPropertyName("fieldDefs")]
    public FieldDefinition[] FieldDefs { get; set; }

    /// <summary>
    /// Pixel height
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; set; }

    /// <summary>
    /// Unique String identifier
    /// </summary>
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    /// <summary>
    /// Pivot X coordinate (from 0 to 1.0)
    /// </summary>
    [JsonPropertyName("pivotX")]
    public float PivotX { get; set; }

    /// <summary>
    /// Pivot Y coordinate (from 0 to 1.0)
    /// </summary>
    [JsonPropertyName("pivotY")]
    public float PivotY { get; set; }

    /// <summary>
    /// Tile ID used for optional tile display
    /// </summary>
    [JsonPropertyName("tileId")]
    public int? TileId { get; set; }

    /// <summary>
    /// Tileset ID used for optional tile display
    /// </summary>
    [JsonPropertyName("tilesetId")]
    public int? TilesetId { get; set; }

    /// <summary>
    /// Unique Int identifier
    /// </summary>
    [JsonPropertyName("uid")]
    public int Uid { get; set; }

    /// <summary>
    /// Pixel width
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; set; }
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
    public int? IconTilesetUid { get; set; }

    /// <summary>
    /// Unique String identifier
    /// </summary>
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    /// <summary>
    /// Unique Int identifier
    /// </summary>
    [JsonPropertyName("uid")]
    public int Uid { get; set; }

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
    public int Color { get; set; }

    /// <summary>
    /// Enum value
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>
    /// The optional ID of the tile
    /// </summary>
    [JsonPropertyName("tileId")]
    public int? TileId { get; set; }

    /// <summary>
    /// An array of 4 Int values that refers to the tile in the tileset image: `[ x, y, width,
    /// height ]`
    /// </summary>
    [JsonPropertyName("__tileSrcRect")]
    public Rectangle _TileSrcRect { get; set; }
}

public partial class LayerDefinition
{
    [JsonPropertyName("autoSourceLayerDefUid")]
    public int? AutoSourceLayerDefUid { get; set; }

    /// <summary>
    /// Reference to the Tileset UID being used by this auto-layer rules. WARNING: some layer
    /// *instances* might use a different tileset. So most of the time, you should probably use
    /// the `__tilesetDefUid` value from layer instances.
    /// </summary>
    [JsonPropertyName("autoTilesetDefUid")]
    public int? AutoTilesetDefUid { get; set; }

    /// <summary>
    /// Opacity of the layer (0 to 1.0)
    /// </summary>
    [JsonPropertyName("displayOpacity")]
    public float DisplayOpacity { get; set; }

    /// <summary>
    /// Width and height of the grid in pixels
    /// </summary>
    [JsonPropertyName("gridSize")]
    public int GridSize { get; set; }

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
    public int PxOffsetX { get; set; }

    /// <summary>
    /// Y offset of the layer, in pixels (IMPORTANT: this should be added to the `LayerInstance`
    /// optional offset)
    /// </summary>
    [JsonPropertyName("pxOffsetY")]
    public int PxOffsetY { get; set; }

    /// <summary>
    /// Reference to the Tileset UID being used by this Tile layer. WARNING: some layer
    /// *instances* might use a different tileset. So most of the time, you should probably use
    /// the `__tilesetDefUid` value from layer instances.
    /// </summary>
    [JsonPropertyName("tilesetDefUid")]
    public int? TilesetDefUid { get; set; }

    /// <summary>
    /// Type of the layer (IntGrid, Entities, Tiles or AutoLayer)
    /// </summary>
    [JsonPropertyName("__type")]
    public LayerType _Type { get; set; }

    /// <summary>
    /// Unique Int identifier
    /// </summary>
    [JsonPropertyName("uid")]
    public int Uid { get; set; }
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
    public int Value { get; set; }
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
    public int _CHei { get; set; }

    /// <summary>
    /// Grid-based width
    /// </summary>
    [JsonPropertyName("__cWid")]
    public int _CWid { get; set; }

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
    public int Padding { get; set; }

    /// <summary>
    /// Image height in pixels
    /// </summary>
    [JsonPropertyName("pxHei")]
    public int PxHei { get; set; }

    /// <summary>
    /// Image width in pixels
    /// </summary>
    [JsonPropertyName("pxWid")]
    public int PxWid { get; set; }

    /// <summary>
    /// Path to the source file, relative to the current project JSON file
    /// </summary>
    [JsonPropertyName("relPath")]
    public string RelPath { get; set; }

    /// <summary>
    /// Space in pixels between all tiles
    /// </summary>
    [JsonPropertyName("spacing")]
    public int Spacing { get; set; }

    /// <summary>
    /// Optional Enum definition UID used for this tileset meta-data
    /// </summary>
    [JsonPropertyName("tagsSourceEnumUid")]
    public int? TagsSourceEnumUid { get; set; }

    [JsonPropertyName("tileGridSize")]
    public int TileGridSize { get; set; }

    /// <summary>
    /// Unique Intidentifier
    /// </summary>
    [JsonPropertyName("uid")]
    public int Uid { get; set; }
}

public partial class FieldInstance
{
    /// <summary>
    /// Reference of the **Field definition** UID
    /// </summary>
    [JsonPropertyName("defUid")]
    public int DefUid { get; set; }

    /// <summary>
    /// Field definition identifier
    /// </summary>
    [JsonPropertyName("__identifier")]
    public string _Identifier { get; set; }

    /// <summary>
    /// Type of the field, such as `Int`, `Float`, `Enum(my_enum_name)`, `Bool`, etc.
    /// </summary>
    [JsonPropertyName("__type")]
    public string _Type { get; set; }

    /// <summary>
    /// Actual value of the field instance. The value type may vary, depending on `__type`
    /// (Integer, Boolean, String etc.)<br/>  It can also be an `Array` of those same types.
    /// </summary>
    [JsonPropertyName("__value")]
    public object _Value { get; set; }

    /// <summary>
    /// Editor internal raw values
    /// </summary>
    [JsonPropertyName("realEditorValues")]
    public object[] RealEditorValues { get; set; }
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
    public int _CHei { get; set; }

    /// <summary>
    /// Grid-based width
    /// </summary>
    [JsonPropertyName("__cWid")]
    public int _CWid { get; set; }

    [JsonPropertyName("entityInstances")]
    public EntityInstance[] EntityInstances { get; set; }

    /// <summary>
    /// Grid size
    /// </summary>
    [JsonPropertyName("__gridSize")]
    public int _GridSize { get; set; }

    [JsonPropertyName("gridTiles")]
    public TileInstance[] GridTiles { get; set; }

    /// <summary>
    /// Layer definition identifier
    /// </summary>
    [JsonPropertyName("__identifier")]
    public string _Identifier { get; set; }

    /// <summary>
    /// A list of all values in the IntGrid layer, stored from left to right, and top to bottom
    /// (ie. first row from left to right, followed by second row, etc). `0` means "empty cell"
    /// and IntGrid values start at 1. This array size is `__cWid` x `__cHei` cells.
    /// </summary>
    [JsonPropertyName("intGridCsv")]
    public int[] IntGridCsv { get; set; }

    /// <summary>
    /// Reference the Layer definition UID
    /// </summary>
    [JsonPropertyName("layerDefUid")]
    public int LayerDefUid { get; set; }

    /// <summary>
    /// Reference to the UID of the level containing this layer instance
    /// </summary>
    [JsonPropertyName("levelId")]
    public int LevelId { get; set; }

    /// <summary>
    /// Layer opacity as Float [0-1]
    /// </summary>
    [JsonPropertyName("__opacity")]
    public float _Opacity { get; set; }

    /// <summary>
    /// This layer can use another tileset by overriding the tileset UID here.
    /// </summary>
    [JsonPropertyName("overrideTilesetUid")]
    public int? OverrideTilesetUid { get; set; }

    /// <summary>
    /// X offset in pixels to render this layer, usually 0 (IMPORTANT: this should be added to
    /// the `LayerDef` optional offset, see `__pxTotalOffsetX`)
    /// </summary>
    [JsonPropertyName("pxOffsetX")]
    public int PxOffsetX { get; set; }

    /// <summary>
    /// Y offset in pixels to render this layer, usually 0 (IMPORTANT: this should be added to
    /// the `LayerDef` optional offset, see `__pxTotalOffsetY`)
    /// </summary>
    [JsonPropertyName("pxOffsetY")]
    public int PxOffsetY { get; set; }

    /// <summary>
    /// Total layer X pixel offset, including both instance and definition offsets.
    /// </summary>
    [JsonPropertyName("__pxTotalOffsetX")]
    public int _PxTotalOffsetX { get; set; }

    /// <summary>
    /// Total layer Y pixel offset, including both instance and definition offsets.
    /// </summary>
    [JsonPropertyName("__pxTotalOffsetY")]
    public int _PxTotalOffsetY { get; set; }

    /// <summary>
    /// The definition UID of corresponding Tileset, if any.
    /// </summary>
    [JsonPropertyName("__tilesetDefUid")]
    public int? _TilesetDefUid { get; set; }

    /// <summary>
    /// The relative path to corresponding Tileset, if any.
    /// </summary>
    [JsonPropertyName("__tilesetRelPath")]
    public string _TilesetRelPath { get; set; }

    /// <summary>
    /// Layer type (possible values: IntGrid, Entities, Tiles or AutoLayer)
    /// </summary>
    [JsonPropertyName("__type")]
    public LayerType _Type { get; set; }

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
    public int F { get; set; }

    /// <summary>
    /// Pixel coordinates of the tile in the **layer** (`[x,y]` format). Don't forget optional
    /// layer offsets, if they exist!
    /// </summary>
    [JsonPropertyName("px")]
    public Point Px { get; set; }

    /// <summary>
    /// Pixel coordinates of the tile in the **tileset** (`[x,y]` format)
    /// </summary>
    [JsonPropertyName("src")]
    public Point Src { get; set; }

    /// <summary>
    /// The *Tile ID* in the corresponding tileset.
    /// </summary>
    [JsonPropertyName("t")]
    public int T { get; set; }
}

public partial class EntityInstance
{
    /// <summary>
    /// Reference of the **Entity definition** UID
    /// </summary>
    [JsonPropertyName("defUid")]
    public int DefUid { get; set; }

    /// <summary>
    /// An array of all custom fields and their values.
    /// </summary>
    [JsonPropertyName("fieldInstances")]
    public FieldInstance[] FieldInstances { get; set; }

    /// <summary>
    /// Grid-based coordinates (`[x,y]` format)
    /// </summary>
    [JsonPropertyName("__grid")]
    public Point _Grid { get; set; }

    /// <summary>
    /// Entity height in pixels. For non-resizable entities, it will be the same as Entity
    /// definition.
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; set; }

    /// <summary>
    /// Entity definition identifier
    /// </summary>
    [JsonPropertyName("__identifier")]
    public string _Identifier { get; set; }

    /// <summary>
    /// Pivot coordinates  (`[x,y]` format, values are from 0 to 1) of the Entity
    /// </summary>
    [JsonPropertyName("__pivot")]
    public Vector2 _Pivot { get; set; }

    /// <summary>
    /// Pixel coordinates (`[x,y]` format) in current level coordinate space. Don't forget
    /// optional layer offsets, if they exist!
    /// </summary>
    [JsonPropertyName("px")]
    public Point Px { get; set; }

    /// <summary>
    /// Optional Tile used to display this entity (it could either be the default Entity tile, or
    /// some tile provided by a field value, like an Enum).
    /// </summary>
    [JsonPropertyName("__tile")]
    public EntityInstanceTile _Tile { get; set; }

    /// <summary>
    /// Entity width in pixels. For non-resizable entities, it will be the same as Entity
    /// definition.
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; set; }
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
    public Rectangle SrcRect { get; set; }

    /// <summary>
    /// Tileset ID
    /// </summary>
    [JsonPropertyName("tilesetUid")]
    public int TilesetUid { get; set; }
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
    public int CoordId { get; set; }

    /// <summary>
    /// IntGrid value
    /// </summary>
    [JsonPropertyName("v")]
    public int V { get; set; }
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
    public int LevelUid { get; set; }
}

/// <summary>
/// This section is mostly only intended for the LDtk editor app itself. You can safely
/// ignore it.
/// </summary>
public partial class FieldDefinition
{
    /// <summary>
    /// Human readable value type (eg. `Int`, `Float`, `Point`, etc.). If the field is an array,
    /// this field will look like `Array<...>` (eg. `Array<Int>`, `Array<Point>` etc.)
    /// </summary>
    [JsonPropertyName("__type")]
    public string Type { get; set; }

    /// <summary>
    /// Optional list of accepted file extensions for FilePath value type. Includes the dot:
    /// `.ext`
    /// </summary>
    [JsonPropertyName("acceptFileTypes")]
    public string[] AcceptFileTypes { get; set; }

    /// <summary>
    /// Array max length
    /// </summary>
    [JsonPropertyName("arrayMaxLength")]
    public long? ArrayMaxLength { get; set; }

    /// <summary>
    /// Array min length
    /// </summary>
    [JsonPropertyName("arrayMinLength")]
    public long? ArrayMinLength { get; set; }

    /// <summary>
    /// TRUE if the value can be null. For arrays, TRUE means it can contain null values
    /// (exception: array of Points can't have null values).
    /// </summary>
    [JsonPropertyName("canBeNull")]
    public bool CanBeNull { get; set; }

    /// <summary>
    /// Default value if selected value is null or invalid.
    /// </summary>
    [JsonPropertyName("defaultOverride")]
    public object DefaultOverride { get; set; }

    [JsonPropertyName("editorAlwaysShow")]
    public bool EditorAlwaysShow { get; set; }

    [JsonPropertyName("editorCutLongValues")]
    public bool EditorCutLongValues { get; set; }

    /// <summary>
    /// Possible values: `Hidden`, `ValueOnly`, `NameAndValue`, `EntityTile`, `Points`,
    /// `PointStar`, `PointPath`, `PointPathLoop`, `RadiusPx`, `RadiusGrid`
    /// </summary>
    [JsonPropertyName("editorDisplayMode")]
    public EditorDisplayMode EditorDisplayMode { get; set; }

    /// <summary>
    /// Possible values: `Above`, `Center`, `Beneath`
    /// </summary>
    [JsonPropertyName("editorDisplayPos")]
    public EditorDisplayPos EditorDisplayPos { get; set; }

    /// <summary>
    /// Unique String identifier
    /// </summary>
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    /// <summary>
    /// TRUE if the value is an array of multiple values
    /// </summary>
    [JsonPropertyName("isArray")]
    public bool IsArray { get; set; }

    /// <summary>
    /// Max limit for value, if applicable
    /// </summary>
    [JsonPropertyName("max")]
    public double? Max { get; set; }

    /// <summary>
    /// Min limit for value, if applicable
    /// </summary>
    [JsonPropertyName("min")]
    public double? Min { get; set; }

    /// <summary>
    /// Optional regular expression that needs to be matched to accept values. Expected format:
    /// `/some_reg_ex/g`, with optional "i" flag.
    /// </summary>
    [JsonPropertyName("regex")]
    public string Regex { get; set; }

    /// <summary>
    /// Possible values: &lt;`null`&gt;, `LangPython`, `LangRuby`, `LangJS`, `LangLua`, `LangC`,
    /// `LangHaxe`, `LangMarkdown`, `LangJson`, `LangXml`
    /// </summary>
    [JsonPropertyName("textLanguageMode")]
    public TextLanguageMode? TextLanguageMode { get; set; }

    /// <summary>
    /// Internal type enum
    /// </summary>
    [JsonPropertyName("type")]
    public object FieldDefinitionType { get; set; }

    /// <summary>
    /// Unique Int identifier
    /// </summary>
    [JsonPropertyName("uid")]
    public long Uid { get; set; }
}

public enum BgPos { Contain, Cover, CoverDirty, Unscaled };

/// <summary>
/// An enum that describes how levels are organized in this project (ie. linearly or in a 2D
/// space). Possible values: `Free`, `GridVania`, `LinearHorizontal`, `LinearVertical`
/// </summary>
public enum WorldLayout { Free, GridVania, LinearHorizontal, LinearVertical };

/// <summary>
/// Type of the layer as Haxe Enum Possible values: `IntGrid`, `Entities`, `Tiles`,
/// `AutoLayer`
/// </summary>
public enum LayerType { IntGrid, Entities, Tiles, AutoLayer };

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
#pragma warning restore 1591, 1570, IDE1006
