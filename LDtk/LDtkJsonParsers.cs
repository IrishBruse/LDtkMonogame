using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Color = Microsoft.Xna.Framework.Color;
using Rect = Microsoft.Xna.Framework.Rectangle;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Vector2Int = Microsoft.Xna.Framework.Point;

namespace LDtk
{
    class ColorConverter : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string str = reader.GetString();
            if (str.StartsWith('#'))
            {
                uint col = Convert.ToUInt32(str[1..], 16);
                return new Color(col);
            }

            throw new Exception(str);
        }

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            string str = "#" + value.R.ToString("X") + value.G.ToString("X") + value.B.ToString("X");
            writer.WriteStringValue(str);
        }
    }

    class RectConverter : JsonConverter<Rect>
    {
        public override Rect Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }

            var value = new List<int>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    return new Rect(value[0], value[1], value[2], value[3]);
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

        public override void Write(Utf8JsonWriter writer, Rect val, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            writer.WriteNumberValue(val.X);
            writer.WriteNumberValue(val.Y);
            writer.WriteNumberValue(val.Width);
            writer.WriteNumberValue(val.Height);
            writer.WriteEndArray();
        }
    }

    class Vector2Converter : JsonConverter<Vector2>
    {
        public override Vector2 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }

            var value = new List<float>();

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

    class Vector2IntConverter : JsonConverter<Vector2Int>
    {
        public override Vector2Int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }

            var value = new List<int>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    if (value.Count > 0)
                    {
                        return new Vector2Int(value[0], value[1]);
                    }
                    else
                    {
                        return new Vector2Int();
                    }
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

        public override void Write(Utf8JsonWriter writer, Vector2Int val, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            writer.WriteNumberValue(val.X);
            writer.WriteNumberValue(val.Y);
            writer.WriteEndArray();
        }
    }
}