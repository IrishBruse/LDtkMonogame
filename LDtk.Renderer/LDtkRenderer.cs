using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDtk.Renderer
{
    public class LDtkRenderer
    {
        readonly Dictionary<string, RenderedLevel> prerenderedLevel = new Dictionary<string, RenderedLevel>();
        readonly SpriteBatch spriteBatch;
        readonly GraphicsDevice graphicsDevice;

        public LDtkRenderer(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            graphicsDevice = spriteBatch.GraphicsDevice;
        }

        public void PrerenderLevel(LDtkLevel level)
        {
            RenderedLevel renderLevel = new RenderedLevel
            {
                layers = new RenderTarget2D[level.LayerInstances.Length]
            };

            Texture2D tilesetTexture;
            tilesetTexture = new Texture2D(graphicsDevice, 1, 1);
            tilesetTexture.SetData(new byte[] { 0xFF, 0x00, 0xFF, 0xFF });

            // Render Tile, Auto and Int grid layers
            for (int i = level.LayerInstances.Length - 1; i >= 0; i--)
            {
                LayerInstance layer = level.LayerInstances[i];

                renderLevel.layers[i] = new RenderTarget2D(graphicsDevice,
                    layer._CWid * layer._GridSize, layer._CHei * layer._GridSize,
                    false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);

                graphicsDevice.SetRenderTarget(renderLevel.layers[i]);

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
                                spriteBatch.Draw(tilesetTexture, position, rect, Color.White, 0, Vector2.Zero, 1f, mirror, 0);
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
                                    spriteBatch.Draw(tilesetTexture, position, rect, Color.White, 0, Vector2.Zero, 1f, mirror, 0);
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

            prerenderedLevel.Add(level.Identifier, renderLevel);
            graphicsDevice.SetRenderTarget(null);
        }

        public void RenderLevel(LDtkLevel level1)
        {
            for (int i = 0; i < prerenderedLevel[level1.Identifier].layers.Length; i++)
            {
                spriteBatch.Draw(prerenderedLevel[level1.Identifier].layers[i], level1.Position.ToVector2(), Color.White);
            }
        }

        struct RenderedLevel
        {
            public RenderTarget2D[] layers;
        }
    }
}
