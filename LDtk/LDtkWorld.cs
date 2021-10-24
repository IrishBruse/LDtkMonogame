using System.IO;
using System.Text.Json;
using LDtk.Exceptions;
using Microsoft.Xna.Framework.Content;
using Vector2Int = Microsoft.Xna.Framework.Point;

namespace LDtk
{
    public partial class LDtkWorld
    {
        /// <summary>
        /// Size of the world grid in pixels.
        /// </summary>
        public Vector2Int WorldGridSize => new Vector2Int((int)WorldGridWidth, (int)WorldGridHeight);

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
            for (int i = 0; i < Levels.Length; i++)
            {
                if (Levels[i].Identifier != identifier)
                {
                    continue;
                }

                if (ExternalLevels == false)
                {
                    return Levels[i];
                }

                string path = Path.Join(RootFolder, Levels[i].ExternalRelPath);

                return JsonSerializer.Deserialize<LDtkLevel>(File.ReadAllText(path), SerializeOptions);
            }

            throw new LevelNotFoundException($"Could not find {identifier} Level in {this}.");
        }

        /// <summary>
        /// Loads the ldtkl world file from disk directly or from the embeded one depending on if externalLevels is set
        /// </summary>
        /// <param name="identifier">The Level identifier</param>
        /// <param name="Content">The optional XNA content manager if you are using the content pipeline</param>
        /// <returns>LDtkWorld</returns>
        public LDtkLevel LoadLevel(string identifier, ContentManager Content)
        {
            for (int i = 0; i < Levels.Length; i++)
            {
                if (Levels[i].Identifier != identifier)
                {
                    continue;
                }

                if (ExternalLevels == false)
                {
                    return Levels[i];
                }

                string path = Path.Join(RootFolder, Levels[i].ExternalRelPath);
                return Content.Load<LDtkLevel>(path.Replace(".ldtkl", ""));
            }

            throw new LevelNotFoundException($"Could not Content.Load {identifier} Level in {this}.");
        }
    }
}
