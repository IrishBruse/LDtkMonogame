# LDtkIntGrid

LDtk IntGrid

## Methods

Used by json deserializer not for use by user!
```csharp
public LDtkIntGrid()
```

Gets the int value at location and return 0 if out of bounds
```csharp
public int GetValueAt(int x, int y);
```

Gets the int value at location and return 0 if out of bounds
```csharp
public int GetValueAt(int x, int y);
```

Gets the int value at location and return 0 if out of bounds
```csharp
public int GetValueAt(int x, int y);
```

Check if point is inside of a grid
```csharp
public bool Contains(Point point);
```

Check if point is inside of a grid
```csharp
public bool Contains(Point point);
```

Convert from world pixel space to int grid space Floors the value based on  to an Integer
```csharp
public Point FromWorldToGridSpace(Vector2 position);
```


## Properties

Size of a tile in pixels
```csharp
public int TileSize { get; set; }
```

The underlying values of the int grid
```csharp
public int[] Values { get; set; }
```

Worldspace start Position of the intgrid
```csharp
public Point WorldPosition { get; set; }
```

Worldspace start Position of the intgrid
```csharp
public Point GridSize { get; set; }
```


