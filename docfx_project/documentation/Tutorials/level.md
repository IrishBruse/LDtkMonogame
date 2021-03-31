# Level

Level's hold all the data for each individual level and include the tiles and all the layers including `Entities`, `Intgrids` and `Autotiles`.
You can get levels from the world class you instantiated before by calling

```csharp
Level myLevel = world.GetLevel("Level1");
```

Now with `myLevel` you can access all the ldtk layers inside of it using the functions
check the api docs for all the functions and what they do

If your level has custom fields you will need to inherit from `Level`
and add the custom fields

```csharp
public class CustomizedLevel : Level
{
    public string biome;
    public int difficulty;
}
```

To load your `CustomizedLevel` you load a level like normal but also pass it your level class

```csharp
CustomizedLevel customizedLevel = world.GetLevel<CustomizedLevel>("Level1");
```
