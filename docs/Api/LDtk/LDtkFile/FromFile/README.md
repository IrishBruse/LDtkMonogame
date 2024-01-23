# LDtkFile\.FromFile Method

[Home](../../../README.md)

**Containing Type**: [LDtkFile](../README.md)

**Assembly**: LDtkMonogame\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| [FromFile(String, ContentManager)](#2029094268) |  Loads the ldtk world file from disk directly\.  |
| [FromFile(String)](#1380513718) |  Loads the ldtk world file from disk directly using json source generator\.  |

<a id="2029094268"></a>

## FromFile\(String, ContentManager\) 

  
 Loads the ldtk world file from disk directly\. 

```csharp
public static LDtk.LDtkFile FromFile(string filePath, Microsoft.Xna.Framework.Content.ContentManager content)
```

### Parameters

**filePath** &ensp; [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)

Path to the \.ldtk file excluding file extension\.

**content** &ensp; [ContentManager](https://docs.microsoft.com/en-us/dotnet/api/microsoft.xna.framework.content.contentmanager)

The optional content manager if you are using the content pipeline\.

### Returns

[LDtkFile](../README.md)

 Returns the file loaded from the path\. <a id="1380513718"></a>

## FromFile\(String\) 

  
 Loads the ldtk world file from disk directly using json source generator\. 

```csharp
public static LDtk.LDtkFile FromFile(string filePath)
```

### Parameters

**filePath** &ensp; [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)

 Path to the \.ldtk file\. 

### Returns

[LDtkFile](../README.md)

 Returns the file loaded from the path\. 