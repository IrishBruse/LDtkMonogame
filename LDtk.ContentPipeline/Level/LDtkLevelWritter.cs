namespace LDtk.ContentPipeline.Level;

using System;
using System.Text.Json;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

[ContentTypeWriter]
public class LDtkLevelWritter : ContentTypeWriter<LDtkLevel>
{
    protected override void Write(ContentWriter output, LDtkLevel json)
    {
        try
        {
            ContentLogger.LogMessage($"Writting");
            output.Write(JsonSerializer.Serialize(json, LDtkWorld.SerializeOptions));
        }
        catch (Exception ex)
        {
            ContentLogger.LogMessage(ex.Message);
            ContentLogger.LogMessage(ex.StackTrace);
            throw;
        }
    }

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "LDtk.ContentPipeline.LDtkLevelReader, LDtk";
    }
}
