// This file was auto generated, any changes will be lost.
namespace LDtk;
#pragma warning disable IDE1006, CA1711, CA1720, CS1591
using System;
using Microsoft.Xna.Framework;

/// <summary>
/// This file is a JSON schema of files created by LDtk level editor (https://ldtk.io).
/// This is the root of any Project JSON file. It contains:  - the project settings, - an
/// array of levels, - a group of definitions (that can probably be safely ignored for most
/// users).
/// </summary>
public partial class LDtkFile
{

    /// <summary>
    /// Project background color
    /// </summary>
    public Color BgColor { get; set; }

    /// <summary>
    /// A structure containing all the definitions of this project
    /// </summary>
    public Definitions Defs { get; set; }

    /// <summary>
    /// If TRUE, one file will be saved for the project (incl. all its definitions) and one file
    /// in a sub-folder for each level.
    /// </summary>
    public bool ExternalLevels { get; set; }

    /// <summary>
    /// File format version
    /// </summary>
    public string JsonVersion { get; set; }


    /// <summary>
    /// WARNING: this field will move to the worlds array after the "multi-worlds" update.
    /// It will then be null. You can enable the Multi-worlds advanced project option to enable
    /// the change immediately. Height of the world grid in pixels.
    /// </summary>
    public int? WorldGridHeight { get; set; }

    /// <summary>
    /// WARNING: this field will move to the worlds array after the "multi-worlds" update.
    /// It will then be null. You can enable the Multi-worlds advanced project option to enable
    /// the change immediately. Width of the world grid in pixels.
    /// </summary>
    public int? WorldGridWidth { get; set; }

    /// <summary>
    /// WARNING: this field will move to the worlds array after the "multi-worlds" update.
    /// It will then be null. You can enable the Multi-worlds advanced project option to enable
    /// the change immediately. An enum that describes how levels are organized in
    /// this project (ie. linearly or in a 2D space). Possible values: &lt;null&gt;, Free,
    /// GridVania, LinearHorizontal, LinearVertical, null
    /// </summary>
    public WorldLayout? WorldLayout { get; set; }

    /// <summary>
    /// This array is not used yet in current LDtk version (so, for now, it's always
    /// empty).In a later update, it will be possible to have multiple Worlds in a
    /// single project, each containing multiple Levels.What will change when "Multiple
    /// worlds" support will be added to LDtk:- in current version, a LDtk project
    /// file can only contain a single world with multiple levels in it. In this case, levels and
    /// world layout related settings are stored in the root of the JSON.- after the
    /// "Multiple worlds" update, there will be a worlds array in root, each world containing
    /// levels and layout settings. Basically, it's pretty much only about moving the levels
    /// array to the worlds array, aint with world layout related values (eg. worldGridWidth
    /// etc).If you want to start supporting this future update easily, please refer to
    /// this documentation: https://github.com/deepnight/ldtk/issues/231
    /// </summary>
    public LDtkWorld[] Worlds { get; set; }
}




/// <summary>
/// If you're writing your own LDtk importer, you should probably just ignore most stuff in
/// the defs section, as it contains data that are mostly important to the editor. To keep
/// you away from the defs section and avoid some unnecessary JSON parsing, important data
/// from definitions is often duplicated in fields prefixed with a float underscore (eg.
/// __identifier or __type).  The 2 only definition types you might
/// need here are Tilesets and Enums.
/// A structure containing all the definitions of this project
/// </summary>
public partial class Definitions
{
    /// <summary>
    /// All entities definitions, including their custom fields
    /// </summary>
    public EntityDefinition[] Entities { get; set; }

    /// <summary>
    /// All internal enums
    /// </summary>
    public EnumDefinition[] Enums { get; set; }

    /// <summary>
    /// Note: external enums are exactly the same as enums, except they have a relPath to
    /// point to an external source file.
    /// </summary>
    public EnumDefinition[] ExternalEnums { get; set; }

    /// <summary>
    /// All layer definitions
    /// </summary>
    public LayerDefinition[] Layers { get; set; }

    /// <summary>
    /// All custom fields available to all levels.
    /// </summary>
    public FieldDefinition[] LevelFields { get; set; }

    /// <summary>
    /// All tilesets
    /// </summary>
    public TilesetDefinition[] Tilesets { get; set; }
}

public partial class EntityDefinition
{
    /// <summary>
    /// Base entity color
    /// </summary>
    public Color Color { get; set; }

    /// <summary>
    /// Pixel height
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// User defined unique identifier
    /// </summary>
    public string Identifier { get; set; }

    /// <summary>
    /// An array of 4 dimensions for the up/right/down/left borders (in this order) when using
    /// 9-slice mode for tileRenderMode. If the tileRenderMode is not NineSlice, then
    /// this array is empty. See: https://en.wikipedia.org/wiki/9-slice_scaling
    /// </summary>
    public int[] NineSliceBorders { get; set; }

    /// <summary>
    /// Pivot X coordinate (from 0 to 1.0)
    /// </summary>
    public float PivotX { get; set; }

    /// <summary>
    /// Pivot Y coordinate (from 0 to 1.0)
    /// </summary>
    public float PivotY { get; set; }

    /// <summary>
    /// An object representing a rectangle from an existing Tileset
    /// </summary>
    public TilesetRectangle TileRect { get; set; }

    /// <summary>
    /// An enum describing how the the Entity tile is rendered inside the Entity bounds. Possible
    /// values: Cover, FitInside, Repeat, Stretch, FullSizeCropped,
    /// FullSizeUncropped, NineSlice
    /// </summary>
    public TileRenderMode TileRenderMode { get; set; }

    /// <summary>
    /// Tileset ID used for optional tile display
    /// </summary>
    public int? TilesetId { get; set; }

    /// <summary>
    /// Unique Int identifier
    /// </summary>
    public int Uid { get; set; }

    /// <summary>
    /// Pixel width
    /// </summary>
    public int Width { get; set; }
}

/// <summary>
/// This object represents a custom sub rectangle in a Tileset image.
/// </summary>
public partial class TilesetRectangle
{
    /// <summary>
    /// Height in pixels
    /// </summary>
    public int H { get; set; }

    /// <summary>
    /// UID of the tileset
    /// </summary>
    public int TilesetUid { get; set; }

    /// <summary>
    /// Width in pixels
    /// </summary>
    public int W { get; set; }

    /// <summary>
    /// X pixels coordinate of the top-left corner in the Tileset image
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Y pixels coordinate of the top-left corner in the Tileset image
    /// </summary>
    public int Y { get; set; }
}

public partial class EnumDefinition
{
    /// <summary>
    /// Relative path to the external file providing this Enum
    /// </summary>
    public string ExternalRelPath { get; set; }

    /// <summary>
    /// Tileset UID if provided
    /// </summary>
    public int? IconTilesetUid { get; set; }

    /// <summary>
    /// User defined unique identifier
    /// </summary>
    public string Identifier { get; set; }

    /// <summary>
    /// An array of user-defined tags to organize the Enums
    /// </summary>
    public string[] Tags { get; set; }

    /// <summary>
    /// Unique Int identifier
    /// </summary>
    public int Uid { get; set; }

    /// <summary>
    /// All possible enum values, with their optional Tile infos.
    /// </summary>
    public EnumValueDefinition[] Values { get; set; }
}

public partial class EnumValueDefinition
{
    /// <summary>
    /// An array of 4 Int values that refers to the tile in the tileset image: [ x, y, width,
    /// height ]
    /// </summary>
    public int[] _TileSrcRect { get; set; }

    /// <summary>
    /// Optional color
    /// </summary>
    public int Color { get; set; }

    /// <summary>
    /// Enum value
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// The optional ID of the tile
    /// </summary>
    public int? TileId { get; set; }
}

public partial class LayerDefinition
{
    /// <summary>
    /// Type of the layer (IntGrid, Entities, Tiles or AutoLayer)
    /// </summary>
    public LayerType _Type { get; set; }

    public int? AutoSourceLayerDefUid { get; set; }

    /// <summary>
    /// Opacity of the layer (0 to 1.0)
    /// </summary>
    public float DisplayOpacity { get; set; }

    /// <summary>
    /// Width and height of the grid in pixels
    /// </summary>
    public int GridSize { get; set; }

    /// <summary>
    /// User defined unique identifier
    /// </summary>
    public string Identifier { get; set; }

    /// <summary>
    /// An array that defines extra optional info for each IntGrid value. WARNING: the
    /// array order is not related to actual IntGrid values! As user can re-order IntGrid values
    /// freely, you may value "2" before value "1" in this array.
    /// </summary>
    public IntGridValueDefinition[] IntGridValues { get; set; }

    /// <summary>
    /// Parallax horizontal factor (from -1 to 1, defaults to 0) which affects the scrolling
    /// speed of this layer, creating a fake 3D (parallax) effect.
    /// </summary>
    public float ParallaxFactorX { get; set; }

    /// <summary>
    /// Parallax vertical factor (from -1 to 1, defaults to 0) which affects the scrolling speed
    /// of this layer, creating a fake 3D (parallax) effect.
    /// </summary>
    public float ParallaxFactorY { get; set; }

    /// <summary>
    /// If true (default), a layer with a parallax factor will also be scaled up/down accordingly.
    /// </summary>
    public bool ParallaxScaling { get; set; }

    /// <summary>
    /// X offset of the layer, in pixels (IMPORTANT: this should be added to the LayerInstance
    /// optional offset)
    /// </summary>
    public int PxOffsetX { get; set; }

    /// <summary>
    /// Y offset of the layer, in pixels (IMPORTANT: this should be added to the LayerInstance
    /// optional offset)
    /// </summary>
    public int PxOffsetY { get; set; }

    /// <summary>
    /// Reference to the default Tileset UID being used by this layer definition.
    /// WARNING: some layer instances might use a different tileset. So most of the time,
    /// you should probably use the __tilesetDefUid value found in layer
    /// instances. Note: since version 1.0.0, the old autoTilesetDefUid was removed and
    /// merged into this value.
    /// </summary>
    public int? TilesetDefUid { get; set; }

    /// <summary>
    /// Unique Int identifier
    /// </summary>
    public int Uid { get; set; }
}

/// <summary>
/// IntGrid value definition
/// </summary>
public partial class IntGridValueDefinition
{
    public Color Color { get; set; }

    /// <summary>
    /// User defined unique identifier
    /// </summary>
    public string Identifier { get; set; }

    /// <summary>
    /// The IntGrid value itself
    /// </summary>
    public int Value { get; set; }
}

/// <summary>
/// This section is mostly only intended for the LDtk editor app itself. You can safely
/// ignore it.
/// </summary>
public partial class FieldDefinition
{
}

/// <summary>
/// The Tileset definition is the most important part among project definitions. It
/// contains some extra informations about each integrated tileset. If you only had to parse
/// one definition section, that would be the one.
/// </summary>
public partial class TilesetDefinition
{
    /// <summary>
    /// Grid-based height
    /// </summary>
    public int _CHei { get; set; }

    /// <summary>
    /// Grid-based width
    /// </summary>
    public int _CWid { get; set; }

    /// <summary>
    /// An array of custom tile metadata
    /// </summary>
    public TileCustomMetadata[] CustomData { get; set; }

    /// <summary>
    /// If this value is set, then it means that this atlas uses an internal LDtk atlas image
    /// instead of a loaded one. Possible values: &lt;null&gt;, LdtkIcons, null
    /// </summary>
    public EmbedAtlas? EmbedAtlas { get; set; }

    /// <summary>
    /// Tileset tags using Enum values specified by tagsSourceEnumId. This array contains 1
    /// element per Enum value, which contains an array of all Tile IDs that are tagged with it.
    /// </summary>
    public EnumTagValue[] EnumTags { get; set; }

    /// <summary>
    /// User defined unique identifier
    /// </summary>
    public string Identifier { get; set; }

    /// <summary>
    /// Distance in pixels from image borders
    /// </summary>
    public int Padding { get; set; }

    /// <summary>
    /// Image height in pixels
    /// </summary>
    public int PxHei { get; set; }

    /// <summary>
    /// Image width in pixels
    /// </summary>
    public int PxWid { get; set; }

    /// <summary>
    /// Path to the source file, relative to the current project JSON file It can be null
    /// if no image was provided, or when using an embed atlas.
    /// </summary>
    public string RelPath { get; set; }

    /// <summary>
    /// Space in pixels between all tiles
    /// </summary>
    public int Spacing { get; set; }

    /// <summary>
    /// An array of user-defined tags to organize the Tilesets
    /// </summary>
    public string[] Tags { get; set; }

    /// <summary>
    /// Optional Enum definition UID used for this tileset meta-data
    /// </summary>
    public int? TagsSourceEnumUid { get; set; }

    public int TileGridSize { get; set; }

    /// <summary>
    /// Unique Intidentifier
    /// </summary>
    public int Uid { get; set; }
}

/// <summary>
/// In a tileset definition, user defined meta-data of a tile.
/// </summary>
public partial class TileCustomMetadata
{
    public string Data { get; set; }
    public int TileId { get; set; }
}

/// <summary>
/// In a tileset definition, enum based tag infos
/// </summary>
public partial class EnumTagValue
{
    public string EnumValueId { get; set; }
    public int[] TileIds { get; set; }
}

public partial class EntityInstance
{
    /// <summary>
    /// Grid-based coordinates ([x,y] format)
    /// </summary>
    public Point _Grid { get; set; }

    /// <summary>
    /// Entity definition identifier
    /// </summary>
    public string _Identifier { get; set; }

    /// <summary>
    /// Pivot coordinates  ([x,y] format, values are from 0 to 1) of the Entity
    /// </summary>
    public Vector2 _Pivot { get; set; }

    /// <summary>
    /// The entity "smart" color, guessed from either Entity definition, or one its field
    /// instances.
    /// </summary>
    public string _SmartColor { get; set; }

    /// <summary>
    /// Array of tags defined in this Entity definition
    /// </summary>
    public string[] _Tags { get; set; }

    /// <summary>
    /// Optional TilesetRect used to display this entity (it could either be the default Entity
    /// tile, or some tile provided by a field value, like an Enum).
    /// </summary>
    public TilesetRectangle _Tile { get; set; }

    /// <summary>
    /// Reference of the Entity definition UID
    /// </summary>
    public int DefUid { get; set; }

    /// <summary>
    /// An array of all custom fields and their values.
    /// </summary>
    public FieldInstance[] FieldInstances { get; set; }

    /// <summary>
    /// Entity height in pixels. For non-resizable entities, it will be the same as Entity
    /// definition.
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Unique instance identifier
    /// </summary>
    public Guid Iid { get; set; }

    /// <summary>
    /// Pixel coordinates ([x,y] format) in current level coordinate space. Don't forget
    /// optional layer offsets, if they exist!
    /// </summary>
    public Point Px { get; set; }

    /// <summary>
    /// Entity width in pixels. For non-resizable entities, it will be the same as Entity
    /// definition.
    /// </summary>
    public int Width { get; set; }
}

public partial class FieldInstance
{
    /// <summary>
    /// Field definition identifier
    /// </summary>
    public string _Identifier { get; set; }

    /// <summary>
    /// Optional TilesetRect used to display this field (this can be the field own Tile, or some
    /// other Tile guessed from the value, like an Enum).
    /// </summary>
    public TilesetRectangle _Tile { get; set; }

    /// <summary>
    /// Type of the field, such as Int, Float, String, Enum(my_enum_name), Bool,
    /// etc. NOTE: if you enable the advanced option Use Multilines type, you will have
    /// "Multilines" instead of "String" when relevant.
    /// </summary>
    public string _Type { get; set; }

