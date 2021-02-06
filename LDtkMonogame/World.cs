using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.Xna.Framework;
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
        private readonly GraphicsDevice GraphicsDevice;

        /// <summary>
        /// <para>Load the LDtk json file.</para>
        /// Throws <see cref="FileNotFoundException"/> if an invalid file path is given
        /// </summary>
        /// <param name="spriteBatch">Monogames <see cref="SpriteBatch"/></param>
        /// <param name="ldtkFile">The path to the .ldtk file</param>
        public World(SpriteBatch spriteBatch, string ldtkFile)
        {
            this.spriteBatch = spriteBatch;
            GraphicsDevice = spriteBatch.GraphicsDevice;
            ReloadProject(ldtkFile);
        }

        /// <summary>
        /// <para>Reload the LDtk json file.</para>
        /// <para><c>Warning</c> make sure to rerender your <see cref="Level"/>'s.</para>
        /// Throws <see cref="FileNotFoundException"/> if an invalid file path is given
        /// </summary>
        public void ReloadProject(string ldtkFile = null)
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

        /// <summary>
        /// This will load and render out the level
        /// DO not call this inside of a spritebatchBegin/End
        /// </summary>
        /// <param name="index">The id of the level</param>
        public void Load(long index)
        {
            LDtkLevel jsonLevel;

            if (levels[index].loaded == true)
            {
                return;
            }

            levels[index].loaded = true;

            if (json.ExternalLevels == true)
            {
                string path = Path.Combine(projectFolder, json.Levels[index].ExternalRelPath);
                jsonLevel = Newtonsoft.Json.JsonConvert.DeserializeObject<LDtkLevel>(File.ReadAllText(path));
            }
            else
            {
                jsonLevel = json.Levels[index];
            }

            LayerInstance[] jsonLayerInstances = jsonLevel.LayerInstances;

            // Set the identifier
            levels[index].owner = this;

            // Set the identifier
            levels[index].Identifier = jsonLevel.Identifier;

            // Cache the Background Color/Clear Color
            levels[index].BgColor = Utility.ConvertStringToColor(jsonLevel.BgColor);

            // Set the world position
            levels[index].WorldPosition = new Vector2(jsonLevel.WorldX, jsonLevel.WorldY);

            // Set the uid
            levels[index].Uid = jsonLevel.Uid;

            // Set the world position
            var neighbours = jsonLevel.Neighbours;
            levels[index].Neighbours = (from neighour in neighbours select neighour.LevelUid).ToArray();

            LoadBackgroundLayer(index, GraphicsDevice, jsonLayerInstances, jsonLevel);
            LoadAllLayers(index, GraphicsDevice, jsonLayerInstances);
        }

        private void LoadBackgroundLayer(long levelId, GraphicsDevice GraphicsDevice, LayerInstance[] jsonLayerInstances, LDtkLevel jsonLevel)
        {
            Texture2D texture;
            // Render background as if it was a layer
            if (jsonLevel.BgRelPath != null)
            {
                levels[levelId].Layers = new RenderTarget2D[jsonLayerInstances.Length + 1];

                levels[levelId].Layers[jsonLayerInstances.Length] = new RenderTarget2D(GraphicsDevice, (int)(jsonLayerInstances[0].CWid * jsonLayerInstances[0].GridSize), (int)(jsonLayerInstances[0].CHei * jsonLayerInstances[0].GridSize), false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
                GraphicsDevice.SetRenderTarget(levels[levelId].Layers[jsonLayerInstances.Length]);

                texture = Texture2D.FromFile(GraphicsDevice, Path.Combine(projectFolder, jsonLevel.BgRelPath));

                spriteBatch.Begin(blendState: BlendState.NonPremultiplied, samplerState: SamplerState.PointClamp);
                {
                    RenderBackgroundAsLayer(levelId, texture);
                }
                spriteBatch.End();
            }
            else
            {
                levels[levelId].Layers = new RenderTarget2D[jsonLayerInstances.Length];
            }
        }

        private void LoadAllLayers(long index, GraphicsDevice GraphicsDevice, LayerInstance[] jsonLayerInstances)
        {
            Texture2D texture;

            // Process intgrids
            List<IntGrid> intGrids = new List<IntGrid>();

            // Render Tile, Auto and Int grid layers
            for (int i = jsonLayerInstances.Length - 1; i >= 0; i--)
            {
                LayerInstance jsonLayer = jsonLayerInstances[i];

                levels[index].Layers[i] = new RenderTarget2D(GraphicsDevice, (int)(jsonLayer.CWid * jsonLayer.GridSize), (int)(jsonLayer.CHei * jsonLayer.GridSize), false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
                GraphicsDevice.SetRenderTarget(levels[index].Layers[i]);

                if (jsonLayer.TilesetRelPath != null)
                {
                    texture = Texture2D.FromFile(GraphicsDevice, Path.Combine(projectFolder, jsonLayer.TilesetRelPath));
                }
                else
                {
                    // Create single pixel texture
                    texture = new Texture2D(GraphicsDevice, 1, 1);
                    texture.SetData(new byte[] { 0xFF, 0x00, 0xFF, 0xFF });
                }

                if (jsonLayer.Type == LayerType.Entities)
                {
                    levels[index].entities = jsonLayer.EntityInstances;
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
                        RenderLayer(index, jsonLayer, texture);
                    }
                    spriteBatch.End();
                }
            }

            levels[index].intGrids = intGrids.ToArray();

            GraphicsDevice.SetRenderTarget(null);
        }

        private void RenderBackgroundAsLayer(long index, Texture2D texture)
        {
            long[] topleft = json.Levels[index].BgPos.TopLeftPx;
            double[] cropRect = json.Levels[index].BgPos.CropRect;
            double[] scaleBG = json.Levels[index].BgPos.Scale;
            Vector2 pos = new Vector2(topleft[0], topleft[1]);
            Rectangle rect = new Rectangle((int)cropRect[0], (int)cropRect[1], (int)cropRect[2], (int)cropRect[3]);
            Vector2 scale = new Vector2((float)scaleBG[0], (float)scaleBG[1]);

            spriteBatch.Draw(texture, pos, rect, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        private void RenderLayer(long index, LayerInstance jsonLayer, Texture2D texture)
        {
            switch (jsonLayer.Type)
            {
                case LayerType.Tiles:
                    foreach (TileInstance tile in jsonLayer.GridTiles)
                    {
                        if (jsonLayer.TilesetDefUid.HasValue)
                        {
                            Vector2 position = new Vector2((int)(tile.Px[0] + jsonLayer.PxTotalOffsetX), (int)(tile.Px[1] + jsonLayer.PxTotalOffsetY));
                            Rectangle rect = new Rectangle((int)tile.Src[0], (int)tile.Src[1], (int)jsonLayer.GridSize, (int)jsonLayer.GridSize);
                            SpriteEffects mirror = (SpriteEffects)tile.F;

                            spriteBatch.Draw(texture, position, rect, Color.White, 0, Vector2.Zero, 1f, mirror, 0);
                        }
                    }
                    break;

                case LayerType.AutoLayer:
                case LayerType.IntGrid:
                    if (jsonLayer.AutoLayerTiles.Length > 0)
                    {
                        foreach (TileInstance tile in jsonLayer.AutoLayerTiles)
                        {
                            if (jsonLayer.TilesetDefUid.HasValue)
                            {
                                Vector2 position = new Vector2((int)(tile.Px[0] + jsonLayer.PxTotalOffsetX), (int)(tile.Px[1] + jsonLayer.PxTotalOffsetY));
                                Rectangle rect = new Rectangle((int)tile.Src[0], (int)tile.Src[1], (int)jsonLayer.GridSize, (int)jsonLayer.GridSize);
                                SpriteEffects mirror = (SpriteEffects)tile.F;

                                spriteBatch.Draw(texture, position, rect, Color.White, 0, Vector2.Zero, 1f, mirror, 0);
                            }
                        }
                    }
                    break;

                default: throw new NotImplementedException(jsonLayer.Type);
            }
        }

        /// <summary>
        /// Gets the level from the current world
        /// <para>Throws <see cref="Exception"/></para>
        /// </summary>
        /// <param name="uid">Uid of level</param>
        /// <returns>Level</returns>
        public Level GetLevel(long uid)
        {
            for (int i = 0; i < json.Levels.Length; i++)
            {
                if (levels[i].loaded)
                {
                    if (levels[i].Uid == uid)
                    {
                        return levels[i];
                    }
                }
                else
                {
                    if (json.Levels[i].Uid == uid)
                    {
                        Load(i);

                        if (levels[i].Uid == uid)
                        {
                            return levels[i];
                        }
                    }
                }
            }

            throw new Exception(uid + " Level not found Exception!");
        }

        /// <summary>
        /// Gets the level from the current world
        /// <para>Throws <see cref="Exception"/></para>
        /// </summary>
        /// <param name="identifier">Identifier of the level to get</param>
        /// <returns>A level</returns>
        public Level GetLevel(string identifier)
        {
            for (int i = 0; i < json.Levels.Length; i++)
            {
                if (levels[i].loaded)
                {
                    if (levels[i].Identifier == identifier)
                    {
                        return levels[i];
                    }
                }
                else
                {
                    if (json.Levels[i].Identifier == identifier)
                    {
                        Load(i);

                        if (levels[i].Identifier == identifier)
                        {
                            return levels[i];
                        }
                    }
                }
            }

            throw new Exception(identifier + " Level not found Exception!");
        }

        internal EntityDefinition GetEntityDefinitionFromUid(long uid)
        {
            for (int i = 0; i < json.Defs.Entities.Length; i++)
            {
                if (json.Defs.Entities[i].Uid == uid)
                {
                    return json.Defs.Entities[i];
                }
            }

            throw new Exception(uid + " is not a entity Definition!");
        }

        internal LayerDefinition GetLayerDefinitionFromUid(long uid)
        {
            for (int i = 0; i < json.Defs.Layers.Length; i++)
            {
                if (json.Defs.Layers[i].Uid == uid)
                {
                    return json.Defs.Layers[i];
                }
            }

            throw new Exception(uid + " is not a layer Definition!");
        }

        internal TilesetDefinition GetTilesetDefinitionFromUid(long uid)
        {
            for (int i = 0; i < json.Defs.Tilesets.Length; i++)
            {
                if (json.Defs.Tilesets[i].Uid == uid)
                {
                    return json.Defs.Tilesets[i];
                }
            }

            throw new Exception(uid + " is not a tileset Definition!");
        }

        internal Texture2D GetTilesetTextureFromUid(long uid)
        {
            for (int i = 0; i < json.Defs.Tilesets.Length; i++)
            {
                if (json.Defs.Tilesets[i].Uid == uid)
                {
                    var texture = Texture2D.FromFile(GraphicsDevice, Path.GetFullPath(json.Defs.Tilesets[i].RelPath, projectFolder));
                    texture.Name = Path.GetFileName(json.Defs.Tilesets[i].RelPath);
                    return texture;
                }
            }

            throw new Exception(uid + " is not a tileset Definition!");
        }
    }
}