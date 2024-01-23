# FieldInstance\.\_Value Property

[Home](../../../README.md)

**Containing Type**: [FieldInstance](../README.md)

**Assembly**: LDtkMonogame\.dll

  
Actual value of the field instance\. The value type varies, depending on \_\_type:
\- For classic types \(ie\. Integer, Float, Boolean, String, Text and FilePath\), you
just get the actual value with the expected type\.   \- For Color, the value is an
hexadecimal string using "\#rrggbb" format\.   \- For Enum, the value is a String
representing the selected enum value\.   \- For Point, the value is a
GridPoint object\.   \- For Tile, the value is a
TilesetRect object\.   \- For EntityRef, the value is an
EntityReferenceInfos object\.  If the field is an
array, then this \_\_value will also be a JSON array\.

```csharp
[System.Text.Json.Serialization.JsonPropertyName("__value")]
public object _Value { get; set; }
```

### Property Value

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)

### Attributes

* [JsonPropertyNameAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonpropertynameattribute)

