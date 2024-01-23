# EntityDefinition\.NineSliceBorders Property

[Home](../../../README.md)

**Containing Type**: [EntityDefinition](../README.md)

**Assembly**: LDtkMonogame\.dll

  
An array of 4 dimensions for the up/right/down/left borders \(in this order\) when using
9\-slice mode for tileRenderMode\.  If the tileRenderMode is not NineSlice, then
this array is empty\.  See: https://en\.wikipedia\.org/wiki/9\-slice\_scaling

```csharp
[System.Text.Json.Serialization.JsonPropertyName("nineSliceBorders")]
public int[] NineSliceBorders { get; set; }
```

### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)\[\]

### Attributes

* [JsonPropertyNameAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonpropertynameattribute)

