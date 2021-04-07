using System;
using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace LDtk.ContentPipeline
{
    [ContentImporter(new string[] { ".ldtk", ".ldtkl" }, DisplayName = "LDtk Importer", DefaultProcessor = "LDtkProcessor")]
    public class LDtkImporter : ContentImporter<string>
    {
        public override string Import(string filename, ContentImporterContext context)
        {
            try
            {
                ContentLogger.Logger = context.Logger;
                ContentLogger.Log($"Importing '{filename}'");

                return File.ReadAllText(Path.GetFullPath(filename));
            }
            catch (Exception e)
            {
                context.Logger.LogImportantMessage(e.StackTrace);
                throw e;
            }
        }
    }
}
