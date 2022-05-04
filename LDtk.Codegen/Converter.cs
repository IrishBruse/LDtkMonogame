namespace LDtk.Codegen;

public static class Converter
{
    public static string ConvertFieldDefinitionTypes(string input)
    {
        input = input.Replace("LocalEnum.", "");
        input = input.Replace("ExternEnum.", "");

        int lessThan = input.IndexOf('<');
        int greaterThan = input.IndexOf('>');

        if (lessThan != -1)
        {
            return TypeConversion(input[(lessThan + 1)..greaterThan]) + "[]";
        }
        else
        {
            return TypeConversion(input);
        }
    }

    static string TypeConversion(string input)
    {
        return input switch
        {
            "Int" => "int",
            "String" => "string",
            "FilePath" => "string",
            "Float" => "float",
            "Bool" => "bool",
            "F_Point" => "Vector2",
            "EntityRef" => "FieldInstanceEntityReference",
            _ => input,
        };
    }
}
