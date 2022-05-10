# LDtkFile

The absolute path to the ldtkFile
```csharp
public string FilePath { get; set; }
```

The content manager used if you are using the contentpipeline
```csharp
public ContentManager Content { get; set; }
```

Loads the ldtk world file from disk directly
Path to the .ldtk file
```csharp
public static LDtkFile FromFile(string filePath)
```

Loads the ldtk world file from disk directly
Path to the .ldtk file excluding file extension
The optional content manager if you are using the content pipeline
```csharp
public static LDtkFile FromFile(string filePath, ContentManager content)
```

Loads the ldtkl world file from disk directly or from the embeded one depending on if the file uses externalworlds
```csharp
public LDtkWorld LoadWorld(Guid iid)
```

Gets an entity from an "entityRef" converted to "T"
```csharp
public T GetEntityRef<T>(EntityRef entityRef) where T : new()
```
