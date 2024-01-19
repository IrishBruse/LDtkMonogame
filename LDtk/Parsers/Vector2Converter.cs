namespace LDtk.Parsers;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.Xna.Framework;

class Vector2Converter : JsonConverter<Vector2>
{
    public override Vector2 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.StartArray)
        {
            _ = reader.Read();

            float x = reader.GetSingle();
            _ = reader.Read();

            float y = reader.GetSingle();
            _ = reader.Read();

            return new(x, y);
        }
        else if (reader.TokenType == JsonTokenType.StartObject)
        {
            _ = reader.Read();
            _ = reader.Read();
            float x = reader.GetSingle();
            _ = reader.Read();
            _ = reader.Read();
            float y = reader.GetSingle();
            _ = reader.Read();

            return new(x, y);
        }
        return Vector2.Zero;
    }

    public override void Write(Utf8JsonWriter writer, Vector2 val, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue(val.X);
        writer.WriteNumberValue(val.Y);
        writer.WriteEndArray();
    }
}
