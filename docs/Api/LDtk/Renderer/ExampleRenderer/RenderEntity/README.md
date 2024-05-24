# ExampleRenderer\.RenderEntity Method

[Home](../../../../README.md)

**Containing Type**: [ExampleRenderer](../README.md)

**Assembly**: LDtkMonogame\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| [RenderEntity\<T\>(T, Texture2D, Int32)](#1131241597) |  Renders the entity with the tile it includes\.  |
| [RenderEntity\<T\>(T, Texture2D, SpriteEffects, Int32)](#2443357953) |  Renders the entity with the tile it includes\.  |
| [RenderEntity\<T\>(T, Texture2D, SpriteEffects)](#640468595) |  Renders the entity with the tile it includes\.  |
| [RenderEntity\<T\>(T, Texture2D)](#1831278512) |  Renders the entity with the tile it includes\.  |

<a id="1131241597"></a>

## RenderEntity\<T\>\(T, Texture2D, Int32\) 

  
 Renders the entity with the tile it includes\. 

```csharp
public void RenderEntity<T>(T entity, Microsoft.Xna.Framework.Graphics.Texture2D texture, int animationFrame) where T : LDtk.ILDtkEntity
```

### Type Parameters

**T**

### Parameters

**entity** &ensp; T

The entity you want to render\.

**texture** &ensp; [Texture2D](https://docs.microsoft.com/en-us/dotnet/api/microsoft.xna.framework.graphics.texture2d)

The spritesheet/texture for rendering the entity\.

**animationFrame** &ensp; [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)

The current frame of animation\. Is a very basic entity animation frames must be to the right of them and be the same size\.<a id="2443357953"></a>

## RenderEntity\<T\>\(T, Texture2D, SpriteEffects, Int32\) 

  
 Renders the entity with the tile it includes\. 

```csharp
public void RenderEntity<T>(T entity, Microsoft.Xna.Framework.Graphics.Texture2D texture, Microsoft.Xna.Framework.Graphics.SpriteEffects flipDirection, int animationFrame) where T : LDtk.ILDtkEntity
```

### Type Parameters

**T**

### Parameters

**entity** &ensp; T

The entity you want to render\.

**texture** &ensp; [Texture2D](https://docs.microsoft.com/en-us/dotnet/api/microsoft.xna.framework.graphics.texture2d)

The spritesheet/texture for rendering the entity\.

**flipDirection** &ensp; [SpriteEffects](https://docs.microsoft.com/en-us/dotnet/api/microsoft.xna.framework.graphics.spriteeffects)

The direction to flip the entity when rendering\.

**animationFrame** &ensp; [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)

The current frame of animation\. Is a very basic entity animation frames must be to the right of them and be the same size\.<a id="640468595"></a>

## RenderEntity\<T\>\(T, Texture2D, SpriteEffects\) 

  
 Renders the entity with the tile it includes\. 

```csharp
public void RenderEntity<T>(T entity, Microsoft.Xna.Framework.Graphics.Texture2D texture, Microsoft.Xna.Framework.Graphics.SpriteEffects flipDirection) where T : LDtk.ILDtkEntity
```

### Type Parameters

**T**

### Parameters

**entity** &ensp; T

The entity you want to render\.

**texture** &ensp; [Texture2D](https://docs.microsoft.com/en-us/dotnet/api/microsoft.xna.framework.graphics.texture2d)

The spritesheet/texture for rendering the entity\.

**flipDirection** &ensp; [SpriteEffects](https://docs.microsoft.com/en-us/dotnet/api/microsoft.xna.framework.graphics.spriteeffects)

The direction to flip the entity when rendering\.<a id="1831278512"></a>

## RenderEntity\<T\>\(T, Texture2D\) 

  
 Renders the entity with the tile it includes\. 

```csharp
public void RenderEntity<T>(T entity, Microsoft.Xna.Framework.Graphics.Texture2D texture) where T : LDtk.ILDtkEntity
```

### Type Parameters

**T**

### Parameters

**entity** &ensp; T

The entity you want to render\.

**texture** &ensp; [Texture2D](https://docs.microsoft.com/en-us/dotnet/api/microsoft.xna.framework.graphics.texture2d)

The spritesheet/texture for rendering the entity\.