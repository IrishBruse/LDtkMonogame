using System;
using System.Runtime.CompilerServices;
using LDtk.Json;
using Microsoft.Xna.Framework;
using Newtonsoft.Json.Linq;

namespace LDtk
{
    /// <summary>
    /// Utility for parsing ldtk json data into more typed versions
    /// </summary>
    internal static class Utility
    {
        /// <summary>
        /// Convert ldtk color string into <see cref="Color"/>
        /// </summary>
        /// <param name="hex">In LDtk format of #BBGGRR hex</param>
        /// <returns></returns>
        public static Color ConvertStringToColor(string hex)
        {
            return ConvertStringToColor(hex, 255);
        }

        /// <summary>
        /// Convert ldtk color string into <see cref="Color"/>
        /// </summary>
        /// <param name="hex">In LDtk format of #BBGGRR hex</param>
        /// <param name="alpha">Alpha</param>
        /// <returns></returns>
        public static Color ConvertStringToColor(string hex, int alpha)
        {
            if (uint.TryParse(hex.Replace("#", ""), System.Globalization.NumberStyles.HexNumber, null, out uint color))
            {
                byte red = (byte)((color & 0xFF0000) >> 16);
                byte green = (byte)((color & 0x00FF00) >> 8);
                byte blue = (byte)((color & 0xFF));

                return new Color(red, green, blue, alpha);
            }
            else
            {
                return new Color(0xFF00FFFF);
            }
        }


        public static void ParseField<T>(T entity, FieldInstance fieldInstance) where T : new()
        {
            string variableName = fieldInstance.Identifier;

            variableName = char.ToLower(variableName[0]) + variableName.Substring(1);

            var field = typeof(T).GetField(variableName);

            if (field == null)
            {
#if DEBUG
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: Entity Field \"{variableName}\" not found in {typeof(T).FullName}");
                Console.ResetColor();
#endif
                return;
            }

            // Split any enums
            string[] variableTypes = fieldInstance.Type.Split('.');

            switch (variableTypes[0])
            {
                case "Int":
                case "Float":
                case "Bool":
                case "Enum":
                case "String":
                    field.SetValue(entity, Convert.ChangeType(fieldInstance.Value, field.FieldType));
                    break;

                case "LocalEnum":
                    field.SetValue(entity, Enum.Parse(field.FieldType, (string)fieldInstance.Value));
                    break;

                case "Color":
                    field.SetValue(entity, Utility.ConvertStringToColor(((string)fieldInstance.Value)[1..]));
                    break;

                case "Point":
                    JToken t = (JToken)fieldInstance.Value;
                    Vector2 point;
                    if (t != null)
                    {
                        point = new Vector2(t.First.First.Value<float>(), t.Last.Last.Value<float>());
                    }
                    else
                    {
                        point = new Vector2(0, 0);
                    }
                    field.SetValue(entity, point);
                    break;

                default:
                    throw new FieldInstanceException("Unknown Variable of type " + fieldInstance.Type);
            }
        }


    }
}
