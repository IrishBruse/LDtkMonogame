#pragma warning disable CS1591
namespace LDtk.Generator
{
    public class LdtkCodeCustomizer
    {

        public virtual void CustomizeEntity(CompilationUnitClass entity, EntityDefinition ed, LdtkGeneratorContext ctx)
        {
            entity.Fields.Add(new CompilationUnitField("Uid", "long"));
            entity.Fields.Add(new CompilationUnitField("Identifier", "string"));
            entity.Fields.Add(new CompilationUnitField("Size", "Vector2"));
            entity.Fields.Add(new CompilationUnitField("Position", "Point"));
            entity.Fields.Add(new CompilationUnitField("Pivot", "Vector2"));
        }

        public virtual void CustomizeLevel(CompilationUnitClass level, LDtkWorld ldtkJson, LdtkGeneratorContext ctx)
        {
        }

    }
}
#pragma warning restore CS1591