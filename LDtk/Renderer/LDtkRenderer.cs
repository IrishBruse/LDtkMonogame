using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LDtk.Exceptions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LDtk.Renderer
{
    /// <summary>
    /// Renderer for the ldtkWorld, ldtkLevel, intgrids and entities.
    /// This can all be done in your own class if you want to reimplement it and customize it differently
    /// </summary>
    public class LDtkRenderer
    {
        static Texture2D pixel;

        readonly Dictionary<string, RenderedLevel> prerenderedLevels = new Dictionary<string, RenderedLevel>();
        readonly SpriteBatch spriteBatch;
        readonly GraphicsDevice GraphicsDevice;
        readonly ContentManager Content;

        /// <summary>
        /// This is used to intizialize the renderer for use with direct file loading
        /// </summary>
        /// <param name="spriteBatch"></param>
        public LDtkRenderer(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            GraphicsDevice = spriteBatch.GraphicsDevice;

            if (pixel == null)
            {
                pixel = new Texture2D(GraphicsDevice, 1, 1);
                pixel.SetData(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });
            }
        }

        /// <summary>
        /// This is used to intizialize the renderer for use with content Pipeline
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="Content"></param>
        public LDtkRenderer(SpriteBatch spriteBatch, ContentManager Content) : this(spriteBatch)
        {
            this.Content = Content;
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

            RenderedLevel renderLevel = new RenderedLevel();

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            {
                renderLevel.layers = RenderLayers(level);
            }
            spriteBatch.End();

            prerenderedLevels.Add(level.Identifier, renderLevel);
            GraphicsDevice.SetRenderTarget(null);
        }

        private Texture2D[] RenderLayers(LDtkLevel level)
        {
            List<Texture2D> layers = new List<Texture2D>();

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
                var renderTarget = new RenderTarget2D(GraphicsDevice, width, height, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);

                GraphicsDevice.SetRenderTarget(renderTarget);
                layers.Add(renderTarget);


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
                }
            }

            return layers.ToArray();
        }

        private Texture2D RenderBackgroundToLayer(LDtkLevel level)
        {
            Texture2D texture = GetTexture(level, level.BgRelPath);

            var layer = new RenderTarget2D(GraphicsDevice, level.PxWid, level.PxHei, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);

            GraphicsDevice.SetRenderTarget(layer);
            {
                LevelBackgroundPosition bg = level._BgPos;
                Vector2 pos = bg.TopLeftPx.ToVector2();
                spriteBatch.Draw(texture, pos, bg.CropRect, Color.White, 0, Vector2.Zero, bg.Scale, SpriteEffects.None, 0);
            }
            GraphicsDevice.SetRenderTarget(null);

            return layer;
        }

        private Texture2D GetTexture(LDtkLevel level, string path)
        {
            if (Content == null)
            {
                return Texture2D.FromFile(GraphicsDevice, Path.Combine(level.parent.RootFolder, path));
            }
            else
            {
                string file = Path.ChangeExtension(path, null);
                return Content.Load<Texture2D>(file);
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
                    spriteBatch.Draw(prerenderedLevel.layers[i], level.Position.ToVector2(), Color.White);
                }
            }
            else
            {
                throw new LevelNotFoundException($"No prerendered level with Identifier {level.Identifier} found.");
            }
        }

        /// <summary>
        /// Render the level directly without prerendering the layers
        /// </summary>
        /// <param name="level"></param>
        public void RenderLevel(LDtkLevel level)
        {
            var layers = RenderLayers(level);

            for (int i = 0; i < layers.Length; i++)
            {
                spriteBatch.Draw(layers[i], level.Position.ToVector2(), Color.White);
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
                        spriteBatch.Draw(pixel, new Vector2(spriteX, spriteY), null, col, 0, Vector2.Zero, new Vector2(intGrid.TileSize), SpriteEffects.None, 0);
                    }
                }
            }
        }

        #endregion

        #region Entities

        /// <summary>
        /// Renders the entity with the tile it includes
        /// </summary>
        public void RenderEntity<T>(T entity, Texture2D texture) where T : ILDtkEntity
        {
            spriteBatch.Draw(texture, entity.Position, entity.Tile, Color.White, 0, entity.Pivot * entity.Size, 1, SpriteEffects.None, 0);
        }

        #endregion

        struct RenderedLevel
        {
            public Texture2D[] layers;
        }
    }
}
