# LDtkLevel\.\_Neighbours Property

[Home](../../../README.md)

**Containing Type**: [LDtkLevel](../README.md)

**Assembly**: LDtkMonogame\.dll

  
 An array listing all other levels touching this one on the world map\. Since 1\.4\.0, this includes levels that overlap in the same world layer, or in nearby world layers\.  Only relevant for world layouts where level spatial positioning is manual \(ie\. GridVania, Free\)\. For Horizontal and Vertical layouts, this array is always empty\. 

```csharp
[System.Text.Json.Serialization.JsonPropertyName("__neighbours")]
public LDtk.NeighbourLevel[] _Neighbours { get; set; }
```

### Property Value

[NeighbourLevel](../../NeighbourLevel/README.md)\[\]

### Attributes

* [JsonPropertyNameAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonpropertynameattribute)

