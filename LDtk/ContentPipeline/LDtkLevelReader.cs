namespace LDtk.ContentPipeline;

using System.Text.Json;

using LDtk;

using Microsoft.Xna.Framework.Content;

/// <summary> LDtkLevelReader. </summary>
public class LDtkLevelReader : ContentTypeReader<LDtkLevel>
{
    /// <summary> Read. </summary>
    /// <param name="input"> The ContentReader. </param>
    /// <param name="existingInstance"> The existingInstance. </param>
    /// <returns> The LDtkLevel read from the content pipeline. </returns>
    protected override LDtkLevel Read(ContentReader input, LDtkLevel existingInstance)
    {
        return existingInstance ?? JsonSerializer.Deserialize<LDtkLevel>(input.ReadString(), Constants.SerializeOptions)!;
    }
}
