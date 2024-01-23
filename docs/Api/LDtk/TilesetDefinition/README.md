# TilesetDefinition Class

[Home](../../README.md) &#x2022; [Constructors](#constructors) &#x2022; [Properties](#properties) &#x2022; [Methods](#methods)

**Namespace**: [LDtk](../README.md)

**Assembly**: LDtkMonogame\.dll

  
The Tileset definition is the most important part among project definitions\. It
contains some extra informations about each integrated tileset\. If you only had to parse
one definition section, that would be the one\.

```csharp
public class TilesetDefinition
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; TilesetDefinition

## Constructors

| Constructor | Summary |
| ----------- | ------- |
| [TilesetDefinition()](-ctor/README.md) | |

## Properties

| Property | Summary |
| -------- | ------- |
| [_CHei](_CHei/README.md) | Grid\-based height |
| [_CWid](_CWid/README.md) | Grid\-based width |
| [CustomData](CustomData/README.md) | An array of custom tile metadata |
| [EmbedAtlas](EmbedAtlas/README.md) | If this value is set, then it means that this atlas uses an internal LDtk atlas image instead of a loaded one\. Possible values: \<null\>, LdtkIcons, null |
| [EnumTags](EnumTags/README.md) | Tileset tags using Enum values specified by tagsSourceEnumId\. This array contains 1 element per Enum value, which contains an array of all Tile IDs that are tagged with it\. |
| [Identifier](Identifier/README.md) | User defined unique identifier |
| [Padding](Padding/README.md) | Distance in pixels from image borders |
| [PxHei](PxHei/README.md) | Image height in pixels |
| [PxWid](PxWid/README.md) | Image width in pixels |
| [RelPath](RelPath/README.md) | Path to the source file, relative to the current project JSON file  It can be null if no image was provided, or when using an embed atlas\. |
| [Spacing](Spacing/README.md) | Space in pixels between all tiles |
| [Tags](Tags/README.md) | An array of user\-defined tags to organize the Tilesets |
| [TagsSourceEnumUid](TagsSourceEnumUid/README.md) | Optional Enum definition UID used for this tileset meta\-data |
| [TileGridSize](TileGridSize/README.md) | |
| [Uid](Uid/README.md) | Unique Intidentifier |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |

