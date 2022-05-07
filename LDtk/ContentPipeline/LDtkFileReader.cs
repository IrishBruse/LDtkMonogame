namespace LDtk.ContentPipeline;

using System.Text.Json;
using Microsoft.Xna.Framework.Content;

/// <summary> LDtkFileReader </summary>
public class LDtkFileReader : ContentTypeReader<LDtkFile>
{
    /// <summary> Read </summary>
    protected override LDtkFile Read(ContentReader input, LDtkFile existingInstance)
    {
        return existingInstance ?? JsonSerializer.Deserialize<LDtkFile>(input.ReadString(), Constants.SerializeOptions);
    }
}
