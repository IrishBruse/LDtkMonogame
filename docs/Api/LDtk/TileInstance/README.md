# TileInstance Class

[Home](../../README.md) &#x2022; [Constructors](#constructors) &#x2022; [Properties](#properties) &#x2022; [Methods](#methods)

**Namespace**: [LDtk](../README.md)

**Assembly**: LDtkMonogame\.dll

  
This structure represents a single tile from a given Tileset\.

```csharp
public class TileInstance
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; TileInstance

## Constructors

| Constructor | Summary |
| ----------- | ------- |
| [TileInstance()](-ctor/README.md) | |

## Properties

| Property | Summary |
| -------- | ------- |
| [A](A/README.md) | Alpha/opacity of the tile \(0\-1, defaults to 1\) |
| [F](F/README.md) | "Flip bits", a 2\-bits integer to represent the mirror transformations of the tile\. \- Bit 0 = X flip   \- Bit 1 = Y flip   Examples: f=0 \(no flip\), f=1 \(X flip only\), f=2 \(Y flip only\), f=3 \(both flips\) |
| [Px](Px/README.md) | Pixel coordinates of the tile in the layer \(\[x,y\] format\)\. Don't forget optional layer offsets, if they exist\! |
| [Src](Src/README.md) | Pixel coordinates of the tile in the tileset \(\[x,y\] format\) |
| [T](T/README.md) | The Tile ID in the corresponding tileset\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |

