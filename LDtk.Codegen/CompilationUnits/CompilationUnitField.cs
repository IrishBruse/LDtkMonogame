namespace LDtk.Codegen.CompilationUnits;

public class CompilationUnitField : CompilationUnitFragment
{
    public enum FieldVisibility
    {
        Private,
        Protected,
        Public,
    }

    public string type;
    public string requiredImport;
    public FieldVisibility? visibility;

    public CompilationUnitField()
    {
    }

    public CompilationUnitField(string name, string type, string requiredImport, FieldVisibility visibility)
    {
        base.name = name;
        this.type = type;
        this.visibility = visibility;
        this.requiredImport = requiredImport;
    }

    public CompilationUnitField(string name, string type)
    {
        base.name = name;
        this.type = type;
        visibility = FieldVisibility.Public;
        requiredImport = null;
    }

    public override void Render(CompilationUnitSource source)
    {
        if (requiredImport != null)
        {
            source.Using(requiredImport);
        }

        string vStr = "";
        if (visibility.HasValue)
        {
            vStr = visibility.GetValueOrDefault().ToString().ToLower();
        }

        source.AddLine($"{vStr} {type} {name} {{ get; set; }}");
    }
}
