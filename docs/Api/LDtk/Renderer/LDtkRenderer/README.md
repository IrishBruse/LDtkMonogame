# ExampleRenderer Class

[Home](../../../README.md) &#x2022; [Constructors](#constructors) &#x2022; [Properties](#properties) &#x2022; [Methods](#methods)

**Namespace**: [LDtk.Renderer](../README.md)

**Assembly**: LDtkMonogame\.dll

  
Renderer for the ldtkWorld, ldtkLevel, intgrids and entities\.
This can all be done in your own class if you want to reimplement it and customize it differently
this one is mostly here to get you up and running quickly\.

```csharp
public class ExampleRenderer : IDisposable
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; ExampleRenderer

### Implements

* [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)

## Constructors

| Constructor                                                                | Summary                                                                                                                                              |
| -------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------- |
| [ExampleRenderer(SpriteBatch, ContentManager)](-ctor/README.md#2642043051) | Initializes a new instance of the [ExampleRenderer](./README.md) class\. This is used to intizialize the renderer for use with content Pipeline\.    |
| [ExampleRenderer(SpriteBatch)](-ctor/README.md#3898746929)                 | Initializes a new instance of the [ExampleRenderer](./README.md) class\. This is used to intizialize the renderer for use with direct file loading\. |

## Properties

| Property                             | Summary                                                              |
| ------------------------------------ | -------------------------------------------------------------------- |
| [SpriteBatch](SpriteBatch/README.md) | Gets or sets the spritebatch used for rendering with this Renderer\. |

## Methods

| Method                                                                                         | Summary                                                                                                                        |
| ---------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------ |
| [Dispose()](Dispose/README.md)                                                                 | Dispose Renderer  \(Implements [IDisposable.Dispose](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable.dispose)\) |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals)             | \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\)                                         |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode)         | \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\)                                         |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype)                 | \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\)                                         |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) | \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\)                                         |
| [PrerenderLevel(LDtkLevel)](PrerenderLevel/README.md)                                          | Prerender out the level to textures to optimize the rendering process\.                                                        |
| [RenderEntity\<T\>(T, Texture2D, Int32)](RenderEntity/README.md#1464074736)                    | Renders the entity with the tile it includes\.                                                                                 |
| [RenderEntity\<T\>(T, Texture2D, SpriteEffects, Int32)](RenderEntity/README.md#122675233)      | Renders the entity with the tile it includes\.                                                                                 |
| [RenderEntity\<T\>(T, Texture2D, SpriteEffects)](RenderEntity/README.md#186185740)             | Renders the entity with the tile it includes\.                                                                                 |
| [RenderEntity\<T\>(T, Texture2D)](RenderEntity/README.md#2638496844)                           | Renders the entity with the tile it includes\.                                                                                 |
| [RenderIntGrid(LDtkIntGrid)](RenderIntGrid/README.md)                                          | Render intgrids by displaying the intgrid as solidcolor squares\.                                                              |
| [RenderLevel(LDtkLevel)](RenderLevel/README.md)                                                | Render the level directly without prerendering the layers alot slower than prerendering\.                                      |
| [RenderPrerenderedLevel(LDtkLevel)](RenderPrerenderedLevel/README.md)                          | Render the prerendered level you created from PrerenderLevel\(\)\.                                                             |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring)               | \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\)                                         |

