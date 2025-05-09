# LDtkFile class

This is the root of any Project JSON file. It contains: - the project settings, - an array of levels, - a group of definitions (that can probably be safely ignored for most users).

```csharp
public class LDtkFile
```

## Public Members

| name | description |
| --- | --- |
| [LDtkFile](LDtkFile/LDtkFile.md)() | Initializes a new instance of the [`LDtkFile`](./LDtkFile.md) class. Used by json deserializer not for use by user. |
| static [FromFile](LDtkFile/FromFile.md)(…) | Loads the ldtk world file from disk directly using json source generator. (2 methods) |
| static [FromFileReflection](LDtkFile/FromFileReflection.md)(…) | Loads the ldtk world file from disk directly. |
| [BgColor](LDtkFile/BgColor.md) { get; set; } | Project background color |
| [Content](LDtkFile/Content.md) { get; set; } | Gets or sets the content manager used if you are using the contentpipeline. |
| [Defs](LDtkFile/Defs.md) { get; set; } | A structure containing all the definitions of this project |
| [ExternalLevels](LDtkFile/ExternalLevels.md) { get; set; } | If TRUE, one file will be saved for the project (incl. all its definitions) and one file in a sub-folder for each level. |
| [FilePath](LDtkFile/FilePath.md) { get; set; } | Gets or sets the absolute path to the ldtkFile. |
| [Flags](LDtkFile/Flags.md) { get; set; } | An array containing various advanced flags (ie. options or other states). Possible values: `DiscardPreCsvIntGrid`, `ExportOldTableOfContentData`, `ExportPreCsvIntGridFormat`, `IgnoreBackupSuggest`, `PrependIndexToLevelFileNames`, `MultiWorlds`, `UseMultilinesType` |
| [Iid](LDtkFile/Iid.md) { get; set; } | Unique project identifier |
| [JsonVersion](LDtkFile/JsonVersion.md) { get; set; } | File format version |
| [Levels](LDtkFile/Levels.md) { get; set; } | All levels. The order of this array is only relevant in `LinearHorizontal` and `linearVertical` world layouts (see `worldLayout` value). Otherwise, you should refer to the `worldX`,`worldY` coordinates of each Level. |
| [Toc](LDtkFile/Toc.md) { get; set; } | All instances of entities that have their `exportToToc` flag enabled are listed in this array. |
| [WorldGridHeight](LDtkFile/WorldGridHeight.md) { get; set; } | WARNING: this field will move to the `worlds` array after the "multi-worlds" update. It will then be `null`. You can enable the Multi-worlds advanced project option to enable the change immediately. Height of the world grid in pixels. |
| [WorldGridWidth](LDtkFile/WorldGridWidth.md) { get; set; } | WARNING: this field will move to the `worlds` array after the "multi-worlds" update. It will then be `null`. You can enable the Multi-worlds advanced project option to enable the change immediately. Width of the world grid in pixels. |
| [WorldLayout](LDtkFile/WorldLayout.md) { get; set; } | WARNING: this field will move to the `worlds` array after the "multi-worlds" update. It will then be `null`. You can enable the Multi-worlds advanced project option to enable the change immediately. An enum that describes how levels are organized in this project (ie. linearly or in a 2D space). Possible values: &lt;`null`&gt;, `Free`, `GridVania`, `LinearHorizontal`, `LinearVertical`, `null` |
| [Worlds](LDtkFile/Worlds.md) { get; set; } | This array will be empty, unless you enable the Multi-Worlds in the project advanced settings. - in current version, a LDtk project file can only contain a single world with multiple levels in it. In this case, levels and world layout related settings are stored in the root of the JSON. - with "Multi-worlds" enabled, there will be a `worlds` array in root, each world containing levels and layout settings. Basically, it's pretty much only about moving the `levels` array to the `worlds` array, along with world layout related values (eg. `worldGridWidth` etc).If you want to start supporting this future update easily, please refer to this documentation: https://github.com/deepnight/ldtk/issues/231 |
| [GetEntityRef&lt;T&gt;](LDtkFile/GetEntityRef.md)(…) | Gets an entity from an *reference* converted to *T*. |
| [LoadSingleWorld](LDtkFile/LoadSingleWorld.md)() | Loads the one and only ldtkl world file from disk directly or from the embeded one depending on if the file uses externalworlds. |
| [LoadWorld](LDtkFile/LoadWorld.md)(…) | Loads the ldtkl world file from disk directly or from the embeded one depending on if the file uses externalworlds. |

## See Also

* namespace [LDtk](../LDtkMonogame.md)

<!-- DO NOT EDIT: generated by xmldocmd for LDtkMonogame.dll -->
