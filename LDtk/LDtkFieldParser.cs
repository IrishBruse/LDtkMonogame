using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using LDtk.Exceptions;
using Microsoft.Xna.Framework;

namespace LDtk
{
    /// <summary>
    /// Utility for parsing ldtk json data into more typed versions
    /// </summary>
    internal static class LDtkFieldParser
    {
        internal static Color ParseStringToColor(string hex)
        {
            return ParseStringToColor(hex, 255);
        }

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

        internal static void Parse<T>(T entity, FieldInstance fieldInstance) where T : new()
        {
            string variableName = fieldInstance._Identifier;

            var field = typeof(T).GetProperty(variableName);

            if (field == null)
            {
                throw new FieldInstanceException($"Error: Field \"{variableName}\" not found in {typeof(T).FullName}. Maybe you should run ldtkgen again to update the files?");
            }

            // Split any enums

            int enumTypeIndex = fieldInstance._Type.LastIndexOf('.');
            int arrayEndIndex = fieldInstance._Type.LastIndexOf('>');

            string variableType = fieldInstance._Type;

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
                    if (fieldInstance._Value != null)
                    {
                        field.SetValue(entity, Convert.ChangeType(fieldInstance._Value.ToString(), field.PropertyType));
                    }
                    break;

                case Field.IntArrayType:
                case Field.BoolArrayType:
                case Field.EnumArrayType:
                case Field.FloatArrayType:
                case Field.StringArrayType:
                case Field.FilePathArrayType:
                case Field.LocalEnumArrayType:
                    object primativeArrayValues = JsonSerializer.Deserialize(fieldInstance._Value.ToString(), field.PropertyType, new JsonSerializerOptions() { Converters = { new JsonStringEnumConverter() } });
                    field.SetValue(entity, Convert.ChangeType(primativeArrayValues, field.PropertyType));
                    break;

                case Field.LocalEnumType:
                    field.SetValue(entity, Enum.Parse(field.PropertyType, fieldInstance._Value.ToString()));
                    break;

                case Field.ColorType:
                    field.SetValue(entity, ParseStringToColor(fieldInstance._Value.ToString()));
                    break;

                case Field.PointType:
                    if (fieldInstance._Value != null)
                    {
                        if (field.PropertyType == typeof(Vector2))
                        {
                            Vector2 vector = (Vector2)fieldInstance._Value;
                            field.SetValue(entity, vector);
                        }
                        else if (field.PropertyType == typeof(Point))
                        {
                            Point point = JsonSerializer.Deserialize<Point>(fieldInstance._Value.ToString(), new JsonSerializerOptions() { Converters = { new CxCyConverter() } });
                            field.SetValue(entity, point);
                        }
                    }
                    else
                    {
                        if (field.PropertyType == typeof(Vector2))
                        {
                            field.SetValue(entity, Vector2.Zero);
                        }
                        else if (field.PropertyType == typeof(Point))
                        {
                            field.SetValue(entity, Point.Zero);
                        }
                    }
                    break;

                case Field.PointArrayType:
                    List<Point> points = JsonSerializer.Deserialize<List<Point>>(fieldInstance._Value.ToString(), new JsonSerializerOptions() { Converters = { new CxCyConverter() } });

                    field.SetValue(entity, points.ToArray());
                    break;

                default:
                    throw new FieldInstanceException("Unknown Variable of type " + fieldInstance._Type);
            }
        }

        internal static void ParseBaseField<T>(T entity, string field, object value)
        {
            // WorldPosition
            PropertyInfo variable = typeof(T).GetProperty(field, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (variable != null)
            {
                variable.SetValue(entity, value);
            }
            else
            {
#if DEBUG
                throw new Exception();
#endif
            }
        }

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
