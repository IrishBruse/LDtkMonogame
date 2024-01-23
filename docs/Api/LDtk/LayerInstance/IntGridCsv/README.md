# LayerInstance\.IntGridCsv Property

[Home](../../../README.md)

**Containing Type**: [LayerInstance](../README.md)

**Assembly**: LDtkMonogame\.dll

  
A list of all values in the IntGrid layer, stored in CSV format \(Comma Separated
Values\)\.  Order is from left to right, and top to bottom \(ie\. first row from left to
right, followed by second row, etc\)\.  0 means "empty cell" and IntGrid values
start at 1\.  The array size is \_\_cWid x \_\_cHei cells\.

```csharp
[System.Text.Json.Serialization.JsonPropertyName("intGridCsv")]
public int[] IntGridCsv { get; set; }
```

### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)\[\]

### Attributes

* [JsonPropertyNameAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonpropertynameattribute)

