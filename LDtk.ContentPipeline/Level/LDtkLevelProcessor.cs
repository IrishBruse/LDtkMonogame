using System;
using System.Diagnostics;
using LDtk.Json;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace LDtk.ContentPipeline
{
    [ContentProcessor(DisplayName = "LDtk Level Processor")]
    public class LDtkLevelProcessor : ContentProcessor<string, LDtkLevel>
    {
        public override LDtkLevel Process(string input, ContentProcessorContext context)
        {
            try
            {
                ContentLogger.Logger = context.Logger;
                ContentLogger.LogMessage($"Processing");

                Debug.Print(input);

                return System.Text.Json.JsonSerializer.Deserialize<LDtkLevel>(input, LDtkWorld.SerializeOptions);
            }
            catch (Exception ex)
            {
                context.Logger.LogImportantMessage(ex.Message);
                throw;
            }
        }
    }
}
