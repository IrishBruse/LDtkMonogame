namespace LDtk;

using System;
using System.Globalization;
using System.Reflection;
using System.Text.Json;

using Microsoft.Xna.Framework;


/// <summary> Utility for parsing ldtk json data into more typed versions. </summary>
static class LDtkFieldParser
{
    /// <summary> Parses the custom level fields. </summary>
    /// <typeparam name="T"> Generic type parameter. </typeparam>
    /// <param name="level"> The level. </param>
    /// <param name="fields"> The fields. </param>
    public static void ParseCustomLevelFields<T>(T level, FieldInstance[] fields)
        where T : new()
    {
        ParseCustomFields(level, fields, null);
    }

    /// <summary> Parses the custom fields. </summary>
    /// <typeparam name="T"> Generic type parameter. </typeparam>
    /// <param name="entity"> The entity. </param>
    /// <param name="fields"> The fields. </param>
    /// <param name="level"> The level. </param>
    public static void ParseCustomEntityFields<T>(T entity, FieldInstance[] fields, LDtkLevel level)
        where T : new()
    {
        ParseCustomFields(entity, fields, level);
    }

    /// <summary> Parses the base entity fields. </summary>
    /// <typeparam name="T"> Generic type parameter. </typeparam>
    /// <param name="entity"> The entity. </param>
    /// <param name="entityInstance"> The entity instance. </param>
    /// <param name="level"> The level. </param>
    public static void ParseBaseEntityFields<T>(T entity, EntityInstance entityInstance, LDtkLevel level)
        where T : new()
    {
        ParseBaseField(entity, nameof(ILDtkEntity.Uid), entityInstance.DefUid);
        ParseBaseField(entity, nameof(ILDtkEntity.Iid), entityInstance.Iid);
        ParseBaseField(entity, nameof(ILDtkEntity.Identifier), entityInstance._Identifier);
        ParseBaseField(entity, nameof(ILDtkEntity.Position), (entityInstance.Px + level.Position).ToVector2());
        ParseBaseField(entity, nameof(ILDtkEntity.Pivot), entityInstance._Pivot);
        ParseBaseField(entity, nameof(ILDtkEntity.Size), new Vector2(entityInstance.Width, entityInstance.Height));
        ParseBaseField(entity, nameof(ILDtkEntity.SmartColor), entityInstance._SmartColor);

        if (entityInstance._Tile != null)
        {
            TilesetRectangle tileDefinition = entityInstance._Tile;
            Rectangle rect = new(tileDefinition.X, tileDefinition.Y, tileDefinition.W, tileDefinition.H);
            ParseBaseField(entity, nameof(ILDtkEntity.Tile), rect);
        }
    }

    static void ParseCustomFields<T>(T classFields, FieldInstance[] fields, LDtkLevel? level)
    {
        foreach (FieldInstance field in fields)
        {
            string variableName = field._Identifier;
            PropertyInfo? variableDef = typeof(T).GetProperty(variableName);

            if (variableDef == null)
            {
                throw new LDtkException($"Field {variableName} does not exist on {typeof(T).Name}");
            }

            if (field._Value.ValueKind == JsonValueKind.Null)
            {
                continue;
            }

            JsonElement value = field._Value;

            if (level != null && field._Type.Contains(Field.PointType))
            {
                HandlePoints(classFields, level, field, variableDef, value);
            }
            else
            {
                switch (value.ValueKind)
                {
                    case JsonValueKind.Object:
                    case JsonValueKind.Array:
                    case JsonValueKind.Number:
                    Type returnType = Nullable.GetUnderlyingType(variableDef.PropertyType) ?? variableDef.PropertyType;
                    variableDef.SetValue(classFields, JsonSerializer.Deserialize(value.ToString(), returnType, Constants.SerializeOptions));
                    break;

                    case JsonValueKind.String:
                    Type t = Nullable.GetUnderlyingType(variableDef.PropertyType) ?? variableDef.PropertyType;
                    bool isEnum = field._Type.Split('.')[0].Contains("Enum");
                    bool isColor = field._Type.Split('.')[0].Contains("Color");

                    if (isEnum)
                    {
                        variableDef.SetValue(classFields, Enum.Parse(t, value.ToString()));
                    }
                    else if (isColor)
                    {
                        variableDef.SetValue(classFields, ParseStringToColor(field._Value.ToString()!, 255));
                    }
                    else
                    {
                        variableDef.SetValue(classFields, value.ToString());
                    }

                    break;

                    case JsonValueKind.True:
                    variableDef.SetValue(classFields, true);
                    break;

                    case JsonValueKind.False:
                    variableDef.SetValue(classFields, false);
                    break;

                    case JsonValueKind.Null:
                    variableDef.SetValue(classFields, null);
                    break;

                    case JsonValueKind.Undefined:
                    throw new LDtkException("Field Undefined");

                    default:
                    throw new LDtkException("Unknown FieldKind");

                }
            }
        }
    }

    static Color ParseStringToColor(string hex, int alpha)
    {
        if (uint.TryParse(hex.Replace("#", string.Empty), NumberStyles.HexNumber, null, out uint color))
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

    static void HandlePoints<T>(T classFields, LDtkLevel level, FieldInstance field, PropertyInfo variableDef, JsonElement element)
    {
        int gridSize = GetGridSize(level);

        if (field._Type == Field.PointType)
        {
            if (variableDef.PropertyType == typeof(Point))
            {
                Point point = JsonSerializer.Deserialize<Point>(element.ToString(), Constants.SerializeOptions);
                point *= new Point(gridSize, gridSize);
                point += level.Position;
                point += new Point(gridSize / 2);
                variableDef.SetValue(classFields, point);
            }
            else
            {
                Vector2 point = JsonSerializer.Deserialize<Vector2>(element.ToString(), Constants.SerializeOptions);
                point *= new Vector2(gridSize, gridSize);
                point += level.Position.ToVector2();
                point += new Vector2(gridSize / 2);
                variableDef.SetValue(classFields, point);
            }
        }
        else if (field._Type == Field.PointArrayType)
        {
            if (variableDef.PropertyType.GetElementType() == typeof(Point))
            {
                Point[] points = JsonSerializer.Deserialize<Point[]>(element.ToString(), Constants.SerializeOptions)!;
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] *= new Point(gridSize, gridSize);
                    points[i] += level.Position;
                    points[i] += new Point(gridSize / 2);
                }

                variableDef.SetValue(classFields, points);
            }
            else
            {
                Vector2[] points = JsonSerializer.Deserialize<Vector2[]>(element.ToString(), Constants.SerializeOptions) ?? [];
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] *= new Vector2(gridSize, gridSize);
                    points[i] += level.Position.ToVector2();
                    points[i] += new Vector2(gridSize / 2);
                }

                variableDef.SetValue(classFields, points);
            }
        }
    }

    static int GetGridSize(LDtkLevel level)
    {
        int gridSize = 0;
        for (int j = 0; j < level.LayerInstances?.Length; j++)
        {
            if (level.LayerInstances[j]._Type == LayerType.Entities)
            {
                gridSize = level.LayerInstances[j]._GridSize;
            }
        }

        return gridSize;
    }

    // Helpers
    static void ParseBaseField<T>(T entity, string fieldName, object value)
    {
        PropertyInfo? variableDef = typeof(T).GetProperty(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        if (variableDef == null)
        {
            throw new LDtkException($"Error: Field \"{fieldName}\" not found in {typeof(T).FullName}. Maybe you should run ldtkgen again to update the files?");
        }

        variableDef.SetValue(entity, value);
    }
}
