# TilesetDefinition

  
The Tileset definition is the most important part among project definitions. It  
contains some extra informations about each integrated tileset. If you only had to parse  
one definition section, that would be the one.  


## Properties

  
Grid-based height  


```csharp
public int _CHei { get; set; }
```

  
Grid-based width  


```csharp
public int _CWid { get; set; }
```

  
An array of custom tile metadata  


```csharp
public TileCustomMetadata[] CustomData { get; set; }
```

  
If this value is set, then it means that this atlas uses an internal LDtk atlas image  
instead of a loaded one. Possible values: <null>, LdtkIcons, null  


```csharp
public EmbedAtlas? EmbedAtlas { get; set; }
```

  
Tileset tags using Enum values specified by tagsSourceEnumId. This array contains 1  
element per Enum value, which contains an array of all Tile IDs that are tagged with it.  


```csharp
public EnumTagValue[] EnumTags { get; set; }
```

  
User defined unique identifier  


```csharp
public string Identifier { get; set; }
```

  
Distance in pixels from image borders  


```csharp
public int Padding { get; set; }
```

  
Image height in pixels  


```csharp
public int PxHei { get; set; }
```

  
Image width in pixels  


```csharp
public int PxWid { get; set; }
```

  
Path to the source file, relative to the current project JSON file It can be null  
if no image was provided, or when using an embed atlas.  


```csharp
public string RelPath { get; set; }
```

  
Space in pixels between all tiles  


```csharp
public int Spacing { get; set; }
```

  
An array of user-defined tags to organize the Tilesets  


```csharp
public string[] Tags { get; set; }
```

  
Optional Enum definition UID used for this tileset meta-data  


```csharp
public int? TagsSourceEnumUid { get; set; }
```

  
Unique Intidentifier  


```csharp
public int Uid { get; set; }
```


