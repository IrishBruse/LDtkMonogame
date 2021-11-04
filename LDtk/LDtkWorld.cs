using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using LDtk.Exceptions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace LDtk
{
    public partial class LDtkWorld
    {
        /// <summary>
        /// Size of the world grid in pixels.
        /// </summary>
        [JsonIgnore]
        public Point WorldGridSize => new Point(WorldGridWidth, WorldGridHeight);

        private string RootFolder;

        /// <summary>
        /// Loads the ldtk world file from disk directly
        /// </summary>
        /// <param name="filePath">Path to the .ldtk file excludeing file extension</param>
        /// <returns>LDtkWorld</returns>
        public static LDtkWorld LoadWorld(string filePath)
        {
            LDtkWorld world = JsonSerializer.Deserialize<LDtkWorld>(File.ReadAllText(filePath + ".ldtk"), SerializeOptions);

            world.RootFolder = Path.GetDirectoryName(filePath);
            return world;
        }

        /// <summary>
        /// Loads the ldtk world file from disk directly
        /// </summary>
        /// <param name="filePath">Path to the .ldtk file excludeing file extension</param>
        /// <param name="Content">The optional XNA content manager if you are using the content pipeline</param>
        /// <returns>LDtkWorld</returns>
        public static LDtkWorld LoadWorld(string filePath, ContentManager Content)
        {
            if (Content != null)
            {
                LDtkWorld world;
                world = Content.Load<LDtkWorld>(filePath);
                world.RootFolder = Path.GetDirectoryName(filePath);

                return world;
            }

            throw new ContentLoadException($"Could not load ldtk world at {filePath}.");
        }

        /// <summary>
        /// Loads the ldtkl world file from disk directly or from the embeded one depending on if externalLevels is set
        /// </summary>
        /// <param name="identifier">The Level identifier</param>
        /// <returns><see cref="LDtkLevel"/></returns>
        /// <exception cref="LevelNotFoundException"></exception>
        public LDtkLevel LoadLevel(string identifier)
        {
            LDtkLevel level = null;

            for (int i = 0; i < Levels.Length; i++)
            {
                if (Levels[i].Identifier != identifier)
                {
                    continue;
                }

                if (ExternalLevels == false)
                {
                    level = Levels[i];
                    break;
                }

                string path = Path.Join(RootFolder, Levels[i].ExternalRelPath);

                level = JsonSerializer.Deserialize<LDtkLevel>(File.ReadAllText(path), SerializeOptions);
                break;
            }

            if (level != null)
            {
                level.parent = this;
                return level;
            }

            throw new LevelNotFoundException($"Could not find {identifier} Level in {this}.");
        }

        /// <summary>
        /// Loads the ldtkl world file from disk directly or from the embeded one depending on if externalLevels is set
        /// </summary>
        /// <param name="uid">The Levels uid</param>
        /// <returns><see cref="LDtkLevel"/></returns>
        /// <exception cref="LevelNotFoundException"></exception>
        public LDtkLevel LoadLevel(int uid)
        {
            LDtkLevel level = null;

            for (int i = 0; i < Levels.Length; i++)
            {
                if (Levels[i].Uid != uid)
                {
                    continue;
                }

                if (ExternalLevels == false)
                {
                    level = Levels[i];
                    break;
                }

                string path = Path.Join(RootFolder, Levels[i].ExternalRelPath);

                level = JsonSerializer.Deserialize<LDtkLevel>(File.ReadAllText(path), SerializeOptions);
                break;
            }

            if (level != null)
            {
                level.parent = this;
                return level;
            }

            throw new LevelNotFoundException($"Could not find {uid} Level in {this}.");
        }



        /// <summary>
        /// Loads the ldtkl world file from disk directly or from the embeded one depending on if externalLevels is set
        /// </summary>
        /// <param name="uid">The Levels uid</param>
        /// <param name="Content">Content Pipeline</param>
        /// <returns><see cref="LDtkLevel"/></returns>
        /// <exception cref="LevelNotFoundException"></exception>
        public LDtkLevel LoadLevel(int uid, ContentManager Content)
        {
            LDtkLevel level = null;

            for (int i = 0; i < Levels.Length; i++)
            {
                if (Levels[i].Uid != uid)
                {
                    continue;
                }

                if (ExternalLevels == false)
                {
                    level = Levels[i];
                    break;
                }

                string path = Path.Join(RootFolder, Levels[i].ExternalRelPath);
                level = Content.Load<LDtkLevel>(path.Replace(".ldtkl", ""));
                break;
            }

            if (level != null)
            {
                level.parent = this;
                return level;
            }

            throw new LevelNotFoundException($"Could not find {uid} Level in {this}.");
        }


        /// <summary>
        /// Loads the ldtkl world file from disk directly or from the embeded one depending on if externalLevels is set
        /// </summary>
        /// <param name="identifier">The Level identifier</param>
        /// <param name="Content">The optional XNA content manager if you are using the content pipeline</param>
        /// <returns>LDtkWorld</returns>
        public LDtkLevel LoadLevel(string identifier, ContentManager Content)
        {
            LDtkLevel level = null;

            for (int i = 0; i < Levels.Length; i++)
            {
                if (Levels[i].Identifier != identifier)
                {
                    continue;
                }

                if (ExternalLevels == false)
                {
                    level = Levels[i];
                    break;
                }

                string path = Path.Join(RootFolder, Levels[i].ExternalRelPath);
                level = Content.Load<LDtkLevel>(path.Replace(".ldtkl", ""));
                break;
            }

            if (level != null)
            {
                level.parent = this;
                return level;
            }

            throw new LevelNotFoundException($"Could not Content.Load `{identifier}` in {this}.");
        }

        /// <summary>
        /// Gets the intgrid value definitions
        /// </summary>
        /// <param name="identifier">leyer identifier</param>
        /// <returns></returns>
        /// <exception cref="FieldInstanceException"></exception>
        public IntGridValueDefinition[] GetIntgridValueDefinitions(string identifier)
        {
            for (int i = 0; i < Defs.Layers.Length; i++)
            {
                if (Defs.Layers[i].Identifier != identifier)
                {
                    continue;
                }

                if (Defs.Layers[i]._Type == LayerType.IntGrid)
                {
                    return Defs.Layers[i].IntGridValues;
                }
            }

            throw new FieldInstanceException();
        }
    }
}
