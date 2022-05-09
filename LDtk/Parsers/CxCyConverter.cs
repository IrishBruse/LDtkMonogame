namespace LDtk;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

class CxCyConverter : JsonConverter<Point>
{
    public override Point Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        reader.Read();

        if (reader.TokenType != JsonTokenType.PropertyName)
        {
            throw new JsonException();
        }

        reader.Read();
        int cx = reader.GetInt32();

        reader.Read();

        if (reader.TokenType != JsonTokenType.PropertyName)
        {
            throw new JsonException();
        }

        reader.Read();
        int cy = reader.GetInt32();

        reader.Read();
        return reader.TokenType != JsonTokenType.EndObject ? throw new JsonException() : new Point(cx, cy);
    }

    public override void Write(Utf8JsonWriter writer, Point val, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue(val.X);
        writer.WriteNumberValue(val.Y);
        writer.WriteEndArray();
    }
}
