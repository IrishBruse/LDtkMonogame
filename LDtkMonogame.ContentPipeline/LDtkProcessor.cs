using System;
using LDtk.Json;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace LDtk.ContentPipeline
{
    [ContentProcessor(DisplayName = "LDtk Processor")]
    public class LDtkProcessor : ContentProcessor<string, LDtkProject>
    {
        public override LDtkProject Process(string input, ContentProcessorContext context)
        {
            try
            {
                ContentLogger.Logger = context.Logger;
                ContentLogger.Log($"Processing");

                return LDtkProject.FromJson(input);
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