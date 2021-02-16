<p align="center">
  <a href="https://github.com/deepnight/ldtk"> <img alt="LDtk Version Support" src="https://img.shields.io/github/v/release/deepnight/ldtk?&label=Supports%20LDtk&color=yellow"></a>
  <a href="https://www.nuget.org/packages/LDtkMonogame/"><img src="https://img.shields.io/nuget/v/LDtkMonogame?" /></a>
  <a href="https://www.nuget.org/packages/LDtkMonogame/"><img alt="Nuget" src="https://img.shields.io/nuget/dt/LDtkMonogame"></a>
</p>
<p align="center">
  <a href="https://github.com/IrishBruse/LDtkMonogame/tree/main/LDtkMonogame"> <img alt="GitHub Package Build Status" src="https://img.shields.io/github/workflow/status/IrishBruse/LDtkMonogame/Build%20Package?label=LDtkMonogame"></a>
  <a href="https://github.com/IrishBruse/LDtkMonogame/tree/main/LDtkMonogame.Examples"> <img alt="GitHub Examples Build Status" src="https://img.shields.io/github/workflow/status/IrishBruse/LDtkMonogame/Build%20Examples?label=LDtkMonogame.Examples"></a>
</p>

# LDtk Monogame
LDtk Monogame is a [LDtk](https://ldtk.io) project loader and renderer for the [Monogame](https://www.monogame.net/) Framework


![LDtk to Monogame Conversion](docfx_project/art/screenshots/LDtk%20to%20Monogame.png "1 to 1 Conversion")
 
# Quick Start Guide
The easiest way to start using LDtkMonogame is to import it into the project using nuget  

<a href="https://www.nuget.org/packages/LDtkMonogame/"><img src="https://img.shields.io/nuget/v/LDtkMonogame?" /></a>

Make sure to import the namespace at the top
```csharp
using LDtk;
``` 

To get started loading ldtk files create a  
```csharp
World ldtkWorld = new World("Assets/MyProject.ldtk");
``` 
 
Now just load the level
```csharp
Level startLevel = ldtkWorld.GetLevel("Level1");
```  

Now to render the level we loaded in `Draw`
```csharp
spriteBatch.Begin(SpriteSortMode.Texture, samplerState: SamplerState.PointClamp);
{
    for(int i = 0; i < startLevel.Layers.Length; i++)
    {
        spriteBatch.Draw(startLevel.Layers[i], startLevel.WorldPosition, Color.White);
    }
}
spriteBatch.End();
```
Thats all thats needed to render your level everything else is handled by LDtkMonogame
  
### For a more detailed introduction and on how to use **IntGrids**, **Levels** and **Entities** check out the wiki

[Wiki and Api Documentation](https://irishbruse.github.io/LDtkMonogame/)
------------

# Example

This small game example [LDtkMonogame.Examples](https://github.com/IrishBruse/LDtkMonogame/tree/main/LDtkMonogame.Examples) showcases how easy it is to get setup and making levels for your game

## How to run
- Open the solution and hit run in visual studio or
- `cd` into the `LDtkMonogame.Examples` folder and use `dotnet run` to play the example game

You can even edit the .ldtk file and run it again to see the changes

![Example Gameplay](docfx_project/art/screenshots/Example%20Project.gif "Gameplay")