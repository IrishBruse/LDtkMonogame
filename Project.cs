using System;
using System.IO;

using LDtk.Internal;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDtk
{
    public class Project
    {
        public Level[] Levels;

        private readonly LdtkJson json;

        private string absoluteProjectFolder;

        /// <summary>
        /// <list type="table"> 
        /// Load the LDtk json file.
        /// Throws <see cref="FileNotFoundException"/> if an invalid 
        /// file path is passed
        /// </list>
        /// </summary>
        /// <param name="ldtkFile">the path to the file</param>
        public Project(string ldtkFile)
        {
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
        /// You need to prerender a level so it can be cached and rendered more effiencntly 
        /// </summary>
        /// <param name="spriteBatch">This is needed to prerender the level layers</param>
        /// <param name="levelId"></param>
        public void Render(SpriteBatch spriteBatch, int levelId)
        {
            // Cache the Background Color
            Levels[levelId].BgColor = Utility.ConvertStringToColor(json.Levels[levelId].BgColor);

            LayerInstance[] jsonLayerInstances = json.Levels[levelId].LayerInstances;
            Levels[levelId].layers = new RenderTarget2D[jsonLayerInstances.Length];

            GraphicsDevice GraphicsDevice = spriteBatch.GraphicsDevice;

            for(int i = jsonLayerInstances.Length - 1; i >= 0; i--)
            {
                LayerInstance jsonLayer = jsonLayerInstances[i];

                Levels[levelId].layers[i] = new RenderTarget2D(GraphicsDevice, (int)(jsonLayer.CWid * jsonLayer.GridSize), (int)(jsonLayer.CHei * jsonLayer.GridSize), false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);

                GraphicsDevice.SetRenderTarget(Levels[levelId].layers[i]);
                Texture2D texture;

                spriteBatch.Begin(samplerState: SamplerState.PointClamp);

                switch(jsonLayer.Type)
                {
                    case Const.LayerTypeTiles:
                    texture = Texture2D.FromFile(GraphicsDevice, Path.Combine(absoluteProjectFolder, jsonLayer.TilesetRelPath));
                    foreach(TileInstance tile in jsonLayer.GridTiles)
                    {
                        if(jsonLayer.TilesetDefUid.HasValue)
                        {
                            Vector2 position = new Vector2((int)(tile.Px[0] + jsonLayer.PxTotalOffsetX), (int)(tile.Px[1] + jsonLayer.PxTotalOffsetY));
                            Rectangle rect = new Rectangle((int)tile.Src[0], (int)tile.Src[1], (int)jsonLayer.GridSize, (int)jsonLayer.GridSize);
                            SpriteEffects mirror = (SpriteEffects)tile.F;

                            spriteBatch.Draw(texture, position, rect, Color.White, 0, Vector2.Zero, 1f, mirror, i * float.Epsilon);
                        }
                    }
                    break;

                    case Const.LayerTypeIntGrid:
                    case Const.LayerTypeAutoTile:
                    texture = Texture2D.FromFile(GraphicsDevice, Path.Combine(absoluteProjectFolder, jsonLayer.TilesetRelPath));
                    foreach(TileInstance tile in jsonLayer.AutoLayerTiles)
                    {
                        if(jsonLayer.TilesetDefUid.HasValue)
                        {
                            Vector2 position = new Vector2((int)(tile.Px[0] + jsonLayer.PxTotalOffsetX), (int)(tile.Px[1] + jsonLayer.PxTotalOffsetY));
                            Rectangle rect = new Rectangle((int)tile.Src[0], (int)tile.Src[1], (int)jsonLayer.GridSize, (int)jsonLayer.GridSize);
                            SpriteEffects mirror = (SpriteEffects)tile.F;

                            spriteBatch.Draw(texture, position, rect, Color.White, 0, Vector2.Zero, 1f, mirror, i * float.Epsilon);
                        }
                    }
                    break;

                    case Const.LayerTypeEntities:
                    foreach(EntityInstance entity in jsonLayer.EntityInstances)
                    {
                        if(entity.Tile == null)
                        {
                            //Vector2 position = new Vector2((int)(level.WorldX + entity.Px[0] + layer.PxTotalOffsetX), (int)(level.WorldY + entity.Px[1] + layer.PxTotalOffsetY));
                            //Vector2 size = new Vector2(entityDefs[entity.DefUid].Width, entityDefs[entity.DefUid].Height);
                            //uint hex = uint.Parse(entityDefs[entity.DefUid].Color.Remove(0, 1) + "FF", NumberStyles.HexNumber);
                            //Color color = new Color(ReverseHex(hex));
                            //spriteBatch.Draw(pixelTexture, position, null, color, 0, Vector2.Zero, size, SpriteEffects.None, i * float.Epsilon);
                        }
                    }
                    break;

                    default: throw new NotImplementedException(jsonLayer.Type);
                }

                spriteBatch.End();
            }
            GraphicsDevice.SetRenderTarget(null);
        }
    }
}