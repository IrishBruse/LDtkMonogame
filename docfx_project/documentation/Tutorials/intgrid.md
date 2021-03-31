# IntGrid

Intgrids can be intgrid rule layers or pure intgrid layers as they both hold integer values. The intgrid is returned from a level when calling `level.GetIntGrid("layer name");`

In the platformer example I used the intgrid for collisions

```csharp
IntGrid collisions = level.GetIntGrid("Collisions");
```
