# LevelBackgroundPosition\.CropRect Property

[Home](../../../README.md)

**Containing Type**: [LevelBackgroundPosition](../README.md)

**Assembly**: LDtkMonogame\.dll

  
 An array of 4 float values describing the cropped sub\-rectangle of the displayed background image\. This cropping happens when original is larger than the level bounds\. Array format: `[ cropX, cropY, cropWidth, cropHeight ]` 

```csharp
[System.Text.Json.Serialization.JsonPropertyName("cropRect")]
public float[] CropRect { get; set; }
```

### Property Value

[Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)\[\]

### Attributes

* [JsonPropertyNameAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonpropertynameattribute)

