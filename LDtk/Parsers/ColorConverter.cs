namespace LDtk;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

class ColorConverter : JsonConverter<Color>
{
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string str = reader.GetString();

        if (str == null)
        {
            return default;
        }

        if (str.StartsWith('#'))
        {
            byte r = Convert.ToByte(str[1..3], 16);
            byte g = Convert.ToByte(str[3..5], 16);
            byte b = Convert.ToByte(str[5..7], 16);
            Color color = new(r, g, b, (byte)255);
            return color;
        }

        throw new LDtkException(str);
    }

    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        string str = "#" + value.R.ToString("X2") + value.G.ToString("X2") + value.B.ToString("X2");
        writer.WriteStringValue(str);
    }
}
