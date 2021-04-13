using System;
using LDtk.Json;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace LDtk.ContentPipeline
{
    [ContentTypeWriter]
    public class LDtkWorldWritter : ContentTypeWriter<LDtkProject>
    {
        protected override void Write(ContentWriter output, LDtkProject json)
        {
            try
            {
                ContentLogger.LogMessage($"Writting");
                output.Write(json.ToJson());
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
            return "LDtk.ContentPipeline.LDtkWorldReader, LDtkMonogame";
        }
    }
}