namespace LDtk.ContentPipeline;

using System.Text.Json;

using LDtk;

using Microsoft.Xna.Framework.Content;

/// <summary> LDtkLevelReader </summary>
public class LDtkLevelReader : ContentTypeReader<LDtkLevel>
{
    /// <summary> Read </summary>
    protected override LDtkLevel Read(ContentReader input, LDtkLevel existingInstance)
    {
        LDtkLevel lDtkLevel = JsonSerializer.Deserialize<LDtkLevel>(input.ReadString(), Constants.SerializeOptions);
        return existingInstance ?? lDtkLevel;
    }
}
