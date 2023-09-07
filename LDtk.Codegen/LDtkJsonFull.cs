#pragma warning disable CS1591, IDE1006, CA1707, CA1716, IDE0130, CA1720, CA1711
// This file was auto generated, any changes will be lost. For LDtk 1.3.3
namespace LDtk.Codegen;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.Xna.Framework;

/// <summary>
/// This file is a JSON schema of files created by LDtk level editor (https://ldtk.io).
/// <br/>
/// This is the root of any Project JSON file. It contains:  - the project settings, - an
/// array of levels, - a group of definitions (that can probably be safely ignored for most
/// users).
/// </summary>
public partial class LDtkFile
{
    /// <summary>
    /// LDtk application build identifier.<br/>  This is only used to identify the LDtk version
    /// that generated this particular project file, which can be useful for specific bug fixing.
    /// Note that the build identifier is just the date of the release, so it's not unique to
    /// each user (one single global ID per LDtk public release), and as a result, completely
    /// anonymous.
    /// </summary>
    [JsonPropertyName("appBuildId")]
    public float AppBuildId { get; set; }

    /// <summary>
    /// Number of backup files to keep, if the backupOnSave is TRUE
    /// </summary>
    [JsonPropertyName("backupLimit")]
    public int BackupLimit { get; set; }

    /// <summary>
    /// If TRUE, an extra copy of the project will be created in a sub folder, when saving.
    /// </summary>
    [JsonPropertyName("backupOnSave")]
    public bool BackupOnSave { get; set; }

    /// <summary>
    /// Target relative path to store backup files
    /// </summary>
    [JsonPropertyName("backupRelPath")]
    public string BackupRelPath { get; set; }

    /// <summary>
    /// Project background color
    /// </summary>
    [JsonPropertyName("bgColor")]
    public Color BgColor { get; set; }

    /// <summary>
    /// An array of command lines that can be ran manually by the user
    /// </summary>
    [JsonPropertyName("customCommands")]
    public LdtkCustomCommand[] CustomCommands { get; set; }

    /// <summary>
    /// Default height for new entities
    /// </summary>
    [JsonPropertyName("defaultEntityHeight")]
    public int DefaultEntityHeight { get; set; }

    /// <summary>
    /// Default width for new entities
    /// </summary>
    [JsonPropertyName("defaultEntityWidth")]
    public int DefaultEntityWidth { get; set; }

    /// <summary>
    /// Default grid size for new layers
    /// </summary>
    [JsonPropertyName("defaultGridSize")]
    public int DefaultGridSize { get; set; }

    /// <summary>
    /// Default background color of levels
    /// </summary>
    [JsonPropertyName("defaultLevelBgColor")]
    public string DefaultLevelBgColor { get; set; }

    /// <summary>
    /// WARNING: this field will move to the worlds array after the "multi-worlds" update.
    /// It will then be null. You can enable the Multi-worlds advanced project option to enable
    /// the change immediately.<br/>  Default new level height
    /// </summary>
    [JsonPropertyName("defaultLevelHeight")]
    public int? DefaultLevelHeight { get; set; }

    /// <summary>
    /// WARNING: this field will move to the worlds array after the "multi-worlds" update.
    /// It will then be null. You can enable the Multi-worlds advanced project option to enable
    /// the change immediately.<br/>  Default new level width
    /// </summary>
    [JsonPropertyName("defaultLevelWidth")]
    public int? DefaultLevelWidth { get; set; }

    /// <summary>
    /// Default X pivot (0 to 1) for new entities
    /// </summary>
    [JsonPropertyName("defaultPivotX")]
    public float DefaultPivotX { get; set; }

    /// <summary>
    /// Default Y pivot (0 to 1) for new entities
    /// </summary>
    [JsonPropertyName("defaultPivotY")]
    public float DefaultPivotY { get; set; }

    /// <summary>
    /// A structure containing all the definitions of this project
    /// </summary>
    [JsonPropertyName("defs")]
    public Definitions Defs { get; set; }

    /// <summary>
    /// If the project isn't in MultiWorlds mode, this is the Guid of the internal "dummy" World.
    /// </summary>
    [JsonPropertyName("dummyWorldIid")]
    public Guid DummyWorldIid { get; set; }

    /// <summary>
    /// If TRUE, the exported PNGs will include the level background (color or image).
    /// </summary>
    [JsonPropertyName("exportLevelBg")]
    public bool ExportLevelBg { get; set; }

    /// <summary>
    /// WARNING: this deprecated value is no longer exported since version 0.9.3  Replaced
    /// by: imageExportMode
    /// </summary>
    [JsonPropertyName("exportPng")]
    public bool? ExportPng { get; set; }

    /// <summary>
    /// If TRUE, a Tiled compatible file will also be generated along with the LDtk JSON file
    /// (default is FALSE)
    /// </summary>
    [JsonPropertyName("exportTiled")]
    public bool ExportTiled { get; set; }

    /// <summary>
    /// If TRUE, one file will be saved for the project (incl. all its definitions) and one file
    /// in a sub-folder for each level.
    /// </summary>
    [JsonPropertyName("externalLevels")]
    public bool ExternalLevels { get; set; }

    /// <summary>
    /// An array containing various advanced flags (ie. options or other states). Possible
    /// values: DiscardPreCsvIntGrid, ExportPreCsvIntGridFormat, IgnoreBackupSuggest,
    /// PrependIndexToLevelFileNames, MultiWorlds, UseMultilinesType
    /// </summary>
    [JsonPropertyName("flags")]
    public Flag[] Flags { get; set; }

    /// <summary>
    /// This object is not actually used by LDtk. It ONLY exists to force explicit references to
    /// all types, to make sure QuickType finds them and integrate all of them. Otherwise,
    /// Quicktype will drop types that are not explicitely used.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("__FORCED_REFS")]
    public ForcedRefs _ForcedRefs { get; set; }

    /// <summary>
    /// Naming convention for Identifiers (first-letter uppercase, full uppercase etc.) Possible
    /// values: Capitalize, Uppercase, Lowercase, Free
    /// </summary>
    [JsonPropertyName("identifierStyle")]
    public IdentifierStyle IdentifierStyle { get; set; }

    /// <summary>
    /// Unique project identifier
    /// </summary>
    [JsonPropertyName("iid")]
    public Guid Iid { get; set; }

    /// <summary>
    /// "Image export" option when saving project. Possible values: None, OneImagePerLayer,
    /// OneImagePerLevel, LayersAndLevels
    /// </summary>
    [JsonPropertyName("imageExportMode")]
    public ImageExportMode ImageExportMode { get; set; }

    /// <summary>
    /// File format version
    /// </summary>
    [JsonPropertyName("jsonVersion")]
    public string JsonVersion { get; set; }

    /// <summary>
    /// The default naming convention for level identifiers.
    /// </summary>
    [JsonPropertyName("levelNamePattern")]
    public string LevelNamePattern { get; set; }

    /// <summary>
    /// All levels. The order of this array is only relevant in LinearHorizontal and
    /// linearVertical world layouts (see worldLayout value).<br/>  Otherwise, you should
    /// refer to the worldX,worldY coordinates of each Level.
    /// </summary>
    [JsonPropertyName("levels")]
    public LDtkLevel[] Levels { get; set; }

    /// <summary>
    /// If TRUE, the Json is partially minified (no indentation, nor line breaks, default is
    /// FALSE)
    /// </summary>
    [JsonPropertyName("minifyJson")]
    public bool MinifyJson { get; set; }

