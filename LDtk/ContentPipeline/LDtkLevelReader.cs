namespace LDtk.ContentPipeline;

using Microsoft.Xna.Framework.Content;

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
        return existingInstance ?? System.Text.Json.JsonSerializer.Deserialize<LDtkLevel>(input.ReadString(), LDtkWorld.SerializeOptions);
    }
}
