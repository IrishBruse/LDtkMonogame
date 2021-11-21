using System;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace LDtk.ContentPipeline
{
    [ContentProcessor(DisplayName = "LDtk World Processor")]
    public class LDtkWorldProcessor : ContentProcessor<string, LDtkWorld>
    {
        public override LDtkWorld Process(string input, ContentProcessorContext context)
        {
            LDtkWorld world;

            try
            {
                ContentLogger.Logger = context.Logger;
                ContentLogger.LogMessage($"Processing");

                world = System.Text.Json.JsonSerializer.Deserialize<LDtkWorld>(input, LDtkWorld.SerializeOptions);
            }
            catch (Exception ex)
            {
                context.Logger.LogImportantMessage(ex.Message);
                throw;
            }

            return world;
        }
    }
}