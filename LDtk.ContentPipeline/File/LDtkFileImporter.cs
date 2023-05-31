namespace LDtk.ContentPipeline.File;

using Microsoft.Xna.Framework.Content.Pipeline;

[ContentImporter(".ldtk", DisplayName = "LDtkFile Importer", DefaultProcessor = "LDtkFileProcessor")]
public class LDtkFileImporter : ContentImporter<string>
{
    public override string Import(string filename, ContentImporterContext context)
    {
        return filename;
    }
}
