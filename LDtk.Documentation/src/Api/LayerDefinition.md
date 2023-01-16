# LayerDefinition

LayerDefinition

## Properties

  
Type of the layer (IntGrid, Entities, Tiles or AutoLayer)  


```csharp
public LayerType _Type { get; set; }
```

  
Opacity of the layer (0 to 1.0)  


```csharp
public float DisplayOpacity { get; set; }
```

  
Width and height of the grid in pixels  


```csharp
public int GridSize { get; set; }
```

  
User defined unique identifier  


```csharp
public string Identifier { get; set; }
```

  
An array that defines extra optional info for each IntGrid value. WARNING: the  
array order is not related to actual IntGrid values! As user can re-order IntGrid values  
freely, you may value "2" before value "1" in this array.  


```csharp
public IntGridValueDefinition[] IntGridValues { get; set; }
```

  
Parallax horizontal factor (from -1 to 1, defaults to 0) which affects the scrolling  
speed of this layer, creating a fake 3D (parallax) effect.  


```csharp
public float ParallaxFactorX { get; set; }
```

  
Parallax vertical factor (from -1 to 1, defaults to 0) which affects the scrolling speed  
of this layer, creating a fake 3D (parallax) effect.  


```csharp
public float ParallaxFactorY { get; set; }
```

  
If true (default), a layer with a parallax factor will also be scaled up/down accordingly.  


```csharp
public bool ParallaxScaling { get; set; }
```

  
X offset of the layer, in pixels (IMPORTANT: this should be added to the LayerInstance  
optional offset)  


```csharp
public int PxOffsetX { get; set; }
```

  
Y offset of the layer, in pixels (IMPORTANT: this should be added to the LayerInstance  
optional offset)  


```csharp
public int PxOffsetY { get; set; }
```

  
Reference to the default Tileset UID being used by this layer definition.  
WARNING: some layer instances might use a different tileset. So most of the time,  
you should probably use the __tilesetDefUid value found in layer  
instances. Note: since version 1.0.0, the old autoTilesetDefUid was removed and  
merged into this value.  


```csharp
public int? TilesetDefUid { get; set; }
```

  
Unique Int identifier  


```csharp
public int Uid { get; set; }
```


