namespace LDtk;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

class PointConverter : JsonConverter<Point>
{
    public override Point Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Point val = Point.Zero;
        if (reader.TokenType == JsonTokenType.StartArray)
        {
            reader.Read();

            int x = reader.GetInt32();
            reader.Read();

            int y = reader.GetInt32();
            reader.Read();

            val = new(x, y);
        }
        else if (reader.TokenType == JsonTokenType.StartObject)
        {
            reader.Read();
            reader.Read();
            int x = reader.GetInt32();
            reader.Read();
            reader.Read();
            int y = reader.GetInt32();
            reader.Read();

            val = new(x, y);
        }
        return val;
    }

    public override void Write(Utf8JsonWriter writer, Point val, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue(val.X);
        writer.WriteNumberValue(val.Y);
        writer.WriteEndArray();
    }
}
