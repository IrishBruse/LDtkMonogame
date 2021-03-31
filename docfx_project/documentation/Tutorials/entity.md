# Entity

Entities are contained inside the level and are returned from the various `GetEntity/GetEntities` functions check the api docs for more information. 

Similar to levels entities can have custom fields you handle that data in the same way by creating a custom inherited class from Entity 

```csharp
public class Door : Entity
{
    // LDtk entity fields
    public string levelIdentifier;
    public int destinationDoor;
}
```

To load your `Door` you load a level like normal but also pass it your level class

```csharp
Door[] doors = level.GetEntities<Door>();
```

If you dont want to inherit from a class you can create the entity fields in your class and the parser will load the data into the field aslong as they have the correct names

-   `Position`
-   `LevelPosition`
-   `Pivot`
-   `Texture` (Optional)
-   `Size`
-   `EditorVisualColor` (Debug Only)
-   `Tile` (Optional)
