namespace LDtk.ContentPipeline.World;

using System.Text.Json;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

[ContentTypeWriter]
public class LDtkFileWriter : ContentTypeWriter<LDtkFile>
{
    protected override void Write(ContentWriter output, LDtkFile value)
    {
        output.Write(JsonSerializer.Serialize(value, Constants.SerializeOptions));
    }

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "LDtk.ContentPipeline.LDtkFileReader, LDtk";
    }
}
