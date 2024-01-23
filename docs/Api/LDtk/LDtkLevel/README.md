# LDtkLevel Class

[Home](../../README.md) &#x2022; [Constructors](#constructors) &#x2022; [Properties](#properties) &#x2022; [Methods](#methods)

**Namespace**: [LDtk](../README.md)

**Assembly**: LDtkMonogame\.dll

  
This section contains all the level data\. It can be found in 2 distinct forms, depending
on Project current settings:  \- If "Separate level files" is disabled \(default\):
full level data is embedded inside the main Project JSON file, \- If "Separate level
files" is enabled: level data is stored in separate standalone \.ldtkl files \(one
per level\)\. In this case, the main Project JSON file will still contain most level data,
except heavy sections, like the layerInstances array \(which will be null\)\. The
externalRelPath string points to the ldtkl file\.  A ldtkl file is just a JSON file
containing exactly what is described below\.

```csharp
public class LDtkLevel
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; LDtkLevel

## Constructors

| Constructor | Summary |
| ----------- | ------- |
| [LDtkLevel()](-ctor/README.md) | Initializes a new instance of the [LDtkLevel](./README.md) class\. Used by json deserializer not for use by user\!\.  |

## Properties

| Property | Summary |
| -------- | ------- |
| [_BgColor](_BgColor/README.md) | Background color of the level \(same as bgColor, except the default value is automatically used here if its value is null\) |
| [_BgPos](_BgPos/README.md) | Position informations of the background image, if there is one\. |
| [_Neighbours](_Neighbours/README.md) | An array listing all other levels touching this one on the world map\. Since 1\.4\.0, this includes levels that overlap in the same world layer, or in nearby world layers\. Only relevant for world layouts where level spatial positioning is manual \(ie\. GridVania, Free\)\. For Horizontal and Vertical layouts, this array is always empty\. |
| [BgRelPath](BgRelPath/README.md) | The optional relative path to the level background image\. |
| [ExternalRelPath](ExternalRelPath/README.md) | This value is not null if the project option "Save levels separately" is enabled\. In this case, this relative path points to the level Json file\. |
| [FieldInstances](FieldInstances/README.md) | An array containing this level custom field values\. |
| [FilePath](FilePath/README.md) |  Gets or sets the absolute filepath to the level\.  |
| [Identifier](Identifier/README.md) | User defined unique identifier |
| [Iid](Iid/README.md) | Unique instance identifier |
| [LayerInstances](LayerInstances/README.md) | An array containing all Layer instances\. IMPORTANT: if the project option "Save levels separately" is enabled, this field will be null\.  This array is sorted in display order: the 1st layer is the top\-most and the last is behind\. |
| [Loaded](Loaded/README.md) |  Gets a value indicating whether the file been loaded externaly\.  |
| [Position](Position/README.md) |  Gets world Position of the level in pixels\.  |
| [PxHei](PxHei/README.md) | Height of the level in pixels |
| [PxWid](PxWid/README.md) | Width of the level in pixels |
| [Size](Size/README.md) |  Gets world size of the level in pixels\.  |
| [Uid](Uid/README.md) | Unique Int identifier |
| [WorldDepth](WorldDepth/README.md) | Index that represents the "depth" of the level in the world\. Default is 0, greater means "above", lower means "below"\.  This value is mostly used for display only and is intended to make stacking of levels easier to manage\. |
| [WorldFilePath](WorldFilePath/README.md) |  Gets or sets the absolute filepath to the world\.  |
| [WorldX](WorldX/README.md) | World X coordinate in pixels\.  Only relevant for world layouts where level spatial positioning is manual \(ie\. GridVania, Free\)\. For Horizontal and Vertical layouts, the value is always \-1 here\. |
| [WorldY](WorldY/README.md) | World Y coordinate in pixels\.  Only relevant for world layouts where level spatial positioning is manual \(ie\. GridVania, Free\)\. For Horizontal and Vertical layouts, the value is always \-1 here\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Contains(Point)](Contains/README.md#4027557345) |  Check if point is inside of a level\.  |
| [Contains(Vector2)](Contains/README.md#1292010405) |  Check if point is inside of a level\.  |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [FromFile(String, ContentManager)](FromFile/README.md#2401859081) |  Loads the ldtk world file from disk directly\.  |
| [FromFile(String)](FromFile/README.md#1344876828) |  Loads the ldtk world file from disk directly using json source generator\.  |
| [FromFileReflection(String)](FromFileReflection/README.md) |  Loads the ldtk world file from disk directly\.  |
| [GetCustomFields\<T\>()](GetCustomFields/README.md) |  Gets the custom fields of the level\.  |
| [GetEntities\<T\>()](GetEntities/README.md) |  Gets an array of entities of type **T** in the current level\.  |
| [GetEntity\<T\>()](GetEntity/README.md) |  Gets one entity of type T in the current level best used with 1 per level constraint\.  |
| [GetEntityRef\<T\>(EntityRef)](GetEntityRef/README.md) |  Gets an entity from an **entityRef** converted to **T**\.  |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetIntGrid(String)](GetIntGrid/README.md) |  Gets an intgrid with the **identifier** in a [LDtkLevel](./README.md)\.  |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |

