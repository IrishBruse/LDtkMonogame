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
is an example of how to make one. Or you can inherit it and extend it.

To get started loading ldtk files load the file in `Initialize`.

```cs
LDtkFile file = LDtkFile.FromFile("World", Content);
// or
LDtkFile file = LDtkFile.FromFile("Data/World.ldtk");
```

Then load the world right after for now ldtk only supports one file but make sure to enable the multiworlds flag in the project settings under advanced.

```cs
LDtkWorld world = file.LoadWorld(Worlds.World.Iid);
```

The `Worlds.World.Iid` is generated from the ldtkgen tool and is recommended that you use it for static typing of entities. It is a class within in a class that represents the world name and the levels name and holds the iid you can use to load that specific level.

Create the renderer in `Initialize`.

```cs
LDtkRenderer renderer = new LDtkRenderer(spriteBatch, Content);
// or
LDtkRenderer renderer = new LDtkRenderer(spriteBatch);
```

Prerender Levels

```cs
foreach (LDtkLevel level in world.Levels)
{
    renderer.PrerenderLevel(level);
}
```

Now to render the level and entities we loaded in `Draw`

```cs
GraphicsDevice.Clear(world.BgColor);

spriteBatch.Begin(samplerState: SamplerState.PointClamp);
{
    foreach (LDtkLevel level in world.Levels)
    {
        renderer.RenderPrerenderedLevel(level);
    }
}
spriteBatch.End();
```
