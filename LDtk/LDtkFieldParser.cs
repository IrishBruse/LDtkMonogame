using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using LDtk.Exceptions;
using Microsoft.Xna.Framework;

namespace LDtk
{
    /// <summary>
    /// Utility for parsing ldtk json data into more typed versions
    /// </summary>
    internal static class LDtkFieldParser
    {
        internal static void Parse<T>(T entity, FieldInstance fieldInstance) where T : new()
        {
            string variableName = fieldInstance._Identifier;

            // make the first letter lowercase
            variableName = char.ToLower(variableName[0]) + variableName[1..];

            var field = typeof(T).GetProperty(variableName);

            if (field == null)
            {
                throw new FieldInstanceException($"Error: Field \"{variableName}\" not found in {typeof(T).FullName}");
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
                    field.SetValue(entity, Convert.ChangeType(fieldInstance._Value.ToString(), field.PropertyType));
                    break;

                case Field.IntArrayType:
                case Field.BoolArrayType:
                case Field.EnumArrayType:
                case Field.FloatArrayType:
                case Field.StringArrayType:
                case Field.FilePathArrayType:
                case Field.LocalEnumArrayType:
                    object primativeArrayValues = JsonSerializer.Deserialize(fieldInstance._Value.ToString(), field.PropertyType);
                    field.SetValue(entity, Convert.ChangeType(primativeArrayValues, field.PropertyType));
                    break;

                case Field.LocalEnumType:
                    field.SetValue(entity, Enum.Parse(field.PropertyType, (string)fieldInstance._Value));
                    break;

                case Field.ColorType:
                    field.SetValue(entity, fieldInstance._Value);
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
                            Point point = (Point)fieldInstance._Value;
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
                    List<Point> points = JsonSerializer.Deserialize<List<Point>>(fieldInstance._Value.ToString());

                    field.SetValue(entity, points);
                    break;

                default:
                    throw new FieldInstanceException("Unknown Variable of type " + fieldInstance._Type);
            }
        }

        internal static void ParseBaseField<T>(T entity, string field, object value)
        {
            // WorldPosition
            FieldInfo variable = typeof(T).GetField(field, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

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
