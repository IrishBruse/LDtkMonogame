namespace LDtk.Codegen.CompilationUnits
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

        public CompilationUnitField(string name, string type, string requiredImport, FieldVisibility visibility)
        {
            base.name = name;
            Type = type;
            Visibility = visibility;
            RequiredImport = requiredImport;
        }

        public CompilationUnitField(string name, string type)
        {
            base.name = name;
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
                vStr = Visibility.GetValueOrDefault().ToString().ToLower();
            }

            source.AddLine($"{vStr} {Type} {name} {{ get; set; }}");
        }
    }
}
