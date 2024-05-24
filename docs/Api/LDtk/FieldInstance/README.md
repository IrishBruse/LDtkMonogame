# FieldInstance Class

[Home](../../README.md) &#x2022; [Constructors](#constructors) &#x2022; [Properties](#properties) &#x2022; [Methods](#methods)

**Namespace**: [LDtk](../README.md)

**Assembly**: LDtkMonogame\.dll

  
 Field instance 

```csharp
public class FieldInstance
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; FieldInstance

## Constructors

| Constructor | Summary |
| ----------- | ------- |
| [FieldInstance()](-ctor/README.md) | |

## Properties

| Property | Summary |
| -------- | ------- |
| [_Identifier](_Identifier/README.md) |  Field definition identifier  |
| [_Tile](_Tile/README.md) |  Optional TilesetRect used to display this field \(this can be the field own Tile, or some other Tile guessed from the value, like an Enum\)\.  |
| [_Type](_Type/README.md) |  Type of the field, such as `Int`, `Float`, `String`, `Enum(my_enum_name)`, `Bool`, etc\.  NOTE: if you enable the advanced option , you will have "\*Multilines\*" instead of "\*String\*" when relevant\.  |
| [_Value](_Value/README.md) |  Actual value of the field instance\. The value type varies, depending on `__type`:   \- For  \(ie\. Integer, Float, Boolean, String, Text and FilePath\), you just get the actual value with the expected type\.   \- For , the value is an hexadecimal string using "\#rrggbb" format\.   \- For , the value is a String representing the selected enum value\.   \- For , the value is a \[GridPoint\]\(\#ldtk\-GridPoint\) object\.   \- For , the value is a \[TilesetRect\]\(\#ldtk\-TilesetRect\) object\.   \- For , the value is an \[EntityReferenceInfos\]\(\#ldtk\-EntityReferenceInfos\) object\.  If the field is an array, then this `__value` will also be a JSON array\.  |
| [DefUid](DefUid/README.md) |  Reference of the  UID  |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |

