# NeighbourLevel\.Dir Property

[Home](../../../README.md)

**Containing Type**: [NeighbourLevel](../README.md)

**Assembly**: LDtkMonogame\.dll

  
 A lowercase string tipping on the level location \(`n`orth, `s`outh, `w`est, `e`ast\)\.  Since 1\.4\.0, this value can also be \< \(neighbour depth is lower\), \> \(neighbour depth is greater\) or `o` \(levels overlap and share the same world depth\)\.  Since 1\.5\.3, this value can also be `nw`,`ne`,`sw` or `se` for levels only touching corners\. 

```csharp
[System.Text.Json.Serialization.JsonPropertyName("dir")]
public string Dir { get; set; }
```

### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)

### Attributes

* [JsonPropertyNameAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonpropertynameattribute)

