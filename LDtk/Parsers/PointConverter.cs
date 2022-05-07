namespace LDtk;

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

class PointConverter : JsonConverter<Point>
{
    public override Point Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException();
        }

        List<int> value = new();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                return value.Count > 0 ? new Point(value[0], value[1]) : new Point();
            }

            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            int element = reader.GetInt32();
            value.Add(element);
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, Point val, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue(val.X);
        writer.WriteNumberValue(val.Y);
        writer.WriteEndArray();
    }
}
