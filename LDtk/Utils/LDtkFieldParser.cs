namespace LDtk;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Xna.Framework;

/// <summary> Utility for parsing ldtk json data into more typed versions </summary>
static class LDtkFieldParser
{
    public static void ParseCustomLevelFields<T>(T level, FieldInstance[] fields) where T : new()
    {
        ParseCustomFields(level, fields, null);
    }

    public static void ParseCustomEntityFields<T>(T entity, FieldInstance[] fields, LDtkLevel level) where T : new()
    {
        ParseCustomFields(entity, fields, level);
    }

    public static void ParseBaseEntityFields<T>(T entity, EntityInstance entityInstance, LDtkLevel level) where T : new()
    {
        ParseBaseField(entity, "Identifier", entityInstance._Identifier);
        ParseBaseField(entity, "Position", (entityInstance.Px + level.Position).ToVector2());
        ParseBaseField(entity, "Pivot", entityInstance._Pivot);
        ParseBaseField(entity, "Size", new Vector2(entityInstance.Width, entityInstance.Height));

        if (entityInstance._Tile != null)
        {
            TilesetRectangle tileDefinition = entityInstance._Tile;
            Rectangle rect = new(tileDefinition.X, tileDefinition.Y, tileDefinition.W, tileDefinition.H);
            ParseBaseField(entity, "Tile", rect);
        }
    }

    // Helpers
    static void ParseCustomFields<T>(T classFields, FieldInstance[] fields, LDtkLevel level)
    {
        for (int i = 0; i < fields.Length; i++)
        {
            FieldInstance fieldInstance = fields[i];
            string variableName = fieldInstance._Identifier;

            PropertyInfo variableDef = typeof(T).GetProperty(variableName);

            if (variableDef == null)
            {
                throw new LDtkException($"Error: Field \"{variableName}\" not found in {typeof(T).FullName}. Maybe you should run ldtkgen again to update the files?");
            }

            // Split any enums
            int enumTypeIndex = fieldInstance._Type.LastIndexOf('.');
            int arrayEndIndex = fieldInstance._Type.LastIndexOf('>');

            string variableType = fieldInstance._Type;

            if (enumTypeIndex != -1)
            {
                variableType = arrayEndIndex != -1 ? variableType.Remove(enumTypeIndex, arrayEndIndex - enumTypeIndex) : variableType.Remove(enumTypeIndex, variableType.Length - enumTypeIndex);
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
                    variableDef.SetValue(classFields, Convert.ChangeType(fieldInstance._Value.ToString(), variableDef.PropertyType));
                }
                break;

                case Field.IntArrayType:
                case Field.BoolArrayType:
                case Field.EnumArrayType:
                case Field.FloatArrayType:
                case Field.StringArrayType:
                case Field.FilePathArrayType:
                case Field.LocalEnumArrayType:
                object primativeArrayValues = JsonSerializer.Deserialize(fieldInstance._Value.ToString(), variableDef.PropertyType, new JsonSerializerOptions() { Converters = { new JsonStringEnumConverter() } });
                variableDef.SetValue(classFields, Convert.ChangeType(primativeArrayValues, variableDef.PropertyType));
                break;

                case Field.LocalEnumType:
                variableDef.SetValue(classFields, Enum.Parse(variableDef.PropertyType, fieldInstance._Value.ToString()));
                break;

                case Field.ColorType:
                variableDef.SetValue(classFields, ParseStringToColor(fieldInstance._Value.ToString()));
                break;

                // Only Entities can have point fields
                case Field.PointType:
                if (fieldInstance._Value != null)
                {
                    if (variableDef.PropertyType == typeof(Vector2))
                    {
                        Vector2 vector = (Vector2)fieldInstance._Value;
                        variableDef.SetValue(classFields, vector);
                    }
                    else if (variableDef.PropertyType == typeof(Point))
                    {
                        Point point = JsonSerializer.Deserialize<Point>(fieldInstance._Value.ToString(), new JsonSerializerOptions() { Converters = { new CxCyConverter() } });
                        variableDef.SetValue(classFields, point);
                    }
                }
                else
                {
                    if (variableDef.PropertyType == typeof(Vector2))
                    {
                        variableDef.SetValue(classFields, Vector2.Zero);
                    }
                    else if (variableDef.PropertyType == typeof(Point))
                    {
                        variableDef.SetValue(classFields, Point.Zero);
                    }
                }
                break;

                case Field.PointArrayType:
                List<Point> points = JsonSerializer.Deserialize<List<Point>>(fieldInstance._Value.ToString(), new JsonSerializerOptions() { Converters = { new CxCyConverter() } });

                int gridSize = 0;
                for (int j = 0; j < level.LayerInstances.Length; j++)
                {
                    if (level.LayerInstances[j]._Type == LayerType.Entities)
                    {
                        gridSize = level.LayerInstances[j]._GridSize;
                    }
                }

                for (int j = 0; j < points.Count; j++)
                {
                    points[j] = new Point(points[j].X * gridSize, points[j].Y * gridSize);
                    points[j] += level.Position;
                    points[j] += new Point(gridSize / 2);
                }

                variableDef.SetValue(classFields, points.ToArray());
                break;

                default:
                throw new LDtkException("Unknown Variable of type " + fieldInstance._Tile);
            }
        }
    }

    static void ParseBaseField<T>(T entity, string fieldName, object value)
    {
        PropertyInfo variableDef = typeof(T).GetProperty(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        if (variableDef == null)
        {
            throw new LDtkException($"Error: Field \"{variableDef.Name}\" not found in {typeof(T).FullName}. Maybe you should run ldtkgen again to update the files?");
        }

        variableDef.SetValue(entity, value);
    }

    static Color ParseStringToColor(string hex)
    {
        return ParseStringToColor(hex, 255);
    }

    static Color ParseStringToColor(string hex, int alpha)
    {
        if (uint.TryParse(hex.Replace("#", ""), NumberStyles.HexNumber, null, out uint color))
        {
            byte red = (byte)((color & 0xFF0000) >> 16);
            byte green = (byte)((color & 0x00FF00) >> 8);
            byte blue = (byte)(color & 0x0000FF);

            return new Color(red, green, blue, alpha);
        }
        else
        {
            return new Color(0xFF00FFFF);
        }
    }
}
