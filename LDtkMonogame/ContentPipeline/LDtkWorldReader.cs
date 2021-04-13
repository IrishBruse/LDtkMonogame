using Microsoft.Xna.Framework.Content;

namespace LDtk.ContentPipeline
{
#pragma warning disable CS1591
    public class LDtkWorldReader : ContentTypeReader<LDtkWorld>
    {
        protected override LDtkWorld Read(ContentReader input, LDtkWorld existingInstance)
        {
            if (existingInstance != null)
            {
                return existingInstance;
            }
            string text = input.ReadString();
            return new LDtkWorld(text, input.ContentManager);
        }
    }
#pragma warning restore CS1591
}