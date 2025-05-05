# LayerDefinition class

Layer definition

```csharp
public class LayerDefinition
```

## Public Members

| name | description |
| --- | --- |
| [LayerDefinition](LayerDefinition/LayerDefinition.md)() | The default constructor. |
| [AutoRuleGroups](LayerDefinition/AutoRuleGroups.md) { get; set; } | Contains all the auto-layer rule definitions. |
| [AutoSourceLayerDefUid](LayerDefinition/AutoSourceLayerDefUid.md) { get; set; } | AutoSourceLayerDefUid |
| [AutoTilesetDefUid](LayerDefinition/AutoTilesetDefUid.md) { get; set; } | WARNING: this deprecated value is no longer exported since version 1.2.0 Replaced by: `tilesetDefUid` |
| [AutoTilesKilledByOtherLayerUid](LayerDefinition/AutoTilesKilledByOtherLayerUid.md) { get; set; } | AutoTilesKilledByOtherLayerUid |
| [BiomeFieldUid](LayerDefinition/BiomeFieldUid.md) { get; set; } | BiomeFieldUid |
| [CanSelectWhenInactive](LayerDefinition/CanSelectWhenInactive.md) { get; set; } | Allow editor selections when the layer is not currently active. |
| [DisplayOpacity](LayerDefinition/DisplayOpacity.md) { get; set; } | Opacity of the layer (0 to 1.0) |
| [Doc](LayerDefinition/Doc.md) { get; set; } | User defined documentation for this element to provide help/tips to level designers. |
| [ExcludedTags](LayerDefinition/ExcludedTags.md) { get; set; } | An array of tags to forbid some Entities in this layer |
| [GridSize](LayerDefinition/GridSize.md) { get; set; } | Width and height of the grid in pixels |
| [GuideGridHei](LayerDefinition/GuideGridHei.md) { get; set; } | Height of the optional "guide" grid in pixels |
| [GuideGridWid](LayerDefinition/GuideGridWid.md) { get; set; } | Width of the optional "guide" grid in pixels |
| [HideFieldsWhenInactive](LayerDefinition/HideFieldsWhenInactive.md) { get; set; } | HideFieldsWhenInactive |
| [HideInList](LayerDefinition/HideInList.md) { get; set; } | Hide the layer from the list on the side of the editor view. |
| [Identifier](LayerDefinition/Identifier.md) { get; set; } | User defined unique identifier |
| [InactiveOpacity](LayerDefinition/InactiveOpacity.md) { get; set; } | Alpha of this layer when it is not the active one. |
| [IntGridValues](LayerDefinition/IntGridValues.md) { get; set; } | An array that defines extra optional info for each IntGrid value. WARNING: the array order is not related to actual IntGrid values! As user can re-order IntGrid values freely, you may value "2" before value "1" in this array. |
| [IntGridValuesGroups](LayerDefinition/IntGridValuesGroups.md) { get; set; } | Group informations for IntGrid values |
| [ParallaxFactorX](LayerDefinition/ParallaxFactorX.md) { get; set; } | Parallax horizontal factor (from -1 to 1, defaults to 0) which affects the scrolling speed of this layer, creating a fake 3D (parallax) effect. |
| [ParallaxFactorY](LayerDefinition/ParallaxFactorY.md) { get; set; } | Parallax vertical factor (from -1 to 1, defaults to 0) which affects the scrolling speed of this layer, creating a fake 3D (parallax) effect. |
| [ParallaxScaling](LayerDefinition/ParallaxScaling.md) { get; set; } | If true (default), a layer with a parallax factor will also be scaled up/down accordingly. |
| [PxOffsetX](LayerDefinition/PxOffsetX.md) { get; set; } | X offset of the layer, in pixels (IMPORTANT: this should be added to the `LayerInstance` optional offset) |
| [PxOffsetY](LayerDefinition/PxOffsetY.md) { get; set; } | Y offset of the layer, in pixels (IMPORTANT: this should be added to the `LayerInstance` optional offset) |
| [RenderInWorldView](LayerDefinition/RenderInWorldView.md) { get; set; } | If TRUE, the content of this layer will be used when rendering levels in a simplified way for the world view |
| [RequiredTags](LayerDefinition/RequiredTags.md) { get; set; } | An array of tags to filter Entities that can be added to this layer |
| [TilePivotX](LayerDefinition/TilePivotX.md) { get; set; } | If the tiles are smaller or larger than the layer grid, the pivot value will be used to position the tile relatively its grid cell. |
| [TilePivotY](LayerDefinition/TilePivotY.md) { get; set; } | If the tiles are smaller or larger than the layer grid, the pivot value will be used to position the tile relatively its grid cell. |
| [TilesetDefUid](LayerDefinition/TilesetDefUid.md) { get; set; } | Reference to the default Tileset UID being used by this layer definition.WARNING: some layer *instances* might use a different tileset. So most of the time, you should probably use the `__tilesetDefUid` value found in layer instances. Note: since version 1.0.0, the old `autoTilesetDefUid` was removed and merged into this value. |
| [Type](LayerDefinition/Type.md) { get; set; } | Type of the layer as Haxe Enum Possible values: `IntGrid`, `Entities`, `Tiles`, `AutoLayer` |
| [UiColor](LayerDefinition/UiColor.md) { get; set; } | User defined color for the UI |
| [Uid](LayerDefinition/Uid.md) { get; set; } | Unique Int identifier |
| [UiFilterTags](LayerDefinition/UiFilterTags.md) { get; set; } | Display tags |
| [UseAsyncRender](LayerDefinition/UseAsyncRender.md) { get; set; } | Asynchronous rendering option for large/complex layers |
| [_Type](LayerDefinition/_Type.md) { get; set; } | Type of the layer (*IntGrid, Entities, Tiles or AutoLayer*) |

## See Also

* namespace [LDtk.Full](../LDtkMonogame.md)

<!-- DO NOT EDIT: generated by xmldocmd for LDtkMonogame.dll -->
