// This file was auto generated, any changes will be lost.
namespace LDtk.Codegen;
#pragma warning disable IDE1006,CA1711,CA1720
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
    /// LDtk application build identifier. This is only used to identify the LDtk version
    /// that generated this particular project file, which can be useful for specific bug fixing.
    /// Note that the build identifier is just the date of the release, so it's not unique to
    /// each user (one single global ID per LDtk public release), and as a result, completely
    /// anonymous.
    /// </summary>
    public float AppBuildId { get; set; }

    /// <summary>
    /// Number of backup files to keep, if the backupOnSave is TRUE
    /// </summary>
    public int BackupLimit { get; set; }

    /// <summary>
    /// If TRUE, an extra copy of the project will be created in a sub folder, when saving.
    /// </summary>
    public bool BackupOnSave { get; set; }

    /// <summary>
    /// Project background color
    /// </summary>
    public Color BgColor { get; set; }

    /// <summary>
    /// Default grid size for new layers
    /// </summary>
    public int DefaultGridSize { get; set; }

    /// <summary>
    /// Default background color of levels
    /// </summary>
    public string DefaultLevelBgColor { get; set; }

    /// <summary>
    /// WARNING: this field will move to the worlds array after the "multi-worlds" update.
    /// It will then be null. You can enable the Multi-worlds advanced project option to enable
    /// the change immediately. Default new level height
    /// </summary>
    public int? DefaultLevelHeight { get; set; }

    /// <summary>
    /// WARNING: this field will move to the worlds array after the "multi-worlds" update.
    /// It will then be null. You can enable the Multi-worlds advanced project option to enable
    /// the change immediately. Default new level width
    /// </summary>
    public int? DefaultLevelWidth { get; set; }

    /// <summary>
    /// Default X pivot (0 to 1) for new entities
    /// </summary>
    public float DefaultPivotX { get; set; }

    /// <summary>
    /// Default Y pivot (0 to 1) for new entities
    /// </summary>
    public float DefaultPivotY { get; set; }

    /// <summary>
    /// A structure containing all the definitions of this project
    /// </summary>
    public Definitions Defs { get; set; }

    /// <summary>
    /// WARNING: this deprecated value is no inter exported since version 0.9.3  Replaced
    /// by: imageExportMode
    /// </summary>
    public bool? ExportPng { get; set; }

    /// <summary>
    /// If TRUE, a Tiled compatible file will also be generated aint with the LDtk JSON file
    /// (default is FALSE)
    /// </summary>
    public bool ExportTiled { get; set; }

    /// <summary>
    /// If TRUE, one file will be saved for the project (incl. all its definitions) and one file
    /// in a sub-folder for each level.
    /// </summary>
    public bool ExternalLevels { get; set; }

    /// <summary>
    /// An array containing various advanced flags (ie. options or other states). Possible
    /// values: DiscardPreCsvIntGrid, ExportPreCsvIntGridFormat, IgnoreBackupSuggest,
    /// PrependIndexToLevelFileNames, MultiWorlds, UseMultilinesType
    /// </summary>
    public Flag[] Flags { get; set; }

    /// <summary>
    /// Naming convention for Identifiers (first-letter uppercase, full uppercase etc.) Possible
    /// values: Capitalize, Uppercase, Lowercase, Free
    /// </summary>
    public IdentifierStyle IdentifierStyle { get; set; }

    /// <summary>
    /// "Image export" option when saving project. Possible values: None, OneImagePerLayer,
    /// OneImagePerLevel, LayersAndLevels
    /// </summary>
    public ImageExportMode ImageExportMode { get; set; }

    /// <summary>
    /// File format version
    /// </summary>
    public string JsonVersion { get; set; }

    /// <summary>
    /// The default naming convention for level identifiers.
    /// </summary>
    public string LevelNamePattern { get; set; }

    /// <summary>
    /// All levels. The order of this array is only relevant in LinearHorizontal and
    /// linearVertical world layouts (see worldLayout value). Otherwise, you should
    /// refer to the worldX,worldY coordinates of each Level.
    /// </summary>
    public LDtkLevel[] Levels { get; set; }

    /// <summary>
    /// If TRUE, the Json is partially minified (no indentation, nor line breaks, default is
    /// FALSE)
    /// </summary>
    public bool MinifyJson { get; set; }

    /// <summary>
    /// Next Unique integer ID available
    /// </summary>
    public int NextUid { get; set; }

    /// <summary>
    /// File naming pattern for exported PNGs
    /// </summary>
    public string PngFilePattern { get; set; }

    /// <summary>
    /// If TRUE, a very simplified will be generated on saving, for quicker & easier engine
    /// integration.
    /// </summary>
    public bool SimplifiedExport { get; set; }

    /// <summary>
    /// This optional description is used by LDtk Samples to show up some informations and
    /// instructions.
    /// </summary>
    public string TutorialDesc { get; set; }

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
    /// GridVania, LinearHorizontal, LinearVertical
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


