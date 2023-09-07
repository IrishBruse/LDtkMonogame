namespace LDtk.Parsers;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

class GuidConverter : JsonConverter<Guid>
{
    public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string iid = reader.GetString()!;
        return Guid.Parse(iid);
    }

    public override void Write(Utf8JsonWriter writer, Guid val, JsonSerializerOptions options)
    {
        writer.WriteStringValue(val.ToString());
    }
}
