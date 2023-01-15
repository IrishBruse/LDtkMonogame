# LDtkWorld

The Levels iterator used in foreach will load external levels each time caching recommended
```cs
public IEnumerable<LDtkLevel> Levels
```

The absolute filepath to the world
```cs
public string FilePath { get; set; }
```

Size of the world grid in pixels.
```cs
public Point WorldGridSize => new(WorldGridWidth, WorldGridHeight);
```

The absolute folder that the world is located in.Used to absolute relative addresses of textures
```cs
public string RootFolder { get; set; }
```

The content manager used if you are using the contentpipeline
```cs
public ContentManager Content { get; set; }
```

Goes through all the loaded levels looking for the entity
```cs
public T GetEntity<T>() where T : new()
```

Get the level with an identifier
```cs
public LDtkLevel LoadLevel(string identifier)
```

Get the level with an iid
```cs
public LDtkLevel LoadLevel(Guid iid)
```

Get the level with an index
```cs
public LDtkLevel LoadLevel(int index)
```

Gets an entity from an <paramref name="entityRef"/> converted to <typeparamref name="T"/>
```cs
public T GetEntityRef<T>(EntityRef entityRef) where T : new()
```
