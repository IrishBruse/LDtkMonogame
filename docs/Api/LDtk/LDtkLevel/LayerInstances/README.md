# LDtkLevel\.LayerInstances Property

[Home](../../../README.md)

**Containing Type**: [LDtkLevel](../README.md)

**Assembly**: LDtkMonogame\.dll

  
 An array containing all Layer instances\. : if the project option "\*Save levels separately\*" is enabled, this field will be `null`\.  This array is : the 1st layer is the top\-most and the last is behind\. 

```csharp
[System.Text.Json.Serialization.JsonPropertyName("layerInstances")]
public LDtk.LayerInstance[]? LayerInstances { get; set; }
```

### Property Value

[LayerInstance](../../LayerInstance/README.md)\[\]

### Attributes

* [JsonPropertyNameAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonpropertynameattribute)

