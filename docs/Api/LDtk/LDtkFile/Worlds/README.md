# LDtkFile\.Worlds Property

[Home](../../../README.md)

**Containing Type**: [LDtkFile](../README.md)

**Assembly**: LDtkMonogame\.dll

  
This array will be empty, unless you enable the Multi\-Worlds in the project advanced
settings\. \- in current version, a LDtk project file can only contain a single
world with multiple levels in it\. In this case, levels and world layout related settings
are stored in the root of the JSON\. \- with "Multi\-worlds" enabled, there will be a
worlds array in root, each world containing levels and layout settings\. Basically, it's
pretty much only about moving the levels array to the worlds array, along with world
layout related values \(eg\. worldGridWidth etc\)\.If you want to start
supporting this future update easily, please refer to this documentation:
https://github\.com/deepnight/ldtk/issues/231

```csharp
[System.Text.Json.Serialization.JsonPropertyName("worlds")]
public LDtk.LDtkWorld[] Worlds { get; set; }
```

### Property Value

[LDtkWorld](../../LDtkWorld/README.md)\[\]

### Attributes

* [JsonPropertyNameAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonpropertynameattribute)

