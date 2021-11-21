#pragma warning disable IDE0057

namespace LDtk.Codegen;
public class LdtkTypeConverter
{
    public bool PointAsVector2 { get; set; }
    public virtual string GetArrayImport()
    {
        return null;
    }

    protected static string GetCSharpTypeFor(string ldtkType)
    {
        if (ldtkType.StartsWith("LocalEnum"))
        {
            return ldtkType.Substring(10);
        }

        return ldtkType switch
        {
            "Int" => "int",
            "Float" => "float",
            "Bool" => "bool",
            "Point" => "Point",
            "Color" => "Color",
            _ => "string",
        };
    }

    public virtual string GetDeclaringTypeFor(FieldDefinition fieldDefinition, LdtkGeneratorContext ctx)
    {
        string baseType = fieldDefinition.Type;
        if (fieldDefinition.IsArray)
            baseType = baseType.Substring(6, baseType.Length - 1);

        string declType = GetCSharpTypeFor(baseType);

        if (declType == "Point" && PointAsVector2)
        {
            declType = "Vector2";
        }

        if (fieldDefinition.IsArray)
            declType += "[]";

        return declType;
    }

    public CompilationUnitField ToCompilationUnitField(FieldDefinition fieldDefinition, LdtkGeneratorContext ctx)
    {
        CompilationUnitField field = new CompilationUnitField
        {
            name = fieldDefinition.Identifier,
            Type = GetDeclaringTypeFor(fieldDefinition, ctx),
            Visibility = CompilationUnitField.FieldVisibility.Public
        };

        if (fieldDefinition.IsArray)
            field.RequiredImport = GetArrayImport();

        return field;
    }
}
#pragma warning restore IDE0057