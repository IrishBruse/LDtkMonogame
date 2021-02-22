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
spriteBatch.Begin(samplerState: SamplerState.PointClamp);
{
    for(int i = 0; i < startLevel.Layers.Length; i++)
    {
        spriteBatch.Draw(startLevel.Layers[i], startLevel.WorldPosition, Color.White);
    }
}
spriteBatch.End();
```