# Definitions Class

[Home](../../README.md) &#x2022; [Constructors](#constructors) &#x2022; [Properties](#properties) &#x2022; [Methods](#methods)

**Namespace**: [LDtk](../README.md)

**Assembly**: LDtkMonogame\.dll

  
If you're writing your own LDtk importer, you should probably just ignore most stuff in
the defs section, as it contains data that are mostly important to the editor\. To keep
you away from the defs section and avoid some unnecessary JSON parsing, important data
from definitions is often duplicated in fields prefixed with a double underscore \(eg\.
\_\_identifier or \_\_type\)\.  The 2 only definition types you might need here are
Tilesets and Enums\.

A structure containing all the definitions of this project

```csharp
public class Definitions
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; Definitions

## Constructors

| Constructor | Summary |
| ----------- | ------- |
| [Definitions()](-ctor/README.md) | |

## Properties

| Property | Summary |
| -------- | ------- |
| [Entities](Entities/README.md) | All entities definitions, including their custom fields |
| [Enums](Enums/README.md) | All internal enums |
| [ExternalEnums](ExternalEnums/README.md) | Note: external enums are exactly the same as enums, except they have a relPath to point to an external source file\. |
| [Layers](Layers/README.md) | All layer definitions |
| [Tilesets](Tilesets/README.md) | All tilesets |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |

