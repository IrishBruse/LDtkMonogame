using LDtk.Codegen.CompilationUnits;

namespace LDtk.Codegen.Core
{
    public class LdtkCodeCustomizer
    {
        public virtual void CustomizeEntity(CompilationUnitClass entity, EntityDefinition ed, LdtkGeneratorContext ctx)
        {
            entity.BaseClass = "ILDtkEntity";

            entity.Fields.Add(new CompilationUnitField("Uid", "long"));
            entity.Fields.Add(new CompilationUnitField("Identifier", "string"));
            entity.Fields.Add(new CompilationUnitField("Size", "Vector2"));
            entity.Fields.Add(new CompilationUnitField("Position", "Vector2"));
            entity.Fields.Add(new CompilationUnitField("Pivot", "Vector2"));
            entity.Fields.Add(new CompilationUnitField("Tile", "Rectangle"));
            entity.Fields.Add(new CompilationUnitField("EditorVisualColor", "Color"));
        }

        public virtual void CustomizeLevel(CompilationUnitClass level, LDtkWorld ldtkJson, LdtkGeneratorContext ctx)
        {
        }
    }
}
