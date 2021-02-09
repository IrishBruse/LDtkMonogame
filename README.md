<p align="center">
 <a href="https://github.com/deepnight/ldtk"> <img alt="LDtk Version Support" src="https://img.shields.io/github/v/release/deepnight/ldtk?&label=Supports%20LDtk"></a>
<a href="https://www.nuget.org/packages/LDtkMonogame/"><img src="https://img.shields.io/nuget/v/LDtkMonogame?" /></a>
<a href="https://www.nuget.org/packages/LDtkMonogame/"><img alt="Nuget" src="https://img.shields.io/nuget/dt/LDtkMonogame"></a>
</p>
<p align="center">
  <img alt="GitHub Workflow Status" src="https://img.shields.io/github/workflow/status/IrishBruse/LDtkMonogame/Build">
</p>

# Ldtk Monogame

Monogame Render for LDtk levels

Example code snippet
```csharp
Project projectFile;
Level startLevel;
Level[] neighbours;

override void Initialize()
{
    projectFile = new Project(spriteBatch, "PATH TO THE LDTK FILE");
    projectFile.Load(0);

    startLevel = projectFile.GetLevel("Level1");
    neighbours = (from neighbour in startLevel.Neighbours select projectFile.GetLevel(neighbour)).ToArray();

    base.Initialize();
}

override void Update(GameTime gameTime)
{

}

override void Draw(GameTime gameTime)
{
    GraphicsDevice.Clear(startLevel.BgColor);

    spriteBatch.Begin(SpriteSortMode.Texture, samplerState: SamplerState.PointClamp);
    {
        for(int i = 0; i < startLevel.Layers.Length; i++)
        {
            spriteBatch.Draw(startLevel.Layers[i], startLevel.WorldPosition, Color.White);
        }

        for(int i = 0; i < neighbours.Length; i++)
        {
            for(int j = 0; j < neighbours[i].Layers.Length; j++)
            {
                spriteBatch.Draw(neighbours[i].Layers[j], neighbours[i].WorldPosition, Color.White);
            }
        }
    }
    spriteBatch.End();

    base.Draw(gameTime);
}
```

Better code examples [LDtkMonogame.Examples](https://github.com/IrishBruse/LDtkMonogame/tree/main/LDtkMonogame.Examples)
