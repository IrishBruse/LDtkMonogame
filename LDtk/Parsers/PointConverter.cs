namespace LDtk.Parsers;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.Xna.Framework;

class PointConverter : JsonConverter<Point>
{
    public override Point Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.StartArray)
        {
            _ = reader.Read();

            int x = reader.GetInt32();
            _ = reader.Read();

            int y = reader.GetInt32();
            _ = reader.Read();

            return new(x, y);
        }
        else if (reader.TokenType == JsonTokenType.StartObject)
        {
            _ = reader.Read();
            _ = reader.Read();
            int x = reader.GetInt32();
            _ = reader.Read();
            _ = reader.Read();
            int y = reader.GetInt32();
            _ = reader.Read();

            return new(x, y);
        }
        return Point.Zero;
    }

    public override void Write(Utf8JsonWriter writer, Point val, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue(val.X);
        writer.WriteNumberValue(val.Y);
        writer.WriteEndArray();
    }
}
