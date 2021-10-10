using Microsoft.Xna.Framework.Content;
namespace LDtk.ContentPipeline
{
#pragma warning disable CS1591
    public class LDtkLevelReader : ContentTypeReader<LDtkLevel>
    {
        protected override LDtkLevel Read(ContentReader input, LDtkLevel existingInstance)
        {
            if (existingInstance != null)
            {
                return existingInstance;
            }

            return System.Text.Json.JsonSerializer.Deserialize<LDtkLevel>(input.ReadString(), LDtkWorld.SerializeOptions);
        }
    }
#pragma warning restore CS1591
}