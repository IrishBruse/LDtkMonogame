# LDtkWorld\.Levels Property

[Home](../../../README.md)

**Containing Type**: [LDtkWorld](../README.md)

**Assembly**: LDtkMonogame\.dll

  
 All levels from this world\. The order of this array is only relevant in `LinearHorizontal` and `linearVertical` world layouts \(see `worldLayout` value\)\. Otherwise, you should refer to the `worldX`,`worldY` coordinates of each Level\. 

```csharp
[System.Text.Json.Serialization.JsonPropertyName("levels")]
public LDtk.LDtkLevel[] Levels { get; set; }
```

### Property Value

[LDtkLevel](../../LDtkLevel/README.md)\[\]

### Attributes

* [JsonPropertyNameAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonpropertynameattribute)

