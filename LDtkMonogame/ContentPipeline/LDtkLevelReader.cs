using LDtk.Json;
using Microsoft.Xna.Framework.Content;

namespace LDtk.ContentPipeline
{
#pragma warning disable CS1591
    public class LDtkLevelReader : ContentTypeReader<Level>
    {
        protected override Level Read(ContentReader input, Level existingInstance)
        {
            if (existingInstance != null)
            {
                return existingInstance;
            }
            string text = input.ReadString();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Level>(text);
        }
    }
#pragma warning restore CS1591
}