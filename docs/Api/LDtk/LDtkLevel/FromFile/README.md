# LDtkLevel\.FromFile Method

[Home](../../../README.md)

**Containing Type**: [LDtkLevel](../README.md)

**Assembly**: LDtkMonogame\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| [FromFile(String, ContentManager)](#2401859081) |  Loads the ldtk world file from disk directly\.  |
| [FromFile(String)](#1344876828) |  Loads the ldtk world file from disk directly using json source generator\.  |

<a id="2401859081"></a>

## FromFile\(String, ContentManager\) 

  
 Loads the ldtk world file from disk directly\. 

```csharp
public static LDtk.LDtkLevel? FromFile(string filePath, Microsoft.Xna.Framework.Content.ContentManager content)
```

### Parameters

**filePath** &ensp; [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)

Path to the \.ldtk file excluding file extension\.

**content** &ensp; [ContentManager](https://docs.microsoft.com/en-us/dotnet/api/microsoft.xna.framework.content.contentmanager)

The optional content manager if you are using the content pipeline\.

### Returns

[LDtkLevel](../README.md)

<a id="1344876828"></a>

## FromFile\(String\) 

  
 Loads the ldtk world file from disk directly using json source generator\. 

```csharp
public static LDtk.LDtkLevel? FromFile(string filePath)
```

### Parameters

**filePath** &ensp; [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)

 Path to the \.ldtk file\. 

### Returns

[LDtkLevel](../README.md)

