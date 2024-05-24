namespace LDtk.Renderer;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using LDtk;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// Renderer for the ldtkWorld, ldtkLevel, intgrids and entities.
/// This can all be done in your own class if you want to reimplement it and customize it differently
/// this one is mostly here to get you up and running quickly.
/// </summary>
public class ExampleRenderer : IDisposable
{
    /// <summary> Gets or sets the spritebatch used for rendering with this Renderer. </summary>
    public SpriteBatch SpriteBatch { get; set; }

    /// <summary> Gets or sets the levels identifier to layers Dictionary. </summary>
    Dictionary<string, RenderedLevel> PrerenderedLevels { get; } = [];

    /// <summary> Gets or sets the levels identifier to layers Dictionary. </summary>
    Dictionary<string, Texture2D> TilemapCache { get; } = [];

    readonly Texture2D pixel;
    readonly Texture2D error;
    readonly GraphicsDevice graphicsDevice;
    readonly ContentManager? content;

    /// <summary> Initializes a new instance of the <see cref="ExampleRenderer"/> class. This is used to intizialize the renderer for use with direct file loading. </summary>
    /// <param name="spriteBatch">Spritebatch</param>
    public ExampleRenderer(SpriteBatch spriteBatch)
    {
        SpriteBatch = spriteBatch;
        graphicsDevice = spriteBatch.GraphicsDevice;

        if (pixel == null)
        {
            pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });
        }

        if (error == null)
        {
            error = new Texture2D(graphicsDevice, 2, 2);
            error.SetData(
                new byte[]
                {
                    0xFF, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0xFF,
                    0xFF, 0xFF, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0x00,
                }
            );
        }
    }

    /// <summary> Initializes a new instance of the <see cref="ExampleRenderer"/> class. This is used to intizialize the renderer for use with content Pipeline. </summary>
    /// <param name="spriteBatch">SpriteBatch</param>
    /// <param name="content">Optional ContentManager</param>
    public ExampleRenderer(SpriteBatch spriteBatch, ContentManager content)
        : this(spriteBatch)
    {
        this.content = content;
    }

    /// <summary> Prerender out the level to textures to optimize the rendering process. </summary>
    /// <param name="level">The level to prerender.</param>
    /// <returns>The prerendered level.</returns>
    /// <exception cref="Exception">The level already has been prerendered.</exception>
    public RenderedLevel PrerenderLevel(LDtkLevel level)
    {
        if (PrerenderedLevels.TryGetValue(level.Identifier, out RenderedLevel cachedLevel))
        {
            return cachedLevel;
        }

        RenderedLevel renderLevel = new();

        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        {
            renderLevel.Layers = RenderLayers(level);
        }
        SpriteBatch.End();

        PrerenderedLevels.Add(level.Identifier, renderLevel);
        graphicsDevice.SetRenderTarget(null);

        return renderLevel;
    }

    Texture2D[] RenderLayers(LDtkLevel level)
    {
        List<Texture2D> layers = [];

        if (level.BgRelPath != null)
        {
            layers.Add(RenderBackgroundToLayer(level));
        }

        if (level.LayerInstances == null)
        {
            if (level.ExternalRelPath != null)
            {
                throw new Exception("Level has not been loaded.");
            }
            else
            {
                throw new Exception("Level has no layers.");
            }
        }

        // Render Tile, Auto and Int grid layers
        for (int i = level.LayerInstances.Length - 1; i >= 0; i--)
        {
            LayerInstance layer = level.LayerInstances[i];

            if (layer._TilesetRelPath == null)
            {
                continue;
            }

            if (layer._Type == LayerType.Entities)
            {
                continue;
            }

            Texture2D texture = GetTexture(level, layer._TilesetRelPath);

            int width = layer._CWid * layer._GridSize;
            int height = layer._CHei * layer._GridSize;
            RenderTarget2D renderTarget = new(graphicsDevice, width, height, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);

            graphicsDevice.SetRenderTarget(renderTarget);
            layers.Add(renderTarget);

            switch (layer._Type)
            {
                case LayerType.Tiles:
                foreach (TileInstance tile in layer.GridTiles.Where(_ => layer._TilesetDefUid.HasValue))
                {
                    Vector2 position = new(tile.Px.X + layer._PxTotalOffsetX, tile.Px.Y + layer._PxTotalOffsetY);
                    Rectangle rect = new(tile.Src.X, tile.Src.Y, layer._GridSize, layer._GridSize);
                    SpriteEffects mirror = (SpriteEffects)tile.F;
                    SpriteBatch.Draw(texture, position, rect, new Color(1f, 1f, 1f, layer._Opacity), 0, Vector2.Zero, 1f, mirror, 0);
                }
                break;

                case LayerType.AutoLayer:
                case LayerType.IntGrid:
                if (layer.AutoLayerTiles.Length > 0)
                {
                    foreach (TileInstance tile in layer.AutoLayerTiles.Where(_ => layer._TilesetDefUid.HasValue))
                    {
                        Vector2 position = new(tile.Px.X + layer._PxTotalOffsetX, tile.Px.Y + layer._PxTotalOffsetY);
                        Rectangle rect = new(tile.Src.X, tile.Src.Y, layer._GridSize, layer._GridSize);
                        SpriteEffects mirror = (SpriteEffects)tile.F;
                        SpriteBatch.Draw(texture, position, rect, new Color(1f, 1f, 1f, layer._Opacity), 0, Vector2.Zero, 1f, mirror, 0);
                    }
                }
                break;
            }
        }

        return layers.ToArray();
    }

    Texture2D RenderBackgroundToLayer(LDtkLevel level)
    {
        Texture2D texture = GetTexture(level, level.BgRelPath);

        RenderTarget2D layer = new(graphicsDevice, level.PxWid, level.PxHei, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);

        graphicsDevice.SetRenderTarget(layer);
        {
            LevelBackgroundPosition? bg = level._BgPos;
            if (bg != null)
            {
                Vector2 pos = bg.TopLeftPx.ToVector2();
                SpriteBatch.Draw(texture, pos, new Rectangle((int)bg.CropRect[0], (int)bg.CropRect[1], (int)bg.CropRect[2], (int)bg.CropRect[3]), Color.White, 0, Vector2.Zero, bg.Scale, SpriteEffects.None, 0);
            }
        }
        graphicsDevice.SetRenderTarget(null);

        return layer;
    }

    Texture2D GetTexture(LDtkLevel level, string? path)
    {
        if (path == null)
        {
            throw new LDtkException("Tileset path is null.");
        }

        if (TilemapCache.TryGetValue(path, out Texture2D? texture))
        {
            return texture;
        }

        Texture2D tilemap;
        if (content == null)
        {
            string directory = Path.GetDirectoryName(level.WorldFilePath)!;
            string assetName = Path.Join(directory, path);
            tilemap = Texture2D.FromFile(graphicsDevice, assetName);
        }
        else
        {
            string file = Path.ChangeExtension(path, null);
            string directory = Path.GetDirectoryName(level.WorldFilePath)!;
            string assetName = Path.Join(directory, file);
            tilemap = content.Load<Texture2D>(assetName);
        }

        TilemapCache.Add(path, tilemap);

        return tilemap;
    }

    /// <summary> Render the prerendered level you created from PrerenderLevel(). </summary>
    /// <param name="level">Level to prerender</param>
    /// <exception cref="LDtkException"></exception>
    public void RenderPrerenderedLevel(LDtkLevel level)
    {
        if (PrerenderedLevels.TryGetValue(level.Identifier, out RenderedLevel prerenderedLevel))
        {
            for (int i = 0; i < prerenderedLevel.Layers.Length; i++)
            {
                SpriteBatch.Draw(prerenderedLevel.Layers[i], level.Position.ToVector2(), Color.White);
            }
        }
        else
        {
            throw new LDtkException($"No prerendered level with Identifier {level.Identifier} found.");
        }
    }

    /// <summary> Render the level directly without prerendering the layers alot slower than prerendering. </summary>
    /// <param name="level">Level to render</param>
    public void RenderLevel(LDtkLevel level)
    {
        ArgumentNullException.ThrowIfNull(level);
        Texture2D[] layers = RenderLayers(level);

        for (int i = 0; i < layers.Length; i++)
        {
            SpriteBatch.Draw(layers[i], level.Position.ToVector2(), Color.White);
        }
    }

    /// <summary> Render intgrids by displaying the intgrid as solidcolor squares. </summary>
    /// <param name="intGrid">Render intgrid</param>
    public void RenderIntGrid(LDtkIntGrid intGrid)
    {
        for (int x = 0; x < intGrid.GridSize.X; x++)
        {
            for (int y = 0; y < intGrid.GridSize.Y; y++)
            {
                int cellValue = intGrid.Values[(y * intGrid.GridSize.X) + x];

                if (cellValue != 0)
                {
                    // Color col = intGrid.GetColorFromValue(cellValue);
                    int spriteX = intGrid.WorldPosition.X + (x * intGrid.TileSize);
                    int spriteY = intGrid.WorldPosition.Y + (y * intGrid.TileSize);
                    SpriteBatch.Draw(pixel, new Vector2(spriteX, spriteY), null, Color.Pink /*col*/, 0, Vector2.Zero, new Vector2(intGrid.TileSize), SpriteEffects.None, 0);
                }
            }
        }
    }

    /// <summary> Renders the entity with the tile it includes. </summary>
    /// <param name="entity">The entity you want to render.</param>
    /// <param name="texture">The spritesheet/texture for rendering the entity.</param>
    public void RenderEntity<T>(T entity, Texture2D texture)
        where T : ILDtkEntity
    {
        SpriteBatch.Draw(texture, entity.Position, entity.Tile, Color.White, 0, entity.Pivot * entity.Size, 1, SpriteEffects.None, 0);
    }

    /// <summary> Renders the entity with the tile it includes. </summary>
    /// <param name="entity">The entity you want to render.</param>
    /// <param name="texture">The spritesheet/texture for rendering the entity.</param>
    /// <param name="flipDirection">The direction to flip the entity when rendering.</param>
    public void RenderEntity<T>(T entity, Texture2D texture, SpriteEffects flipDirection)
        where T : ILDtkEntity
    {
        SpriteBatch.Draw(texture, entity.Position, entity.Tile, Color.White, 0, entity.Pivot * entity.Size, 1, flipDirection, 0);
    }

    /// <summary> Renders the entity with the tile it includes. </summary>
    /// <param name="entity">The entity you want to render.</param>
    /// <param name="texture">The spritesheet/texture for rendering the entity.</param>
    /// <param name="animationFrame">The current frame of animation. Is a very basic entity animation frames must be to the right of them and be the same size.</param>
    public void RenderEntity<T>(T entity, Texture2D texture, int animationFrame)
        where T : ILDtkEntity
    {
        Rectangle animatedTile = entity.Tile;
        animatedTile.Offset(animatedTile.Width * animationFrame, 0);
        SpriteBatch.Draw(texture, entity.Position, animatedTile, Color.White, 0, entity.Pivot * entity.Size, 1, SpriteEffects.None, 0);
    }

    /// <summary> Renders the entity with the tile it includes. </summary>
    /// <param name="entity">The entity you want to render.</param>
    /// <param name="texture">The spritesheet/texture for rendering the entity.</param>
    /// <param name="flipDirection">The direction to flip the entity when rendering.</param>
    /// <param name="animationFrame">The current frame of animation. Is a very basic entity animation frames must be to the right of them and be the same size.</param>
    public void RenderEntity<T>(T entity, Texture2D texture, SpriteEffects flipDirection, int animationFrame)
        where T : ILDtkEntity
    {
        Rectangle animatedTile = entity.Tile;
        animatedTile.Offset(animatedTile.Width * animationFrame, 0);
        SpriteBatch.Draw(texture, entity.Position, animatedTile, Color.White, 0, entity.Pivot * entity.Size, 1, flipDirection, 0);
    }

    /// <summary> Dispose Renderer </summary>
    public void Dispose()
    {
        pixel.Dispose();
        GC.SuppressFinalize(this);
    }
}
