# EnumDefinition

EnumDefinition

## Properties

  
Relative path to the external file providing this Enum  


```csharp
public string ExternalRelPath { get; set; }
```

  
Tileset UID if provided  


```csharp
public int? IconTilesetUid { get; set; }
```

  
User defined unique identifier  


```csharp
public string Identifier { get; set; }
```

  
An array of user-defined tags to organize the Enums  


```csharp
public string[] Tags { get; set; }
```

  
Unique Int identifier  


```csharp
public int Uid { get; set; }
```

  
All possible enum values, with their optional Tile infos.  


```csharp
public EnumValueDefinition[] Values { get; set; }
```