public partial class AutoLayerRuleGroup
{
    public bool Active { get; set; }

    /// <summary>
    /// This field was removed in 1.0.0 and should no inter be used.
    /// </summary>
    public bool? Collapsed { get; set; }

    public bool IsOptional { get; set; }
    public string Name { get; set; }
    public AutoLayerRuleDefinition[] Rules { get; set; }
    public int Uid { get; set; }
}

/// <summary>
/// This complex section isn't meant to be used by game devs at all, as these rules are
/// completely resolved internally by the editor before any saving. You should just ignore
/// this part.
/// </summary>
public partial class AutoLayerRuleDefinition
{
    /// <summary>
    /// If FALSE, the rule effect isn't applied, and no tiles are generated.
    /// </summary>
    public bool Active { get; set; }

    /// <summary>
    /// When TRUE, the rule will prevent other rules to be applied in the same cell if it matches
    /// (TRUE by default).
    /// </summary>
    public bool BreakOnMatch { get; set; }

    /// <summary>
    /// Chances for this rule to be applied (0 to 1)
    /// </summary>
    public float Chance { get; set; }

    /// <summary>
    /// Checker mode Possible values: None, Horizontal, Vertical
    /// </summary>
    public Checker Checker { get; set; }

    /// <summary>
    /// If TRUE, allow rule to be matched by flipping its pattern horizontally
    /// </summary>
    public bool FlipX { get; set; }

    /// <summary>
    /// If TRUE, allow rule to be matched by flipping its pattern vertically
    /// </summary>
    public bool FlipY { get; set; }

    /// <summary>
    /// Default IntGrid value when checking cells outside of level bounds
    /// </summary>
    public int? OutOfBoundsValue { get; set; }

    /// <summary>
    /// Rule pattern (size x size)
    /// </summary>
    public int[] Pattern { get; set; }

    /// <summary>
    /// If TRUE, enable Perlin filtering to only apply rule on specific random area
    /// </summary>
    public bool PerlinActive { get; set; }

    public float PerlinOctaves { get; set; }
    public float PerlinScale { get; set; }
    public float PerlinSeed { get; set; }

    /// <summary>
    /// X pivot of a tile stamp (0-1)
    /// </summary>
    public float PivotX { get; set; }

    /// <summary>
    /// Y pivot of a tile stamp (0-1)
    /// </summary>
    public float PivotY { get; set; }

    /// <summary>
    /// Pattern width & height. Should only be 1,3,5 or 7.
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// Array of all the tile IDs. They are used randomly or as stamps, based on tileMode value.
    /// </summary>
    public int[] TileIds { get; set; }

    /// <summary>
    /// Defines how tileIds array is used Possible values: Single, Stamp
    /// </summary>
    public TileMode TileMode { get; set; }

    /// <summary>
    /// Unique Int identifier
    /// </summary>
    public int Uid { get; set; }

    /// <summary>
    /// X cell coord modulo
    /// </summary>
    public int XModulo { get; set; }

    /// <summary>
    /// X cell start offset
    /// </summary>
    public int XOffset { get; set; }

    /// <summary>
    /// Y cell coord modulo
    /// </summary>
    public int YModulo { get; set; }

    /// <summary>
    /// Y cell start offset
    /// </summary>
    public int YOffset { get; set; }
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
    /// Array of field definitions
    /// </summary>
    public FieldDefinition[] FieldDefs { get; set; }

    public float FillOpacity { get; set; }

    /// <summary>
    /// Pixel height
    /// </summary>
    public int Height { get; set; }

    public bool Hollow { get; set; }

    /// <summary>
    /// User defined unique identifier
    /// </summary>
    public string Identifier { get; set; }

    /// <summary>
    /// Only applies to entities resizable on both X/Y. If TRUE, the entity instance width/height
    /// will keep the same aspect ratio as the definition.
    /// </summary>
    public bool KeepAspectRatio { get; set; }

    /// <summary>
    /// Possible values: DiscardOldOnes, PreventAdding, MoveLastOne
    /// </summary>
    public LimitBehavior LimitBehavior { get; set; }

    /// <summary>
    /// If TRUE, the maxCount is a "per world" limit, if FALSE, it's a "per level". Possible
    /// values: PerLayer, PerLevel, PerWorld
    /// </summary>
    public LimitScope LimitScope { get; set; }

    public float LineOpacity { get; set; }

    /// <summary>
    /// Max instances count
    /// </summary>
    public int MaxCount { get; set; }

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
    /// Possible values: Rectangle, Ellipse, Tile, Cross
    /// </summary>
    public RenderMode RenderMode { get; set; }

    /// <summary>
    /// If TRUE, the entity instances will be resizable horizontally
    /// </summary>
    public bool ResizableX { get; set; }

    /// <summary>
    /// If TRUE, the entity instances will be resizable vertically
    /// </summary>
    public bool ResizableY { get; set; }

    /// <summary>
    /// Display entity name in editor
    /// </summary>
    public bool ShowName { get; set; }

    /// <summary>
    /// An array of strings that classifies this entity
    /// </summary>
    public string[] Tags { get; set; }

    /// <summary>
    /// WARNING: this deprecated value will be removed completely on version 1.2.0+
    /// Replaced by: tileRect
    /// </summary>
    public int? TileId { get; set; }

    public float TileOpacity { get; set; }

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
/// This section is mostly only intended for the LDtk editor app itself. You can safely
/// ignore it.
/// </summary>
public partial class FieldDefinition
{
    /// <summary>
    /// Human readable value type. Possible values: Int, Float, String, Bool, Color,
    /// ExternEnum.XXX, LocalEnum.XXX, Point, FilePath. If the field is an array, this
    /// field will look like <![CDATA[ Array<...> (eg. Array<Int>, Array<Point> ]]> etc.) NOTE: if
    /// you enable the advanced option Use Multilines type, you will have "Multilines"
    /// instead of "String" when relevant.
    /// </summary>
    public string _Type { get; set; }

    /// <summary>
    /// Optional list of accepted file extensions for FilePath value type. Includes the dot:
    /// .ext
    /// </summary>
    public string[] AcceptFileTypes { get; set; }

    public bool AllowOutOfLevelRef { get; set; }
    public string[] AllowedRefTags { get; set; }

    /// <summary>
    /// Possible values: Any, OnlySame, OnlyTags
    /// </summary>
    public AllowedRefs AllowedRefs { get; set; }

    /// <summary>
    /// Array max length
    /// </summary>
    public int? ArrayMaxLength { get; set; }

    /// <summary>
    /// Array min length
    /// </summary>
    public int? ArrayMinLength { get; set; }

    public bool AutoChainRef { get; set; }

    /// <summary>
    /// TRUE if the value can be null. For arrays, TRUE means it can contain null values
    /// (exception: array of Points can't have null values).
    /// </summary>
    public bool CanBeNull { get; set; }

    /// <summary>
    /// Default value if selected value is null or invalid.
    /// </summary>
    public object DefaultOverride { get; set; }

    public bool EditorAlwaysShow { get; set; }
    public bool EditorCutLongValues { get; set; }

