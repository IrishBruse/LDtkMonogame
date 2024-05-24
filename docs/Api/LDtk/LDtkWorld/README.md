# LDtkWorld Class

[Home](../../README.md) &#x2022; [Constructors](#constructors) &#x2022; [Properties](#properties) &#x2022; [Methods](#methods)

**Namespace**: [LDtk](../README.md)

**Assembly**: LDtkMonogame\.dll

  
 : this type is available as a preview\. You can rely on it to update your importers, for when it will be officially available\.  A World contains multiple levels, and it has its own layout settings\. 

```csharp
public class LDtkWorld
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; LDtkWorld

## Constructors

| Constructor | Summary |
| ----------- | ------- |
| [LDtkWorld()](-ctor/README.md) |  Initializes a new instance of the [LDtkWorld](./README.md) class\. Used by json deserializer not for use by user\!\.  |

## Properties

| Property | Summary |
| -------- | ------- |
| [Content](Content/README.md) |  Gets or sets the content manager used if you are using the contentpipeline\.  |
| [FilePath](FilePath/README.md) |  Gets or sets the absolute filepath to the world\.  |
| [Identifier](Identifier/README.md) |  User defined unique identifier  |
| [Iid](Iid/README.md) |  Unique instance identifer  |
| [Levels](Levels/README.md) |  All levels from this world\. The order of this array is only relevant in `LinearHorizontal` and `linearVertical` world layouts \(see `worldLayout` value\)\. Otherwise, you should refer to the `worldX`,`worldY` coordinates of each Level\.  |
| [WorldGridHeight](WorldGridHeight/README.md) |  Height of the world grid in pixels\.  |
| [WorldGridSize](WorldGridSize/README.md) |  Gets size of the world grid in pixels\.  |
| [WorldGridWidth](WorldGridWidth/README.md) |  Width of the world grid in pixels\.  |
| [WorldLayout](WorldLayout/README.md) |  An enum that describes how levels are organized in this project \(ie\. linearly or in a 2D space\)\. Possible values: `Free`, `GridVania`, `LinearHorizontal`, `LinearVertical`, `null`, `null`  |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetEntity\<T\>()](GetEntity/README.md) |  Goes through all the loaded levels looking for the entity\.  |
| [GetEntityRef\<T\>(EntityReference)](GetEntityRef/README.md) |  Gets an entity from an **reference** converted to **T**\.  |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [LoadLevel(Guid)](LoadLevel/README.md#262007456) |  Get the level with an iid\.  |
| [LoadLevel(Int32)](LoadLevel/README.md#2827945550) |  Get the level with an index\.  |
| [LoadLevel(String)](LoadLevel/README.md#3019622167) |  Get the level with an identifier\.  |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |

