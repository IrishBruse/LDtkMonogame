namespace LDtk.Renderer;

using System;
using System.Collections.Generic;
using System.IO;

using LDtk;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// Renderer for the ldtkWorld, ldtkLevel, intgrids and entities.
/// This can all be done in your own class if you want to reimplement it and customize it differently
/// this one is mostly here to get you up and running quickly.
/// </summary>
public class LevelRenderer : IDisposable
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

    /// <summary> Initializes a new instance of the <see cref="LDtkRenderer"/> class. This is used to intizialize the renderer for use with direct file loading. </summary>
    /// <param name="spriteBatch">Spritebatch</param>
    public LevelRenderer(SpriteBatch spriteBatch)
    {
        SpriteBatch = spriteBatch;
        graphicsDevice = spriteBatch.GraphicsDevice;

        if (pixel == null)
        {
            pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData(new byte[]
                {
                    0xFF, 0xFF, 0xFF, 0xFF
                }
            );
        }

        if (error == null)
        {
            error = new Texture2D(graphicsDevice, 2, 2);
            error.SetData(new byte[]
                {
                    0xFF, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0xFF,
                    0xFF, 0xFF, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0x00,
                }
            );
        }
    }

    /// <summary> Initializes a new instance of the <see cref="LDtkRenderer"/> class. This is used to intizialize the renderer for use with content Pipeline. </summary>
    /// <param name="spriteBatch">SpriteBatch</param>
    /// <param name="content">Optional ContentManager</param>
    public LevelRenderer(SpriteBatch spriteBatch, ContentManager content)
        : this(spriteBatch)
    {
        this.content = content;
    }

    /// <summary> Prerender out the level to textures to optimize the rendering process. </summary>
    /// <param name="level">The level to prerender.</param>
    public void PrerenderLevel(LDtkLevel level)
    {
        if (PrerenderedLevels.ContainsKey(level.Identifier))
        {
            return;
        }

        RenderedLevel renderLevel = new()
        {
            Layers = RenderLayers(level)
        };

        PrerenderedLevels.Add(level.Identifier, renderLevel);
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
            return layers.ToArray();
        }

        // Render Tile, Auto and Int grid layers
        for (int i = level.LayerInstances.Length - 1; i >= 0; i--)
        {
            LayerInstance layer = level.LayerInstances[i];

            if (string.IsNullOrEmpty(layer._TilesetRelPath))
            {
                continue;
            }

            if (layer._Type == LayerType.Entities)
            {
                continue;
            }

            SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            {
                Texture2D texture = GetTexture(level, layer._TilesetRelPath);

                int width = layer._CWid * layer._GridSize;
                int height = layer._CHei * layer._GridSize;

                RenderTarget2D renderTarget = new(graphicsDevice, width, height)
                {
                    Name = layer._Identifier
                };

                graphicsDevice.SetRenderTarget(renderTarget);
                graphicsDevice.Clear(Color.Transparent);

                var tiles = layer._Type switch
                {
                    LayerType.Tiles => layer.GridTiles,
                    LayerType.AutoLayer => layer.AutoLayerTiles,
                    LayerType.IntGrid => layer.AutoLayerTiles,
                    LayerType.Entities => [],
                    _ => [],
                };

                foreach (TileInstance tile in tiles)
                {
                    DrawTile(layer, texture, tile);
                }

                layers.Add(renderTarget);
            }
            SpriteBatch.End();
        }

        graphicsDevice.SetRenderTarget(null);
        return layers.ToArray();
    }

    void DrawTile(LayerInstance layer, Texture2D texture, TileInstance tile)
    {
        Vector2 position = new(tile.Px.X + layer._PxTotalOffsetX, tile.Px.Y + layer._PxTotalOffsetY);
        Rectangle rect = new(tile.Src.X, tile.Src.Y, layer._GridSize, layer._GridSize);
        SpriteEffects mirror = (SpriteEffects)tile.F;
        SpriteBatch.Draw(
            texture,
            position,
            rect,
            Color.White,
            0,
            Vector2.Zero,
            1f,
            mirror,
            0.5f);
    }

    RenderTarget2D RenderBackgroundToLayer(LDtkLevel level)
    {
        Texture2D texture = GetTexture(level, level.BgRelPath);

        RenderTarget2D layer = new(graphicsDevice, level.PxWid, level.PxHei)
        {
            Name = "Background"
        };

        graphicsDevice.SetRenderTarget(layer);
        {
            LevelBackgroundPosition? bg = level._BgPos;
            if (bg != null)
            {
                Vector2 position = bg.TopLeftPx.ToVector2();
                SpriteBatch.Draw(
                    texture,
                    position,
                    new Rectangle((int)bg.CropRect[0], (int)bg.CropRect[1], (int)bg.CropRect[2], (int)bg.CropRect[3]),
                    Color.White,
                    0,
                    Vector2.Zero,
                    bg.Scale,
                    SpriteEffects.None,
                    0);
            }
        }
        graphicsDevice.SetRenderTarget(null);

        return layer;
    }

    Texture2D GetTexture(LDtkLevel level, string? path)
    {
        if (path == null)
        {
            return error;
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
                Texture2D layer = prerenderedLevel.Layers[i];

                float depth = 1f - (i * 0.05f);
                // if (layer.Name == "Buildings")
                // {
                SpriteBatch.Draw(
                    layer,
                    new(level.Position.X, level.Position.Y, layer.Bounds.Width, layer.Bounds.Height),
                    layer.Bounds,
                    new(0xffffffff),
                    0,
                    Vector2.Zero,
                    SpriteEffects.None,
                    depth);
                // }
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
            layers[i].Dispose();
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
                    int spriteX = intGrid.WorldPosition.X + (x * intGrid.TileSize);
                    int spriteY = intGrid.WorldPosition.Y + (y * intGrid.TileSize);
                    SpriteBatch.Draw(
                        pixel,
                        new Vector2(spriteX, spriteY),
                        null,
                        Color.Pink,
                        0,
                        Vector2.Zero,
                        new Vector2(intGrid.TileSize),
                        SpriteEffects.None,
                        0);
                }
            }
        }
    }

    /// <summary> Dispose Renderer </summary>
    public void Dispose()
    {
        pixel.Dispose();
        error.Dispose();
        GC.SuppressFinalize(this);
    }
}
