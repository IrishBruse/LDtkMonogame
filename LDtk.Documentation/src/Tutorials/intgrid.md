# IntGrid

Intgrids can be intgrid rule layers or pure intgrid layers as they both hold integer values.
The intgrid is returned from a level when calling `level.GetIntGrid("layer name");`.

In the [example game](https://github.com/IrishBruse/LDtkMonogameExample/blob/d4784bcd4849582ba66ff2f0bb5281e6069aaeda/Entities/PlayerEntity.cs#L133) I used the intgrid for collisions.

```cs
LDtkIntGrid collisions = level.GetIntGrid("Tiles");
```

# API

Size of a tile in pixels
```cs
public int TileSize { get; set; }
```

The underlying values of the int grid
```cs
public int[] Values { get; set; }
```

Worldspace start Position of the intgrid
```cs
public Point WorldPosition { get; set; }
```

Worldspace start Position of the intgrid
```cs
public Point GridSize { get; set; }
```

Used by json deserializer not for use by user!
```
[Obsolete("Used by json deserializer not for use by user!")]
public LDtkIntGrid() { }
```

Gets the int value at location and return 0 if out of bounds
```cs
public int GetValueAt(int x, int y)
```


Gets the int value at location and return 0 if out of bounds
```cs
public int GetValueAt(Point position)
```


Gets the int value at location and return 0 if out of bounds
```cs
public int GetValueAt(Vector2 position)
```


Check if point is inside of a grid
```cs
public bool Contains(Point point)
```


Check if point is inside of a grid
```cs
public bool Contains(Vector2 point)
```


Convert from world pixel space to int grid space Floors the value based on <see cref="TileSize"/> to an Integer
```cs
public Point FromWorldToGridSpace(Vector2 position)
```
