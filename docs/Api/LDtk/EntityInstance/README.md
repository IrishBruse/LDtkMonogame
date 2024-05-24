# EntityInstance Class

[Home](../../README.md) &#x2022; [Constructors](#constructors) &#x2022; [Properties](#properties) &#x2022; [Methods](#methods)

**Namespace**: [LDtk](../README.md)

**Assembly**: LDtkMonogame\.dll

  
 Entity instance 

```csharp
public class EntityInstance
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; EntityInstance

## Constructors

| Constructor | Summary |
| ----------- | ------- |
| [EntityInstance()](-ctor/README.md) | |

## Properties

| Property | Summary |
| -------- | ------- |
| [_Grid](_Grid/README.md) |  Grid\-based coordinates \(`[x,y]` format\)  |
| [_Identifier](_Identifier/README.md) |  Entity definition identifier  |
| [_Pivot](_Pivot/README.md) |  Pivot coordinates  \(`[x,y]` format, values are from 0 to 1\) of the Entity  |
| [_SmartColor](_SmartColor/README.md) |  The entity "smart" color, guessed from either Entity definition, or one its field instances\.  |
| [_Tags](_Tags/README.md) |  Array of tags defined in this Entity definition  |
| [_Tile](_Tile/README.md) |  Optional TilesetRect used to display this entity \(it could either be the default Entity tile, or some tile provided by a field value, like an Enum\)\.  |
| [_WorldX](_WorldX/README.md) |  X world coordinate in pixels\. Only available in GridVania or Free world layouts\.  |
| [_WorldY](_WorldY/README.md) |  Y world coordinate in pixels Only available in GridVania or Free world layouts\.  |
| [DefUid](DefUid/README.md) |  Reference of the  UID  |
| [FieldInstances](FieldInstances/README.md) |  An array of all custom fields and their values\.  |
| [Height](Height/README.md) |  Entity height in pixels\. For non\-resizable entities, it will be the same as Entity definition\.  |
| [Iid](Iid/README.md) |  Unique instance identifier  |
| [Px](Px/README.md) |  Pixel coordinates \(`[x,y]` format\) in current level coordinate space\. Don't forget optional layer offsets, if they exist\!  |
| [Width](Width/README.md) |  Entity width in pixels\. For non\-resizable entities, it will be the same as Entity definition\.  |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |

