namespace LDtk;

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

class Vector2Converter : JsonConverter<Vector2>
{
    public override Vector2 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException();
        }

        List<float> value = new();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                return new Vector2(value[0], value[1]);
            }

            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            float element = reader.GetSingle();
            value.Add(element);
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, Vector2 val, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue(val.X);
        writer.WriteNumberValue(val.Y);
        writer.WriteEndArray();
    }
}
