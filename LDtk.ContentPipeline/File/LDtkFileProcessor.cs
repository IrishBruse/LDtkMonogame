namespace LDtk.ContentPipeline.File;

using Microsoft.Xna.Framework.Content.Pipeline;

[ContentProcessor(DisplayName = "LDtkFile Processor")]
public class LDtkFileProcessor : ContentProcessor<string, LDtkFile>
{
    public override LDtkFile Process(string input, ContentProcessorContext context)
    {
        return LDtkFile.FromFile(input);
    }
}
