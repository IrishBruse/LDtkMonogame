using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LDtk.Exceptions;
using LDtk.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LDtk
{
    /// <summary>
    /// The main class for loading .ldtk and .ldtkl files
    /// </summary>
    public class LDtkWorld
    {
        private readonly LDtkLevel[] levels;
        private readonly LDtkProject json;
#pragma warning disable CS1591

        public SpriteBatch spriteBatch;
        public GraphicsDevice GraphicsDevice;
#pragma warning restore CS1591
        private readonly ContentManager Content;

        /// <summary>
        /// Load the LDtk json file
        /// </summary>
        internal LDtkWorld(string jsonContent, ContentManager Content)
        {
            this.Content = Content;
            json = LDtkProject.FromJson(jsonContent);
            levels = new LDtkLevel[json.Levels.Length];
        }

        // Level Handling

        /// <summary>
        /// Gets the level from the current world
        /// </summary>
        /// <param name="uid">Uid of level</param>
        /// <returns>Level</returns>
        public LDtkLevel GetLevel(long uid)
        {
            return ParseLevel<LDtkLevel>("", uid);
        }

        /// <summary>
        /// Gets the level from the current world
        /// </summary>
        /// <param name="identifier">Identifier of the level to get</param>
        /// <returns>Level</returns>
        public LDtkLevel GetLevel(string identifier)
        {
            return ParseLevel<LDtkLevel>(identifier, -1);
        }

        /// <summary>
        /// Gets the level from the current world
        /// </summary>
        /// <param name="uid">Uid of level</param>
        /// <typeparam name="T">Your custom level with the added fields</typeparam>
        /// <returns>Level</returns>
        public T GetLevel<T>(long uid) where T : LDtkLevel, new()
        {
            return ParseLevel<T>("", uid);
        }

        /// <summary>
        /// Gets the level from the current world
        /// </summary>
        /// <param name="identifier">Identifier of the level to get</param>
        /// <returns>Level</returns>
        public T GetLevel<T>(string identifier) where T : LDtkLevel, new()
        {
            return ParseLevel<T>(identifier, -1);
        }

        private T ParseLevel<T>(string identifier, long uid) where T : LDtkLevel, new()
        {
            for (int i = 0; i < levels.Length; i++)
            {
                if (levels[i]?.Identifier == identifier || levels[i]?.Uid == uid)
                {
                    T level = new T
                    {
                        BgColor = levels[i].BgColor,
                        entities = levels[i].entities,
                        Identifier = levels[i].Identifier,
                        intGrids = levels[i].intGrids,
                        Layers = levels[i].Layers,
                        Neighbours = levels[i].Neighbours,
                        owner = levels[i].owner,
                        Position = levels[i].Position,
                        Size = levels[i].Size
                    };

                    for (int fieldIndex = 0; fieldIndex < json.Levels[i].FieldInstances.Length; fieldIndex++)
                    {
                        Parser.ParseField(level, json.Levels[i].FieldInstances[fieldIndex]);
                    }

                    return level;
                }
            }

            return null;
        }

        /// <summary>
        /// Prerenders the level for later drawing
        /// </summary>
        /// <param name="uid">Uid</param>
        public void LoadLevel(long uid)
        {
            for (int i = 0; i < json.Levels.Length; i++)
            {
                if (json.Levels[i].Uid == uid)
                {
                    if (levels[i] == null)
                    {
                        LoadLevel(ref levels[i], json.Levels[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Prerenders the level for later drawing
        /// </summary>
        /// <param name="identifier">Identifier</param>
        public void LoadLevel(string identifier)
        {
            for (int i = 0; i < json.Levels.Length; i++)
            {
                if (json.Levels[i].Identifier == identifier)
                {
                    if (levels[i] == null)
                    {
                        LoadLevel(ref levels[i], json.Levels[i]);
                    }
                }
            }
        }

        private void LoadLevel(ref LDtkLevel level, Level jsonLevel)
        {
            if (json.ExternalLevels)
            {
                string externalLevel = jsonLevel.ExternalRelPath[0..^6];
                jsonLevel = Content.Load<Level>(externalLevel);
            }

            LayerInstance[] jsonLayerInstances = jsonLevel.LayerInstances;

            level = new LDtkLevel
            {
                owner = this,

                // Set the identifier
                Identifier = jsonLevel.Identifier,

                // Cache the Background Color/Clear Color
                BgColor = Parser.ParseStringToColor(jsonLevel.BgColor),

                // Set the world position
                Position = new Vector2(jsonLevel.WorldX, jsonLevel.WorldY),

                // Set the world size
                Size = new Vector2(jsonLevel.PxWid, jsonLevel.PxHei),

                // Set the uid
                Uid = jsonLevel.Uid
            };

            // Set the world position
            NeighbourLevel[] neighbours = jsonLevel.Neighbours;
            level.Neighbours = (from neighour in neighbours select neighour.LevelUid).ToArray();

            LoadBackgroundLayer(ref level, jsonLevel, jsonLayerInstances);
            LoadAllLayers(ref level, jsonLayerInstances);
        }


        // Layer Handling
        private void LoadBackgroundLayer(ref LDtkLevel level, Level jsonLevel, LayerInstance[] jsonLayerInstances)
        {
            Texture2D texture;
            // Render background as if it was a layer
            if (jsonLevel.BgRelPath != null)
            {
                level.Layers = new RenderTarget2D[jsonLayerInstances.Length + 1];

                level.Layers[jsonLayerInstances.Length] = new RenderTarget2D(GraphicsDevice, (int)(jsonLayerInstances[0].CWid * jsonLayerInstances[0].GridSize), (int)(jsonLayerInstances[0].CHei * jsonLayerInstances[0].GridSize), false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
                GraphicsDevice.SetRenderTarget(level.Layers[jsonLayerInstances.Length]);

                texture = Texture2D.FromFile(GraphicsDevice, jsonLevel.BgRelPath);

                spriteBatch.Begin(blendState: BlendState.NonPremultiplied, samplerState: SamplerState.PointClamp);
                {
                    RenderBackgroundAsLayer(jsonLevel, texture);
                }
                spriteBatch.End();
            }
            else
            {
                level.Layers = new RenderTarget2D[jsonLayerInstances.Length];
            }
        }

        private void LoadAllLayers(ref LDtkLevel level, LayerInstance[] jsonLayerInstances)
        {
            Texture2D texture;

            // Process intgrids
            List<LDtkIntGrid> intGrids = new List<LDtkIntGrid>();

            // Render Tile, Auto and Int grid layers
            for (int i = jsonLayerInstances.Length - 1; i >= 0; i--)
            {
                LayerInstance jsonLayer = jsonLayerInstances[i];

                level.Layers[i] = new RenderTarget2D(GraphicsDevice, (int)(jsonLayer.CWid * jsonLayer.GridSize), (int)(jsonLayer.CHei * jsonLayer.GridSize), false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
                GraphicsDevice.SetRenderTarget(level.Layers[i]);

                if (jsonLayer.TilesetRelPath != null)
                {
                    if (Content != null)
                    {
                        string file = Path.ChangeExtension(jsonLayer.TilesetRelPath, null);
                        texture = Content.Load<Texture2D>(file);
                    }
                    else
                    {
                        texture = Texture2D.FromFile(GraphicsDevice, jsonLayer.TilesetRelPath);
                    }
                }
                else
                {
                    // Create single pixel texture
                    texture = new Texture2D(GraphicsDevice, 1, 1);
                    texture.SetData(new byte[] { 0xFF, 0x00, 0xFF, 0xFF });
                }

                if (jsonLayer.Type == LayerType.Entities)
                {
                    level.entities = jsonLayer.EntityInstances;
                }
                else
                {
                    if (jsonLayer.Type == LayerType.IntGrid)
                    {
                        LDtkIntGrid intGrid = new LDtkIntGrid
                        {
                            grid = new long[jsonLayer.CWid, jsonLayer.CHei],
                            identifier = jsonLayer.Identifier,
                            tileSize = (int)jsonLayer.GridSize
                        };

                        if (jsonLayer.IntGridCsv != null)
                        {
                            for (int j = 0; j < jsonLayer.IntGridCsv.Length; j++)
                            {
                                int y = (int)(j / jsonLayer.CWid);
                                int x = (int)(j - (y * jsonLayer.CWid));
                                intGrid.grid[x, y] = jsonLayer.IntGridCsv[j];
                            }
                        }
                        else
                        {
                            for (int x = 0; x < jsonLayer.CWid; x++)
                            {
                                for (int y = 0; y < jsonLayer.CHei; y++)
                                {
                                    intGrid.grid[x, y] = -1;
                                }
                            }

                            for (int j = 0; j < jsonLayer.IntGrid.Length; j++)
                            {
                                int y = (int)(jsonLayer.IntGrid[j].CoordId / jsonLayer.CWid);
                                int x = (int)(jsonLayer.IntGrid[j].CoordId - (y * jsonLayer.CWid));
                                intGrid.grid[x, y] = jsonLayer.IntGrid[j].V;
                            }
                        }
                        intGrids.Add(intGrid);
                    }

                    // Render all renderable layers
                    spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                    {
                        RenderLayer(jsonLayer, texture);
                    }
                    spriteBatch.End();
                }
            }

            level.intGrids = intGrids.ToArray();

            GraphicsDevice.SetRenderTarget(null);
        }

        private void RenderBackgroundAsLayer(Level jsonLevel, Texture2D texture)
        {
            long[] topleft = jsonLevel.BgPos.TopLeftPx;
            double[] cropRect = jsonLevel.BgPos.CropRect;
            double[] scaleBG = jsonLevel.BgPos.Scale;
            Vector2 pos = new Vector2(topleft[0], topleft[1]);
            Rectangle rect = new Rectangle((int)cropRect[0], (int)cropRect[1], (int)cropRect[2], (int)cropRect[3]);
            Vector2 scale = new Vector2((float)scaleBG[0], (float)scaleBG[1]);

            spriteBatch.Draw(texture, pos, rect, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        private void RenderLayer(LayerInstance jsonLayer, Texture2D texture)
        {
            switch (jsonLayer.Type)
            {
                case LayerType.Tiles:
                    RenderTilesInLayer(jsonLayer, texture, jsonLayer.GridTiles);
                    break;

                case LayerType.AutoLayer:
                case LayerType.IntGrid:
                    if (jsonLayer.AutoLayerTiles.Length > 0)
                    {
                        RenderTilesInLayer(jsonLayer, texture, jsonLayer.AutoLayerTiles);
                    }
                    break;

                default: throw new NotImplementedException(jsonLayer.Type);
            }
        }

        private void RenderTilesInLayer(LayerInstance jsonLayer, Texture2D texture, TileInstance[] gridTiles)
        {
            foreach (TileInstance tile in gridTiles.Where(tile => jsonLayer.TilesetDefUid.HasValue))
            {
                Vector2 position = new Vector2((int)(tile.Px[0] + jsonLayer.PxTotalOffsetX), (int)(tile.Px[1] + jsonLayer.PxTotalOffsetY));
                Rectangle rect = new Rectangle((int)tile.Src[0], (int)tile.Src[1], (int)jsonLayer.GridSize, (int)jsonLayer.GridSize);
                SpriteEffects mirror = (SpriteEffects)tile.F;
                spriteBatch.Draw(texture, position, rect, Color.White, 0, Vector2.Zero, 1f, mirror, 0);
            }
        }


        // Json Helper Functions

        internal EntityDefinition GetEntityDefinitionFromUid(long uid)
        {
            for (int i = 0; i < json.Defs.Entities.Length; i++)
            {
                if (json.Defs.Entities[i].Uid == uid)
                {
                    return json.Defs.Entities[i];
                }
            }

            throw new UidException("GetEntityDefinitionFromUid(" + uid + ")");
        }

        internal Texture2D GetTilesetTextureFromUid(long uid)
        {
            for (int i = 0; i < json.Defs.Tilesets.Length; i++)
            {
                if (json.Defs.Tilesets[i].Uid == uid)
                {
                    if (Content != null)
                    {
                        string file = Path.ChangeExtension(json.Defs.Tilesets[i].RelPath, null);
                        Texture2D texture = Content.Load<Texture2D>(file);
                        texture.Name = Path.GetFileName(json.Defs.Tilesets[i].RelPath);
                        return texture;
                    }
                    else
                    {
                        Texture2D texture = Texture2D.FromFile(GraphicsDevice, json.Defs.Tilesets[i].RelPath);
                        texture.Name = Path.GetFileName(json.Defs.Tilesets[i].RelPath);
                        return texture;
                    }
                }
            }

            throw new UidException("GetTilesetTextureFromUid(" + uid + ")");
        }

        /// <summary>
        /// Layer Types
        /// </summary>
        internal static class LayerType
        {
            public const string Tiles = "Tiles";
            public const string IntGrid = "IntGrid";
            public const string AutoLayer = "AutoLayer";
            public const string Entities = "Entities";
        }
    }
}