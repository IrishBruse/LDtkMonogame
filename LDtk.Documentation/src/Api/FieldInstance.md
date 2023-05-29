# FieldInstance

FieldInstance

## Properties

  
Reference of the Field definition UID  


```csharp
public int DefUid { get; set; }
```

  
Field definition identifier  


```csharp
public string _Identifier { get; set; }
```

  
Optional TilesetRect used to display this field (this can be the field own Tile, or some  
other Tile guessed from the value, like an Enum).  


```csharp
public TilesetRectangle _Tile { get; set; }
```

  
Type of the field, such as Int, Float, String, Enum(my_enum_name), Bool,  
etc.  NOTE: if you enable the advanced option Use Multilines type, you will have  
"Multilines" instead of "String" when relevant.  


```csharp
public string _Type { get; set; }
```

  
Actual value of the field instance. The value type varies, depending on __type:  
- For classic types (ie. Integer, Float, Boolean, String, Text and FilePath), you  
just get the actual value with the expected type.   - For Color, the value is an  
hexadecimal string using "#rrggbb" format.   - For Enum, the value is a String  
representing the selected enum value.   - For Point, the value is a  
GridPoint object.   - For Tile, the value is a  
TilesetRect object.   - For EntityRef, the value is an  
EntityReferenceInfos object.  If the field is an  
array, then this __value will also be a JSON array.  


```csharp
public object _Value { get; set; }
```


