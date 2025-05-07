# Documentation

[![LDtkMonogame.ContentPipeline](https://img.shields.io/nuget/v/LDtkMonogame.ContentPipeline?label=LDtkMonogame.ContentPipeline) ](https://www.nuget.org/packages/LDtkMonogame.ContentPipeline/)

Here are the docs for each class in LDtkMonogame

## World

To open a ldtk project file you should start by creating a new `World` object as this is what holds all the data to a ldtk project.

This will load `.ldtk` worlds directly from disk.

```csharp
LDtkWorld world = LDtkWorld.LoadWorld("Assets/World.ldtk");
```

This will load `.ldtk` world from the content pipeline.

```csharp
LDtkWorld world = LDtkWorld.LoadWorld("World", Content);
```

You will need to make sure your files are in the same relative folders as the origonal files in the project.
If you are unsure check out the example project [here](https://github.com/IrishBruse/LDtkMonogameExample).

Now that you have a reference to the world you can use it for handling levels you want to use in your game.

For more information on the world refer to the ldtk documentation [ldtk-ProjectJson](https://ldtk.io/json/#ldtk-ProjectJson)

## Level

Level's hold all the data for each individual level and include the tiles and all the layers including `Entities`, `Intgrids` and `Autotiles`.

If you are using external levels with ldtk. You will need to load the level otherwise ignore this next step.

```csharp
LDtkLevel level0 = world.LoadLevel("Level_0"); // Identifier
LDtkLevel level1 = world.LoadLevel(Worlds.World.Level_1); // Guid/Iid
```

Now with `myLevel` you can access all the ldtk layers inside of it.
If your level has custom fields you will need to load the data using `GetCustomFields()`.

If you are using the [Codegen](tutorials/codegen.md) tool the `LDtkLevelData` file will be created for you automatically.

```csharp
public class LDtkLevelData
{
    public string biome;
    public int difficulty;
}
```

```csharp
public class LDtkLevelData
{
    public string biome;
    public int difficulty;
}
```

```csharp
Gun_Pickup level1 = level0.GetEntityRef<Gun_Pickup>();// Guid/Iid
```

If you have custom fields specified ldtkCodegen should have generated a file named `LDtkLevelData`

To get the fields from the level

```csharp
LDtkLevelData levelData = levelName.GetCustomFields<LDtkLevelData>();
```

For more information on the level refer to the ldtk documentation [ldtk-LevelJson](../LDtkReference.md#level)

## IntGrid

Intgrids can be intgrid rule layers or pure intgrid layers as they both hold integer values.
The intgrid is returned from a level when calling `level.GetIntGrid("layer name");`.

In the [example game](https://github.com/IrishBruse/LDtkMonogameExample/blob/d4784bcd4849582ba66ff2f0bb5281e6069aaeda/Entities/PlayerEntity.cs#L133) I used the intgrid for collisions.

```csharp
LDtkIntGrid collisions = level.GetIntGrid("Tiles");
```

## Entity

Entities are contained inside the level and are returned from the various `GetEntity()`/`GetEntities()` methods, check the api docs for more information.
Similar to levels entities can have custom fields you handle that data in the same way by creating a custom inherited class from ILDtkEntity.

If you are using the [Codegen](codegen.md) tool the `Gun_Pickup` file would be created for you automatically.

```csharp
public class Gun_Pickup : ILDtkEntity
{
    public System.Guid Iid { get; set; }
    public long Uid { get; set; }
    public string Identifier { get; set; }
    public Vector2 Size { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Pivot { get; set; }
    public Rectangle Tile { get; set; }

    public Color SmartColor { get; set; }

    public EntityRef Test { get; set; }
    ...
}
```

To load your `Gun_Pickup` you load a level like normal but also pass it your level class

```csharp
Gun_Pickup[] gunPickups = level.GetEntities<Gun_Pickup>();
```

If you have one entity `Player` you could load it

```csharp
Player playerSpawn = level.GetEntity<Player>();
```

EntityRef works be letting you get another entity from a level or world

```csharp
Gun_Pickup otherGun = level.GetEntityRef<Gun_Pickup>(gun.Test);
Gun_Pickup otherGun = world.GetEntityRef<Gun_Pickup>(gun.Test);
```
