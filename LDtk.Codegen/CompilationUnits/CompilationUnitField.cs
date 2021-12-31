using System.Globalization;

namespace LDtk.Codegen.CompilationUnits;

public class CompilationUnitField : CompilationUnitFragment
{
    public enum FieldVisibility
    {
        Private,
        Protected,
        Public,
    }

    public string RequiredImport { get; set; }
    public FieldVisibility? Visibility { get; set; }
    public string Type { get; set; }

    public CompilationUnitField()
    {
    }

    public CompilationUnitField(string name, string type, string requiredImport, FieldVisibility visibility)
    {
        Name = name;
        Type = type;
        Visibility = visibility;
        RequiredImport = requiredImport;
    }

    public CompilationUnitField(string name, string type)
    {
        Name = name;
        Type = type;
        Visibility = FieldVisibility.Public;
        RequiredImport = null;
    }

    public override void Render(CompilationUnitSource source)
    {
        if (RequiredImport != null)
        {
            source.Using(RequiredImport);
        }

        string vStr = "";
        if (Visibility.HasValue)
        {
            vStr = Visibility.GetValueOrDefault().ToString().ToLower(CultureInfo.InvariantCulture);
        }

        source.AddLine($"{vStr} {Type} {Name} {{ get; set; }}");
    }
}
