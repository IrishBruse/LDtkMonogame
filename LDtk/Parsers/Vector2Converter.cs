namespace LDtk;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

class Vector2Converter : JsonConverter<Vector2>
{
    public override Vector2 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Vector2 val = Vector2.Zero;
        if (reader.TokenType == JsonTokenType.StartArray)
        {
            reader.Read();

            float x = reader.GetSingle();
            reader.Read();

            float y = reader.GetSingle();
            reader.Read();

            val = new(x, y);
        }
        else if (reader.TokenType == JsonTokenType.StartObject)
        {
            reader.Read();
            reader.Read();
            float x = reader.GetSingle();
            reader.Read();
            reader.Read();
            float y = reader.GetSingle();
            reader.Read();

            val = new(x, y);
        }
        return val;
    }

    public override void Write(Utf8JsonWriter writer, Vector2 val, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue(val.X);
        writer.WriteNumberValue(val.Y);
        writer.WriteEndArray();
    }
}
