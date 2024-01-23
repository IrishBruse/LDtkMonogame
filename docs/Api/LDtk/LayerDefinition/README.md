# LayerDefinition Class

[Home](../../README.md) &#x2022; [Constructors](#constructors) &#x2022; [Properties](#properties) &#x2022; [Methods](#methods)

**Namespace**: [LDtk](../README.md)

**Assembly**: LDtkMonogame\.dll

```csharp
public class LayerDefinition
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; LayerDefinition

## Constructors

| Constructor | Summary |
| ----------- | ------- |
| [LayerDefinition()](-ctor/README.md) | |

## Properties

| Property | Summary |
| -------- | ------- |
| [_Type](_Type/README.md) | Type of the layer \(IntGrid, Entities, Tiles or AutoLayer\) |
| [AutoSourceLayerDefUid](AutoSourceLayerDefUid/README.md) | |
| [DisplayOpacity](DisplayOpacity/README.md) | Opacity of the layer \(0 to 1\.0\) |
| [GridSize](GridSize/README.md) | Width and height of the grid in pixels |
| [Identifier](Identifier/README.md) | User defined unique identifier |
| [IntGridValues](IntGridValues/README.md) | An array that defines extra optional info for each IntGrid value\.  WARNING: the array order is not related to actual IntGrid values\! As user can re\-order IntGrid values freely, you may value "2" before value "1" in this array\. |
| [IntGridValuesGroups](IntGridValuesGroups/README.md) | Group informations for IntGrid values |
| [ParallaxFactorX](ParallaxFactorX/README.md) | Parallax horizontal factor \(from \-1 to 1, defaults to 0\) which affects the scrolling speed of this layer, creating a fake 3D \(parallax\) effect\. |
| [ParallaxFactorY](ParallaxFactorY/README.md) | Parallax vertical factor \(from \-1 to 1, defaults to 0\) which affects the scrolling speed of this layer, creating a fake 3D \(parallax\) effect\. |
| [ParallaxScaling](ParallaxScaling/README.md) | If true \(default\), a layer with a parallax factor will also be scaled up/down accordingly\. |
| [PxOffsetX](PxOffsetX/README.md) | X offset of the layer, in pixels \(IMPORTANT: this should be added to the LayerInstance optional offset\) |
| [PxOffsetY](PxOffsetY/README.md) | Y offset of the layer, in pixels \(IMPORTANT: this should be added to the LayerInstance optional offset\) |
| [TilesetDefUid](TilesetDefUid/README.md) | Reference to the default Tileset UID being used by this layer definition\. WARNING: some layer instances might use a different tileset\. So most of the time, you should probably use the \_\_tilesetDefUid value found in layer instances\.  Note: since version 1\.0\.0, the old autoTilesetDefUid was removed and merged into this value\. |
| [Uid](Uid/README.md) | Unique Int identifier |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |

