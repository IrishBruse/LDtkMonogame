namespace LDtk;

using System;
using System.Reflection;
using System.Text.Json;
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

    static void ParseCustomFields<T>(T classFields, FieldInstance[] fields, LDtkLevel level)
    {
        foreach (FieldInstance field in fields)
        {
            string variableName = field._Identifier;
            PropertyInfo variableDef = typeof(T).GetProperty(variableName);

            if (field._Value == null)
            {
                continue;
            }

            JsonElement element = (JsonElement)field._Value;

            if (field._Type.Contains(Field.PointType))
            {
                HandlePoints(classFields, level, field, variableDef, element);
            }
            else
            {
                switch (element.ValueKind)
                {
                    case JsonValueKind.Object:
                    case JsonValueKind.Array:
                    case JsonValueKind.Number:
                    variableDef.SetValue(classFields, JsonSerializer.Deserialize(element.ToString(), variableDef.PropertyType, Constants.SerializeOptions));
                    break;

                    case JsonValueKind.String:
                    bool isEnum = field._Type.Split('.')[0].Contains("Enum");

                    if (isEnum)
                    {
                        Type t = Nullable.GetUnderlyingType(variableDef.PropertyType) ?? variableDef.PropertyType;
                        variableDef.SetValue(classFields, Enum.Parse(t, element.ToString()));
                    }
                    else
                    {
                        variableDef.SetValue(classFields, element.ToString());
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

                    default:
                    case JsonValueKind.Undefined:
                    throw new LDtkException("Oops");
                }
            }
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
                Point[] points = JsonSerializer.Deserialize<Point[]>(element.ToString(), Constants.SerializeOptions);
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
                Vector2[] points = JsonSerializer.Deserialize<Vector2[]>(element.ToString(), Constants.SerializeOptions);
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
        for (int j = 0; j < level.LayerInstances.Length; j++)
        {
            if (level.LayerInstances[j]._Type == LayerType.Entities)
            {
                gridSize = level.LayerInstances[j]._GridSize;
            }
        }

        return gridSize;
    }

    public static void ParseBaseEntityFields<T>(T entity, EntityInstance entityInstance, LDtkLevel level) where T : new()
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

    // Helpers
    static void ParseBaseField<T>(T entity, string fieldName, object value)
    {
        PropertyInfo variableDef = typeof(T).GetProperty(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        if (variableDef == null)
        {
            throw new LDtkException($"Error: Field \"{variableDef.Name}\" not found in {typeof(T).FullName}. Maybe you should run ldtkgen again to update the files?");
        }

        variableDef.SetValue(entity, value);
    }

    class CxCyPoint
    {
        public int Cx { get; set; }
        public int Cy { get; set; }
    }
}
