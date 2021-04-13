using System;
using LDtk.Json;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace LDtk.ContentPipeline
{
    [ContentProcessor(DisplayName = "LDtk Processor")]
    public class LDtkLevelProcessor : ContentProcessor<string, Level>
    {
        public override Level Process(string input, ContentProcessorContext context)
        {
            try
            {
                ContentLogger.Logger = context.Logger;
                ContentLogger.LogMessage($"Processing");

                return Newtonsoft.Json.JsonConvert.DeserializeObject<Level>(input);
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