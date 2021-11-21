using System.Collections.Generic;

namespace LDtk.Codegen;

public class LdtkCodeGenerator
{
    public virtual void GenerateCode(LDtkWorld ldtkJson, LdtkGeneratorContext ctx, ICodeOutput output)
    {
        List<CompilationUnitFragment> fragments = new List<CompilationUnitFragment>();

        foreach (EnumDefinition ed in ldtkJson.Defs.Enums)
        {
            fragments.Add(GenerateEnum(ed, ctx));
        }

        foreach (EntityDefinition ed in ldtkJson.Defs.Entities)
        {
            CompilationUnitClass entity = GenerateEntity(ed, ctx);
            fragments.Add(entity);

            if (ctx.CodeCustomizer != null)
            {
                ctx.CodeCustomizer.CustomizeEntity(entity, ed, ctx);
            }
        }

        CompilationUnitClass level = GenerateLevel(ldtkJson, ctx);
        fragments.Add(level);

        if (ctx.CodeCustomizer != null)
        {
            ctx.CodeCustomizer.CustomizeLevel(level, ldtkJson, ctx);
        }

        output.OutputCode(fragments, ctx);
    }

    public virtual CompilationUnitEnum GenerateEnum(EnumDefinition enumDefinition, LdtkGeneratorContext ctx)
    {
        CompilationUnitEnum enumFragment = new CompilationUnitEnum
        {
            name = enumDefinition.Identifier
        };

        foreach (EnumValueDefinition evd in enumDefinition.Values)
        {
            enumFragment.Literals.Add(evd.Id);
        }

        return enumFragment;
    }

    public virtual CompilationUnitClass GenerateEntity(EntityDefinition ed, LdtkGeneratorContext ctx)
    {
        CompilationUnitClass classFragment = new CompilationUnitClass
        {
            name = ed.Identifier
        };

        foreach (FieldDefinition fd in ed.FieldDefs)
        {
            classFragment.Fields.Add(ctx.TypeConverter.ToCompilationUnitField(fd, ctx));
        }

        return classFragment;
    }

    public virtual CompilationUnitClass GenerateLevel(LDtkWorld ldtkJson, LdtkGeneratorContext ctx)
    {
        CompilationUnitClass levelClass = new CompilationUnitClass
        {
            name = ctx.LevelClassName
        };

        foreach (FieldDefinition fd in ldtkJson.Defs.LevelFields)
        {
            levelClass.Fields.Add(ctx.TypeConverter.ToCompilationUnitField(fd, ctx));
        }

        return levelClass;
    }
}
