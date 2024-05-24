# LevelBackgroundPosition Class

[Home](../../README.md) &#x2022; [Constructors](#constructors) &#x2022; [Properties](#properties) &#x2022; [Methods](#methods)

**Namespace**: [LDtk](../README.md)

**Assembly**: LDtkMonogame\.dll

  
 Level background image position info 

```csharp
public class LevelBackgroundPosition
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; LevelBackgroundPosition

## Constructors

| Constructor | Summary |
| ----------- | ------- |
| [LevelBackgroundPosition()](-ctor/README.md) | |

## Properties

| Property | Summary |
| -------- | ------- |
| [CropRect](CropRect/README.md) |  An array of 4 float values describing the cropped sub\-rectangle of the displayed background image\. This cropping happens when original is larger than the level bounds\. Array format: `[ cropX, cropY, cropWidth, cropHeight ]`  |
| [Scale](Scale/README.md) |  An array containing the `[scaleX,scaleY]` values of the  background image, depending on `bgPos` option\.  |
| [TopLeftPx](TopLeftPx/README.md) |  An array containing the `[x,y]` pixel coordinates of the top\-left corner of the  background image, depending on `bgPos` option\.  |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |

