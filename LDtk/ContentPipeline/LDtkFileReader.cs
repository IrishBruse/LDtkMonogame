using LDtk.Json;
using Microsoft.Xna.Framework.Content;

namespace LDtk.ContentPipeline
{
#pragma warning disable CS1591
    public class LDtkFileReader : ContentTypeReader<LDtkWorld>
    {
        protected override LDtkWorld Read(ContentReader input, LDtkWorld existingInstance)
        {
            if (existingInstance != null)
            {
                return existingInstance;
            }

            return System.Text.Json.JsonSerializer.Deserialize<LDtkWorld>(input.ReadString(), LDtkWorld.SerializeOptions);
        }
    }
#pragma warning restore CS1591
}