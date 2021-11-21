
using Microsoft.Xna.Framework.Content;

namespace LDtk.ContentPipeline;
/// <summary>
/// LDtkLevelReader
/// </summary>
public class LDtkLevelReader : ContentTypeReader<LDtkLevel>
{
    /// <summary>
    /// Read
    /// </summary>
    protected override LDtkLevel Read(ContentReader input, LDtkLevel existingInstance)
    {
        if (existingInstance != null)
        {
            return existingInstance;
        }

        return System.Text.Json.JsonSerializer.Deserialize<LDtkLevel>(input.ReadString(), LDtkWorld.SerializeOptions);
    }
}
