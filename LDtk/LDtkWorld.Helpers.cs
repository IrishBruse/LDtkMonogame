using System;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework.Content;

namespace LDtk
{
    public partial class LDtkWorld
    {
        private string RootFolder;

        /// <summary>
        /// Loads the ldtk world file from disk directly
        /// </summary>
        /// <param name="filePath">Path to the .ldtk file</param>
        /// <param name="Content">The optional XNA content manager if you are using the content pipeline</param>
        /// <returns>LDtkWorld</returns>
        public static LDtkWorld LoadWorld(string filePath, ContentManager Content = null)
        {
            LDtkWorld world;
            if (Content != null)
            {
                world = Content.Load<LDtkWorld>(filePath);
            }
            else
            {
                world = JsonSerializer.Deserialize<LDtkWorld>(File.ReadAllText(filePath), SerializeOptions);
            }

            world.RootFolder = Path.GetDirectoryName(filePath);
            return world;
        }

        /// <summary>
        /// Loads the ldtkl world file from disk directly or from the embeded one depending on if externalLevels is set
        /// </summary>
        /// <param name="identifier">The Level identifier</param>
        /// <param name="Content">The optional XNA content manager if you are using the content pipeline</param>
        /// <returns>LDtkWorld</returns>
        public LDtkLevel LoadLevel(string identifier, ContentManager Content = null)
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
                if (Content != null)
                {
                    return Content.Load<LDtkLevel>(path.Replace(".ldtkl", ""));
                }
                else
                {
                    return JsonSerializer.Deserialize<LDtkLevel>(File.ReadAllText(path), SerializeOptions);
                }
            }

            throw new LevelNotFoundException($"Could not find {identifier} Level in {this}.", new Exception());
        }


    }
    class LevelNotFoundException : Exception
    {
        public LevelNotFoundException() : base() { }
        public LevelNotFoundException(string message) : base(message) { }
        public LevelNotFoundException(string message, Exception innerException) : base(message, innerException) { }

    }
}