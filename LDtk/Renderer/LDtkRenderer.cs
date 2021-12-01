using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LDtk.Exceptions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LDtk.Renderer;

/// <summary>
/// Renderer for the ldtkWorld, ldtkLevel, intgrids and entities.
/// This can all be done in your own class if you want to reimplement it and customize it differently
/// this one is mostly here to get you up and running quickly.
/// </summary>
public class LDtkRenderer
{
    /// <summary>
    /// The spritebatch used for rendering with this Renderer
    /// </summary>
    public SpriteBatch SpriteBatch { get; set; }
    private static Texture2D pixel;
    private readonly Dictionary<string, RenderedLevel> prerenderedLevels = new();
    private readonly GraphicsDevice graphicsDevice;
    private readonly ContentManager content;

    /// <summary>
    /// This is used to intizialize the renderer for use with direct file loading
    /// </summary>
    /// <param name="spriteBatch"></param>
    public LDtkRenderer(SpriteBatch spriteBatch)
    {
        SpriteBatch = spriteBatch;
        graphicsDevice = spriteBatch.GraphicsDevice;

        if (pixel == null)
        {
            pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });
        }
    }

    /// <summary>
    /// This is used to intizialize the renderer for use with content Pipeline
    /// </summary>
    /// <param name="spriteBatch"></param>
    /// <param name="content"></param>
    public LDtkRenderer(SpriteBatch spriteBatch, ContentManager content) : this(spriteBatch)
    {
        this.content = content;
    }

    #region Levels

    /// <summary>
    /// Prerender out the level to textures to optimize the rendering process
    /// </summary>
    /// <param name="level">The level to prerender</param>
    /// <exception cref="Exception">The level already has been prerendered</exception>
    public void PrerenderLevel(LDtkLevel level)
    {
        if (prerenderedLevels.ContainsKey(level.Identifier))
        {
            return;
        }

        RenderedLevel renderLevel = new();

        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        {
            renderLevel.layers = RenderLayers(level);
        }

        SpriteBatch.End();

        prerenderedLevels.Add(level.Identifier, renderLevel);
        graphicsDevice.SetRenderTarget(null);
    }

    private Texture2D[] RenderLayers(LDtkLevel level)
    {
        List<Texture2D> layers = new();

        if (level.BgRelPath != null)
        {
            layers.Add(RenderBackgroundToLayer(level));
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
                    foreach (TileInstance tile in layer.GridTiles.Where(tile => layer._TilesetDefUid.HasValue))
                    {
                        Vector2 position = new(tile.Px.X + layer._PxTotalOffsetX, tile.Px.Y + layer._PxTotalOffsetY);
                        Rectangle rect = new(tile.Src.X, tile.Src.Y, layer._GridSize, layer._GridSize);
                        SpriteEffects mirror = (SpriteEffects)tile.F;
                        SpriteBatch.Draw(texture, position, rect, Color.White, 0, Vector2.Zero, 1f, mirror, 0);
                    }

                    break;

                case LayerType.AutoLayer:
                case LayerType.IntGrid:
                    if (layer.AutoLayerTiles.Length > 0)
                    {
                        foreach (TileInstance tile in layer.AutoLayerTiles.Where(tile => layer._TilesetDefUid.HasValue))
                        {
                            Vector2 position = new(tile.Px.X + layer._PxTotalOffsetX, tile.Px.Y + layer._PxTotalOffsetY);
                            Rectangle rect = new(tile.Src.X, tile.Src.Y, layer._GridSize, layer._GridSize);
                            SpriteEffects mirror = (SpriteEffects)tile.F;
                            SpriteBatch.Draw(texture, position, rect, Color.White, 0, Vector2.Zero, 1f, mirror, 0);
                        }
                    }

                    break;
                case LayerType.Entities:
                    break;
                default:
                    break;
            }
        }

        return layers.ToArray();
    }

    private Texture2D RenderBackgroundToLayer(LDtkLevel level)
    {
        Texture2D texture = GetTexture(level, level.BgRelPath);

        RenderTarget2D layer = new(graphicsDevice, level.PxWid, level.PxHei, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);

        graphicsDevice.SetRenderTarget(layer);
        {
            LevelBackgroundPosition bg = level._BgPos;
            Vector2 pos = bg.TopLeftPx.ToVector2();
            SpriteBatch.Draw(texture, pos, bg.CropRect, Color.White, 0, Vector2.Zero, bg.Scale, SpriteEffects.None, 0);
        }

        graphicsDevice.SetRenderTarget(null);

        return layer;
    }

    private Texture2D GetTexture(LDtkLevel level, string path)
    {
        if (content == null)
        {
            return Texture2D.FromFile(graphicsDevice, Path.Combine(level.Parent.RootFolder, path));
        }
        else
        {
            string file = Path.ChangeExtension(path, null);
            return content.Load<Texture2D>(file);
        }
    }

    /// <summary>
    /// Render the prerendered level you created from PrerenderLevel()
    /// </summary>
    /// <param name="level"></param>
    public void RenderPrerenderedLevel(LDtkLevel level)
    {
        if (prerenderedLevels.TryGetValue(level.Identifier, out RenderedLevel prerenderedLevel))
        {
            for (int i = 0; i < prerenderedLevel.layers.Length; i++)
            {
                SpriteBatch.Draw(prerenderedLevel.layers[i], level.Position.ToVector2(), Color.White);
            }
        }
        else
        {
            throw new LevelNotFoundException($"No prerendered level with Identifier {level.Identifier} found.");
        }
    }

    /// <summary>
    /// Render the level directly without prerendering the layers alot slower than prerendering
    /// </summary>
    /// <param name="level"></param>
    public void RenderLevel(LDtkLevel level)
    {
        Texture2D[] layers = RenderLayers(level);

        for (int i = 0; i < layers.Length; i++)
        {
            SpriteBatch.Draw(layers[i], level.Position.ToVector2(), Color.White);
        }
    }

    /// <summary>
    /// Render intgrids by displaying the intgrid as solidcolor squares
    /// </summary>
    /// <param name="intGrid"></param>
    public void RenderIntGrid(LDtkIntGrid intGrid)
    {
        for (int x = 0; x < intGrid.Values.GetLength(0); x++)
        {
            for (int y = 0; y < intGrid.Values.GetLength(1); y++)
            {
                int cellValue = intGrid.Values[x, y];

                if (cellValue != 0)
                {
                    Color col = intGrid.GetColorFromValue(cellValue);

                    int spriteX = intGrid.WorldPosition.X + (x * intGrid.TileSize);
                    int spriteY = intGrid.WorldPosition.Y + (y * intGrid.TileSize);
                    SpriteBatch.Draw(pixel, new Vector2(spriteX, spriteY), null, col, 0, Vector2.Zero, new Vector2(intGrid.TileSize), SpriteEffects.None, 0);
                }
            }
        }
    }

    #endregion

    #region Entities

    /// <summary>
    /// Renders the entity with the tile it includes
    /// </summary>
    /// <param name="entity">The entity you want to render</param>
    /// <param name="texture">The spritesheet/texture for rendering the entity</param>
    public void RenderEntity<T>(T entity, Texture2D texture) where T : ILDtkEntity
    {
        SpriteBatch.Draw(texture, entity.Position, entity.Tile, Color.White, 0, entity.Pivot * entity.Size, 1, SpriteEffects.None, 0);
    }

    /// <summary>
    /// Renders the entity with the tile it includes
    /// </summary>
    /// <param name="entity">The entity you want to render</param>
    /// <param name="texture">The spritesheet/texture for rendering the entity</param>
    /// <param name="flipDirection">The direction to flip the entity when rendering</param>
    public void RenderEntity<T>(T entity, Texture2D texture, SpriteEffects flipDirection) where T : ILDtkEntity
    {
        SpriteBatch.Draw(texture, entity.Position, entity.Tile, Color.White, 0, entity.Pivot * entity.Size, 1, flipDirection, 0);
    }

    /// <summary>
    /// Renders the entity with the tile it includes
    /// </summary>
    /// <param name="entity">The entity you want to render</param>
    /// <param name="texture">The spritesheet/texture for rendering the entity</param>
    /// <param name="animationFrame">The current frame of animation. Is a very basic entity animation frames must be to the right of them and be the same size</param>
    public void RenderEntity<T>(T entity, Texture2D texture, int animationFrame) where T : ILDtkEntity
    {
        Rectangle animatedTile = entity.Tile;
        animatedTile.Offset(animatedTile.Width * animationFrame, 0);
        SpriteBatch.Draw(texture, entity.Position, animatedTile, Color.White, 0, entity.Pivot * entity.Size, 1, SpriteEffects.None, 0);
    }

    /// <summary>
    /// Renders the entity with the tile it includes
    /// </summary>
    /// <param name="entity">The entity you want to render</param>
    /// <param name="texture">The spritesheet/texture for rendering the entity</param>
    /// <param name="flipDirection">The direction to flip the entity when rendering</param>
    /// <param name="animationFrame">The current frame of animation. Is a very basic entity animation frames must be to the right of them and be the same size</param>
    public void RenderEntity<T>(T entity, Texture2D texture, SpriteEffects flipDirection, int animationFrame) where T : ILDtkEntity
    {
        Rectangle animatedTile = entity.Tile;
        animatedTile.Offset(animatedTile.Width * animationFrame, 0);
        SpriteBatch.Draw(texture, entity.Position, animatedTile, Color.White, 0, entity.Pivot * entity.Size, 1, flipDirection, 0);
    }

    #endregion

    private struct RenderedLevel
    {
        public Texture2D[] layers;
    }
}
