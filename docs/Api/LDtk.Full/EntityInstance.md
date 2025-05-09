# EntityInstance class

Entity instance

```csharp
public class EntityInstance
```

## Public Members

| name | description |
| --- | --- |
| [EntityInstance](EntityInstance/EntityInstance.md)() | The default constructor. |
| [DefUid](EntityInstance/DefUid.md) { get; set; } | Reference of the Entity definition UID |
| [FieldInstances](EntityInstance/FieldInstances.md) { get; set; } | An array of all custom fields and their values. |
| [Height](EntityInstance/Height.md) { get; set; } | Entity height in pixels. For non-resizable entities, it will be the same as Entity definition. |
| [Iid](EntityInstance/Iid.md) { get; set; } | Unique instance identifier |
| [Px](EntityInstance/Px.md) { get; set; } | Pixel coordinates (`[x,y]` format) in current level coordinate space. Don't forget optional layer offsets, if they exist! |
| [Width](EntityInstance/Width.md) { get; set; } | Entity width in pixels. For non-resizable entities, it will be the same as Entity definition. |
| [_Grid](EntityInstance/_Grid.md) { get; set; } | Grid-based coordinates (`[x,y]` format) |
| [_Identifier](EntityInstance/_Identifier.md) { get; set; } | Entity definition identifier |
| [_Pivot](EntityInstance/_Pivot.md) { get; set; } | Pivot coordinates (`[x,y]` format, values are from 0 to 1) of the Entity |
| [_SmartColor](EntityInstance/_SmartColor.md) { get; set; } | The entity "smart" color, guessed from either Entity definition, or one its field instances. |
| [_Tags](EntityInstance/_Tags.md) { get; set; } | Array of tags defined in this Entity definition |
| [_Tile](EntityInstance/_Tile.md) { get; set; } | Optional TilesetRect used to display this entity (it could either be the default Entity tile, or some tile provided by a field value, like an Enum). |
| [_WorldX](EntityInstance/_WorldX.md) { get; set; } | X world coordinate in pixels. Only available in GridVania or Free world layouts. |
| [_WorldY](EntityInstance/_WorldY.md) { get; set; } | Y world coordinate in pixels Only available in GridVania or Free world layouts. |

## See Also

* namespace [LDtk.Full](../LDtkMonogame.md)

<!-- DO NOT EDIT: generated by xmldocmd for LDtkMonogame.dll -->
