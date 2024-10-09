namespace LDtk;

using System.Text.Json;
using System.Text.Json.Serialization;

public class DefaultOverride
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    
    [JsonPropertyName("params")]
    public JsonElement Params { get; set; }
}
