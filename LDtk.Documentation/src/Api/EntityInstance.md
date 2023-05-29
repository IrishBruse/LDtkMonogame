# EntityInstance

EntityInstance

## Properties

  
Reference of the Entity definition UID  


```csharp
public int DefUid { get; set; }
```

  
An array of all custom fields and their values.  


```csharp
public FieldInstance[] FieldInstances { get; set; }
```

  
Grid-based coordinates ([x,y] format)  


```csharp
public Point _Grid { get; set; }
```

  
Entity height in pixels. For non-resizable entities, it will be the same as Entity  
definition.  


```csharp
public int Height { get; set; }
```

  
Entity definition identifier  


```csharp
public string _Identifier { get; set; }
```

  
Unique instance identifier  


```csharp
public Guid Iid { get; set; }
```

  
Pivot coordinates  ([x,y] format, values are from 0 to 1) of the Entity  


```csharp
public Vector2 _Pivot { get; set; }
```

  
Pixel coordinates ([x,y] format) in current level coordinate space. Don't forget  
optional layer offsets, if they exist!  


```csharp
public Point Px { get; set; }
```

  
The entity "smart" color, guessed from either Entity definition, or one its field  
instances.  


```csharp
public Color _SmartColor { get; set; }
```

  
Array of tags defined in this Entity definition  


```csharp
public string[] _Tags { get; set; }
```

  
Optional TilesetRect used to display this entity (it could either be the default Entity  
tile, or some tile provided by a field value, like an Enum).  


```csharp
public TilesetRectangle _Tile { get; set; }
```

  
Entity width in pixels. For non-resizable entities, it will be the same as Entity  
definition.  


```csharp
public int Width { get; set; }
```