    /// <summary>
    /// Next Unique integer ID available
    /// </summary>
    [JsonPropertyName("nextUid")]
    public int NextUid { get; set; }

    /// <summary>
    /// File naming pattern for exported PNGs
    /// </summary>
    [JsonPropertyName("pngFilePattern")]
    public string PngFilePattern { get; set; }

    /// <summary>
    /// If TRUE, a very simplified will be generated on saving, for quicker & easier engine
    /// integration.
    /// </summary>
    [JsonPropertyName("simplifiedExport")]
    public bool SimplifiedExport { get; set; }

    /// <summary>
    /// All instances of entities that have their exportToToc flag enabled are listed in this
    /// array.
    /// </summary>
    [JsonPropertyName("toc")]
    public LdtkTableOfContentEntry[] Toc { get; set; }

    /// <summary>
    /// This optional description is used by LDtk Samples to show up some informations and
    /// instructions.
    /// </summary>
    [JsonPropertyName("tutorialDesc")]
    public string TutorialDesc { get; set; }

    /// <summary>
    /// WARNING: this field will move to the worlds array after the "multi-worlds" update.
    /// It will then be null. You can enable the Multi-worlds advanced project option to enable
    /// the change immediately.<br/>  Height of the world grid in pixels.
    /// </summary>
    [JsonPropertyName("worldGridHeight")]
    public int? WorldGridHeight { get; set; }

    /// <summary>
    /// WARNING: this field will move to the worlds array after the "multi-worlds" update.
    /// It will then be null. You can enable the Multi-worlds advanced project option to enable
    /// the change immediately.<br/>  Width of the world grid in pixels.
    /// </summary>
    [JsonPropertyName("worldGridWidth")]
    public int? WorldGridWidth { get; set; }

    /// <summary>
    /// WARNING: this field will move to the worlds array after the "multi-worlds" update.
    /// It will then be null. You can enable the Multi-worlds advanced project option to enable
    /// the change immediately.<br/>  An enum that describes how levels are organized in
    /// this project (ie. linearly or in a 2D space). Possible values:  &lt; null &gt; , Free,
    /// GridVania, LinearHorizontal, LinearVertical
    /// </summary>
    [JsonPropertyName("worldLayout")]
    public WorldLayout? WorldLayout { get; set; }

    /// <summary>
    /// This array will be empty, unless you enable the Multi-Worlds in the project advanced
    /// settings.<br/> - in current version, a LDtk project file can only contain a single
    /// world with multiple levels in it. In this case, levels and world layout related settings
    /// are stored in the root of the JSON.<br/> - with "Multi-worlds" enabled, there will be a
    /// worlds array in root, each world containing levels and layout settings. Basically, it's
    /// pretty much only about moving the levels array to the worlds array, along with world
    /// layout related values (eg. worldGridWidth etc).<br/>If you want to start
    /// supporting this future update easily, please refer to this documentation:
    /// https://github.com/deepnight/ldtk/issues/231
    /// </summary>
    [JsonPropertyName("worlds")]
    public LDtkWorld[] Worlds { get; set; }
}

public partial class LdtkCustomCommand
{
    [JsonPropertyName("command")]
    public string Command { get; set; }

    /// <summary>
    /// Possible values: Manual, AfterLoad, BeforeSave, AfterSave
    /// </summary>
    [JsonPropertyName("when")]
    public When When { get; set; }
}

/// <summary>
/// If you're writing your own LDtk importer, you should probably just ignore most stuff in
/// the defs section, as it contains data that are mostly important to the editor. To keep
/// you away from the defs section and avoid some unnecessary JSON parsing, important data
/// from definitions is often duplicated in fields prefixed with a double underscore (eg.
/// __identifier or __type).  The 2 only definition types you might need here are
/// Tilesets and Enums.
/// <br/>
/// A structure containing all the definitions of this project
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
    /// Note: external enums are exactly the same as enums, except they have a relPath to
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
    /// All custom fields available to all levels.
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
    /// User defined documentation for this element to provide help/tips to level designers.
    /// </summary>
    [JsonPropertyName("doc")]
    public string Doc { get; set; }

    /// <summary>
    /// If enabled, all instances of this entity will be listed in the project "Table of content"
    /// object.
    /// </summary>
    [JsonPropertyName("exportToToc")]
    public bool ExportToToc { get; set; }

    /// <summary>
    /// Array of field definitions
    /// </summary>
    [JsonPropertyName("fieldDefs")]
    public FieldDefinition[] FieldDefs { get; set; }

    [JsonPropertyName("fillOpacity")]
    public float FillOpacity { get; set; }

    /// <summary>
    /// Pixel height
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("hollow")]
    public bool Hollow { get; set; }

    /// <summary>
    /// User defined unique identifier
    /// </summary>
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    /// <summary>
    /// Only applies to entities resizable on both X/Y. If TRUE, the entity instance width/height
    /// will keep the same aspect ratio as the definition.
    /// </summary>
    [JsonPropertyName("keepAspectRatio")]
    public bool KeepAspectRatio { get; set; }

    /// <summary>
    /// Possible values: DiscardOldOnes, PreventAdding, MoveLastOne
    /// </summary>
    [JsonPropertyName("limitBehavior")]
    public LimitBehavior LimitBehavior { get; set; }

    /// <summary>
    /// If TRUE, the maxCount is a "per world" limit, if FALSE, it's a "per level". Possible
    /// values: PerLayer, PerLevel, PerWorld
    /// </summary>
    [JsonPropertyName("limitScope")]
    public LimitScope LimitScope { get; set; }

    [JsonPropertyName("lineOpacity")]
    public float LineOpacity { get; set; }

    /// <summary>
    /// Max instances count
    /// </summary>
    [JsonPropertyName("maxCount")]
    public int MaxCount { get; set; }

    /// <summary>
    /// Max pixel height (only applies if the entity is resizable on Y)
    /// </summary>
    [JsonPropertyName("maxHeight")]
    public int? MaxHeight { get; set; }

    /// <summary>
    /// Max pixel width (only applies if the entity is resizable on X)
    /// </summary>
    [JsonPropertyName("maxWidth")]
    public int? MaxWidth { get; set; }

    /// <summary>
    /// Min pixel height (only applies if the entity is resizable on Y)
    /// </summary>
    [JsonPropertyName("minHeight")]
    public int? MinHeight { get; set; }

    /// <summary>
    /// Min pixel width (only applies if the entity is resizable on X)
    /// </summary>
    [JsonPropertyName("minWidth")]
    public int? MinWidth { get; set; }

    /// <summary>
    /// An array of 4 dimensions for the up/right/down/left borders (in this order) when using
    /// 9-slice mode for tileRenderMode.<br/>  If the tileRenderMode is not NineSlice, then
    /// this array is empty.<br/>  See: https://en.wikipedia.org/wiki/9-slice_scaling
    /// </summary>
    [JsonPropertyName("nineSliceBorders")]
    public int[] NineSliceBorders { get; set; }

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
    /// Possible values: Rectangle, Ellipse, Tile, Cross
    /// </summary>
    [JsonPropertyName("renderMode")]
    public RenderMode RenderMode { get; set; }

    /// <summary>
    /// If TRUE, the entity instances will be resizable horizontally
    /// </summary>
    [JsonPropertyName("resizableX")]
    public bool ResizableX { get; set; }

    /// <summary>
    /// If TRUE, the entity instances will be resizable vertically
    /// </summary>
    [JsonPropertyName("resizableY")]
    public bool ResizableY { get; set; }

    /// <summary>
    /// Display entity name in editor
    /// </summary>
    [JsonPropertyName("showName")]
    public bool ShowName { get; set; }

    /// <summary>
    /// An array of strings that classifies this entity
    /// </summary>
    [JsonPropertyName("tags")]
    public string[] Tags { get; set; }

    /// <summary>
    /// WARNING: this deprecated value is no longer exported since version 1.2.0  Replaced
    /// by: tileRect
    /// </summary>
    [JsonPropertyName("tileId")]
    public int? TileId { get; set; }

    [JsonPropertyName("tileOpacity")]
    public float TileOpacity { get; set; }

    /// <summary>
    /// An object representing a rectangle from an existing Tileset
    /// </summary>
    [JsonPropertyName("tileRect")]
    public TilesetRectangle TileRect { get; set; }

    /// <summary>
    /// An enum describing how the the Entity tile is rendered inside the Entity bounds. Possible
    /// values: Cover, FitInside, Repeat, Stretch, FullSizeCropped,
    /// FullSizeUncropped, NineSlice
    /// </summary>
    [JsonPropertyName("tileRenderMode")]
    public TileRenderMode TileRenderMode { get; set; }

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

