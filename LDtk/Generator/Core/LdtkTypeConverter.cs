#pragma warning disable CS1591
namespace LDtk.Generator
{
    public class LdtkTypeConverter
    {
        public virtual string GetArrayImport()
        {
            return null;
        }

        protected string GetCSharpTypeFor(string ldtkType)
        {
            if (ldtkType.StartsWith("LocalEnum"))
            {
                return ldtkType[10..];
            }

            return ldtkType switch
            {
                "Int" => "int",
                "Float" => "float",
                "Bool" => "bool",
                "Point" => "float[]",
                "Color" => "int[]",
                _ => "string",
            };
        }

        public virtual string GetDeclaringTypeFor(FieldDefinition fieldDefinition, LdtkGeneratorContext ctx)
        {
            string baseType = fieldDefinition.Type;
            if (fieldDefinition.IsArray)
                baseType = baseType[6..^1];

            string declType = GetCSharpTypeFor(baseType);

            if (fieldDefinition.IsArray)
                declType += "[]";

            return declType;
        }

        public CompilationUnitField ToCompilationUnitField(FieldDefinition fieldDefinition, LdtkGeneratorContext ctx)
        {
            CompilationUnitField field = new CompilationUnitField();
            field.Name = fieldDefinition.Identifier;
            field.Type = GetDeclaringTypeFor(fieldDefinition, ctx);
            field.Visibility = CompilationUnitField.FieldVisibility.Public;

            if (fieldDefinition.IsArray)
                field.RequiredImport = GetArrayImport();

            return field;
        }
    }
}
#pragma warning restore CS1591