using System;
using System.IO;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDtk
{
    /// <summary>
    /// The main class for loading .ldtk and .ldtkl files
    /// </summary>
    public class Project
    {
        public int LevelsCount => levels.Length;

        private Level[] levels;

        private string jsonFilePath;
        private LDtkJson json;

        private string absoluteProjectFolder;
        private readonly SpriteBatch spriteBatch;

        /// <summary>
        /// <para>Load the LDtk json file.</para>
        /// Throws <see cref="FileNotFoundException"/> if an invalid file path is given
        /// </summary>
        /// <param name="spriteBatch">Monogames <see cref="SpriteBatch"/></param>
        /// <param name="ldtkFile">The path to the .ldtk file</param>
        public Project(SpriteBatch spriteBatch, string ldtkFile)
        {
            this.spriteBatch = spriteBatch;
            ReloadProject(ldtkFile);
        }

        /// <summary>
        /// <para>Reload the LDtk json file.</para>
        /// <para><c>Warning</c> make sure to rerender your <see cref="Level"/>'s.</para>
        /// Throws <see cref="FileNotFoundException"/> if an invalid file path is given
        /// </summary>
        public void ReloadProject(string ldtkFile = null)
        {
            if(ldtkFile != null)
            {
                jsonFilePath = ldtkFile;
            }

            // Throw error if file not found
            if(File.Exists(jsonFilePath) == false)
            {
                throw new FileNotFoundException("Ldtk project File not found " + ldtkFile);
            }

            json = LDtkJson.FromJson(File.ReadAllText(jsonFilePath));
            absoluteProjectFolder = Path.GetDirectoryName(Path.GetFullPath(jsonFilePath));
            levels = new Level[json.Levels.Length];
        }

        /// <summary>
        /// This will load and render out the level
        /// </summary>
        /// <param name="index">The id of the level</param>
        public void Load(long index)
        {
            GraphicsDevice GraphicsDevice = spriteBatch.GraphicsDevice;
            LDtkLevel jsonLevel;

            if(levels[index].Loaded == true)
            {
                return;
            }

            levels[index].Loaded = true;

            if(json.ExternalLevels == true)
            {
                string path = Path.Combine(absoluteProjectFolder, json.Levels[index].ExternalRelPath);
                jsonLevel = Newtonsoft.Json.JsonConvert.DeserializeObject<LDtkLevel>(File.ReadAllText(path));
            }
            else
            {
                jsonLevel = json.Levels[index];
            }

            LayerInstance[] jsonLayerInstances = jsonLevel.LayerInstances;

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
            if(jsonLevel.BgRelPath != null)
            {
                levels[levelId].Layers = new RenderTarget2D[jsonLayerInstances.Length + 1];

                levels[levelId].Layers[jsonLayerInstances.Length] = new RenderTarget2D(GraphicsDevice, (int)(jsonLayerInstances[0].CWid * jsonLayerInstances[0].GridSize), (int)(jsonLayerInstances[0].CHei * jsonLayerInstances[0].GridSize), false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
                GraphicsDevice.SetRenderTarget(levels[levelId].Layers[jsonLayerInstances.Length]);

                texture = Texture2D.FromFile(GraphicsDevice, Path.Combine(absoluteProjectFolder, jsonLevel.BgRelPath));

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

        private void LoadAllLayers(long levelId, GraphicsDevice GraphicsDevice, LayerInstance[] jsonLayerInstances)
        {
            Texture2D texture;

            // Render Tile, Auto and Int grid layers
            for(int i = jsonLayerInstances.Length - 1; i >= 0; i--)
            {
                LayerInstance jsonLayer = jsonLayerInstances[i];

                levels[levelId].Layers[i] = new RenderTarget2D(GraphicsDevice, (int)(jsonLayer.CWid * jsonLayer.GridSize), (int)(jsonLayer.CHei * jsonLayer.GridSize), false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
                GraphicsDevice.SetRenderTarget(levels[levelId].Layers[i]);

                if(jsonLayer.TilesetRelPath != null)
                {
                    texture = Texture2D.FromFile(GraphicsDevice, Path.Combine(absoluteProjectFolder, jsonLayer.TilesetRelPath));
                }
                else
                {
                    // Create single pixel texture
                    texture = new Texture2D(GraphicsDevice, 1, 1);
                    texture.SetData(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });
                }

                if(jsonLayer.Type == LayerTypeEnum.Entities)
                {
                    Entity[] entities = new Entity[jsonLayer.EntityInstances.Length];

                    for(int j = 0; j < jsonLayer.EntityInstances.Length; j++)
                    {
                        EntityInstance entity = jsonLayer.EntityInstances[j];
                        EntityDefinition def = GetEntityDefinitionFromUid(entity.DefUid);
                        Vector2 position = new Vector2((int)(entity.Px[0] + jsonLayer.PxTotalOffsetX), (int)(entity.Px[1] + jsonLayer.PxTotalOffsetY));
                        Vector2 size = new Vector2(def.Width, def.Height);
                        Color color = Utility.ConvertStringToColor(def.Color);
                        //entity.Pivot
                        //entities[j]
                    }

                    levels[levelId].Entities = entities;
                }
                else
                {
                    // Render all renderable layers
                    spriteBatch.Begin(blendState: BlendState.NonPremultiplied, samplerState: SamplerState.PointClamp);
                    {
                        RenderLayer(jsonLayer, texture);
                    }
                    spriteBatch.End();
                }
            }

            GraphicsDevice.SetRenderTarget(null);

        }

        private void RenderBackgroundAsLayer(long levelId, Texture2D texture)
        {
            long[] topleft = json.Levels[levelId].BgPos.TopLeftPx;
            double[] cropRect = json.Levels[levelId].BgPos.CropRect;
            double[] scaleBG = json.Levels[levelId].BgPos.Scale;
            Vector2 pos = new Vector2(topleft[0], topleft[1]);
            Rectangle rect = new Rectangle((int)cropRect[0], (int)cropRect[1], (int)cropRect[2], (int)cropRect[3]);
            Vector2 scale = new Vector2((float)scaleBG[0], (float)scaleBG[1]);

            spriteBatch.Draw(texture, pos, rect, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        private void RenderLayer(LayerInstance jsonLayer, Texture2D texture)
        {
            switch(jsonLayer.Type)
            {
                case LayerTypeEnum.Tiles:
                foreach(TileInstance tile in jsonLayer.GridTiles)
                {
                    if(jsonLayer.TilesetDefUid.HasValue)
                    {
                        Vector2 position = new Vector2((int)(tile.Px[0] + jsonLayer.PxTotalOffsetX), (int)(tile.Px[1] + jsonLayer.PxTotalOffsetY));
                        Rectangle rect = new Rectangle((int)tile.Src[0], (int)tile.Src[1], (int)jsonLayer.GridSize, (int)jsonLayer.GridSize);
                        SpriteEffects mirror = (SpriteEffects)tile.F;

                        spriteBatch.Draw(texture, position, rect, Color.White, 0, Vector2.Zero, 1f, mirror, 0);
                    }
                }
                break;

                case LayerTypeEnum.IntGrid:
                case LayerTypeEnum.AutoLayer:
                if(jsonLayer.AutoLayerTiles.Length > 0)
                {
                    foreach(TileInstance tile in jsonLayer.AutoLayerTiles)
                    {
                        if(jsonLayer.TilesetDefUid.HasValue)
                        {
                            Vector2 position = new Vector2((int)(tile.Px[0] + jsonLayer.PxTotalOffsetX), (int)(tile.Px[1] + jsonLayer.PxTotalOffsetY));
                            Rectangle rect = new Rectangle((int)tile.Src[0], (int)tile.Src[1], (int)jsonLayer.GridSize, (int)jsonLayer.GridSize);
                            SpriteEffects mirror = (SpriteEffects)tile.F;

                            spriteBatch.Draw(texture, position, rect, Color.White, 0, Vector2.Zero, 1f, mirror, 0);
                        }
                    }
                }
                //else
                //{
                //    LayerDefinition layerInstance = GetLayerDefinitionFromUid(jsonLayer.LayerDefUid);

                //    foreach(IntGridValueInstance tile in jsonLayer.IntGrid)
                //    {
                //        long x = tile.CoordId % jsonLayer.CWid;
                //        long y = tile.CoordId / jsonLayer.CWid;

                //        Vector2 position = new Vector2(x * jsonLayer.GridSize, y * jsonLayer.GridSize);
                //        Color color = Utility.ConvertStringToColor(layerInstance.IntGridValues[tile.V].Color);

                //        spriteBatch.Draw(texture, position, null, color, 0, Vector2.Zero, jsonLayer.GridSize, SpriteEffects.None, 0);
                //    }
                //}
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
            for(int i = 0; i < json.Levels.Length; i++)
            {
                if(levels[i].Loaded)
                {
                    if(levels[i].Uid == uid)
                    {
                        return levels[i];
                    }
                }
                else
                {
                    if(json.Levels[i].Uid == uid)
                    {
                        Load(i);

                        if(levels[i].Uid == uid)
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
            for(int i = 0; i < json.Levels.Length; i++)
            {
                if(levels[i].Loaded)
                {
                    if(levels[i].Identifier == identifier)
                    {
                        return levels[i];
                    }
                }
                else
                {
                    if(json.Levels[i].Identifier == identifier)
                    {
                        Load(i);

                        if(levels[i].Identifier == identifier)
                        {
                            return levels[i];
                        }
                    }
                }
            }

            throw new Exception(identifier + " Level not found Exception!");
        }

        /// <summary>
        /// Will return the level at index if the level is not loaded it will load it
        /// </summary>
        /// <param name="index">Index into array</param>
        /// <returns>Level or null if outside of bounds</returns>
        public Level GetEntities(long index)
        {
            if(index > 0 || index < levels.Length)
            {
                if(levels[index].Loaded == false)
                {
                    Load(index);
                }

                return levels[index];
            }
            else
            {
                return new Level() { Layers = new RenderTarget2D[0] };
            }
        }

        private EntityDefinition GetEntityDefinitionFromUid(long uid)
        {
            for(int i = 0; i < json.Defs.Entities.Length; i++)
            {
                if(json.Defs.Entities[i].Uid == uid)
                {
                    return json.Defs.Entities[i];
                }
            }

            return null;
        }

        private LayerDefinition GetLayerDefinitionFromUid(long uid)
        {
            for(int i = 0; i < json.Defs.Layers.Length; i++)
            {
                if(json.Defs.Layers[i].Uid == uid)
                {
                    return json.Defs.Layers[i];
                }
            }

            return null;
        }
    }
}