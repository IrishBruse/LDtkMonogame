# World

To open a ldtk project file you should start by creating a new `World` object as this is what holds all the data to a ldtk project.

This will load `.ldtk` worlds directly from disk.

```cs
LDtkWorld world = LDtkWorld.LoadWorld("Assets/World.ldtk");
```

This will load `.ldtk` world from the content pipeline.

```cs
LDtkWorld world = LDtkWorld.LoadWorld("World", Content);
```

You will need to make sure your files are in the same relative folders as the origonal files in the project.
If you are unsure check out the example project [here](https://github.com/IrishBruse/LDtkMonogameExample).

Now that you have a reference to the world you can use it for handling levels you want to use in your game.

For more information on the world refer to the ldtk documentation [ldtk-ProjectJson](https://ldtk.io/json/#ldtk-ProjectJson)
