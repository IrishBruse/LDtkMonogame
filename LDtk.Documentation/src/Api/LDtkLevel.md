# LDtkLevel.cs

The absolute filepath to the level
```cs
public string FilePath { get; set; }
```

World Position of the level in pixels
```cs
public Point Position => new(WorldX, WorldY);
```

World size of the level in pixels
```cs
public Point Size => new(PxWid, PxHei);
```

Has the file been loaded if the level is external
```cs
public bool Loaded { get; internal set; }
```

Loads the ldtk world file from disk directly

Path to the .ldtk file
```cs
public static LDtkLevel FromFile(string filePath)
```

Loads the ldtk world file from disk directly

Path to the .ldtk file excluding file extension

The optional content manager if you are using the content pipeline
```cs
public static LDtkLevel FromFile(string filePath, ContentManager content)
```

Gets an intgrid with the "identifier" in a "LDtkLevel"
```cs
public LDtkIntGrid GetIntGrid(string identifier)
```

Gets one entity of type T in the current level best used with 1 per level constraint
```cs
public T GetEntity<T>() where T : new()
```

Gets an entity from an "entityRef" converted to "T"
```cs
public T GetEntityRef<T>(EntityRef entityRef) where T : new()
```

Gets an array of entities of type "T" in the current level
```cs
public T[] GetEntities<T>() where T : new()
```

T GetEntityFromInstance<T>(EntityInstance entityInstance) where T : new()

Check if point is inside of a level
```cs
public bool Contains(Vector2 point)
```

Check if point is inside of a level
```cs
public bool Contains(Point point)
```
