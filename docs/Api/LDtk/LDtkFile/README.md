# LDtkFile Class

[Home](../../README.md) &#x2022; [Constructors](#constructors) &#x2022; [Properties](#properties) &#x2022; [Methods](#methods)

**Namespace**: [LDtk](../README.md)

**Assembly**: LDtkMonogame\.dll

  
This file is a JSON schema of files created by LDtk level editor \(https://ldtk\.io\)\.

This is the root of any Project JSON file\. It contains:  \- the project settings, \- an
array of levels, \- a group of definitions \(that can probably be safely ignored for most
users\)\.

```csharp
public class LDtkFile
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; LDtkFile

## Constructors

| Constructor | Summary |
| ----------- | ------- |
| [LDtkFile()](-ctor/README.md) |  Initializes a new instance of the [LDtkFile](./README.md) class\. Used by json deserializer not for use by user\.  |

## Properties

| Property | Summary |
| -------- | ------- |
| [BgColor](BgColor/README.md) | Project background color |
| [Content](Content/README.md) |  Gets or sets the content manager used if you are using the contentpipeline\.  |
| [Defs](Defs/README.md) | A structure containing all the definitions of this project |
| [ExternalLevels](ExternalLevels/README.md) | If TRUE, one file will be saved for the project \(incl\. all its definitions\) and one file in a sub\-folder for each level\. |
| [FilePath](FilePath/README.md) |  Gets or sets the absolute path to the ldtkFile\.  |
| [Iid](Iid/README.md) | Unique project identifier |
| [JsonVersion](JsonVersion/README.md) | File format version |
| [Toc](Toc/README.md) | All instances of entities that have their exportToToc flag enabled are listed in this array\. |
| [WorldGridHeight](WorldGridHeight/README.md) | WARNING: this field will move to the worlds array after the "multi\-worlds" update\. It will then be null\. You can enable the Multi\-worlds advanced project option to enable the change immediately\.  Height of the world grid in pixels\. |
| [WorldGridWidth](WorldGridWidth/README.md) | WARNING: this field will move to the worlds array after the "multi\-worlds" update\. It will then be null\. You can enable the Multi\-worlds advanced project option to enable the change immediately\.  Width of the world grid in pixels\. |
| [WorldLayout](WorldLayout/README.md) | WARNING: this field will move to the worlds array after the "multi\-worlds" update\. It will then be null\. You can enable the Multi\-worlds advanced project option to enable the change immediately\.  An enum that describes how levels are organized in this project \(ie\. linearly or in a 2D space\)\. Possible values: \<null\>, Free, GridVania, LinearHorizontal, LinearVertical, null |
| [Worlds](Worlds/README.md) | This array will be empty, unless you enable the Multi\-Worlds in the project advanced settings\. \- in current version, a LDtk project file can only contain a single world with multiple levels in it\. In this case, levels and world layout related settings are stored in the root of the JSON\. \- with "Multi\-worlds" enabled, there will be a worlds array in root, each world containing levels and layout settings\. Basically, it's pretty much only about moving the levels array to the worlds array, along with world layout related values \(eg\. worldGridWidth etc\)\.If you want to start supporting this future update easily, please refer to this documentation: https://github\.com/deepnight/ldtk/issues/231 |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [FromFile(String, ContentManager)](FromFile/README.md#2029094268) |  Loads the ldtk world file from disk directly\.  |
| [FromFile(String)](FromFile/README.md#1380513718) |  Loads the ldtk world file from disk directly using json source generator\.  |
| [FromFileReflection(String)](FromFileReflection/README.md) |  Loads the ldtk world file from disk directly\.  |
| [GetEntityRef\<T\>(EntityRef)](GetEntityRef/README.md) |  Gets an entity from an **entityRef** converted to **T**\.  |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [LoadWorld(Guid)](LoadWorld/README.md) |  Loads the ldtkl world file from disk directly or from the embeded one depending on if the file uses externalworlds\.  |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |

