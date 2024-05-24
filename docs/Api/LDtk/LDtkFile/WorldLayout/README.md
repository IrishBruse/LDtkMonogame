# LDtkFile\.WorldLayout Property

[Home](../../../README.md)

**Containing Type**: [LDtkFile](../README.md)

**Assembly**: LDtkMonogame\.dll

  
 : this field will move to the `worlds` array after the "multi\-worlds" update\. It will then be `null`\. You can enable the Multi\-worlds advanced project option to enable the change immediately\.  An enum that describes how levels are organized in this project \(ie\. linearly or in a 2D space\)\. Possible values: \<`null`\>, `Free`, `GridVania`, `LinearHorizontal`, `LinearVertical`, `null` 

```csharp
[System.Text.Json.Serialization.JsonPropertyName("worldLayout")]
public LDtk.WorldLayout? WorldLayout { get; set; }
```

### Property Value

[WorldLayout](../../WorldLayout/README.md)?

### Attributes

* [JsonPropertyNameAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonpropertynameattribute)

