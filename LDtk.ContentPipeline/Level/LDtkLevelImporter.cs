namespace LDtk.ContentPipeline.Level;

using Microsoft.Xna.Framework.Content.Pipeline;

[ContentImporter(".ldtkl", DisplayName = "LDtkLevel Importer", DefaultProcessor = "LDtkLevelProcessor")]
public class LDtkLevelImporter : ContentImporter<string>
{
    public override string Import(string filename, ContentImporterContext context)
    {
        return filename;
    }
}
