using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LDtk.Renderer
{
    /// <summary>
    /// Renderer for the ldtkWorld and ldtkLevel
    /// </summary>
    public class LDtkRenderer
    {
        readonly Dictionary<string, RenderedLevel> prerenderedLevels = new Dictionary<string, RenderedLevel>();
        readonly SpriteBatch spriteBatch;
        readonly GraphicsDevice graphicsDevice;
        readonly ContentManager content;
        readonly Texture2D pixel;


        /// <summary>
        /// This is used to intizialize the renderer for use with direct file loading
        /// </summary>
        /// <param name="spriteBatch"></param>
        public LDtkRenderer(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            graphicsDevice = spriteBatch.GraphicsDevice;

            pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });
        }

        /// <summary>
        /// This is used to intizialize the renderer for use with content Pipeline
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="Content"></param>
        public LDtkRenderer(SpriteBatch spriteBatch, ContentManager Content)
        {
            content = Content;
            this.spriteBatch = spriteBatch;
            graphicsDevice = spriteBatch.GraphicsDevice;

            pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });
        }

        /// <summary>
        /// Prerender out the level to textures to optimize the rendering process
        /// </summary>
        /// <param name="level">The level to prerender</param>
        /// <exception cref="Exception">The level already has been prerendered</exception>
        public void PrerenderLevel(LDtkLevel level)
        {
            if (prerenderedLevels.ContainsKey(level.Identifier))
            {
#if DEBUG
                Console.WriteLine();
#else
                return;
#endif
            }

            RenderedLevel renderLevel = new RenderedLevel();

            List<RenderTarget2D> layers = new List<RenderTarget2D>();

            // Render Tile, Auto and Int grid layers
            for (int i = level.LayerInstances.Length - 1; i >= 0; i--)
            {
                LayerInstance layer = level.LayerInstances[i];
                if (layer._TilesetRelPath == null)
                {
                    continue;
                }

                Texture2D texture;
                if (content == null)
                {
                    texture = Texture2D.FromFile(graphicsDevice, layer._TilesetRelPath);
                }
                else
                {
                    string file = Path.ChangeExtension(layer._TilesetRelPath, null);
                    texture = content.Load<Texture2D>(file);
                }


                var renderTarget = new RenderTarget2D(graphicsDevice,
                    layer._CWid * layer._GridSize, layer._CHei * layer._GridSize,
                    false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);

                graphicsDevice.SetRenderTarget(renderTarget);

                layers.Add(renderTarget);

                spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                {
                    switch (layer._Type)
                    {
                        case LayerType.Tiles:
                            foreach (TileInstance tile in layer.GridTiles.Where(tile => layer._TilesetDefUid.HasValue))
                            {
                                Vector2 position = new Vector2(tile.Px.X + layer._PxTotalOffsetX, tile.Px.Y + layer._PxTotalOffsetY);
                                Rectangle rect = new Rectangle(tile.Src.X, tile.Src.Y, layer._GridSize, layer._GridSize);
                                SpriteEffects mirror = (SpriteEffects)tile.F;
                                spriteBatch.Draw(texture, position, rect, Color.White, 0, Vector2.Zero, 1f, mirror, 0);
                            }
                            break;

                        case LayerType.AutoLayer:
                        case LayerType.IntGrid:
                            if (layer.AutoLayerTiles.Length > 0)
                            {
                                foreach (TileInstance tile in layer.AutoLayerTiles.Where(tile => layer._TilesetDefUid.HasValue))
                                {
                                    Vector2 position = new Vector2(tile.Px.X + layer._PxTotalOffsetX, tile.Px.Y + layer._PxTotalOffsetY);
                                    Rectangle rect = new Rectangle(tile.Src.X, tile.Src.Y, layer._GridSize, layer._GridSize);
                                    SpriteEffects mirror = (SpriteEffects)tile.F;
                                    spriteBatch.Draw(texture, position, rect, Color.White, 0, Vector2.Zero, 1f, mirror, 0);
                                }
                            }
                            break;
                        case LayerType.Entities:
                            break;

                        default: throw new NotImplementedException(layer._Type.ToString());
                    }
                }
                spriteBatch.End();
            }

            renderLevel.layers = layers.ToArray();

            prerenderedLevels.Add(level.Identifier, renderLevel);
            graphicsDevice.SetRenderTarget(null);
        }

        /// <summary>
        /// The level that has been prerendered and you want to render now
        /// </summary>
        /// <param name="level"></param>
        public void RenderLevel(LDtkLevel level)
        {
            var prerenderedLevel = prerenderedLevels[level.Identifier];
            for (int i = 0; i < prerenderedLevel.layers.Length; i++)
            {
                spriteBatch.Draw(prerenderedLevel.layers[i], level.Position.ToVector2(), Color.White);
            }
        }

        /// <summary>
        /// Render intgrids by displaying the intgrid as solidcolor squared
        /// </summary>
        /// <param name="intGrid"></param>
        /// <param name="excludeValues"></param>
        public void RenderIntGrid(LDtkIntGrid intGrid, params int[] excludeValues)
        {
            for (int x = 0; x < intGrid.Values.GetLength(0); x++)
            {
                for (int y = 0; y < intGrid.Values.GetLength(1); y++)
                {
                    int cellValue = intGrid.Values[x, y];
                    bool skip = false;
                    for (int i = 0; i < excludeValues.Length; i++)
                    {
                        if (excludeValues[i] == cellValue)
                        {
                            skip = true;
                        }
                    }

                    if (skip == true)
                    {
                        continue;
                    }

                    if (cellValue != 0)
                    {
                        Color col = intGrid.GetColorFromValue(cellValue);
                        spriteBatch.Draw(pixel, new Rectangle(x * intGrid.TileSize, y * intGrid.TileSize, intGrid.TileSize, intGrid.TileSize), col);
                    }
                }
            }
        }

        struct RenderedLevel
        {
            public RenderTarget2D[] layers;
        }
    }
}
