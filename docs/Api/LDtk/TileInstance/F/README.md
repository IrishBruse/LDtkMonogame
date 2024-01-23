# TileInstance\.F Property

[Home](../../../README.md)

**Containing Type**: [TileInstance](../README.md)

**Assembly**: LDtkMonogame\.dll

  
"Flip bits", a 2\-bits integer to represent the mirror transformations of the tile\.
\- Bit 0 = X flip   \- Bit 1 = Y flip   Examples: f=0 \(no flip\), f=1 \(X flip
only\), f=2 \(Y flip only\), f=3 \(both flips\)

```csharp
[System.Text.Json.Serialization.JsonPropertyName("f")]
public int F { get; set; }
```

### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)

### Attributes

* [JsonPropertyNameAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonpropertynameattribute)

