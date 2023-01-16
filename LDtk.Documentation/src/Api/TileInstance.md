# TileInstance

  
This structure represents a single tile from a given Tileset.  


## Properties

  
"Flip bits", a 2-bits integer to represent the mirror transformations of the tile.  
- Bit 0 = X flip  - Bit 1 = Y flip  Examples: f=0 (no flip), f=1 (X flip  
only), f=2 (Y flip only), f=3 (both flips)  


```csharp
public int F { get; set; }
```

  
Pixel coordinates of the tile in the layer ([x,y] format). Don't forget optional  
layer offsets, if they exist!  


```csharp
public Point Px { get; set; }
```

  
Pixel coordinates of the tile in the tileset ([x,y] format)  


```csharp
public Point Src { get; set; }
```

  
The Tile ID in the corresponding tileset.  


```csharp
public int T { get; set; }
```


