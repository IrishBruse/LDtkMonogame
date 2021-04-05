using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using LDtk.Exceptions;
using LDtk.Json;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LDtk
{
    /// <summary>
    /// Utility for parsing ldtk json data into more typed versions
    /// </summary>
    internal static class Parser
    {
        /// <summary>
        /// Convert ldtk color string into <see cref="Color"/>
        /// </summary>
        /// <param name="hex">In LDtk format of #BBGGRR hex</param>
        /// <returns></returns>
        public static Color ParseStringToColor(string hex)
        {
            return ParseStringToColor(hex, 255);
        }

        /// <summary>
        /// Convert ldtk color string into <see cref="Color"/>
        /// </summary>
        /// <param name="hex">In LDtk format of #BBGGRR hex</param>
        /// <param name="alpha">Alpha</param>
        /// <returns></returns>
        public static Color ParseStringToColor(string hex, int alpha)
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

            // make the first letter lowercase
            variableName = char.ToLower(variableName[0]) + variableName[1..];

            var field = typeof(T).GetField(variableName);

            if (field == null)
            {
#if DEBUG
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: Field \"{variableName}\" not found in {typeof(T).FullName}");
                Console.ResetColor();
#endif
                return;
            }

            // Split any enums

            int enumTypeIndex = fieldInstance.Type.LastIndexOf('.');
            int arrayEndIndex = fieldInstance.Type.LastIndexOf('>');

            string variableType = fieldInstance.Type;

            if (enumTypeIndex != -1)
            {
                if (arrayEndIndex != -1)
                {
                    string enumType = variableType[enumTypeIndex..arrayEndIndex];
                    variableType = variableType.Remove(enumTypeIndex, arrayEndIndex - enumTypeIndex);
                }
                else
                {
                    string enumType = variableType[enumTypeIndex..];
                    variableType = variableType.Remove(enumTypeIndex, variableType.Length - enumTypeIndex);
                }
            }

            switch (variableType)
            {
                case Field.IntType:
                case Field.BoolType:
                case Field.EnumType:
                case Field.FloatType:
                case Field.StringType:
                case Field.FilePathType:
                    field.SetValue(entity, Convert.ChangeType(fieldInstance.Value, field.FieldType));
                    break;

                case Field.IntArrayType:
                case Field.BoolArrayType:
                case Field.EnumArrayType:
                case Field.FloatArrayType:
                case Field.StringArrayType:
                case Field.FilePathArrayType:
                case Field.LocalEnumArrayType:
                    var primativeArrayValues = JsonConvert.DeserializeObject(fieldInstance.Value.ToString(), field.FieldType);
                    field.SetValue(entity, Convert.ChangeType(primativeArrayValues, field.FieldType));
                    break;

                case Field.LocalEnumType:
                    field.SetValue(entity, Enum.Parse(field.FieldType, (string)fieldInstance.Value));
                    break;

                case Field.ColorType:
                    field.SetValue(entity, ParseStringToColor(((string)fieldInstance.Value)[1..]));
                    break;

                case Field.PointType:
                    Point point = JsonConvert.DeserializeObject<LDtkPoint>(fieldInstance.Value.ToString());
                    field.SetValue(entity, point);
                    break;

                case Field.PointArrayType:
                    List<LDtkPoint> points = JsonConvert.DeserializeObject<List<LDtkPoint>>(fieldInstance.Value.ToString());

                    Point[] pointArray = new Point[fieldInstance.RealEditorValues.Length];

                    for (int i = 0; i < points.Count; i++)
                    {
                        pointArray[i] = points[i];
                    }

                    field.SetValue(entity, pointArray);
                    break;

                default:
                    throw new FieldInstanceException("Unknown Variable of type " + fieldInstance.Type);
            }
        }

        public static void ParseBaseField<T>(T entity, string field, object value)
        {
            // WorldPosition
            var variable = typeof(T).GetField(field, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) ??
                    typeof(Entity).GetField(field, BindingFlags.Instance | BindingFlags.NonPublic);

            if (variable != null)
            {
                variable.SetValue(entity, value);
            }
#if DEBUG
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: Base Field \"{field}\" not found add it to {typeof(T).FullName}");
                Console.ResetColor();
            }
#endif
        }

        class LDtkPoint
        {
            [JsonProperty()]
            public int cx;
            [JsonProperty()]
            public int cy;
            public static implicit operator Point(LDtkPoint p) => new Point(p.cx, p.cy);
        }
    }
}
