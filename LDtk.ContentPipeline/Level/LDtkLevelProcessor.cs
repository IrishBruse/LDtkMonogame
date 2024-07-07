namespace LDtk.ContentPipeline.Level;

using Microsoft.Xna.Framework.Content.Pipeline;

[ContentProcessor(DisplayName = "LDtkLevel Processor")]
public class LDtkLevelProcessor : ContentProcessor<string, LDtkLevel>
{
    public override LDtkLevel Process(string input, ContentProcessorContext context)
    {
        return LDtkLevel.FromFile(input);
    }
}