/// <summary>
/// This section is mostly only intended for the LDtk editor app itself. You can safely
/// ignore it.
/// </summary>
public partial class FieldDefinition
{
    /// <summary>
    /// Optional list of accepted file extensions for FilePath value type. Includes the dot:
    /// .ext
    /// </summary>
    [JsonPropertyName("acceptFileTypes")]
    public string[] AcceptFileTypes { get; set; }

    [JsonPropertyName("allowOutOfLevelRef")]
    public bool AllowOutOfLevelRef { get; set; }

    [JsonPropertyName("allowedRefTags")]
    public string[] AllowedRefTags { get; set; }

    /// <summary>
    /// Possible values: Any, OnlySame, OnlyTags, OnlySpecificEntity
    /// </summary>
    [JsonPropertyName("allowedRefs")]
    public AllowedRefs AllowedRefs { get; set; }

    [JsonPropertyName("allowedRefsEntityUid")]
    public int? AllowedRefsEntityUid { get; set; }

    /// <summary>
    /// Array max length
    /// </summary>
    [JsonPropertyName("arrayMaxLength")]
    public int? ArrayMaxLength { get; set; }

    /// <summary>
    /// Array min length
    /// </summary>
    [JsonPropertyName("arrayMinLength")]
    public int? ArrayMinLength { get; set; }

    [JsonPropertyName("autoChainRef")]
    public bool AutoChainRef { get; set; }

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

    /// <summary>
    /// User defined documentation for this field to provide help/tips to level designers about
    /// accepted values.
    /// </summary>
    [JsonPropertyName("doc")]
    public string Doc { get; set; }

    [JsonPropertyName("editorAlwaysShow")]
    public bool EditorAlwaysShow { get; set; }

    [JsonPropertyName("editorCutLongValues")]
    public bool EditorCutLongValues { get; set; }

    [JsonPropertyName("editorDisplayColor")]
    public string EditorDisplayColor { get; set; }

    /// <summary>
    /// Possible values: Hidden, ValueOnly, NameAndValue, EntityTile, LevelTile,
    /// Points, PointStar, PointPath, PointPathLoop, RadiusPx, RadiusGrid,
    /// ArrayCountWithLabel, ArrayCountNoLabel, RefLinkBetweenPivots,
    /// RefLinkBetweenCenters
    /// </summary>
    [JsonPropertyName("editorDisplayMode")]
    public EditorDisplayMode EditorDisplayMode { get; set; }

    /// <summary>
    /// Possible values: Above, Center, Beneath
    /// </summary>
    [JsonPropertyName("editorDisplayPos")]
    public EditorDisplayPos EditorDisplayPos { get; set; }

    [JsonPropertyName("editorDisplayScale")]
    public float EditorDisplayScale { get; set; }

    /// <summary>
    /// Possible values: ZigZag, StraightArrow, CurvedArrow, ArrowsLine, DashedLine
    /// </summary>
    [JsonPropertyName("editorLinkStyle")]
    public EditorLinkStyle EditorLinkStyle { get; set; }

    [JsonPropertyName("editorShowInWorld")]
    public bool EditorShowInWorld { get; set; }

    [JsonPropertyName("editorTextPrefix")]
    public string EditorTextPrefix { get; set; }

    [JsonPropertyName("editorTextSuffix")]
    public string EditorTextSuffix { get; set; }

    /// <summary>
    /// Internal enum representing the possible field types. Possible values: F_Int, F_Float,
    /// F_String, F_Text, F_Bool, F_Color, F_Enum(...), F_Point, F_Path, F_EntityRef, F_Tile
    /// </summary>
    [JsonPropertyName("type")]
    public string FieldDefinitionType { get; set; }

    /// <summary>
    /// User defined unique identifier
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
    public float? Max { get; set; }

    /// <summary>
    /// Min limit for value, if applicable
    /// </summary>
    [JsonPropertyName("min")]
    public float? Min { get; set; }

    /// <summary>
    /// Optional regular expression that needs to be matched to accept values. Expected format:
    /// /some_reg_ex/g, with optional "i" flag.
    /// </summary>
    [JsonPropertyName("regex")]
    public string Regex { get; set; }

    [JsonPropertyName("symmetricalRef")]
    public bool SymmetricalRef { get; set; }

    /// <summary>
    /// Possible values:  &lt; null &gt; , LangPython, LangRuby, LangJS, LangLua, LangC,
    /// LangHaxe, LangMarkdown, LangJson, LangXml, LangLog
    /// </summary>
    [JsonPropertyName("textLanguageMode")]
    public TextLanguageMode? TextLanguageMode { get; set; }

    /// <summary>
    /// UID of the tileset used for a Tile
    /// </summary>
    [JsonPropertyName("tilesetUid")]
    public int? TilesetUid { get; set; }

    /// <summary>
    /// Human readable value type. Possible values: Int, Float, String, Bool, Color,
    /// ExternEnum.XXX, LocalEnum.XXX, Point, FilePath.<br/>  If the field is an array, this
    /// field will look like <![CDATA[ Array<...> (eg. Array<Int>, Array<Point> ]]> etc.)<br/>  NOTE: if
    /// you enable the advanced option Use Multilines type, you will have "Multilines"
    /// instead of "String" when relevant.
    /// </summary>
    [JsonPropertyName("__type")]
    public string _Type { get; set; }

    /// <summary>
    /// Unique Int identifier
    /// </summary>
    [JsonPropertyName("uid")]
    public int Uid { get; set; }

    /// <summary>
    /// If TRUE, the color associated with this field will override the Entity or Level default
    /// color in the editor UI. For Enum fields, this would be the color associated to their
    /// values.
    /// </summary>
    [JsonPropertyName("useForSmartColor")]
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
    [JsonPropertyName("h")]
    public int H { get; set; }

    /// <summary>
    /// UID of the tileset
    /// </summary>
    [JsonPropertyName("tilesetUid")]
    public int TilesetUid { get; set; }

