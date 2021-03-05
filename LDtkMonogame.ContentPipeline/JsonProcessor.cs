using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;
using Newtonsoft.Json;
using TInput = System.String;
using TOutput = LDtk.Json.LDtkJson;

namespace LDtk.ContentPipeline
{
    [ContentProcessor(DisplayName = "Json Processor")]
    class JsonProcessor : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            context.Logger.LogMessage(input);
            return JsonConvert.DeserializeObject<TOutput>(File.ReadAllText(input));
        }
    }
}