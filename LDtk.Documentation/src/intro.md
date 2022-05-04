# Quick Start Guide

The easiest way to start using LDtkMonogame is to import it into the project using

| Name                         | Package                                                                                                                                                      | Description                                                                                  |
| ---------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------ | -------------------------------------------------------------------------------------------- |
| LDtkMonogame                 | [![NuGet Badge](https://buildstats.info/nuget/LDtkMonogame)](https://www.nuget.org/packages/LDtkMonogame/)                                                   | Core LDtk Package                                                                            |
| LDtkMonogame.Codegen         | [![LDtkMonogame.Codegen](https://buildstats.info/nuget/LDtkMonogame.Codegen) ](https://www.nuget.org/packages/LDtkMonogame.Codegen/)                         | Codegen tool for ldtk thanks to [ldtk_codegen](https://github.com/codefrommars/ldtk_codegen) |
| LDtkMonogame.ContentPipeline | [![LDtkMonogame.ContentPipeline](https://buildstats.info/nuget/LDtkMonogame.ContentPipeline) ](https://www.nuget.org/packages/LDtkMonogame.ContentPipeline/) | Includes the dll needed for the MGCP tool                                                    |

Make sure to import the namespace at the top

```csharp
using LDtk;

// Optional
using LDtk.Renderer;
```

LDtk.Renderer is a premade renderer for the levels, you can create your own if you have more specific needs
[LDtkRenderer.cs](https://github.com/IrishBruse/LDtkMonogame/blob/main/LDtk/Renderer/LDtkRenderer.cs)
is an example of how to make one.

To get started loading ldtk files load a world in `Initialize`.

```csharp
World world = LDtkWorld.LoadWorld("World", Content);
// or
World world = LDtkWorld.LoadWorld("Data/World.ldtk");
```

Create the renderer in `Initialize`.

```csharp
LDtkRenderer renderer = new LDtkRenderer(spriteBatch, Content);
// or
LDtkRenderer renderer = new LDtkRenderer(spriteBatch);
```

Prerender Levels

```csharp
for (int i = 0; i < world.Levels.Length; i++)
{
    renderer.PrerenderLevel(world.Levels[i]);
}
```

Now to render the level and entities we loaded in `Draw`

```csharp
GraphicsDevice.Clear(world.BgColor);

spriteBatch.Begin(samplerState: SamplerState.PointClamp);
{
    renderer.RenderPrerenderedLevel(world.Levels[i]);
}
spriteBatch.End();
```
