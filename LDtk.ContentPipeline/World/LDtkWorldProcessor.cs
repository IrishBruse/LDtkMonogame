using System;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace LDtk.ContentPipeline
{
    [ContentProcessor(DisplayName = "LDtk World Processor")]
    public class LDtkWorldProcessor : ContentProcessor<string, LDtkWorld>
    {
        public override LDtkWorld Process(string input, ContentProcessorContext context)
        {
            try
            {
                ContentLogger.Logger = context.Logger;
                ContentLogger.LogMessage($"Processing");

                return System.Text.Json.JsonSerializer.Deserialize<LDtkWorld>(input, LDtkWorld.SerializeOptions);
            }
            catch (Exception ex)
            {
                context.Logger.LogImportantMessage("Test");
                context.Logger.LogImportantMessage(ex.Message);
                throw;
            }
        }
    }
}