    /// <summary>
    /// Width in pixels
    /// </summary>
    [JsonPropertyName("w")]
    public int W { get; set; }

    /// <summary>
    /// X pixels coordinate of the top-left corner in the Tileset image
    /// </summary>
    [JsonPropertyName("x")]
    public int X { get; set; }

    /// <summary>
    /// Y pixels coordinate of the top-left corner in the Tileset image
    /// </summary>
    [JsonPropertyName("y")]
    public int Y { get; set; }
}

public partial class EnumDefinition
{
    [JsonPropertyName("externalFileChecksum")]
    public string ExternalFileChecksum { get; set; }

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
    /// User defined unique identifier
    /// </summary>
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    /// <summary>
    /// An array of user-defined tags to organize the Enums
    /// </summary>
    [JsonPropertyName("tags")]
    public string[] Tags { get; set; }

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
    /// WARNING: this deprecated value will be removed completely on version 1.4.0+
    /// Replaced by: tileRect
    /// </summary>
    [JsonPropertyName("tileId")]
    public int? TileId { get; set; }

    /// <summary>
    /// Optional tileset rectangle to represents this value
    /// </summary>
    [JsonPropertyName("tileRect")]
    public TilesetRectangle TileRect { get; set; }

    /// <summary>
    /// WARNING: this deprecated value will be removed completely on version 1.4.0+
    /// Replaced by: tileRect
    /// </summary>
    [JsonPropertyName("__tileSrcRect")]
    public int[] _TileSrcRect { get; set; }
}

public partial class LayerDefinition
{
    /// <summary>
    /// Contains all the auto-layer rule definitions.
    /// </summary>
    [JsonPropertyName("autoRuleGroups")]
    public AutoLayerRuleGroup[] AutoRuleGroups { get; set; }

    [JsonPropertyName("autoSourceLayerDefUid")]
    public int? AutoSourceLayerDefUid { get; set; }

    /// <summary>
    /// WARNING: this deprecated value is no longer exported since version 1.2.0  Replaced
    /// by: tilesetDefUid
    /// </summary>
    [JsonPropertyName("autoTilesetDefUid")]
    public int? AutoTilesetDefUid { get; set; }

    /// <summary>
    /// Allow editor selections when the layer is not currently active.
    /// </summary>
    [JsonPropertyName("canSelectWhenInactive")]
    public bool CanSelectWhenInactive { get; set; }

    /// <summary>
    /// Opacity of the layer (0 to 1.0)
    /// </summary>
    [JsonPropertyName("displayOpacity")]
    public float DisplayOpacity { get; set; }

    /// <summary>
    /// User defined documentation for this element to provide help/tips to level designers.
    /// </summary>
    [JsonPropertyName("doc")]
    public string Doc { get; set; }

    /// <summary>
    /// An array of tags to forbid some Entities in this layer
    /// </summary>
    [JsonPropertyName("excludedTags")]
    public string[] ExcludedTags { get; set; }

    /// <summary>
    /// Width and height of the grid in pixels
    /// </summary>
    [JsonPropertyName("gridSize")]
    public int GridSize { get; set; }

    /// <summary>
    /// Height of the optional "guide" grid in pixels
    /// </summary>
    [JsonPropertyName("guideGridHei")]
    public int GuideGridHei { get; set; }

    /// <summary>
    /// Width of the optional "guide" grid in pixels
    /// </summary>
    [JsonPropertyName("guideGridWid")]
    public int GuideGridWid { get; set; }

    [JsonPropertyName("hideFieldsWhenInactive")]
    public bool HideFieldsWhenInactive { get; set; }

    /// <summary>
    /// Hide the layer from the list on the side of the editor view.
    /// </summary>
    [JsonPropertyName("hideInList")]
    public bool HideInList { get; set; }

    /// <summary>
    /// User defined unique identifier
    /// </summary>
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    /// <summary>
    /// Alpha of this layer when it is not the active one.
    /// </summary>
    [JsonPropertyName("inactiveOpacity")]
    public float InactiveOpacity { get; set; }

    /// <summary>
    /// An array that defines extra optional info for each IntGrid value.<br/>  WARNING: the
    /// array order is not related to actual IntGrid values! As user can re-order IntGrid values
    /// freely, you may value "2" before value "1" in this array.
    /// </summary>
    [JsonPropertyName("intGridValues")]
    public IntGridValueDefinition[] IntGridValues { get; set; }

    /// <summary>
    /// Type of the layer as Haxe Enum Possible values: IntGrid, Entities, Tiles,
    /// AutoLayer
    /// </summary>
    [JsonPropertyName("type")]
    public TypeEnum LayerDefinitionType { get; set; }

    /// <summary>
    /// Parallax horizontal factor (from -1 to 1, defaults to 0) which affects the scrolling
    /// speed of this layer, creating a fake 3D (parallax) effect.
    /// </summary>
    [JsonPropertyName("parallaxFactorX")]
    public float ParallaxFactorX { get; set; }

    /// <summary>
    /// Parallax vertical factor (from -1 to 1, defaults to 0) which affects the scrolling speed
    /// of this layer, creating a fake 3D (parallax) effect.
    /// </summary>
    [JsonPropertyName("parallaxFactorY")]
    public float ParallaxFactorY { get; set; }

    /// <summary>
    /// If true (default), a layer with a parallax factor will also be scaled up/down accordingly.
    /// </summary>
    [JsonPropertyName("parallaxScaling")]
    public bool ParallaxScaling { get; set; }

    /// <summary>
    /// X offset of the layer, in pixels (IMPORTANT: this should be added to the LayerInstance
    /// optional offset)
    /// </summary>
    [JsonPropertyName("pxOffsetX")]
    public int PxOffsetX { get; set; }

    /// <summary>
    /// Y offset of the layer, in pixels (IMPORTANT: this should be added to the LayerInstance
    /// optional offset)
    /// </summary>
    [JsonPropertyName("pxOffsetY")]
    public int PxOffsetY { get; set; }

    /// <summary>
    /// If TRUE, the content of this layer will be used when rendering levels in a simplified way
    /// for the world view
    /// </summary>
    [JsonPropertyName("renderInWorldView")]
    public bool RenderInWorldView { get; set; }

    /// <summary>
    /// An array of tags to filter Entities that can be added to this layer
    /// </summary>
    [JsonPropertyName("requiredTags")]
    public string[] RequiredTags { get; set; }

    /// <summary>
    /// If the tiles are smaller or larger than the layer grid, the pivot value will be used to
    /// position the tile relatively its grid cell.
    /// </summary>
    [JsonPropertyName("tilePivotX")]
    public float TilePivotX { get; set; }

    /// <summary>
    /// If the tiles are smaller or larger than the layer grid, the pivot value will be used to
    /// position the tile relatively its grid cell.
    /// </summary>
    [JsonPropertyName("tilePivotY")]
    public float TilePivotY { get; set; }

    /// <summary>
    /// Reference to the default Tileset UID being used by this layer definition.<br/>
    /// WARNING: some layer instances might use a different tileset. So most of the time,
    /// you should probably use the __tilesetDefUid value found in layer instances.<br/>  Note:
    /// since version 1.0.0, the old autoTilesetDefUid was removed and merged into this value.
    /// </summary>
    [JsonPropertyName("tilesetDefUid")]
    public int? TilesetDefUid { get; set; }

    /// <summary>
    /// Type of the layer (IntGrid, Entities, Tiles or AutoLayer)
    /// </summary>
    [JsonPropertyName("__type")]
    public LayerType _Type { get; set; }

