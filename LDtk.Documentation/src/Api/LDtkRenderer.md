# LDtkRenderer

  
Renderer for the ldtkWorld, ldtkLevel, intgrids and entities.  
This can all be done in your own class if you want to reimplement it and customize it differently  
this one is mostly here to get you up and running quickly.  


## Methods

This is used to intizialize the renderer for use with direct file loading
```csharp
public LDtkRenderer(SpriteBatch)
```

This is used to intizialize the renderer for use with content Pipeline
```csharp
public LDtkRenderer(SpriteBatch,ContentManager)
```

Prerender out the level to textures to optimize the rendering process
```csharp
public Void PrerenderLevel(LDtkLevel level);
```

Render the prerendered level you created from PrerenderLevel()
```csharp
public Void RenderPrerenderedLevel(LDtkLevel level);
```

Render the level directly without prerendering the layers alot slower than prerendering
```csharp
public Void RenderLevel(LDtkLevel level);
```

Render intgrids by displaying the intgrid as solidcolor squares
```csharp
public Void RenderIntGrid(LDtkIntGrid intGrid);
```

- else **LDtk.Renderer.LDtkRenderer.RenderEntity``1**
- else **LDtk.Renderer.LDtkRenderer.RenderEntity``1**
- else **LDtk.Renderer.LDtkRenderer.RenderEntity``1**
- else **LDtk.Renderer.LDtkRenderer.RenderEntity``1**

## Properties

The spritebatch used for rendering with this Renderer
```csharp
public SpriteBatch SpriteBatch { get; set; }
```

- else **LDtk.Renderer.LDtkRenderer.PrerenderedLevels**

