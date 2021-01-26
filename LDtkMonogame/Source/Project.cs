using System;
using System.IO;

using LDtk.Internal;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDtk
{
    public class Project
    {
        public Level[] Levels { get; private set; }

        private readonly LdtkJson json;

        private readonly string absoluteProjectFolder;
        private readonly SpriteBatch spriteBatch;

        /// <summary>
        /// <para>Load the LDtk json file.</para>
        /// Throws <see cref="FileNotFoundException"/> if an invalid 
        /// file path is passed
        /// </summary>
        /// <param name="spriteBatch">Monogames <see cref="SpriteBatch"/></param>
        /// <param name="ldtkFile">The path to the file</param>
        public Project(SpriteBatch spriteBatch, string ldtkFile)
        {
            this.spriteBatch = spriteBatch;

            // Throw error if file not found
            if(File.Exists(ldtkFile) == false)
            {
                throw new FileNotFoundException("Ldtk project File not found " + ldtkFile);
            }

            json = LdtkJson.FromJson(File.ReadAllText(ldtkFile));
            absoluteProjectFolder = Path.GetDirectoryName(Path.GetFullPath(ldtkFile));
            Levels = new Level[json.Levels.Length];
        }

        /// <summary>
        /// This will render out the level using the level id 
        /// </summary>
        /// <param name="levelId">The id of the level</param>
        public void Render(int levelId)
        {
            GraphicsDevice GraphicsDevice = spriteBatch.GraphicsDevice;

            Internal.Level level;

            if(json.ExternalLevels == true)
            {
                string path = Path.Combine(absoluteProjectFolder, json.Levels[levelId].ExternalRelPath);
                level = Newtonsoft.Json.JsonConvert.DeserializeObject<Internal.Level>(File.ReadAllText(path));
            }
            else
            {
                level = json.Levels[levelId];
            }

            // Cache the Background Color
            Levels[levelId].BgColor = Utility.ConvertStringToColor(level.BgColor);

            // The current Level has a background
            if(level.BgRelPath != null)
            {
                Background background = Levels[levelId].Background;

                background.Image = Texture2D.FromFile(GraphicsDevice, Path.Combine(absoluteProjectFolder, level.BgRelPath));

                long[] topleft = level.BgPos.TopLeftPx;
                background.TopLeft = new Vector2(topleft[0], topleft[1]);

                double[] rect = level.BgPos.CropRect;
                background.CropRect = new Rectangle((int)rect[0], (int)rect[1], (int)rect[2], (int)rect[3]);

                var scale = level.BgPos.Scale;
                background.Scale = new Vector2((float)scale[0], (float)scale[1]);

                Levels[levelId].Background = background;
            }

            LayerInstance[] jsonLayerInstances = level.LayerInstances;
            Levels[levelId].Layers = new RenderTarget2D[jsonLayerInstances.Length];

            for(int i = jsonLayerInstances.Length - 1; i >= 0; i--)
            {
                LayerInstance jsonLayer = jsonLayerInstances[i];

                Levels[levelId].Layers[i] = new RenderTarget2D(GraphicsDevice, (int)(jsonLayer.CWid * jsonLayer.GridSize), (int)(jsonLayer.CHei * jsonLayer.GridSize), false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);

                GraphicsDevice.SetRenderTarget(Levels[levelId].Layers[i]);
                Texture2D texture;

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

                spriteBatch.Begin(samplerState: SamplerState.PointClamp);

                switch(jsonLayer.Type)
                {
                    case Const.LayerTypeTiles:
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

                    case Const.LayerTypeIntGrid:
                    case Const.LayerTypeAutoTile:
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
                    else
                    {
                        LayerDefinition layerInstance = GetLayerDefinitionFromUid(jsonLayer.LayerDefUid);

                        foreach(IntGridValueInstance tile in jsonLayer.IntGrid)
                        {
                            long x = tile.CoordId % jsonLayer.CWid;
                            long y = tile.CoordId / jsonLayer.CWid;

                            Vector2 position = new Vector2(x * jsonLayer.GridSize, y * jsonLayer.GridSize);
                            Color color = Utility.ConvertStringToColor(layerInstance.IntGridValues[tile.V].Color);

                            spriteBatch.Draw(texture, position, null, color, 0, Vector2.Zero, jsonLayer.GridSize, SpriteEffects.None, 0);
                        }
                    }
                    break;

                    case Const.LayerTypeEntities:
                    foreach(EntityInstance entity in jsonLayer.EntityInstances)
                    {
                        //EntityDefinition def = GetEntityDefinitionFromUid(entity.DefUid);
                        //Vector2 position = new Vector2((int)(entity.Px[0] + jsonLayer.PxTotalOffsetX), (int)(entity.Px[1] + jsonLayer.PxTotalOffsetY));
                        //Vector2 size = new Vector2(def.Width, def.Height);
                        //Color color = Utility.ConvertStringToColor(def.Color);
                        //
                        //spriteBatch.Draw(texture, position, null, color, 0, Vector2.Zero, size, SpriteEffects.None, 0);
                    }
                    break;

                    default: throw new NotImplementedException(jsonLayer.Type);
                }

                spriteBatch.End();
            }
            GraphicsDevice.SetRenderTarget(null);
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