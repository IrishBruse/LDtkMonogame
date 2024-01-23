# LDtkIntGrid Class

[Home](../../README.md) &#x2022; [Constructors](#constructors) &#x2022; [Properties](#properties) &#x2022; [Methods](#methods)

**Namespace**: [LDtk](../README.md)

**Assembly**: LDtkMonogame\.dll

  
 LDtk IntGrid\. 

```csharp
public class LDtkIntGrid
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; LDtkIntGrid

## Constructors

| Constructor | Summary |
| ----------- | ------- |
| [LDtkIntGrid()](-ctor/README.md) | Initializes a new instance of the [LDtkIntGrid](./README.md) class\. Used by json deserializer not for use by user\!\.  |

## Properties

| Property | Summary |
| -------- | ------- |
| [GridSize](GridSize/README.md) |  Gets or sets worldspace start Position of the intgrid\.  |
| [TileSize](TileSize/README.md) |  Gets or sets size of a tile in pixels\.  |
| [Values](Values/README.md) |  Gets or sets the underlying values of the int grid\.  |
| [WorldPosition](WorldPosition/README.md) |  Gets or sets worldspace start Position of the intgrid\.  |

## Methods

| Method | Summary |
| ------ | ------- |
| [Contains(Point)](Contains/README.md#1537213663) |  Check if point is inside of a grid\.  |
| [Contains(Vector2)](Contains/README.md#1573660870) |  Check if point is inside of a grid\.  |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [FromWorldToGridSpace(Vector2)](FromWorldToGridSpace/README.md) |  Convert from world pixel space to int grid space Floors the value based on [LDtkIntGrid.TileSize](TileSize/README.md) to an Integer\.  |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetValueAt(Int32, Int32)](GetValueAt/README.md#3080842826) |  Gets the int value at location and return 0 if out of bounds\.  |
| [GetValueAt(Point)](GetValueAt/README.md#3374558993) |  Gets the int value at location and return 0 if out of bounds\.  |
| [GetValueAt(Vector2)](GetValueAt/README.md#3547435012) |  Gets the int value at location and return 0 if out of bounds\.  |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |

