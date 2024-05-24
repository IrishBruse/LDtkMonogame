# LayerDefinition\.TilesetDefUid Property

[Home](../../../README.md)

**Containing Type**: [LayerDefinition](../README.md)

**Assembly**: LDtkMonogame\.dll

  
 Reference to the default Tileset UID being used by this layer definition\.  : some layer \*instances\* might use a different tileset\. So most of the time, you should probably use the `__tilesetDefUid` value found in layer instances\.  Note: since version 1\.0\.0, the old `autoTilesetDefUid` was removed and merged into this value\. 

```csharp
[System.Text.Json.Serialization.JsonPropertyName("tilesetDefUid")]
public int? TilesetDefUid { get; set; }
```

### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)?

### Attributes

* [JsonPropertyNameAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonpropertynameattribute)

