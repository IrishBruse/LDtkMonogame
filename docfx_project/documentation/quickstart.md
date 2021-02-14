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
------------
