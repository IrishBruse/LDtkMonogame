using System;
using LDtk.Json;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace LDtk.ContentPipeline
{
    [ContentTypeWriter]
    public class LDtkWorldWritter : ContentTypeWriter<LDtkWorld>
    {
        protected override void Write(ContentWriter output, LDtkWorld json)
        {
            try
            {
                ContentLogger.LogMessage($"Writting");
                // TODO: binary serialize this eventually
                output.Write(System.Text.Json.JsonSerializer.Serialize(json));
            }
            catch (Exception ex)
            {
                ContentLogger.LogMessage("Test");
                ContentLogger.LogMessage(ex.Message);
                ContentLogger.LogMessage(ex.StackTrace);
                throw;
            }
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "LDtk.ContentPipeline.LDtkFileReader, LDtk";
        }
    }
}