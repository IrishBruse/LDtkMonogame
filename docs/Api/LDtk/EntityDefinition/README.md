# EntityDefinition Class

[Home](../../README.md) &#x2022; [Constructors](#constructors) &#x2022; [Properties](#properties) &#x2022; [Methods](#methods)

**Namespace**: [LDtk](../README.md)

**Assembly**: LDtkMonogame\.dll

```csharp
public class EntityDefinition
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; EntityDefinition

## Constructors

| Constructor | Summary |
| ----------- | ------- |
| [EntityDefinition()](-ctor/README.md) | |

## Properties

| Property | Summary |
| -------- | ------- |
| [Color](Color/README.md) | Base entity color |
| [Height](Height/README.md) | Pixel height |
| [Identifier](Identifier/README.md) | User defined unique identifier |
| [NineSliceBorders](NineSliceBorders/README.md) | An array of 4 dimensions for the up/right/down/left borders \(in this order\) when using 9\-slice mode for tileRenderMode\.  If the tileRenderMode is not NineSlice, then this array is empty\.  See: https://en\.wikipedia\.org/wiki/9\-slice\_scaling |
| [PivotX](PivotX/README.md) | Pivot X coordinate \(from 0 to 1\.0\) |
| [PivotY](PivotY/README.md) | Pivot Y coordinate \(from 0 to 1\.0\) |
| [TileRect](TileRect/README.md) | An object representing a rectangle from an existing Tileset |
| [TileRenderMode](TileRenderMode/README.md) | An enum describing how the the Entity tile is rendered inside the Entity bounds\. Possible values: Cover, FitInside, Repeat, Stretch, FullSizeCropped, FullSizeUncropped, NineSlice |
| [TilesetId](TilesetId/README.md) | Tileset ID used for optional tile display |
| [Uid](Uid/README.md) | Unique Int identifier |
| [UiTileRect](UiTileRect/README.md) | This tile overrides the one defined in tileRect in the UI |
| [Width](Width/README.md) | Pixel width |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |

