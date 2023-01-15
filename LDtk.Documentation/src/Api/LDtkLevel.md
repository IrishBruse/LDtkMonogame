# LDtkLevel

  
This section contains all the level data. It can be found in 2 distinct forms, depending  
on Project current settings:  - If "Separate level files" is disabled (default):  
full level data is embedded inside the main Project JSON file, - If "Separate level  
files" is enabled: level data is stored in separate standalone .ldtkl files (one  
per level). In this case, the main Project JSON file will still contain most level data,  
except heavy sections, like the layerInstances array (which will be null). The  
externalRelPath string points to the ldtkl file.  A ldtkl file is just a JSON file  
containing exactly what is described below.  


## Methods

Used by json deserializer not for use by user!
```csharp
public LDtkLevel()
```

Loads the ldtk world file from disk directly
```csharp
public static LDtkLevel FromFile(string filePath);
```

Loads the ldtk world file from disk directly
```csharp
public static LDtkLevel FromFile(string filePath);
```

Gets an intgrid with the  in a
```csharp
public LDtkIntGrid GetIntGrid(string identifier);
```

- else **LDtk.LDtkLevel.GetCustomFields``1**
- else **LDtk.LDtkLevel.GetEntity``1**
- else **LDtk.LDtkLevel.GetEntityRef``1**
- else **LDtk.LDtkLevel.GetEntities``1**
Check if point is inside of a level
```csharp
public bool Contains(Vector2 point);
```

Check if point is inside of a level
```csharp
public bool Contains(Vector2 point);
```


## Properties

The absolute filepath to the level
```csharp
public string FilePath { get; set; }
```

World Position of the level in pixels
```csharp
public Point Position { get; set; }
```

World size of the level in pixels
```csharp
public Point Size { get; set; }
```

Has the file been loaded if the level is external
```csharp
public bool Loaded { get; set; }
```

  
Background color of the level (same as bgColor, except the default value is  
automatically used here if its value is null)  

```csharp
public string _BgColor { get; set; }
```

  
Position informations of the background image, if there is one.  

```csharp
public LevelBackgroundPosition _BgPos { get; set; }
```

  
An array listing all other levels touching this one on the world map. Only relevant  
for world layouts where level spatial positioning is manual (ie. GridVania, Free). For  
Horizontal and Vertical layouts, this array is always empty.  

```csharp
public NeighbourLevel[] _Neighbours { get; set; }
```

  
The optional relative path to the level background image.  

```csharp
public string BgRelPath { get; set; }
```

  
This value is not null if the project option "Save levels separately" is enabled. In  
this case, this relative path points to the level Json file.  

```csharp
public string ExternalRelPath { get; set; }
```

  
An array containing this level custom field values.  

```csharp
public FieldInstance[] FieldInstances { get; set; }
```

  
User defined unique identifier  

```csharp
public string Identifier { get; set; }
```

  
Unique instance identifier  

```csharp
public Guid Iid { get; set; }
```

  
An array containing all Layer instances. IMPORTANT: if the project option "Save  
levels separately" is enabled, this field will be null. This array is sorted  
in display order: the 1st layer is the top-most and the last is behind.  

```csharp
public LayerInstance[] LayerInstances { get; set; }
```

  
Height of the level in pixels  

```csharp
public int PxHei { get; set; }
```

  
Width of the level in pixels  

```csharp
public int PxWid { get; set; }
```

  
Unique Int identifier  

```csharp
public int Uid { get; set; }
```

  
Index that represents the "depth" of the level in the world. Default is 0, greater means  
"above", lower means "below". This value is mostly used for display only and is  
intended to make stacking of levels easier to manage.  

```csharp
public int WorldDepth { get; set; }
```

  
World X coordinate in pixels. Only relevant for world layouts where level spatial  
positioning is manual (ie. GridVania, Free). For Horizontal and Vertical layouts, the  
value is always -1 here.  

```csharp
public int WorldX { get; set; }
```

  
World Y coordinate in pixels. Only relevant for world layouts where level spatial  
positioning is manual (ie. GridVania, Free). For Horizontal and Vertical layouts, the  
value is always -1 here.  

```csharp
public int WorldY { get; set; }
```


