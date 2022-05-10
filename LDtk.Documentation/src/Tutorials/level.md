# Level

Level's hold all the data for each individual level and include the tiles and all the layers including `Entities`, `Intgrids` and `Autotiles`.

If you are using external levels with ldtk. You will need to load the level otherwise ignore this next step.

```cs
LDtkLevel level0 = world.LoadLevel("Level_0"); // Identifier
LDtkLevel level1 = world.LoadLevel(Worlds.World.Level_1);// Guid/Iid
```

Now with `myLevel` you can access all the ldtk layers inside of it.
If your level has custom fields you will need to load the data using `GetCustomFields()`.

If you are using the [Codegen](codegen.md) tool the `LDtkLevelData` file will be created for you automatically.

```cs
public class LDtkLevelData
{
    public string biome;
    public int difficulty;
}
```

```cs
public class LDtkLevelData:
{
    public string biome;
    public int difficulty;
}
```

```cs
Gun_Pickup level1 = level0.GetEntityRef<Gun_Pickup>();// Guid/Iid
```

For more information on the level refer to the ldtk documentation [ldtk-LevelJson](../LDtkReference.md#level)
