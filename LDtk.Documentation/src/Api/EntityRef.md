# EntityRef

  
This object is used in Field Instances to describe an EntityRef value.  


## Properties

  
Guid of the refered EntityInstance  

```csharp
public Guid EntityIid { get; set; }
```

  
Guid of the LayerInstance containing the refered EntityInstance  

```csharp
public Guid LayerIid { get; set; }
```

  
Guid of the Level containing the refered EntityInstance  

```csharp
public Guid LevelIid { get; set; }
```

  
Guid of the World containing the refered EntityInstance  

```csharp
public Guid WorldIid { get; set; }
```


