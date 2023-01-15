# Constants

General Constants used in LDtkMonogame

## Fields

The supported version of ldtk so you are in a newer version any new features may not be added yet please create an issue on the github requesting them
```csharp
public static string SupportedLDtkVersion { get; set; }
```

The converter used internally with JsonSerializer.Deserialize(, Constants.SerializeOptions) not needed by the user just use .FromFile instead
```csharp
public static JsonSerializerOptions SerializeOptions { get; set; }
```


