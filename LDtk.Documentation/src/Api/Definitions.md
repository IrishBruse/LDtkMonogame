# Definitions

  
If you're writing your own LDtk importer, you should probably just ignore most stuff in  
the defs section, as it contains data that are mostly important to the editor. To keep  
you away from the defs section and avoid some unnecessary JSON parsing, important data  
from definitions is often duplicated in fields prefixed with a double underscore (eg.  
__identifier or __type).  The 2 only definition types you might need here are  
Tilesets and Enums.  
  
A structure containing all the definitions of this project  


## Properties

  
All entities definitions, including their custom fields  


```csharp
public EntityDefinition[] Entities { get; set; }
```

  
All internal enums  


```csharp
public EnumDefinition[] Enums { get; set; }
```

  
Note: external enums are exactly the same as enums, except they have a relPath to  
point to an external source file.  


```csharp
public EnumDefinition[] ExternalEnums { get; set; }
```

  
All layer definitions  


```csharp
public LayerDefinition[] Layers { get; set; }
```

  
All tilesets  


```csharp
public TilesetDefinition[] Tilesets { get; set; }
```


