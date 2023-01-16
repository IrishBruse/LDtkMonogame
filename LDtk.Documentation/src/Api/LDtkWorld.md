# LDtkWorld

  
IMPORTANT: this type is not used yet in current LDtk version. It's only presented  
here as a preview of a planned feature.  A World contains multiple levels, and it has its  
own layout settings.  


## Methods

Used by json deserializer not for use by user!

```csharp
LDtkWorld.#ctor
```

Goes through all the loaded levels looking for the entity

```csharp
public T GetEntity<T>()
```

Get the level with an identifier

```csharp
public LDtkLevel LoadLevel(string)
```

Get the level with an iid

```csharp
public LDtkLevel LoadLevel(Guid)
```

Get the level with an index

```csharp
public LDtkLevel LoadLevel(int)
```

Gets an entity from an  converted to

```csharp
public T GetEntityRef<T>(EntityRef)
```


## Properties

The Real LDtk Levels Json data Use indexer directly on the world eg world[0] instead as that will load external files if that setting is enabled.

```csharp
public LDtkLevel[] RawLevels { get; set; }
```

The Levels iterator used in foreach will load external levels each time caching recommended

```csharp
public LDtkLevel] Levels { get; set; }
```

The absolute filepath to the world

```csharp
public string FilePath { get; set; }
```

Size of the world grid in pixels.

```csharp
public Point WorldGridSize { get; set; }
```

The content manager used if you are using the contentpipeline

```csharp
public ContentManager Content { get; set; }
```

  
User defined unique identifier  


```csharp
public string Identifier { get; set; }
```

  
Unique instance identifer  


```csharp
public Guid Iid { get; set; }
```

  
Height of the world grid in pixels.  


```csharp
public int WorldGridHeight { get; set; }
```

  
Width of the world grid in pixels.  


```csharp
public int WorldGridWidth { get; set; }
```

  
An enum that describes how levels are organized in this project (ie. linearly or in a 2D  
space). Possible values: Free, GridVania, LinearHorizontal, LinearVertical,  
null, null  


```csharp
public WorldLayout? WorldLayout { get; set; }
```


