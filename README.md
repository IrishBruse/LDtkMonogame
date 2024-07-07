<p align="center">
<img src="https://raw.githubusercontent.com/IrishBruse/LDtkMonogame/main/Icon.png" height="128px"/>
</p>

<p align="center">
    <a href="https://ldtk.io/go/discord"><img src="https://img.shields.io/badge/Discord-LDtk-yellow" alt="Discord Link"></a> &nbsp;
    <a href="https://github.com/deepnight/ldtk"><img src="https://img.shields.io/badge/LDtk-1.5.3-yellow" alt=""></a>
</p>

LDtkMonogame is an level importer for the LDtk level editor

# Getting Started

The easiest way to start using LDtkMonogame is to import it into the project using [NuGet package](https://www.nuget.org/packages/LDtkMonogame/).

-   [LDtkMonogame](https://www.nuget.org/packages/LDtkMonogame/)
-   [LDtkMonogame.ContentPipeline](https://www.nuget.org/packages/LDtkMonogame.ContentPipeline/)
-   [LDtkMonogame.Codegen](https://www.nuget.org/packages/LDtkMonogame.Codegen/)

Make sure to import the namespace at the top

```cs
using LDtk;
// Optional
using LDtk.Renderer;
```

LDtk.Renderer is a premade renderer for the levels, you can create your own if you have more specific needs
[ExampleRenderer.cs](https://github.com/IrishBruse/LDtkMonogame/blob/main/LDtk/Renderer/ExampleRenderer.cs)
is an example of how to make one. Or you can inherit it and extend it.

To get started loading ldtk files load the file in `Initialize`.

```cs
LDtkFile file = LDtkFile.FromFile("World", Content);
LDtkFile file = LDtkFile.FromFile("Data/World.ldtk");
```

Then load the world right after for now ldtk only supports one file but make sure to enable the multiworlds flag in the project settings under advanced.

```cs
LDtkWorld world = file.LoadWorld(Worlds.World.Iid);
```

The `Worlds.World.Iid` is generated from the ldtkgen tool and is recommended that you use it for static typing of entities and levels.  
It is a class within in a class that represents the world name and the levels name and holds the iid you can use to load that specific level.

Create the renderer in `Initialize`.

```cs
ExampleRenderer renderer = new ExampleRenderer(spriteBatch, Content);
ExampleRenderer renderer = new ExampleRenderer(spriteBatch);
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

# Showcase

## Unnamed

![screenshot](https://raw.githubusercontent.com/IrishBruse/LDtkMonogame/main/docs/Unnamed.png)

> by Fypur

[Play the game on Itch](https://fypur.itch.io/unnamed)

## Example Game

![screenshot](https://raw.githubusercontent.com/IrishBruse/LDtkMonogame/main/LDtk.Example/Screenshot.png)

> by IrishBruse

[Source code here](./LDtk.Example/)