    /// <summary>
    /// User defined color for the UI
    /// </summary>
    [JsonPropertyName("uiColor")]
    public string UiColor { get; set; }

    /// <summary>
    /// Unique Int identifier
    /// </summary>
    [JsonPropertyName("uid")]
    public int Uid { get; set; }
}

public partial class AutoLayerRuleGroup
{
    [JsonPropertyName("active")]
    public bool Active { get; set; }

    /// <summary>
    /// This field was removed in 1.0.0 and should no longer be used.
    /// </summary>
    [JsonPropertyName("collapsed")]
    public bool? Collapsed { get; set; }

    [JsonPropertyName("isOptional")]
    public bool IsOptional { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("rules")]
    public AutoLayerRuleDefinition[] Rules { get; set; }

    [JsonPropertyName("uid")]
    public int Uid { get; set; }

    [JsonPropertyName("usesWizard")]
    public bool UsesWizard { get; set; }
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
    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("alpha")]
    public float Alpha { get; set; }

    /// <summary>
    /// When TRUE, the rule will prevent other rules to be applied in the same cell if it matches
    /// (TRUE by default).
    /// </summary>
    [JsonPropertyName("breakOnMatch")]
    public bool BreakOnMatch { get; set; }

    /// <summary>
    /// Chances for this rule to be applied (0 to 1)
    /// </summary>
    [JsonPropertyName("chance")]
    public float Chance { get; set; }

    /// <summary>
    /// Checker mode Possible values: None, Horizontal, Vertical
    /// </summary>
    [JsonPropertyName("checker")]
    public Checker Checker { get; set; }

    /// <summary>
    /// If TRUE, allow rule to be matched by flipping its pattern horizontally
    /// </summary>
    [JsonPropertyName("flipX")]
    public bool FlipX { get; set; }

    /// <summary>
    /// If TRUE, allow rule to be matched by flipping its pattern vertically
    /// </summary>
    [JsonPropertyName("flipY")]
    public bool FlipY { get; set; }

    /// <summary>
    /// Default IntGrid value when checking cells outside of level bounds
    /// </summary>
    [JsonPropertyName("outOfBoundsValue")]
    public int? OutOfBoundsValue { get; set; }

    /// <summary>
    /// Rule pattern (size x size)
    /// </summary>
    [JsonPropertyName("pattern")]
    public int[] Pattern { get; set; }

    /// <summary>
    /// If TRUE, enable Perlin filtering to only apply rule on specific random area
    /// </summary>
    [JsonPropertyName("perlinActive")]
    public bool PerlinActive { get; set; }

    [JsonPropertyName("perlinOctaves")]
    public float PerlinOctaves { get; set; }

    [JsonPropertyName("perlinScale")]
    public float PerlinScale { get; set; }

    [JsonPropertyName("perlinSeed")]
    public float PerlinSeed { get; set; }

    /// <summary>
    /// X pivot of a tile stamp (0-1)
    /// </summary>
    [JsonPropertyName("pivotX")]
    public float PivotX { get; set; }

    /// <summary>
    /// Y pivot of a tile stamp (0-1)
    /// </summary>
    [JsonPropertyName("pivotY")]
    public float PivotY { get; set; }

    /// <summary>
    /// Pattern width & height. Should only be 1,3,5 or 7.
    /// </summary>
    [JsonPropertyName("size")]
    public int Size { get; set; }

    /// <summary>
    /// Array of all the tile IDs. They are used randomly or as stamps, based on tileMode value.
    /// </summary>
    [JsonPropertyName("tileIds")]
    public int[] TileIds { get; set; }

    /// <summary>
    /// Defines how tileIds array is used Possible values: Single, Stamp
    /// </summary>
    [JsonPropertyName("tileMode")]
    public TileMode TileMode { get; set; }

    /// <summary>
    /// Max random offset for X tile pos
    /// </summary>
    [JsonPropertyName("tileRandomXMax")]
    public int TileRandomXMax { get; set; }

    /// <summary>
    /// Min random offset for X tile pos
    /// </summary>
    [JsonPropertyName("tileRandomXMin")]
    public int TileRandomXMin { get; set; }

    /// <summary>
    /// Max random offset for Y tile pos
    /// </summary>
    [JsonPropertyName("tileRandomYMax")]
    public int TileRandomYMax { get; set; }

    /// <summary>
    /// Min random offset for Y tile pos
    /// </summary>
    [JsonPropertyName("tileRandomYMin")]
    public int TileRandomYMin { get; set; }

    /// <summary>
    /// Tile X offset
    /// </summary>
    [JsonPropertyName("tileXOffset")]
    public int TileXOffset { get; set; }

    /// <summary>
    /// Tile Y offset
    /// </summary>
    [JsonPropertyName("tileYOffset")]
    public int TileYOffset { get; set; }

    /// <summary>
    /// Unique Int identifier
    /// </summary>
    [JsonPropertyName("uid")]
    public int Uid { get; set; }

    /// <summary>
    /// X cell coord modulo
    /// </summary>
    [JsonPropertyName("xModulo")]
    public int XModulo { get; set; }

    /// <summary>
    /// X cell start offset
    /// </summary>
    [JsonPropertyName("xOffset")]
    public int XOffset { get; set; }

    /// <summary>
    /// Y cell coord modulo
    /// </summary>
    [JsonPropertyName("yModulo")]
    public int YModulo { get; set; }

    /// <summary>
    /// Y cell start offset
    /// </summary>
    [JsonPropertyName("yOffset")]
    public int YOffset { get; set; }
}

/// <summary>
/// IntGrid value definition
/// </summary>
public partial class IntGridValueDefinition
{
    [JsonPropertyName("color")]
    public Color Color { get; set; }

    /// <summary>
    /// User defined unique identifier
    /// </summary>
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    [JsonPropertyName("tile")]
    public TilesetRectangle Tile { get; set; }

    /// <summary>
    /// The IntGrid value itself
    /// </summary>
    [JsonPropertyName("value")]
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
    [JsonPropertyName("__cHei")]
    public int _CHei { get; set; }

    /// <summary>
    /// Grid-based width
    /// </summary>
    [JsonPropertyName("__cWid")]
    public int _CWid { get; set; }

    /// <summary>
    /// The following data is used internally for various optimizations. It's always synced with
    /// source image changes.
    /// </summary>
    [JsonPropertyName("cachedPixelData")]
    public Dictionary<string, object> CachedPixelData { get; set; }

    /// <summary>
    /// An array of custom tile metadata
    /// </summary>
    [JsonPropertyName("customData")]
    public TileCustomMetadata[] CustomData { get; set; }

    /// <summary>
    /// If this value is set, then it means that this atlas uses an internal LDtk atlas image
    /// instead of a loaded one. Possible values:  &lt; null &gt; , LdtkIcons
    /// </summary>
    [JsonPropertyName("embedAtlas")]
    public EmbedAtlas? EmbedAtlas { get; set; }

    /// <summary>
    /// Tileset tags using Enum values specified by tagsSourceEnumId. This array contains 1
    /// element per Enum value, which contains an array of all Tile IDs that are tagged with it.
    /// </summary>
    [JsonPropertyName("enumTags")]
    public EnumTagValue[] EnumTags { get; set; }

