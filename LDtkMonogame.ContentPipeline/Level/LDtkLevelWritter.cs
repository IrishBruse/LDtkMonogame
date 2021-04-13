using System;
using LDtk.Json;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace LDtk.ContentPipeline
{
    [ContentTypeWriter]
    public class LDtkLevelWritter : ContentTypeWriter<Level>
    {
        protected override void Write(ContentWriter output, Level json)
        {
            try
            {
                ContentLogger.LogMessage($"Writting");
                output.Write(Newtonsoft.Json.JsonConvert.SerializeObject(json));
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
            return "LDtk.ContentPipeline.LDtkLevelReader, LDtkMonogame";
        }
    }
}