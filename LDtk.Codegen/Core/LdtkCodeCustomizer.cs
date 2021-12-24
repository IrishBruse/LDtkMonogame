using LDtk.Codegen.CompilationUnits;

namespace LDtk.Codegen.Core;

public class LdtkCodeCustomizer
{
    public virtual void CustomizeEntity(CompilationUnitClass entity, EntityDefinition ed, LdtkGeneratorContext ctx)
    {
        entity.BaseClass = "ILDtkEntity";

        entity.fields.Add(new CompilationUnitField("Uid", "long"));
        entity.fields.Add(new CompilationUnitField("Identifier", "string"));
        entity.fields.Add(new CompilationUnitField("Size", "Vector2"));
        entity.fields.Add(new CompilationUnitField("Position", "Vector2"));
        entity.fields.Add(new CompilationUnitField("Pivot", "Vector2"));
        entity.fields.Add(new CompilationUnitField("Tile", "Rectangle"));
        entity.fields.Add(new CompilationUnitField("EditorVisualColor", "Color"));
    }

    public virtual void CustomizeLevel(CompilationUnitClass level, LDtkWorld ldtkJson, LdtkGeneratorContext ctx)
    {
    }
}
