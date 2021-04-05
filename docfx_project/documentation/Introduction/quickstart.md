# Quick Start Guide

The easiest way to start using LDtkMonogame is to import it into the project using  

<a href="https://www.nuget.org/packages/LDtkMonogame/"><img src="https://img.shields.io/nuget/v/LDtkMonogame?" /></a>

Make sure to import the namespace at the top

```csharp
using LDtk;
```

To get started loading ldtk files create a  

```csharp
World world = new World(spriteBatch,"Assets/MyProject.ldtk");
```

Now to load the level

```csharp
Level startLevel = world.GetLevel("Level1");
```

Now to load the entities

```csharp
Entity[] diamonds = startLevel.GetEntities<Entity>("Diamond");
```

Now to render the level and entities we loaded in `Draw`

```csharp
spriteBatch.Begin(samplerState: SamplerState.PointClamp);
{
    for(int i = 0; i < startLevel.Layers.Length; i++)
    {
        spriteBatch.Draw(startLevel.Layers[i], startLevel.Position, Color.White);
    }

    for (int i = 0; i < entities.Count; i++)
    {
        spriteBatch.Draw(diamonds[i].texture, diamonds[i].position, diamonds.size,
        Color.White, 0, diamonds[i].pivot * diamonds[i].size, 1, SpriteEffects.None, 0);
    }
}
spriteBatch.End();
```