    /// <summary>
    /// Actual value of the field instance. The value type varies, depending on
    /// __type:  - For classic types (ie. Integer, Float, Boolean, String,
    /// Text and FilePath), you just get the actual value with the expected type.  - For
    /// Color, the value is an hexadecimal string using "#rrggbb" format.  - For
    /// Enum, the value is a String representing the selected enum value.  - For
    /// Point, the value is a GridPoint object.  - For Tile, the
    /// value is a TilesetRect object.  - For EntityRef, the value
    /// is an EntityReferenceInfos object. If the field
    /// is an array, then this __value will also be a JSON array.
    /// </summary>
    public object _Value { get; set; }

    /// <summary>
    /// Reference of the Field definition UID
    /// </summary>
    public int DefUid { get; set; }
}

/// <summary>
/// This object is used in Field Instances to describe an EntityRef value.
/// </summary>
public partial class FieldInstanceEntityReference
{
    /// <summary>
    /// Guid of the refered EntityInstance
    /// </summary>
    public Guid EntityIid { get; set; }

    /// <summary>
    /// Guid of the LayerInstance containing the refered EntityInstance
    /// </summary>
    public Guid LayerIid { get; set; }

    /// <summary>
    /// Guid of the Level containing the refered EntityInstance
    /// </summary>
    public Guid LevelIid { get; set; }

    /// <summary>
    /// Guid of the World containing the refered EntityInstance
    /// </summary>
    public Guid WorldIid { get; set; }
}

/// <summary>
/// This object is just a grid-based coordinate used in Field values.
/// </summary>
public partial class FieldInstanceGridPoint
{
    /// <summary>
    /// X grid-based coordinate
    /// </summary>
    public int Cx { get; set; }

    /// <summary>
    /// Y grid-based coordinate
    /// </summary>
    public int Cy { get; set; }
}

/// <summary>
/// IntGrid value instance
/// </summary>
public partial class IntGridValueInstance
{
    /// <summary>
    /// Coordinate ID in the layer grid
    /// </summary>
    public int CoordId { get; set; }

    /// <summary>
    /// IntGrid value
    /// </summary>
    public int V { get; set; }
}

/// <summary>
/// This section contains all the level data. It can be found in 2 distinct forms, depending
/// on Project current settings:  - If "Separate level files" is disabled (default):
/// full level data is embedded inside the main Project JSON file, - If "Separate level
/// files" is enabled: level data is stored in separate standalone .ldtkl files (one
/// per level). In this case, the main Project JSON file will still contain most level data,
/// except heavy sections, like the layerInstances array (which will be null). The
/// externalRelPath string points to the ldtkl file.  A ldtkl file is just a JSON file
/// containing exactly what is described below.
/// </summary>
public partial class LDtkLevel
{
    /// <summary>
    /// Background color of the level (same as bgColor, except the default value is
    /// automatically used here if its value is null)
    /// </summary>
    public string _BgColor { get; set; }

    /// <summary>
    /// Position informations of the background image, if there is one.
    /// </summary>
    public LevelBackgroundPosition _BgPos { get; set; }

    /// <summary>
    /// An array listing all other levels touching this one on the world map. Only relevant
    /// for world layouts where level spatial positioning is manual (ie. GridVania, Free). For
    /// Horizontal and Vertical layouts, this array is always empty.
    /// </summary>
    public NeighbourLevel[] _Neighbours { get; set; }

    /// <summary>
    /// The optional relative path to the level background image.
    /// </summary>
    public string BgRelPath { get; set; }

    /// <summary>
    /// This value is not null if the project option "Save levels separately" is enabled. In
    /// this case, this relative path points to the level Json file.
    /// </summary>
    public string ExternalRelPath { get; set; }

    /// <summary>
    /// An array containing this level custom field values.
    /// </summary>
    public FieldInstance[] FieldInstances { get; set; }

    /// <summary>
    /// User defined unique identifier
    /// </summary>
    public string Identifier { get; set; }

    /// <summary>
    /// Unique instance identifier
    /// </summary>
    public Guid Iid { get; set; }

    /// <summary>
    /// An array containing all Layer instances. IMPORTANT: if the project option "Save
    /// levels separately" is enabled, this field will be null. This array is sorted
    /// in display order: the 1st layer is the top-most and the last is behind.
    /// </summary>
    public LayerInstance[] LayerInstances { get; set; }

    /// <summary>
    /// Height of the level in pixels
    /// </summary>
    public int PxHei { get; set; }

    /// <summary>
    /// Width of the level in pixels
    /// </summary>
    public int PxWid { get; set; }

    /// <summary>
    /// Unique Int identifier
    /// </summary>
    public int Uid { get; set; }

    /// <summary>
    /// Index that represents the "depth" of the level in the world. Default is 0, greater means
    /// "above", lower means "below". This value is mostly used for display only and is
    /// intended to make stacking of levels easier to manage.
    /// </summary>
    public int WorldDepth { get; set; }

    /// <summary>
    /// World X coordinate in pixels. Only relevant for world layouts where level spatial
    /// positioning is manual (ie. GridVania, Free). For Horizontal and Vertical layouts, the
    /// value is always -1 here.
    /// </summary>
    public int WorldX { get; set; }

    /// <summary>
    /// World Y coordinate in pixels. Only relevant for world layouts where level spatial
    /// positioning is manual (ie. GridVania, Free). For Horizontal and Vertical layouts, the
    /// value is always -1 here.
    /// </summary>
    public int WorldY { get; set; }
}

/// <summary>
/// Level background image position info
/// </summary>
public partial class LevelBackgroundPosition
{
    /// <summary>
    /// An array of 4 float values describing the cropped sub-rectangle of the displayed
    /// background image. This cropping happens when original is larger than the level bounds.
    /// Array format: [ cropX, cropY, cropWidth, cropHeight ]
    /// </summary>
    public Rectangle CropRect { get; set; }

    /// <summary>
    /// An array containing the [scaleX,scaleY] values of the cropped background image,
    /// depending on bgPos option.
    /// </summary>
    public Vector2 Scale { get; set; }

    /// <summary>
    /// An array containing the [x,y] pixel coordinates of the top-left corner of the
    /// cropped background image, depending on bgPos option.
    /// </summary>
    public Point TopLeftPx { get; set; }
}

/// <summary>
/// Nearby level info
/// </summary>
public partial class NeighbourLevel
{
    /// <summary>
    /// A single lowercase character tipping on the level location (north, south, west,
    /// east).
    /// </summary>
    public string Dir { get; set; }

    /// <summary>
    /// Neighbour Instance Identifier
    /// </summary>
    public Guid LevelIid { get; set; }
}

public partial class LayerInstance
{
    /// <summary>
    /// Grid-based height
    /// </summary>
    public int _CHei { get; set; }

    /// <summary>
    /// Grid-based width
    /// </summary>
    public int _CWid { get; set; }

    /// <summary>
    /// Grid size
    /// </summary>
    public int _GridSize { get; set; }

    /// <summary>
    /// Layer definition identifier
    /// </summary>
    public string _Identifier { get; set; }

    /// <summary>
    /// Layer opacity as Float [0-1]
    /// </summary>
    public float _Opacity { get; set; }

    /// <summary>
    /// Total layer X pixel offset, including both instance and definition offsets.
    /// </summary>
    public int _PxTotalOffsetX { get; set; }

    /// <summary>
    /// Total layer Y pixel offset, including both instance and definition offsets.
    /// </summary>
    public int _PxTotalOffsetY { get; set; }

    /// <summary>
    /// The definition UID of corresponding Tileset, if any.
    /// </summary>
    public int? _TilesetDefUid { get; set; }

    /// <summary>
    /// The relative path to corresponding Tileset, if any.
    /// </summary>
    public string _TilesetRelPath { get; set; }

    /// <summary>
    /// Layer type (possible values: IntGrid, Entities, Tiles or AutoLayer)
    /// </summary>
    public LayerType _Type { get; set; }

    /// <summary>
    /// An array containing all tiles generated by Auto-layer rules. The array is already sorted
    /// in display order (ie. 1st tile is beneath 2nd, which is beneath 3rd etc.).
    /// Note: if multiple tiles are stacked in the same cell as the result of different rules,
    /// all tiles behind opaque ones will be discarded.
    /// </summary>
    public TileInstance[] AutoLayerTiles { get; set; }

    public EntityInstance[] EntityInstances { get; set; }
    public TileInstance[] GridTiles { get; set; }

    /// <summary>
    /// Unique layer instance identifier
    /// </summary>
    public Guid Iid { get; set; }

    /// <summary>
    /// A list of all values in the IntGrid layer, stored in CSV format (Comma Separated
    /// Values). Order is from left to right, and top to bottom (ie. first row from left to
    /// right, followed by second row, etc). 0 means "empty cell" and IntGrid values
    /// start at 1. The array size is __cWid x __cHei cells.
    /// </summary>
    public int[] IntGridCsv { get; set; }

    /// <summary>
    /// Reference the Layer definition UID
    /// </summary>
    public int LayerDefUid { get; set; }

    /// <summary>
    /// Reference to the UID of the level containing this layer instance
    /// </summary>
    public int LevelId { get; set; }

    /// <summary>
    /// This layer can use another tileset by overriding the tileset UID here.
    /// </summary>
    public int? OverrideTilesetUid { get; set; }

    /// <summary>
    /// X offset in pixels to render this layer, usually 0 (IMPORTANT: this should be added to
    /// the LayerDef optional offset, see __pxTotalOffsetX)
    /// </summary>
    public int PxOffsetX { get; set; }

    /// <summary>
    /// Y offset in pixels to render this layer, usually 0 (IMPORTANT: this should be added to
    /// the LayerDef optional offset, see __pxTotalOffsetY)
    /// </summary>
    public int PxOffsetY { get; set; }

    /// <summary>
    /// Layer instance visibility
    /// </summary>
    public bool Visible { get; set; }
}

/// <summary>
/// This structure represents a single tile from a given Tileset.
/// </summary>
public partial class TileInstance
{
    /// <summary>
    /// "Flip bits", a 2-bits integer to represent the mirror transformations of the tile.
    /// - Bit 0 = X flip  - Bit 1 = Y flip  Examples: f=0 (no flip), f=1 (X flip
    /// only), f=2 (Y flip only), f=3 (both flips)
    /// </summary>
    public int F { get; set; }

    /// <summary>
    /// Pixel coordinates of the tile in the layer ([x,y] format). Don't forget optional
    /// layer offsets, if they exist!
    /// </summary>
    public Point Px { get; set; }

    /// <summary>
    /// Pixel coordinates of the tile in the tileset ([x,y] format)
    /// </summary>
    public Point Src { get; set; }

    /// <summary>
    /// The Tile ID in the corresponding tileset.
    /// </summary>
    public int T { get; set; }
}

/// <summary>
/// IMPORTANT: this type is not used yet in current LDtk version. It's only presented
/// here as a preview of a planned feature.  A World contains multiple levels, and it has its
/// own layout settings.
/// </summary>
public partial class LDtkWorld
{
    /// <summary>
    /// User defined unique identifier
    /// </summary>
    public string Identifier { get; set; }

    /// <summary>
    /// Unique instance identifer
    /// </summary>
    public Guid Iid { get; set; }


    /// <summary>
    /// Height of the world grid in pixels.
    /// </summary>
    public int WorldGridHeight { get; set; }

    /// <summary>
    /// Width of the world grid in pixels.
    /// </summary>
    public int WorldGridWidth { get; set; }

    /// <summary>
    /// An enum that describes how levels are organized in this project (ie. linearly or in a 2D
    /// space). Possible values: Free, GridVania, LinearHorizontal, LinearVertical,
    /// null, null
    /// </summary>
    public WorldLayout? WorldLayout { get; set; }
}

/// <summary>
/// An enum describing how the the Entity tile is rendered inside the Entity bounds. Possible
/// values: Cover, FitInside, Repeat, Stretch, FullSizeCropped,
/// FullSizeUncropped, NineSlice
/// </summary>
public enum TileRenderMode { Cover, FitInside, FullSizeCropped, FullSizeUncropped, NineSlice, Repeat, Stretch };

public enum EmbedAtlas { LdtkIcons };

public enum WorldLayout { Free, GridVania, LinearHorizontal, LinearVertical };
#pragma warning restore