    /// <summary>
    /// User defined unique identifier
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
    /// Path to the source file, relative to the current project JSON file<br/>  It can be null
    /// if no image was provided, or when using an embed atlas.
    /// </summary>
    [JsonPropertyName("relPath")]
    public string RelPath { get; set; }

    /// <summary>
    /// Array of group of tiles selections, only meant to be used in the editor
    /// </summary>
    [JsonPropertyName("savedSelections")]
    public Dictionary<string, object>[] SavedSelections { get; set; }

    /// <summary>
    /// Space in pixels between all tiles
    /// </summary>
    [JsonPropertyName("spacing")]
    public int Spacing { get; set; }

    /// <summary>
    /// An array of user-defined tags to organize the Tilesets
    /// </summary>
    [JsonPropertyName("tags")]
    public string[] Tags { get; set; }

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

/// <summary>
/// In a tileset definition, user defined meta-data of a tile.
/// </summary>
public partial class TileCustomMetadata
{
    [JsonPropertyName("data")]
    public string Data { get; set; }

    [JsonPropertyName("tileId")]
    public int TileId { get; set; }
}

/// <summary>
/// In a tileset definition, enum based tag infos
/// </summary>
public partial class EnumTagValue
{
    [JsonPropertyName("enumValueId")]
    public string EnumValueId { get; set; }

    [JsonPropertyName("tileIds")]
    public int[] TileIds { get; set; }
}

/// <summary>
/// This object is not actually used by LDtk. It ONLY exists to force explicit references to
/// all types, to make sure QuickType finds them and integrate all of them. Otherwise,
/// Quicktype will drop types that are not explicitely used.
/// </summary>
public partial class ForcedRefs
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("AutoLayerRuleGroup")]
    public AutoLayerRuleGroup AutoLayerRuleGroup { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("AutoRuleDef")]
    public AutoLayerRuleDefinition AutoRuleDef { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("CustomCommand")]
    public LdtkCustomCommand CustomCommand { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("Definitions")]
    public Definitions Definitions { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("EntityDef")]
    public EntityDefinition EntityDef { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("EntityInstance")]
    public EntityInstance EntityInstance { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("EntityReferenceInfos")]
    public EntityRef EntityReferenceInfos { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("EnumDef")]
    public EnumDefinition EnumDef { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("EnumDefValues")]
    public EnumValueDefinition EnumDefValues { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("EnumTagValue")]
    public EnumTagValue EnumTagValue { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("FieldDef")]
    public FieldDefinition FieldDef { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("FieldInstance")]
    public FieldInstance FieldInstance { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("GridPoint")]
    public GridPoint GridPoint { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("IntGridValueDef")]
    public IntGridValueDefinition IntGridValueDef { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("IntGridValueInstance")]
    public IntGridValueInstance IntGridValueInstance { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("LayerDef")]
    public LayerDefinition LayerDef { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("LayerInstance")]
    public LayerInstance LayerInstance { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("Level")]
    public LDtkLevel Level { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("LevelBgPosInfos")]
    public LevelBackgroundPosition LevelBgPosInfos { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("NeighbourLevel")]
    public NeighbourLevel NeighbourLevel { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("TableOfContentEntry")]
    public LdtkTableOfContentEntry TableOfContentEntry { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("Tile")]
    public TileInstance Tile { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("TileCustomMetadata")]
    public TileCustomMetadata TileCustomMetadata { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("TilesetDef")]
    public TilesetDefinition TilesetDef { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("TilesetRect")]
    public TilesetRectangle TilesetRect { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("World")]
    public LDtkWorld World { get; set; }
}

public partial class EntityInstance
{
    /// <summary>
    /// Reference of the Entity definition UID
    /// </summary>
    [JsonPropertyName("defUid")]
    public int DefUid { get; set; }

    /// <summary>
    /// An array of all custom fields and their values.
    /// </summary>
    [JsonPropertyName("fieldInstances")]
    public FieldInstance[] FieldInstances { get; set; }

    /// <summary>
    /// Grid-based coordinates ([x,y] format)
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
    /// Unique instance identifier
    /// </summary>
    [JsonPropertyName("iid")]
    public Guid Iid { get; set; }

    /// <summary>
    /// Pivot coordinates  ([x,y] format, values are from 0 to 1) of the Entity
    /// </summary>
    [JsonPropertyName("__pivot")]
    public Vector2 _Pivot { get; set; }

    /// <summary>
    /// Pixel coordinates ([x,y] format) in current level coordinate space. Don't forget
    /// optional layer offsets, if they exist!
    /// </summary>
    [JsonPropertyName("px")]
    public Point Px { get; set; }

    /// <summary>
    /// The entity "smart" color, guessed from either Entity definition, or one its field
    /// instances.
    /// </summary>
    [JsonPropertyName("__smartColor")]
    public Color _SmartColor { get; set; }

    /// <summary>
    /// Array of tags defined in this Entity definition
    /// </summary>
    [JsonPropertyName("__tags")]
    public string[] _Tags { get; set; }

    /// <summary>
    /// Optional TilesetRect used to display this entity (it could either be the default Entity
    /// tile, or some tile provided by a field value, like an Enum).
    /// </summary>
    [JsonPropertyName("__tile")]
    public TilesetRectangle _Tile { get; set; }

    /// <summary>
    /// Entity width in pixels. For non-resizable entities, it will be the same as Entity
    /// definition.
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; set; }

    /// <summary>
    /// X world coordinate in pixels
    /// </summary>
    [JsonPropertyName("__worldX")]
    public int _WorldX { get; set; }

    /// <summary>
    /// Y world coordinate in pixels
    /// </summary>
    [JsonPropertyName("__worldY")]
    public int _WorldY { get; set; }
}

public partial class FieldInstance
{
    /// <summary>
    /// Reference of the Field definition UID
    /// </summary>
    [JsonPropertyName("defUid")]
    public int DefUid { get; set; }

    /// <summary>
    /// Field definition identifier
    /// </summary>
    [JsonPropertyName("__identifier")]
    public string _Identifier { get; set; }

    /// <summary>
    /// Editor internal raw values
    /// </summary>
    [JsonPropertyName("realEditorValues")]
    public object[] RealEditorValues { get; set; }

    /// <summary>
    /// Optional TilesetRect used to display this field (this can be the field own Tile, or some
    /// other Tile guessed from the value, like an Enum).
    /// </summary>
    [JsonPropertyName("__tile")]
    public TilesetRectangle _Tile { get; set; }

    /// <summary>
    /// Type of the field, such as Int, Float, String, Enum(my_enum_name), Bool,
    /// etc.<br/>  NOTE: if you enable the advanced option Use Multilines type, you will have
    /// "Multilines" instead of "String" when relevant.
    /// </summary>
    [JsonPropertyName("__type")]
    public string _Type { get; set; }

    /// <summary>
    /// Actual value of the field instance. The value type varies, depending on __type:<br/>
    /// - For classic types (ie. Integer, Float, Boolean, String, Text and FilePath), you
    /// just get the actual value with the expected type.<br/>   - For Color, the value is an
    /// hexadecimal string using "#rrggbb" format.<br/>   - For Enum, the value is a String
    /// representing the selected enum value.<br/>   - For Point, the value is a
    /// GridPoint object.<br/>   - For Tile, the value is a
    /// TilesetRect object.<br/>   - For EntityRef, the value is an
    /// EntityReferenceInfos object.<br/>  If the field is an
    /// array, then this __value will also be a JSON array.
    /// </summary>
    [JsonPropertyName("__value")]
    public object _Value { get; set; }
}

