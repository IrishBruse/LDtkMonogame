# LDtkRenderer

  
Renderer for the ldtkWorld, ldtkLevel, intgrids and entities.  
This can all be done in your own class if you want to reimplement it and customize it differently  
this one is mostly here to get you up and running quickly.  


## Methods

This is used to intizialize the renderer for use with direct file loading

```csharp
LDtkRenderer.#ctor(Microsoft.Xna.Framework.Graphics.SpriteBatch)
```

This is used to intizialize the renderer for use with content Pipeline

```csharp
LDtkRenderer.#ctor(Microsoft.Xna.Framework.Graphics.SpriteBatch,Microsoft.Xna.Framework.Content.ContentManager)
```

Prerender out the level to textures to optimize the rendering process

```csharp
public void PrerenderLevel(LDtkLevel)
```

Render the prerendered level you created from PrerenderLevel()

```csharp
public void RenderPrerenderedLevel(LDtkLevel)
```

Render the level directly without prerendering the layers alot slower than prerendering

```csharp
public void RenderLevel(LDtkLevel)
```

Render intgrids by displaying the intgrid as solidcolor squares

```csharp
public void RenderIntGrid(LDtkIntGrid)
```

Renders the entity with the tile it includes

```csharp
public void RenderEntity<T>(Texture2D)
```

Renders the entity with the tile it includes

```csharp
public void RenderEntity<T>(Texture2D,SpriteEffects)
```

Renders the entity with the tile it includes

```csharp
public void RenderEntity<T>(Texture2D,int)
```

Renders the entity with the tile it includes

```csharp
public void RenderEntity<T>(Texture2D,SpriteEffects,int)
```


## Properties

The spritebatch used for rendering with this Renderer

```csharp
public SpriteBatch SpriteBatch { get; set; }
```

The levels identifier to layers Dictionary

```csharp
public RenderedLevel] PrerenderedLevels { get; set; }
```


