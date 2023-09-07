namespace LDtk.Codegen;

public static class Converter
{
    public static string ConvertFieldDefinitionTypes(string input, bool pointAsVector)
    {
        input = input.Replace("LocalEnum.", "");
        input = input.Replace("ExternEnum.", "");

        int lessThan = input.IndexOf('<');
        int greaterThan = input.IndexOf('>');

        string type;

        if (lessThan != -1)
        {
            type = TypeConversion(input[(lessThan + 1)..greaterThan]) + "[]";
        }
        else
        {
            type = TypeConversion(input);
        }

        if (type.Contains("Point") && pointAsVector)
        {
            type = type.Replace("Point", "Vector2");
        }

        return type;
    }

    static string TypeConversion(string input) => input switch
    {
        "Int" => "int",
        "String" => "string",
        "FilePath" => "string",
        "Multilines" => "string",
        "Float" => "float",
        "Bool" => "bool",
        "Tile" => "TilesetRectangle",
        _ => input,
    };
}
