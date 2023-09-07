namespace LDtk.Parsers;

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.Xna.Framework;

class RectangleConverter : JsonConverter<Rectangle>
{
    public override Rectangle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return default;
        }

        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException();
        }

        List<int> value = new();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                return new Rectangle(value[0], value[1], value[2], value[3]);
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

    public override void Write(Utf8JsonWriter writer, Rectangle val, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue(val.X);
        writer.WriteNumberValue(val.Y);
        writer.WriteNumberValue(val.Width);
        writer.WriteNumberValue(val.Height);
        writer.WriteEndArray();
    }
}
