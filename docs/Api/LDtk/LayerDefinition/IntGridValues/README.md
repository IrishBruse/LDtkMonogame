# LayerDefinition\.IntGridValues Property

[Home](../../../README.md)

**Containing Type**: [LayerDefinition](../README.md)

**Assembly**: LDtkMonogame\.dll

  
 An array that defines extra optional info for each IntGrid value\.  WARNING: the array order is not related to actual IntGrid values\! As user can re\-order IntGrid values freely, you may value "2" before value "1" in this array\. 

```csharp
[System.Text.Json.Serialization.JsonPropertyName("intGridValues")]
public LDtk.IntGridValueDefinition[] IntGridValues { get; set; }
```

### Property Value

[IntGridValueDefinition](../../IntGridValueDefinition/README.md)\[\]

### Attributes

* [JsonPropertyNameAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonpropertynameattribute)

