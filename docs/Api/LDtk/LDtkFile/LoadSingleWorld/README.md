# LDtkFile\.LoadSingleWorld\(\) Method

[Home](../../../README.md)

**Containing Type**: [LDtkFile](../README.md)

**Assembly**: LDtkMonogame\.dll

  
 Loads the one and only ldtkl world file from disk directly or from the embeded one depending on if the file uses externalworlds\. 

```csharp
public LDtk.LDtkWorld? LoadSingleWorld()
```

### Returns

[LDtkWorld](../../LDtkWorld/README.md)

 Returns the world from the iid\. 

### Exceptions

[LDtkException](../../LDtkException/README.md)

Throws if more than 1 world exsists