    /// <summary>
    /// Possible values: Hidden, ValueOnly, NameAndValue, EntityTile, Points,
    /// PointStar, PointPath, PointPathLoop, RadiusPx, RadiusGrid,
    /// ArrayCountWithLabel, ArrayCountNoLabel, RefLinkBetweenPivots,
    /// RefLinkBetweenCenters
    /// </summary>
    public EditorDisplayMode EditorDisplayMode { get; set; }

    /// <summary>
    /// Possible values: Above, Center, Beneath
    /// </summary>
    public EditorDisplayPos EditorDisplayPos { get; set; }

    public string EditorTextPrefix { get; set; }
    public string EditorTextSuffix { get; set; }

    /// <summary>
    /// User defined unique identifier
    /// </summary>
    public string Identifier { get; set; }

    /// <summary>
    /// TRUE if the value is an array of multiple values
    /// </summary>
    public bool IsArray { get; set; }

    /// <summary>
    /// Max limit for value, if applicable
    /// </summary>
    public float? Max { get; set; }

    /// <summary>
    /// Min limit for value, if applicable
    /// </summary>
    public float? Min { get; set; }

    /// <summary>
    /// Optional regular expression that needs to be matched to accept values. Expected format:
    /// /some_reg_ex/g, with optional "i" flag.
    /// </summary>
    public string Regex { get; set; }

    public bool SymmetricalRef { get; set; }

    /// <summary>
    /// Possible values: &lt;null&gt;, LangPython, LangRuby, LangJS, LangLua, LangC,
    /// LangHaxe, LangMarkdown, LangJson, LangXml, LangLog
    /// </summary>
    public TextLanguageMode? TextLanguageMode { get; set; }

    /// <summary>
    /// UID of the tileset used for a Tile
    /// </summary>
    public int? TilesetUid { get; set; }

    /// <summary>
    /// Internal enum representing the possible field types. Possible values: F_Int, F_Float,
    /// F_String, F_Text, F_Bool, F_Color, F_Enum(...), F_Point, F_Path, F_EntityRef, F_Tile
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Unique Int identifier
    /// </summary>
    public int Uid { get; set; }

    /// <summary>
    /// If TRUE, the color associated with this field will override the Entity or Level default
    /// color in the editor UI. For Enum fields, this would be the color associated to their
    /// values.
    /// </summary>
    public bool UseForSmartColor { get; set; }
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
    public string ExternalFileChecksum { get; set; }

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

    /// <summary>
    /// Contains all the auto-layer rule definitions.
    /// </summary>
    public AutoLayerRuleGroup[] AutoRuleGroups { get; set; }

    public int? AutoSourceLayerDefUid { get; set; }

    /// <summary>
    /// WARNING: this deprecated value will be removed completely on version 1.2.0+
    /// Replaced by: tilesetDefUid
    /// </summary>
    public int? AutoTilesetDefUid { get; set; }

    /// <summary>
    /// Opacity of the layer (0 to 1.0)
    /// </summary>
    public float DisplayOpacity { get; set; }

    /// <summary>
    /// An array of tags to forbid some Entities in this layer
    /// </summary>
    public string[] ExcludedTags { get; set; }

    /// <summary>
    /// Width and height of the grid in pixels
    /// </summary>
    public int GridSize { get; set; }

    /// <summary>
    /// Height of the optional "guide" grid in pixels
    /// </summary>
    public int GuideGridHei { get; set; }

    /// <summary>
    /// Width of the optional "guide" grid in pixels
    /// </summary>
    public int GuideGridWid { get; set; }

    public bool HideFieldsWhenInactive { get; set; }

    /// <summary>
    /// Hide the layer from the list on the side of the editor view.
    /// </summary>
    public bool HideInList { get; set; }

    /// <summary>
    /// User defined unique identifier
    /// </summary>
    public string Identifier { get; set; }

    /// <summary>
    /// Alpha of this layer when it is not the active one.
    /// </summary>
    public float InactiveOpacity { get; set; }

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
    /// An array of tags to filter Entities that can be added to this layer
    /// </summary>
    public string[] RequiredTags { get; set; }

    /// <summary>
    /// If the tiles are smaller or larger than the layer grid, the pivot value will be used to
    /// position the tile relatively its grid cell.
    /// </summary>
    public float TilePivotX { get; set; }

    /// <summary>
    /// If the tiles are smaller or larger than the layer grid, the pivot value will be used to
    /// position the tile relatively its grid cell.
    /// </summary>
    public float TilePivotY { get; set; }

    /// <summary>
    /// Reference to the default Tileset UID being used by this layer definition.
    /// WARNING: some layer instances might use a different tileset. So most of the time,
    /// you should probably use the __tilesetDefUid value found in layer
    /// instances. Note: since version 1.0.0, the old autoTilesetDefUid was removed and
    /// merged into this value.
    /// </summary>
    public int? TilesetDefUid { get; set; }

    /// <summary>
    /// Type of the layer as Haxe Enum Possible values: IntGrid, Entities, Tiles,
    /// AutoLayer
    /// </summary>
    public LayerType Type { get; set; }

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
    /// The following data is used internally for various optimizations. It's always synced with
    /// source image changes.
    /// </summary>
    public System.Collections.Generic.Dictionary<string, object> CachedPixelData { get; set; }

    /// <summary>
    /// An array of custom tile metadata
    /// </summary>
    public TileCustomMetadata[] CustomData { get; set; }

    /// <summary>
    /// If this value is set, then it means that this atlas uses an internal LDtk atlas image
    /// instead of a loaded one. Possible values: &lt;null&gt;, LdtkIcons
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
    /// Array of group of tiles selections, only meant to be used in the editor
    /// </summary>
    public System.Collections.Generic.Dictionary<string, object>[] SavedSelections { get; set; }

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

    /// <summary>
    /// Editor internal raw values
    /// </summary>
    public object[] RealEditorValues { get; set; }
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
    /// The "guessed" color for this level in the editor, decided using either the background
    /// color or an existing custom field.
    /// </summary>
    public string _SmartColor { get; set; }

    /// <summary>
    /// Background color of the level. If null, the project defaultLevelBgColor should be
    /// used.
    /// </summary>
    public Color BgColor { get; set; }

    /// <summary>
    /// Background image X pivot (0-1)
    /// </summary>
    public float BgPivotX { get; set; }

    /// <summary>
    /// Background image Y pivot (0-1)
    /// </summary>
    public float BgPivotY { get; set; }

    /// <summary>
    /// An enum defining the way the background image (if any) is positioned on the level. See
    /// __bgPos for resulting position info. Possible values: &lt;null&gt;,
    /// Unscaled, Contain, Cover, CoverDirty
    /// </summary>
    public BgPos? BgPos { get; set; }

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
    /// If TRUE, the level identifier will always automatically use the naming pattern as defined
    /// in Project.levelNamePattern. Becomes FALSE if the identifier is manually modified by
    /// user.
    /// </summary>
    public bool UseAutoIdentifier { get; set; }

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

    /// <summary>
    /// WARNING: this deprecated value will be removed completely on version 1.2.0+
    /// Replaced by: levelIid
    /// </summary>
    public int? LevelUid { get; set; }
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
    /// WARNING: this deprecated value is no inter exported since version 1.0.0  Replaced
    /// by: intGridCsv
    /// </summary>
    public IntGridValueInstance[] IntGrid { get; set; }

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
    /// An Array containing the UIDs of optional rules that were enabled in this specific layer
    /// instance.
    /// </summary>
    public int[] OptionalRules { get; set; }

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
    /// Random seed used for Auto-Layers rendering
    /// </summary>
    public int Seed { get; set; }

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
    /// Internal data used by the editor. For auto-layer tiles: [ruleId, coordId].
    /// For tile-layer tiles: [coordId].
    /// </summary>
    public int[] D { get; set; }

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
    /// Default new level height
    /// </summary>
    public int DefaultLevelHeight { get; set; }

    /// <summary>
    /// Default new level width
    /// </summary>
    public int DefaultLevelWidth { get; set; }

    /// <summary>
    /// User defined unique identifier
    /// </summary>
    public string Identifier { get; set; }

