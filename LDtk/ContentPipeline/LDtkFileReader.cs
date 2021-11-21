
using Microsoft.Xna.Framework.Content;

namespace LDtk.ContentPipeline;
/// <summary>
/// LDtkFileReader
/// </summary>
public class LDtkFileReader : ContentTypeReader<LDtkWorld>
{
    /// <summary>
    /// Read
    /// </summary>
    protected override LDtkWorld Read(ContentReader input, LDtkWorld existingInstance)
    {
        if (existingInstance != null)
        {
            return existingInstance;
        }

        return System.Text.Json.JsonSerializer.Deserialize<LDtkWorld>(input.ReadString(), LDtkWorld.SerializeOptions);
    }
}
