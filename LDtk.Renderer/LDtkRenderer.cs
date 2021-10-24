using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LDtk.Renderer
{
    public class LDtkRenderer
    {
        readonly Dictionary<string, RenderedLevel> prerenderedLevel = new Dictionary<string, RenderedLevel>();
        readonly SpriteBatch spriteBatch;
        readonly GraphicsDevice graphicsDevice;
        readonly ContentManager content;

        public LDtkRenderer(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            graphicsDevice = spriteBatch.GraphicsDevice;
        }

        public LDtkRenderer(SpriteBatch spriteBatch, ContentManager Content)
        {
            content = Content;
            this.spriteBatch = spriteBatch;
            graphicsDevice = spriteBatch.GraphicsDevice;
        }

        public void PrerenderLevel(LDtkLevel level)
        {
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

            prerenderedLevel.Add(level.Identifier, renderLevel);
            graphicsDevice.SetRenderTarget(null);
        }

        public void RenderLevel(LDtkLevel level)
        {
            for (int i = 0; i < prerenderedLevel[level.Identifier].layers.Length; i++)
            {
                spriteBatch.Draw(prerenderedLevel[level.Identifier].layers[i], level.Position.ToVector2(), Color.White);
            }
        }

        struct RenderedLevel
        {
            public RenderTarget2D[] layers;
        }
    }
}
