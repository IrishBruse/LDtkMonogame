# LDtkLevel class

This section contains all the level data. It can be found in 2 distinct forms, depending on Project current settings: - If "*Separate level files*" is disabled (default): full level data is *embedded* inside the main Project JSON file, - If "*Separate level files*" is enabled: level data is stored in *separate* standalone `.ldtkl` files (one per level). In this case, the main Project JSON file will still contain most level data, except heavy sections, like the `layerInstances` array (which will be null). The `externalRelPath` string points to the `ldtkl` file. A `ldtkl` file is just a JSON file containing exactly what is described below.

```csharp
public class LDtkLevel
```

## Public Members

| name | description |
| --- | --- |
| [LDtkLevel](LDtkLevel/LDtkLevel.md)() | Initializes a new instance of the [`LDtkLevel`](./LDtkLevel.md) class. Used by json deserializer not for use by user!. |
| static [FromFile](LDtkLevel/FromFile.md)(…) | Loads the ldtk world file from disk directly using json source generator. (2 methods) |
| static [FromFileReflection](LDtkLevel/FromFileReflection.md)(…) | Loads the ldtk world file from disk directly. |
| [BgRelPath](LDtkLevel/BgRelPath.md) { get; set; } | The *optional* relative path to the level background image. |
| [ExternalRelPath](LDtkLevel/ExternalRelPath.md) { get; set; } | This value is not null if the project option "*Save levels separately*" is enabled. In this case, this relative path points to the level Json file. |
| [FieldInstances](LDtkLevel/FieldInstances.md) { get; set; } | An array containing this level custom field values. |
| [FilePath](LDtkLevel/FilePath.md) { get; set; } | Gets or sets the absolute filepath to the level. |
| [Identifier](LDtkLevel/Identifier.md) { get; set; } | User defined unique identifier |
| [Iid](LDtkLevel/Iid.md) { get; set; } | Unique instance identifier |
| [LayerInstances](LDtkLevel/LayerInstances.md) { get; set; } | An array containing all Layer instances. IMPORTANT: if the project option "*Save levels separately*" is enabled, this field will be `null`. This array is sorted in display order: the 1st layer is the top-most and the last is behind. |
| [Loaded](LDtkLevel/Loaded.md) { get; } | Gets a value indicating whether the file been loaded externaly. |
| [Position](LDtkLevel/Position.md) { get; } | Gets world Position of the level in pixels. |
| [PxHei](LDtkLevel/PxHei.md) { get; set; } | Height of the level in pixels |
| [PxWid](LDtkLevel/PxWid.md) { get; set; } | Width of the level in pixels |
| [Size](LDtkLevel/Size.md) { get; } | Gets world size of the level in pixels. |
| [Uid](LDtkLevel/Uid.md) { get; set; } | Unique Int identifier |
| [WorldDepth](LDtkLevel/WorldDepth.md) { get; set; } | Index that represents the "depth" of the level in the world. Default is 0, greater means "above", lower means "below". This value is mostly used for display only and is intended to make stacking of levels easier to manage. |
| [WorldFilePath](LDtkLevel/WorldFilePath.md) { get; set; } | Gets or sets the absolute filepath to the world. |
| [WorldX](LDtkLevel/WorldX.md) { get; set; } | World X coordinate in pixels. Only relevant for world layouts where level spatial positioning is manual (ie. GridVania, Free). For Horizontal and Vertical layouts, the value is always -1 here. |
| [WorldY](LDtkLevel/WorldY.md) { get; set; } | World Y coordinate in pixels. Only relevant for world layouts where level spatial positioning is manual (ie. GridVania, Free). For Horizontal and Vertical layouts, the value is always -1 here. |
| [_BgColor](LDtkLevel/_BgColor.md) { get; set; } | Background color of the level (same as `bgColor`, except the default value is automatically used here if its value is `null`) |
| [_BgPos](LDtkLevel/_BgPos.md) { get; set; } | Position informations of the background image, if there is one. |
| [_Neighbours](LDtkLevel/_Neighbours.md) { get; set; } | An array listing all other levels touching this one on the world map. Since 1.4.0, this includes levels that overlap in the same world layer, or in nearby world layers. Only relevant for world layouts where level spatial positioning is manual (ie. GridVania, Free). For Horizontal and Vertical layouts, this array is always empty. |
| [Contains](LDtkLevel/Contains.md)(…) | Check if point is inside of a level. (2 methods) |
| [GetCustomFields&lt;T&gt;](LDtkLevel/GetCustomFields.md)() | Gets the custom fields of the level. |
| [GetEntities&lt;T&gt;](LDtkLevel/GetEntities.md)() | Gets an array of entities of type *T* in the current level. |
| [GetEntity&lt;T&gt;](LDtkLevel/GetEntity.md)() | Gets one entity of type T in the current level best used with 1 per level constraint. |
| [GetEntityRef&lt;T&gt;](LDtkLevel/GetEntityRef.md)(…) | Gets an entity from an *reference* converted to *T*. |
| [GetIntGrid](LDtkLevel/GetIntGrid.md)(…) | Gets an intgrid with the *identifier* in a [`LDtkLevel`](./LDtkLevel.md). |

## See Also

* namespace [LDtk](../LDtkMonogame.md)

<!-- DO NOT EDIT: generated by xmldocmd for LDtkMonogame.dll -->
