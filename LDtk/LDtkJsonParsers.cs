namespace LDtk;

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

class ColorConverter : JsonConverter<Color>
{
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string str = reader.GetString();
        if (str.StartsWith('#'))
        {
            byte r = Convert.ToByte(str[1..3], 16);
            byte g = Convert.ToByte(str[3..5], 16);
            byte b = Convert.ToByte(str[5..7], 16);
#pragma warning disable IDE0090
            Color color = new Color(r, g, b, (byte)255);
#pragma warning restore IDE0090
            return color;
        }

        throw new Exception(str);
    }

    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        string str = "#" + value.R.ToString("X2") + value.G.ToString("X2") + value.B.ToString("X2");
        writer.WriteStringValue(str);
    }
}

class RectConverter : JsonConverter<Rectangle>
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
