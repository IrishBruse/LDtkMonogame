using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LDtk.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LDtk
{
    /// <summary>
    /// The main class for loading .ldtk and .ldtkl files
    /// </summary>
    public class World
    {
        private Level[] levels;
        private string jsonFilePath;
        private LDtkJson json;
        private string projectFolder;
        private readonly SpriteBatch spriteBatch;
        private readonly ContentManager Content;
        private readonly GraphicsDevice GraphicsDevice;

        /// <summary>
        /// <para>Load the LDtk json file.</para>
        /// </summary>
        /// <param name="spriteBatch">Monogame's <see cref="SpriteBatch"/></param>
        /// <param name="ldtkFile">The path to the .ldtk file</param>
        public World(SpriteBatch spriteBatch, string ldtkFile)
        {
            this.spriteBatch = spriteBatch;
            GraphicsDevice = spriteBatch.GraphicsDevice;
            ReloadProject(ldtkFile);
        }

        /// <summary>
        /// <para>Load the LDtk json file.</para>
        /// </summary>
        /// <param name="spriteBatch">Monogame's <see cref="SpriteBatch"/></param>
        /// <param name="ldtkFile">The path to the .ldtk file</param>
        /// <param name="content">The <see cref="ContentManager"/> used to load <see cref="Texture2D"/> from content</param>
        public World(SpriteBatch spriteBatch, string ldtkFile, ContentManager content)
        {
            this.spriteBatch = spriteBatch;
            this.Content = content;
            GraphicsDevice = spriteBatch.GraphicsDevice;
            ReloadProject(ldtkFile);
        }

        /// <summary>
        /// <para>Reload the LDtk json file.</para>
        /// <para><c>Warning</c> make sure to rerender your <see cref="Level"/>'s.</para>
        /// </summary>
        void ReloadProject(string ldtkFile = null)
        {
            if (ldtkFile != null)
            {
                jsonFilePath = ldtkFile;
            }

            // Throw error if file not found
            if (File.Exists(jsonFilePath) == false)
            {
                throw new FileNotFoundException("Ldtk project File not found " + ldtkFile);
            }

            json = LDtkJson.FromJson(File.ReadAllText(jsonFilePath));
            projectFolder = Path.GetDirectoryName(Path.GetFullPath(jsonFilePath));
            levels = new Level[json.Levels.Length];
        }


        // Level Handling

        /// <summary>
        /// Gets the level from the current world
        /// </summary>
        /// <param name="uid">Uid of level</param>
        /// <returns>Level</returns>
        public Level GetLevel(long uid)
        {
            for (int i = 0; i < levels.Length; i++)
            {
                if (levels[i]?.Uid == uid)
                {
                    return levels[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the level from the current world
        /// </summary>
        /// <param name="identifier">Identifier of the level to get</param>
        /// <returns>Level</returns>
        public Level GetLevel(string identifier)
        {
            for (int i = 0; i < levels.Length; i++)
            {
                if (levels[i]?.Identifier == identifier)
                {
                    return levels[i];
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

        private void LoadLevel(ref Level level, LDtkLevel jsonLevel)
        {
            if (json.ExternalLevels == true)
            {
                string path = Path.Combine(projectFolder, jsonLevel.ExternalRelPath);
                jsonLevel = Newtonsoft.Json.JsonConvert.DeserializeObject<LDtkLevel>(File.ReadAllText(path));
            }

            LayerInstance[] jsonLayerInstances = jsonLevel.LayerInstances;

            level = new Level();

            // Set the identifier
            level.owner = this;

            // Set the identifier
            level.Identifier = jsonLevel.Identifier;

            // Cache the Background Color/Clear Color
            level.BgColor = Utility.ConvertStringToColor(jsonLevel.BgColor);

            // Set the world position
            level.Position = new Vector2(jsonLevel.WorldX, jsonLevel.WorldY);

            // Set the world size
            level.Size = new Vector2(jsonLevel.PxWid, jsonLevel.PxHei);

            // Set the uid
            level.Uid = jsonLevel.Uid;

            // Set the world position
            var neighbours = jsonLevel.Neighbours;
            level.Neighbours = (from neighour in neighbours select neighour.LevelUid).ToArray();

            LoadBackgroundLayer(ref level, jsonLevel, jsonLayerInstances);
            LoadAllLayers(ref level, jsonLayerInstances);
        }


        // Layer Handling

        private void LoadBackgroundLayer(ref Level level, LDtkLevel jsonLevel, LayerInstance[] jsonLayerInstances)
        {
            Texture2D texture;
            // Render background as if it was a layer
            if (jsonLevel.BgRelPath != null)
            {
                level.Layers = new RenderTarget2D[jsonLayerInstances.Length + 1];

                level.Layers[jsonLayerInstances.Length] = new RenderTarget2D(GraphicsDevice, (int)(jsonLayerInstances[0].CWid * jsonLayerInstances[0].GridSize), (int)(jsonLayerInstances[0].CHei * jsonLayerInstances[0].GridSize), false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
                GraphicsDevice.SetRenderTarget(level.Layers[jsonLayerInstances.Length]);

                texture = Texture2D.FromFile(GraphicsDevice, Path.Combine(projectFolder, jsonLevel.BgRelPath));

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

        private void LoadAllLayers(ref Level level, LayerInstance[] jsonLayerInstances)
        {
            Texture2D texture;

            // Process intgrids
            List<IntGrid> intGrids = new List<IntGrid>();

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
                        texture = Texture2D.FromFile(GraphicsDevice, Path.Combine(projectFolder, jsonLayer.TilesetRelPath));
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
                        IntGrid grid = new IntGrid();

                        grid.grid = new long[jsonLayer.CWid, jsonLayer.CHei];
                        grid.identifier = jsonLayer.Identifier;
                        grid.tileSize = (int)jsonLayer.GridSize;

                        for (int x = 0; x < jsonLayer.CWid; x++)
                        {
                            for (int y = 0; y < jsonLayer.CHei; y++)
                            {
                                grid.grid[x, y] = -1;
                            }
                        }

                        for (int j = 0; j < jsonLayer.IntGrid.Length; j++)
                        {
                            int y = (int)(jsonLayer.IntGrid[j].CoordId / jsonLayer.CWid);
                            int x = (int)(jsonLayer.IntGrid[j].CoordId - y * jsonLayer.CWid);
                            grid.grid[x, y] = jsonLayer.IntGrid[j].V;
                        }

                        intGrids.Add(grid);
                    }

                    // Render all renderable layers
                    spriteBatch.Begin(blendState: BlendState.NonPremultiplied, samplerState: SamplerState.PointClamp);
                    {
                        RenderLayer(jsonLayer, texture);
                    }
                    spriteBatch.End();
                }
            }

            level.intGrids = intGrids.ToArray();

            GraphicsDevice.SetRenderTarget(null);
        }

        private void RenderBackgroundAsLayer(LDtkLevel jsonLevel, Texture2D texture)
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
            foreach (var tile in gridTiles.Where(tile => jsonLayer.TilesetDefUid.HasValue))
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
                        var texture = Content.Load<Texture2D>(file);
                        texture.Name = Path.GetFileName(json.Defs.Tilesets[i].RelPath);
                        return texture;
                    }
                    else
                    {
                        var texture = Texture2D.FromFile(GraphicsDevice, Path.Combine(projectFolder, json.Defs.Tilesets[i].RelPath));
                        texture.Name = Path.GetFileName(json.Defs.Tilesets[i].RelPath);
                        return texture;
                    }
                }
            }

            throw new UidException("GetTilesetTextureFromUid(" + uid + ")");
        }
    }
}