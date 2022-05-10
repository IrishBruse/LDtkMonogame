# LDtkIntGrid


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
