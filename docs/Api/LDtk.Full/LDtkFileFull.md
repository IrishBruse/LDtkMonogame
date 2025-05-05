# LDtkFileFull class

This is the root of any Project JSON file. It contains: - the project settings, - an array of levels, - a group of definitions (that can probably be safely ignored for most users).

```csharp
public class LDtkFileFull
```

## Public Members

| name | description |
| --- | --- |
| [LDtkFileFull](LDtkFileFull/LDtkFileFull.md)() | The default constructor. |
| static [FromFile](LDtkFileFull/FromFile.md)(…) | Loads the ldtk world file from disk directly using json source generator. |
| [AppBuildId](LDtkFileFull/AppBuildId.md) { get; set; } | LDtk application build identifier. This is only used to identify the LDtk version that generated this particular project file, which can be useful for specific bug fixing. Note that the build identifier is just the date of the release, so it's not unique to each user (one single global ID per LDtk public release), and as a result, completely anonymous. |
| [BackupLimit](LDtkFileFull/BackupLimit.md) { get; set; } | Number of backup files to keep, if the `backupOnSave` is TRUE |
| [BackupOnSave](LDtkFileFull/BackupOnSave.md) { get; set; } | If TRUE, an extra copy of the project will be created in a sub folder, when saving. |
| [BackupRelPath](LDtkFileFull/BackupRelPath.md) { get; set; } | Target relative path to store backup files |
| [BgColor](LDtkFileFull/BgColor.md) { get; set; } | Project background color |
| [CustomCommands](LDtkFileFull/CustomCommands.md) { get; set; } | An array of command lines that can be ran manually by the user |
| [DefaultEntityHeight](LDtkFileFull/DefaultEntityHeight.md) { get; set; } | Default height for new entities |
| [DefaultEntityWidth](LDtkFileFull/DefaultEntityWidth.md) { get; set; } | Default width for new entities |
| [DefaultGridSize](LDtkFileFull/DefaultGridSize.md) { get; set; } | Default grid size for new layers |
| [DefaultLevelBgColor](LDtkFileFull/DefaultLevelBgColor.md) { get; set; } | Default background color of levels |
| [DefaultLevelHeight](LDtkFileFull/DefaultLevelHeight.md) { get; set; } | WARNING: this field will move to the `worlds` array after the "multi-worlds" update. It will then be `null`. You can enable the Multi-worlds advanced project option to enable the change immediately. Default new level height |
| [DefaultLevelWidth](LDtkFileFull/DefaultLevelWidth.md) { get; set; } | WARNING: this field will move to the `worlds` array after the "multi-worlds" update. It will then be `null`. You can enable the Multi-worlds advanced project option to enable the change immediately. Default new level width |
| [DefaultPivotX](LDtkFileFull/DefaultPivotX.md) { get; set; } | Default X pivot (0 to 1) for new entities |
| [DefaultPivotY](LDtkFileFull/DefaultPivotY.md) { get; set; } | Default Y pivot (0 to 1) for new entities |
| [Defs](LDtkFileFull/Defs.md) { get; set; } | A structure containing all the definitions of this project |
| [DummyWorldIid](LDtkFileFull/DummyWorldIid.md) { get; set; } | If the project isn't in MultiWorlds mode, this is the IID of the internal "dummy" World. |
| [ExportLevelBg](LDtkFileFull/ExportLevelBg.md) { get; set; } | If TRUE, the exported PNGs will include the level background (color or image). |
| [ExportPng](LDtkFileFull/ExportPng.md) { get; set; } | WARNING: this deprecated value is no longer exported since version 0.9.3 Replaced by: `imageExportMode` |
| [ExportTiled](LDtkFileFull/ExportTiled.md) { get; set; } | If TRUE, a Tiled compatible file will also be generated along with the LDtk JSON file (default is FALSE) |
| [ExternalLevels](LDtkFileFull/ExternalLevels.md) { get; set; } | If TRUE, one file will be saved for the project (incl. all its definitions) and one file in a sub-folder for each level. |
| [FilePath](LDtkFileFull/FilePath.md) { get; set; } | Gets or sets the absolute path to the ldtkFile. |
| [Flags](LDtkFileFull/Flags.md) { get; set; } | An array containing various advanced flags (ie. options or other states). Possible values: `DiscardPreCsvIntGrid`, `ExportOldTableOfContentData`, `ExportPreCsvIntGridFormat`, `IgnoreBackupSuggest`, `PrependIndexToLevelFileNames`, `MultiWorlds`, `UseMultilinesType` |
| [IdentifierStyle](LDtkFileFull/IdentifierStyle.md) { get; set; } | Naming convention for Identifiers (first-letter uppercase, full uppercase etc.) Possible values: `Capitalize`, `Uppercase`, `Lowercase`, `Free` |
| [Iid](LDtkFileFull/Iid.md) { get; set; } | Unique project identifier |
| [ImageExportMode](LDtkFileFull/ImageExportMode.md) { get; set; } | "Image export" option when saving project. Possible values: `None`, `OneImagePerLayer`, `OneImagePerLevel`, `LayersAndLevels` |
| [JsonVersion](LDtkFileFull/JsonVersion.md) { get; set; } | File format version |
| [LevelNamePattern](LDtkFileFull/LevelNamePattern.md) { get; set; } | The default naming convention for level identifiers. |
| [Levels](LDtkFileFull/Levels.md) { get; set; } | All levels. The order of this array is only relevant in `LinearHorizontal` and `linearVertical` world layouts (see `worldLayout` value). Otherwise, you should refer to the `worldX`,`worldY` coordinates of each Level. |
| [MinifyJson](LDtkFileFull/MinifyJson.md) { get; set; } | If TRUE, the Json is partially minified (no indentation, nor line breaks, default is FALSE) |
| [NextUid](LDtkFileFull/NextUid.md) { get; set; } | Next Unique integer ID available |
| [PngFilePattern](LDtkFileFull/PngFilePattern.md) { get; set; } | File naming pattern for exported PNGs |
| [SimplifiedExport](LDtkFileFull/SimplifiedExport.md) { get; set; } | If TRUE, a very simplified will be generated on saving, for quicker &amp; easier engine integration. |
| [Toc](LDtkFileFull/Toc.md) { get; set; } | All instances of entities that have their `exportToToc` flag enabled are listed in this array. |
| [TutorialDesc](LDtkFileFull/TutorialDesc.md) { get; set; } | This optional description is used by LDtk Samples to show up some informations and instructions. |
| [WorldGridHeight](LDtkFileFull/WorldGridHeight.md) { get; set; } | WARNING: this field will move to the `worlds` array after the "multi-worlds" update. It will then be `null`. You can enable the Multi-worlds advanced project option to enable the change immediately. Height of the world grid in pixels. |
| [WorldGridWidth](LDtkFileFull/WorldGridWidth.md) { get; set; } | WARNING: this field will move to the `worlds` array after the "multi-worlds" update. It will then be `null`. You can enable the Multi-worlds advanced project option to enable the change immediately. Width of the world grid in pixels. |
| [WorldLayout](LDtkFileFull/WorldLayout.md) { get; set; } | WARNING: this field will move to the `worlds` array after the "multi-worlds" update. It will then be `null`. You can enable the Multi-worlds advanced project option to enable the change immediately. An enum that describes how levels are organized in this project (ie. linearly or in a 2D space). Possible values: &lt;`null`&gt;, `Free`, `GridVania`, `LinearHorizontal`, `LinearVertical` |
| [Worlds](LDtkFileFull/Worlds.md) { get; set; } | This array will be empty, unless you enable the Multi-Worlds in the project advanced settings. - in current version, a LDtk project file can only contain a single world with multiple levels in it. In this case, levels and world layout related settings are stored in the root of the JSON. - with "Multi-worlds" enabled, there will be a `worlds` array in root, each world containing levels and layout settings. Basically, it's pretty much only about moving the `levels` array to the `worlds` array, along with world layout related values (eg. `worldGridWidth` etc).If you want to start supporting this future update easily, please refer to this documentation: https://github.com/deepnight/ldtk/issues/231 |
| [_FORCED_REFS](LDtkFileFull/_FORCED_REFS.md) { get; set; } | This object is not actually used by LDtk. It ONLY exists to force explicit references to all types, to make sure QuickType finds them and integrate all of them. Otherwise, Quicktype will drop types that are not explicitely used. |

## See Also

* namespace [LDtk.Full](../LDtkMonogame.md)

<!-- DO NOT EDIT: generated by xmldocmd for LDtkMonogame.dll -->
