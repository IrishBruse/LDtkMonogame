using System;
using System.Collections.Generic;
using System.Reflection;
using LDtk.Exceptions;
using LDtk.Json;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

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
        internal static Color ParseStringToColor(string hex)
        {
            return ParseStringToColor(hex, 255);
        }

        /// <summary>
        /// Convert ldtk color string into <see cref="Color"/>
        /// </summary>
        /// <param name="hex">In LDtk format of #BBGGRR hex</param>
        /// <param name="alpha">Alpha</param>
        /// <returns></returns>
        internal static Color ParseStringToColor(string hex, int alpha)
        {
            if (uint.TryParse(hex.Replace("#", ""), System.Globalization.NumberStyles.HexNumber, null, out uint color))
            {
                byte red = (byte)((color & 0xFF0000) >> 16);
                byte green = (byte)((color & 0x00FF00) >> 8);
                byte blue = (byte)(color & 0xFF);

                return new Color(red, green, blue, alpha);
            }
            else
            {
                return new Color(0xFF00FFFF);
            }
        }


        internal static void ParseField<T>(T entity, FieldInstance fieldInstance) where T : new()
        {
            string variableName = fieldInstance.Identifier;

            // make the first letter lowercase
            variableName = char.ToLower(variableName[0]) + variableName[1..];

            FieldInfo field = typeof(T).GetField(variableName);

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
                    variableType = variableType.Remove(enumTypeIndex, arrayEndIndex - enumTypeIndex);
                }
                else
                {
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
                    object primativeArrayValues = JsonConvert.DeserializeObject(fieldInstance.Value.ToString(), field.FieldType);
                    field.SetValue(entity, Convert.ChangeType(primativeArrayValues, field.FieldType));
                    break;

                case Field.LocalEnumType:
                    field.SetValue(entity, Enum.Parse(field.FieldType, (string)fieldInstance.Value));
                    break;

                case Field.ColorType:
                    field.SetValue(entity, ParseStringToColor(((string)fieldInstance.Value)[1..]));
                    break;

                case Field.PointType:
                    if (fieldInstance.Value != null)
                    {
                        if (field.FieldType == typeof(Vector2))
                        {
                            Vector2 vector = JsonConvert.DeserializeObject<LDtkPoint>(fieldInstance.Value.ToString());
                            field.SetValue(entity, vector);
                        }
                        else if (field.FieldType == typeof(Point))
                        {
                            Point point = JsonConvert.DeserializeObject<LDtkPoint>(fieldInstance.Value.ToString());
                            field.SetValue(entity, point);
                        }
                    }
                    else
                    {
                        if (field.FieldType == typeof(Vector2))
                        {
                            field.SetValue(entity, Vector2.Zero);
                        }
                        else if (field.FieldType == typeof(Point))
                        {
                            field.SetValue(entity, Point.Zero);
                        }
                    }
                    break;

                case Field.PointArrayType:
                    List<LDtkPoint> points = JsonConvert.DeserializeObject<List<LDtkPoint>>(fieldInstance.Value.ToString());

                    Vector2[] pointArray = new Vector2[fieldInstance.RealEditorValues.Length];

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

        internal static void ParseBaseField<T>(T entity, string field, object value)
        {
            // WorldPosition
            FieldInfo variable = typeof(T).GetField(field, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) ??
                    typeof(LDtkEntity).GetField(field, BindingFlags.Instance | BindingFlags.NonPublic);

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

#pragma warning disable CS0649
        private class LDtkPoint
        {
            [JsonProperty]
            public int cx;
            [JsonProperty]
            public int cy;
            public static implicit operator Vector2(LDtkPoint p)
            {
                return new Vector2(p.cx, p.cy);
            }

            public static implicit operator Point(LDtkPoint p)
            {
                return new Point(p.cx, p.cy);
            }
        }
#pragma warning restore CS0649

        /// <summary>
        /// Entity and Level Field
        /// </summary>
        private static class Field
        {
            public const string IntType = "Int";
            public const string IntArrayType = "Array<Int>";
            public const string FloatType = "Float";
            public const string FloatArrayType = "Array<Float>";
            public const string BoolType = "Bool";
            public const string BoolArrayType = "Array<Bool>";
            public const string EnumType = "Enum";
            public const string EnumArrayType = "Array<Enum>";
            public const string LocalEnumType = "LocalEnum";
            public const string LocalEnumArrayType = "Array<LocalEnum>";
            public const string StringType = "String";
            public const string StringArrayType = "Array<String>";
            public const string FilePathType = "FilePath";
            public const string FilePathArrayType = "Array<FilePath>";
            public const string ColorType = "Color";
            public const string ColorArrayType = "Array<Color>";
            public const string PointType = "Point";
            public const string PointArrayType = "Array<Point>";
        }
    }
}