    /// <summary>
    /// Unique instance identifer
    /// </summary>
    public Guid Iid { get; set; }

    /// <summary>
    /// All levels from this world. The order of this array is only relevant in
    /// LinearHorizontal and linearVertical world layouts (see worldLayout value).
    /// Otherwise, you should refer to the worldX,worldY coordinates of each Level.
    /// </summary>
    public LDtkLevel[] Levels { get; set; }

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
    /// space). Possible values: Free, GridVania, LinearHorizontal, LinearVertical, null
    /// </summary>
    public WorldLayout? WorldLayout { get; set; }
}

/// <summary>
/// Checker mode Possible values: None, Horizontal, Vertical
/// </summary>
public enum Checker { Horizontal, None, Vertical };

/// <summary>
/// Defines how tileIds array is used Possible values: Single, Stamp
/// </summary>
public enum TileMode { Single, Stamp };

/// <summary>
/// Possible values: Any, OnlySame, OnlyTags
/// </summary>
public enum AllowedRefs { Any, OnlySame, OnlyTags };

/// <summary>
/// Possible values: Hidden, ValueOnly, NameAndValue, EntityTile, Points,
/// PointStar, PointPath, PointPathLoop, RadiusPx, RadiusGrid,
/// ArrayCountWithLabel, ArrayCountNoLabel, RefLinkBetweenPivots,
/// RefLinkBetweenCenters
/// </summary>
public enum EditorDisplayMode { ArrayCountNoLabel, ArrayCountWithLabel, EntityTile, Hidden, NameAndValue, PointPath, PointPathLoop, PointStar, Points, RadiusGrid, RadiusPx, RefLinkBetweenCenters, RefLinkBetweenPivots, ValueOnly };

/// <summary>
/// Possible values: Above, Center, Beneath
/// </summary>
public enum EditorDisplayPos { Above, Beneath, Center };

public enum TextLanguageMode { LangC, LangHaxe, LangJs, LangJson, LangLog, LangLua, LangMarkdown, LangPython, LangRuby, LangXml };

/// <summary>
/// Possible values: DiscardOldOnes, PreventAdding, MoveLastOne
/// </summary>
public enum LimitBehavior { DiscardOldOnes, MoveLastOne, PreventAdding };

/// <summary>
/// If TRUE, the maxCount is a "per world" limit, if FALSE, it's a "per level". Possible
/// values: PerLayer, PerLevel, PerWorld
/// </summary>
public enum LimitScope { PerLayer, PerLevel, PerWorld };

/// <summary>
/// Possible values: Rectangle, Ellipse, Tile, Cross
/// </summary>
public enum RenderMode { Cross, Ellipse, Rectangle, Tile };

/// <summary>
/// An enum describing how the the Entity tile is rendered inside the Entity bounds. Possible
/// values: Cover, FitInside, Repeat, Stretch, FullSizeCropped,
/// FullSizeUncropped, NineSlice
/// </summary>
public enum TileRenderMode { Cover, FitInside, FullSizeCropped, FullSizeUncropped, NineSlice, Repeat, Stretch };


public enum EmbedAtlas { LdtkIcons };

public enum BgPos { Contain, Cover, CoverDirty, Unscaled };

public enum WorldLayout { Free, GridVania, LinearHorizontal, LinearVertical };

public enum Flag { DiscardPreCsvIntGrid, ExportPreCsvIntGridFormat, IgnoreBackupSuggest, MultiWorlds, PrependIndexToLevelFileNames, UseMultilinesType };

/// <summary>
/// Naming convention for Identifiers (first-letter uppercase, full uppercase etc.) Possible
/// values: Capitalize, Uppercase, Lowercase, Free
/// </summary>
public enum IdentifierStyle { Capitalize, Free, Lowercase, Uppercase };

/// <summary>
/// "Image export" option when saving project. Possible values: None, OneImagePerLayer,
/// OneImagePerLevel, LayersAndLevels
/// </summary>
public enum ImageExportMode { LayersAndLevels, None, OneImagePerLayer, OneImagePerLevel };
#pragma warning restore
