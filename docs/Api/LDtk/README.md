# LDtk Namespace

[Home](../README.md) &#x2022; [Classes](#classes) &#x2022; [Interfaces](#interfaces) &#x2022; [Enums](#enums)

## Classes

| Class | Summary |
| ----- | ------- |
| [AutoLayerRuleGroup](AutoLayerRuleGroup/README.md) |  Auto\-layer rule group  |
| [Constants](Constants/README.md) |  General Constants used in LDtkMonogame\.  |
| [CustomCommand](CustomCommand/README.md) |  ldtk\.CustomCommand  |
| [Definitions](Definitions/README.md) |  If you're writing your own LDtk importer, you should probably just ignore \*most\* stuff in the `defs` section, as it contains data that are mostly important to the editor\. To keep you away from the `defs` section and avoid some unnecessary JSON parsing, important data from definitions is often duplicated in fields prefixed with a double underscore \(eg\. `__identifier` or `__type`\)\.  The 2 only definition types you might need here are  and \.  |
| [EntityDefinition](EntityDefinition/README.md) |  Entity definition  |
| [EntityInstance](EntityInstance/README.md) |  Entity instance  |
| [EntityReference](EntityReference/README.md) |  This object describes the "location" of an Entity instance in the project worlds\.  |
| [EnumDefinition](EnumDefinition/README.md) |  Enum definition  |
| [EnumTagValue](EnumTagValue/README.md) |  In a tileset definition, enum based tag infos  |
| [EnumValueDefinition](EnumValueDefinition/README.md) |  Enum value definition  |
| [FieldInstance](FieldInstance/README.md) |  Field instance  |
| [GridPoint](GridPoint/README.md) |  This object is just a grid\-based coordinate used in Field values\.  |
| [IntGridValueDefinition](IntGridValueDefinition/README.md) |  IntGrid value definition  |
| [IntGridValueGroupDefinition](IntGridValueGroupDefinition/README.md) |  IntGrid value group definition  |
| [IntGridValueInstance](IntGridValueInstance/README.md) |  IntGrid value instance  |
| [LayerDefinition](LayerDefinition/README.md) |  Layer definition  |
| [LayerInstance](LayerInstance/README.md) |  Layer instance  |
| [LDtkException](LDtkException/README.md) | Generic LDtk Exception\. |
| [LDtkFile](LDtkFile/README.md) |  This is the root of any Project JSON file\. It contains:  \- the project settings, \- an array of levels, \- a group of definitions \(that can probably be safely ignored for most users\)\.  |
| [LDtkIntGrid](LDtkIntGrid/README.md) |  LDtk IntGrid\.  |
| [LDtkJsonSourceGenerator](LDtkJsonSourceGenerator/README.md) |  The json source generator for LDtk files\.  |
| [LDtkLevel](LDtkLevel/README.md) |  This section contains all the level data\. It can be found in 2 distinct forms, depending on Project current settings:  \- If "\*Separate level files\*" is  \(default\): full level data is \*embedded\* inside the main Project JSON file, \- If "\*Separate level files\*" is : level data is stored in \*separate\* standalone `.ldtkl` files \(one per level\)\. In this case, the main Project JSON file will still contain most level data, except heavy sections, like the `layerInstances` array \(which will be null\)\. The `externalRelPath` string points to the `ldtkl` file\.  A `ldtkl` file is just a JSON file containing exactly what is described below\.  |
| [LDtkTableOfContentEntry](LDtkTableOfContentEntry/README.md) |  ldtk\.TableOfContentEntry  |
| [LDtkWorld](LDtkWorld/README.md) |  : this type is available as a preview\. You can rely on it to update your importers, for when it will be officially available\.  A World contains multiple levels, and it has its own layout settings\.  |
| [LevelBackgroundPosition](LevelBackgroundPosition/README.md) |  Level background image position info  |
| [NeighbourLevel](NeighbourLevel/README.md) |  Nearby level info  |
| [TileCustomMetadata](TileCustomMetadata/README.md) |  In a tileset definition, user defined meta\-data of a tile\.  |
| [TileInstance](TileInstance/README.md) |  This structure represents a single tile from a given Tileset\.  |
| [TilesetDefinition](TilesetDefinition/README.md) |  The `Tileset` definition is the most important part among project definitions\. It contains some extra informations about each integrated tileset\. If you only had to parse one definition section, that would be the one\.  |
| [TilesetRectangle](TilesetRectangle/README.md) |  This object represents a custom sub rectangle in a Tileset image\.  |
| [TocInstanceData](TocInstanceData/README.md) |  ldtk\.TocInstanceData  |

## Interfaces

| Interface | Summary |
| --------- | ------- |
| [ILDtkEntity](ILDtkEntity/README.md) | Interface that implements the Entity\. |

## Enums

| Enum | Summary |
| ---- | ------- |
| [EmbedAtlas](EmbedAtlas/README.md) |  If this value is set, then it means that this atlas uses an internal LDtk atlas image instead of a loaded one\. Possible values: \<`null`\>, `LdtkIcons`, `null`  |
| [LayerType](LayerType/README.md) |  The Type of layer\.  |
| [TileRenderMode](TileRenderMode/README.md) |  An enum describing how the Entity tile is rendered inside the Entity bounds\. Possible values: `Cover`, `FitInside`, `Repeat`, `Stretch`, `FullSizeCropped`, `FullSizeUncropped`, `NineSlice`  |
| [When](When/README.md) |  Possible values: `Manual`, `AfterLoad`, `BeforeSave`, `AfterSave`  |
| [WorldLayout](WorldLayout/README.md) |  : this field will move to the `worlds` array after the "multi\-worlds" update\. It will then be `null`\. You can enable the Multi\-worlds advanced project option to enable the change immediately\.  An enum that describes how levels are organized in this project \(ie\. linearly or in a 2D space\)\. Possible values: \<`null`\>, `Free`, `GridVania`, `LinearHorizontal`, `LinearVertical`, `null`  |

