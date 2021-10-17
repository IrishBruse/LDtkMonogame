#pragma warning disable CS1591
namespace LDtk.Generator
{
    public class CompilationUnitField : CompilationUnitFragment
    {
        public enum FieldVisibility
        {
            Private,
            Protected,
            Public,
        }

        public string Type;
        public string RequiredImport;
        public FieldVisibility? Visibility;

        public CompilationUnitField()
        {

        }

        public CompilationUnitField(string name, string type, string requiredImport = null, FieldVisibility visibility = FieldVisibility.Public)
        {
            Name = name;
            Type = type;
            Visibility = visibility;
            RequiredImport = requiredImport;
        }
        public override void Render(CompilationUnitSource source)
        {
            if (RequiredImport != null)
                source.Using(RequiredImport);

            string vStr = "";
            if (Visibility.HasValue)
            {
                vStr = Visibility.GetValueOrDefault().ToString().ToLower();
            }

            source.AddLine($"{vStr} {Type} {Name} {{ get; set; }}");
        }
    }
}
#pragma warning restore CS1591