# EntityDefinition

EntityDefinition

## Properties

  
Base entity color  


```csharp
public Color Color { get; set; }
```

  
Pixel height  


```csharp
public int Height { get; set; }
```

  
User defined unique identifier  


```csharp
public string Identifier { get; set; }
```

  
An array of 4 dimensions for the up/right/down/left borders (in this order) when using  
9-slice mode for tileRenderMode. If the tileRenderMode is not NineSlice, then  
this array is empty. See: https://en.wikipedia.org/wiki/9-slice_scaling  


```csharp
public int[] NineSliceBorders { get; set; }
```

  
Pivot X coordinate (from 0 to 1.0)  


```csharp
public float PivotX { get; set; }
```

  
Pivot Y coordinate (from 0 to 1.0)  


```csharp
public float PivotY { get; set; }
```

  
An object representing a rectangle from an existing Tileset  


```csharp
public TilesetRectangle TileRect { get; set; }
```

  
An enum describing how the the Entity tile is rendered inside the Entity bounds. Possible  
values: Cover, FitInside, Repeat, Stretch, FullSizeCropped,  
FullSizeUncropped, NineSlice  


```csharp
public TileRenderMode TileRenderMode { get; set; }
```

  
Tileset ID used for optional tile display  


```csharp
public int? TilesetId { get; set; }
```

  
Unique Int identifier  


```csharp
public int Uid { get; set; }
```

  
Pixel width  


```csharp
public int Width { get; set; }
```


