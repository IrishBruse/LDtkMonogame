# FieldInstance\.\_Value Property

[Home](../../../README.md)

**Containing Type**: [FieldInstance](../README.md)

**Assembly**: LDtkMonogame\.dll

  
 Actual value of the field instance\. The value type varies, depending on `__type`:   \- For  \(ie\. Integer, Float, Boolean, String, Text and FilePath\), you just get the actual value with the expected type\.   \- For , the value is an hexadecimal string using "\#rrggbb" format\.   \- For , the value is a String representing the selected enum value\.   \- For , the value is a \[GridPoint\]\(\#ldtk\-GridPoint\) object\.   \- For , the value is a \[TilesetRect\]\(\#ldtk\-TilesetRect\) object\.   \- For , the value is an \[EntityReferenceInfos\]\(\#ldtk\-EntityReferenceInfos\) object\.  If the field is an array, then this `__value` will also be a JSON array\. 

```csharp
[System.Text.Json.Serialization.JsonPropertyName("__value")]
public object _Value { get; set; }
```

### Property Value

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)

### Attributes

* [JsonPropertyNameAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonpropertynameattribute)