/// <summary>
/// This object describes the "location" of an Entity instance in the project worlds.
/// </summary>
public partial class EntityRef
{
    /// <summary>
    /// Guid of the refered EntityInstance
    /// </summary>
    [JsonPropertyName("entityIid")]
    public Guid EntityIid { get; set; }

    /// <summary>
    /// Guid of the LayerInstance containing the refered EntityInstance
    /// </summary>
    [JsonPropertyName("layerIid")]
    public Guid LayerIid { get; set; }

    /// <summary>
    /// Guid of the Level containing the refered EntityInstance
    /// </summary>
    [JsonPropertyName("levelIid")]
    public Guid LevelIid { get; set; }

    /// <summary>
    /// Guid of the World containing the refered EntityInstance
    /// </summary>
    [JsonPropertyName("worldIid")]
    public Guid WorldIid { get; set; }
}

/// <summary>
/// This object is just a grid-based coordinate used in Field values.
/// </summary>
public partial class GridPoint
{
    /// <summary>
    /// X grid-based coordinate
    /// </summary>
    [JsonPropertyName("cx")]
    public int Cx { get; set; }

    /// <summary>
    /// Y grid-based coordinate
    /// </summary>
    [JsonPropertyName("cy")]
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
    [JsonPropertyName("coordId")]
    public int CoordId { get; set; }

    /// <summary>
    /// IntGrid value
    /// </summary>
    [JsonPropertyName("v")]
    public int V { get; set; }
}

public partial class LayerInstance
{
    /// <summary>
    /// An array containing all tiles generated by Auto-layer rules. The array is already sorted
    /// in display order (ie. 1st tile is beneath 2nd, which is beneath 3rd etc.).<br/>
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
    /// Unique layer instance identifier
    /// </summary>
    [JsonPropertyName("iid")]
    public Guid Iid { get; set; }

    /// <summary>
    /// WARNING: this deprecated value is no longer exported since version 1.0.0  Replaced
    /// by: intGridCsv
    /// </summary>
    [JsonPropertyName("intGrid")]
    public IntGridValueInstance[] IntGrid { get; set; }

    /// <summary>
    /// A list of all values in the IntGrid layer, stored in CSV format (Comma Separated
    /// Values).<br/>  Order is from left to right, and top to bottom (ie. first row from left to
    /// right, followed by second row, etc).<br/>  0 means "empty cell" and IntGrid values
    /// start at 1.<br/>  The array size is __cWid x __cHei cells.
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
    /// An Array containing the UIDs of optional rules that were enabled in this specific layer
    /// instance.
    /// </summary>
    [JsonPropertyName("optionalRules")]
    public int[] OptionalRules { get; set; }

    /// <summary>
    /// This layer can use another tileset by overriding the tileset UID here.
    /// </summary>
    [JsonPropertyName("overrideTilesetUid")]
    public int? OverrideTilesetUid { get; set; }

    /// <summary>
    /// X offset in pixels to render this layer, usually 0 (IMPORTANT: this should be added to
    /// the LayerDef optional offset, so you should probably prefer using __pxTotalOffsetX
    /// which contains the total offset value)
    /// </summary>
    [JsonPropertyName("pxOffsetX")]
    public int PxOffsetX { get; set; }

    /// <summary>
    /// Y offset in pixels to render this layer, usually 0 (IMPORTANT: this should be added to
    /// the LayerDef optional offset, so you should probably prefer using __pxTotalOffsetX
    /// which contains the total offset value)
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
    /// Random seed used for Auto-Layers rendering
    /// </summary>
    [JsonPropertyName("seed")]
    public int Seed { get; set; }

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
    /// Alpha/opacity of the tile (0-1, defaults to 1)
    /// </summary>
    [JsonPropertyName("a")]
    public float A { get; set; }

    /// <summary>
    /// Internal data used by the editor.<br/>  For auto-layer tiles: [ruleId, coordId].<br/>
    /// For tile-layer tiles: [coordId].
    /// </summary>
    [JsonPropertyName("d")]
    public int[] D { get; set; }

    /// <summary>
    /// "Flip bits", a 2-bits integer to represent the mirror transformations of the tile.<br/>
    /// - Bit 0 = X flip<br/>   - Bit 1 = Y flip<br/>   Examples: f=0 (no flip), f=1 (X flip
    /// only), f=2 (Y flip only), f=3 (both flips)
    /// </summary>
    [JsonPropertyName("f")]
    public int F { get; set; }

    /// <summary>
    /// Pixel coordinates of the tile in the layer ([x,y] format). Don't forget optional
    /// layer offsets, if they exist!
    /// </summary>
    [JsonPropertyName("px")]
    public Point Px { get; set; }

    /// <summary>
    /// Pixel coordinates of the tile in the tileset ([x,y] format)
    /// </summary>
    [JsonPropertyName("src")]
    public Point Src { get; set; }

    /// <summary>
    /// The Tile ID in the corresponding tileset.
    /// </summary>
    [JsonPropertyName("t")]
    public int T { get; set; }
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
    [JsonPropertyName("__bgColor")]
    public string _BgColor { get; set; }

    /// <summary>
    /// Background image X pivot (0-1)
    /// </summary>
    [JsonPropertyName("bgPivotX")]
    public float BgPivotX { get; set; }

    /// <summary>
    /// Background image Y pivot (0-1)
    /// </summary>
    [JsonPropertyName("bgPivotY")]
    public float BgPivotY { get; set; }

    /// <summary>
    /// Position informations of the background image, if there is one.
    /// </summary>
    [JsonPropertyName("__bgPos")]
    public LevelBackgroundPosition _BgPos { get; set; }

    /// <summary>
    /// The optional relative path to the level background image.
    /// </summary>
    [JsonPropertyName("bgRelPath")]
    public string BgRelPath { get; set; }

    /// <summary>
    /// This value is not null if the project option "Save levels separately" is enabled. In
    /// this case, this relative path points to the level Json file.
    /// </summary>
    [JsonPropertyName("externalRelPath")]
    public string ExternalRelPath { get; set; }

    /// <summary>
    /// An array containing this level custom field values.
    /// </summary>
    [JsonPropertyName("fieldInstances")]
    public FieldInstance[] FieldInstances { get; set; }

    /// <summary>
    /// User defined unique identifier
    /// </summary>
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    /// <summary>
    /// Unique instance identifier
    /// </summary>
    [JsonPropertyName("iid")]
    public Guid Iid { get; set; }

    /// <summary>
    /// An array containing all Layer instances. IMPORTANT: if the project option "Save
    /// levels separately" is enabled, this field will be null.<br/>  This array is sorted
    /// in display order: the 1st layer is the top-most and the last is behind.
    /// </summary>
    [JsonPropertyName("layerInstances")]
    public LayerInstance[] LayerInstances { get; set; }

    /// <summary>
    /// Background color of the level. If null, the project defaultLevelBgColor should be
    /// used.
    /// </summary>
    [JsonPropertyName("bgColor")]
    public string LevelBgColor { get; set; }

    /// <summary>
    /// An enum defining the way the background image (if any) is positioned on the level. See
    /// __bgPos for resulting position info. Possible values:  &lt; null &gt; , Unscaled,
    /// Contain, Cover, CoverDirty, Repeat
    /// </summary>
    [JsonPropertyName("bgPos")]
    public BgPos? LevelBgPos { get; set; }

