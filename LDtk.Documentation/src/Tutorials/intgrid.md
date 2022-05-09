# IntGrid

Intgrids can be intgrid rule layers or pure intgrid layers as they both hold integer values.
The intgrid is returned from a level when calling `level.GetIntGrid("layer name");`.

In the [example game](https://github.com/IrishBruse/LDtkMonogameExample/blob/d4784bcd4849582ba66ff2f0bb5281e6069aaeda/Entities/PlayerEntity.cs#L133) I used the intgrid for collisions.

```cs
LDtkIntGrid collisions = level.GetIntGrid("Tiles");
```
