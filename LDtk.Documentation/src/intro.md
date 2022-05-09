# Introduction

The easiest way to start using LDtkMonogame is to import it into the project using

[![Nuget](https://img.shields.io/nuget/dt/LDtkMonogame?label=LDtkMonogame&logo=nuget&color=brightgreen)](https://www.nuget.org/packages/LDtkMonogame/)

[![Nuget](https://img.shields.io/nuget/dt/LDtkMonogame.ContentPipeline?label=ContentPipeline&logo=nuget&color=brightgreen)](https://www.nuget.org/packages/LDtkMonogame.ContentPipeline/)

[![Nuget](https://img.shields.io/nuget/dt/LDtkMonogame.Codegen?label=Codegen&logo=nuget&color=brightgreen)](https://www.nuget.org/packages/LDtkMonogame.Codegen/)

Make sure to import the namespace at the top
```cs
using LDtk;
// Optional
using LDtk.Renderer;
```

LDtk.Renderer is a premade renderer for the levels, you can create your own if you have more specific needs
[LDtkRenderer.cs](https://github.com/IrishBruse/LDtkMonogame/blob/main/LDtk/Renderer/LDtkRenderer.cs)
is an example of how to make one.

To get started loading ldtk files load a world in `Initialize`.

```cs
LDtkWorld world = LDtkFile.FromFile("World", Content);
// or
LDtkWorld world = LDtkFile.FromFile("Data/World.ldtk");
```

Create the renderer in `Initialize`.

```cs
LDtkRenderer renderer = new LDtkRenderer(spriteBatch, Content);
// or
LDtkRenderer renderer = new LDtkRenderer(spriteBatch);
```

Prerender Levels

```cs
for (int i = 0; i < world.Levels.Length; i++)
{
    renderer.PrerenderLevel(world.Levels[i]);
}
```

Now to render the level and entities we loaded in `Draw`

```cs
GraphicsDevice.Clear(world.BgColor);

spriteBatch.Begin(samplerState: SamplerState.PointClamp);
{
    renderer.RenderPrerenderedLevel(world.Levels[i]);
}
spriteBatch.End();
```
