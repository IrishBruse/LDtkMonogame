using System;
using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace LDtk.ContentPipeline
{
    [ContentImporter(".ldtkl", DisplayName = "LDtk World Importer")]
    public class LDtkLevelImporter : ContentImporter<string>
    {
        public override string Import(string filename, ContentImporterContext context)
        {
            try
            {
                ContentLogger.Logger = context.Logger;
                ContentLogger.LogMessage($"Importing '{filename}'");

                return File.ReadAllText(Path.GetFullPath(filename));
            }
            catch (Exception e)
            {
                context.Logger.LogImportantMessage(e.StackTrace);
                throw;
            }
        }
    }
}