    /// <summary>
    /// An array listing all other levels touching this one on the world map.<br/>  Only relevant
    /// for world layouts where level spatial positioning is manual (ie. GridVania, Free). For
    /// Horizontal and Vertical layouts, this array is always empty.
    /// </summary>
    [JsonPropertyName("__neighbours")]
    public NeighbourLevel[] _Neighbours { get; set; }

    /// <summary>
    /// Height of the level in pixels
    /// </summary>
    [JsonPropertyName("pxHei")]
    public int PxHei { get; set; }

    /// <summary>
    /// Width of the level in pixels
    /// </summary>
    [JsonPropertyName("pxWid")]
    public int PxWid { get; set; }

    /// <summary>
    /// The "guessed" color for this level in the editor, decided using either the background
    /// color or an existing custom field.
    /// </summary>
    [JsonPropertyName("__smartColor")]
    public Color _SmartColor { get; set; }

    /// <summary>
    /// Unique Int identifier
    /// </summary>
    [JsonPropertyName("uid")]
    public int Uid { get; set; }

    /// <summary>
    /// If TRUE, the level identifier will always automatically use the naming pattern as defined
    /// in Project.levelNamePattern. Becomes FALSE if the identifier is manually modified by
    /// user.
    /// </summary>
    [JsonPropertyName("useAutoIdentifier")]
    public bool UseAutoIdentifier { get; set; }

    /// <summary>
    /// Index that represents the "depth" of the level in the world. Default is 0, greater means
    /// "above", lower means "below".<br/>  This value is mostly used for display only and is
    /// intended to make stacking of levels easier to manage.
    /// </summary>
    [JsonPropertyName("worldDepth")]
    public int WorldDepth { get; set; }

    /// <summary>
    /// World X coordinate in pixels.<br/>  Only relevant for world layouts where level spatial
    /// positioning is manual (ie. GridVania, Free). For Horizontal and Vertical layouts, the
    /// value is always -1 here.
    /// </summary>
    [JsonPropertyName("worldX")]
    public int WorldX { get; set; }

    /// <summary>
    /// World Y coordinate in pixels.<br/>  Only relevant for world layouts where level spatial
    /// positioning is manual (ie. GridVania, Free). For Horizontal and Vertical layouts, the
    /// value is always -1 here.
    /// </summary>
    [JsonPropertyName("worldY")]
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
    [JsonPropertyName("cropRect")]
    public float[] CropRect { get; set; }

    /// <summary>
    /// An array containing the [scaleX,scaleY] values of the cropped background image,
    /// depending on bgPos option.
    /// </summary>
    [JsonPropertyName("scale")]
    public Vector2 Scale { get; set; }

    /// <summary>
    /// An array containing the [x,y] pixel coordinates of the top-left corner of the
    /// cropped background image, depending on bgPos option.
    /// </summary>
    [JsonPropertyName("topLeftPx")]
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
    [JsonPropertyName("dir")]
    public string Dir { get; set; }

    /// <summary>
    /// Neighbour Instance Identifier
    /// </summary>
    [JsonPropertyName("levelIid")]
    public Guid LevelIid { get; set; }

    /// <summary>
    /// WARNING: this deprecated value is no longer exported since version 1.2.0  Replaced
    /// by: levelIid
    /// </summary>
    [JsonPropertyName("levelUid")]
    public int? LevelUid { get; set; }
}

public partial class LdtkTableOfContentEntry
{
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    [JsonPropertyName("instances")]
    public EntityRef[] Instances { get; set; }
}

/// <summary>
/// IMPORTANT: this type is available as a preview. You can rely on it to update your
/// importers, for when it will be officially available.  A World contains multiple levels,
/// and it has its own layout settings.
/// </summary>
public partial class LDtkWorld
{
    /// <summary>
    /// Default new level height
    /// </summary>
    [JsonPropertyName("defaultLevelHeight")]
    public int DefaultLevelHeight { get; set; }

    /// <summary>
    /// Default new level width
    /// </summary>
    [JsonPropertyName("defaultLevelWidth")]
    public int DefaultLevelWidth { get; set; }

    /// <summary>
    /// User defined unique identifier
    /// </summary>
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    /// <summary>
    /// Unique instance identifer
    /// </summary>
    [JsonPropertyName("iid")]
    public Guid Iid { get; set; }

    /// <summary>
    /// All levels from this world. The order of this array is only relevant in
    /// LinearHorizontal and linearVertical world layouts (see worldLayout value).
    /// Otherwise, you should refer to the worldX,worldY coordinates of each Level.
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
    /// space). Possible values: Free, GridVania, LinearHorizontal, LinearVertical, null
    /// </summary>
    [JsonPropertyName("worldLayout")]
    public WorldLayout? WorldLayout { get; set; }
}

/// <summary>
/// Possible values: Manual, AfterLoad, BeforeSave, AfterSave
/// </summary>
public enum When { AfterLoad, AfterSave, BeforeSave, Manual };

/// <summary>
/// Possible values: Any, OnlySame, OnlyTags, OnlySpecificEntity
/// </summary>
public enum AllowedRefs { Any, OnlySame, OnlySpecificEntity, OnlyTags };

/// <summary>
/// Possible values: Hidden, ValueOnly, NameAndValue, EntityTile, LevelTile,
/// Points, PointStar, PointPath, PointPathLoop, RadiusPx, RadiusGrid,
/// ArrayCountWithLabel, ArrayCountNoLabel, RefLinkBetweenPivots,
/// RefLinkBetweenCenters
/// </summary>
public enum EditorDisplayMode { ArrayCountNoLabel, ArrayCountWithLabel, EntityTile, Hidden, LevelTile, NameAndValue, PointPath, PointPathLoop, PointStar, Points, RadiusGrid, RadiusPx, RefLinkBetweenCenters, RefLinkBetweenPivots, ValueOnly };

/// <summary>
/// Possible values: Above, Center, Beneath
/// </summary>
public enum EditorDisplayPos { Above, Beneath, Center };

/// <summary>
/// Possible values: ZigZag, StraightArrow, CurvedArrow, ArrowsLine, DashedLine
/// </summary>
public enum EditorLinkStyle { ArrowsLine, CurvedArrow, DashedLine, StraightArrow, ZigZag };

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

/// <summary>
/// Checker mode Possible values: None, Horizontal, Vertical
/// </summary>
public enum Checker { Horizontal, None, Vertical };

/// <summary>
/// Defines how tileIds array is used Possible values: Single, Stamp
/// </summary>
public enum TileMode { Single, Stamp };

/// <summary>
/// Type of the layer as Haxe Enum Possible values: IntGrid, Entities, Tiles,
/// AutoLayer
/// </summary>
public enum TypeEnum { AutoLayer, Entities, IntGrid, Tiles };

public enum EmbedAtlas { LdtkIcons };

public enum Flag { DiscardPreCsvIntGrid, ExportPreCsvIntGridFormat, IgnoreBackupSuggest, MultiWorlds, PrependIndexToLevelFileNames, UseMultilinesType };

public enum BgPos { Contain, Cover, CoverDirty, Repeat, Unscaled };

public enum WorldLayout { Free, GridVania, LinearHorizontal, LinearVertical };

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

#pragma warning restore CS1591, IDE1006, CA1707, CA1716, IDE0130, CA1720, CA1711
