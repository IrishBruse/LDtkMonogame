using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;
using Newtonsoft.Json;
using TImport = LDtk.Json.LDtkJson;

namespace LDtk.ContentPipeline
{
    [ContentImporter(".json ,.ldtk ,.ldtkl", DisplayName = "Json Importer", DefaultProcessor = "JsonProcessor")]
    public class JsonImporter : ContentImporter<TImport>
    {
        public override TImport Import(string filename, ContentImporterContext context)
        {
            context.Logger.LogMessage("Importing Json file: {0}", filename);
            return JsonConvert.DeserializeObject<TImport>(File.ReadAllText(filename));
        }
    }
}
