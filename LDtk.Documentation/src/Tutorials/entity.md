# Entity

Entities are contained inside the level and are returned from the various `GetEntity()`/`GetEntities()` methods, check the api docs for more information.
Similar to levels entities can have custom fields you handle that data in the same way by creating a custom inherited class from ILDtkEntity.

If you are using the [Codegen](codegen.md) tool the `Gun_Pickup` file would be created for you automatically.

```cs
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

```cs
Gun_Pickup[] gunPickups = level.GetEntities<Gun_Pickup>();
```

If you have one entity `Player` you could load it

```cs
Player playerSpawn = level.GetEntity<Player>();
```

EntityRef works be letting you get another entity from a level or world
```cs
Gun_Pickup otherGun = level.GetEntityRef<Gun_Pickup>(gun.Test);
Gun_Pickup otherGun = world.GetEntityRef<Gun_Pickup>(gun.Test);
```
