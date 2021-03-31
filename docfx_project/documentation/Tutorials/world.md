# World

To open a ldtk project file you should start by creating a new `World` object as this is what hold all the data to a ldtk project.

This will load the images from normal files (\*.png, \*.jpg etc)

```csharp
World world = new World(spriteBatch, "Assets/MyProject.ldtk");
```

or if you want to use Content/.xnb files you can pass `Content` to it.

```csharp
World world = new World(spriteBatch, "Assets/MyProject.ldtk", Content);
```

You will need to make sure your content is in the same relative folders as the origonal files in the project.
If you are unsure check out the example project [here](https://github.com/IrishBruse/LDtkMonogame/tree/main/LDtkMonogame.Examples/Platformer/).

Now that you have a reference to the world you can use it for handling  
levels you want to use in your game.
