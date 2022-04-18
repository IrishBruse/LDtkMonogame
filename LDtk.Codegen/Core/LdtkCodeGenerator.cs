namespace LDtk.Codegen.Core;

using System.Collections.Generic;
using LDtk.Codegen.CompilationUnits;
using LDtk.Codegen.Outputs;

public class LdtkCodeGenerator
{
    public virtual void GenerateCode(LDtkWorld ldtkJson, LdtkGeneratorContext ctx, ICodeOutput output)
    {
        List<CompilationUnitFragment> fragments = new();

        foreach (EnumDefinition ed in ldtkJson.Defs.Enums)
        {
            fragments.Add(GenerateEnum(ed, ctx));
        }

        foreach (EntityDefinition ed in ldtkJson.Defs.Entities)
        {
            ClassCompilationUnit entity = GenerateEntity(ed, ctx);
            fragments.Add(entity);

            if (ctx.CodeCustomizer != null)
            {
                ctx.CodeCustomizer.CustomizeEntity(entity, ed, ctx);
            }
        }

        ClassCompilationUnit level = GenerateLevel(ldtkJson, ctx);
        fragments.Add(level);

        if (ctx.CodeCustomizer != null)
        {
            ctx.CodeCustomizer.CustomizeLevel(level, ldtkJson, ctx);
        }

        output.OutputCode(fragments, ctx);
    }

    public virtual EnumCompilationUnit GenerateEnum(EnumDefinition enumDefinition, LdtkGeneratorContext ctx)
    {
        EnumCompilationUnit enumFragment = new()
        {
            Name = enumDefinition.Identifier
        };

        foreach (EnumValueDefinition evd in enumDefinition.Values)
        {
            enumFragment.Literals.Add(evd.Id);
        }

        return enumFragment;
    }

    public virtual ClassCompilationUnit GenerateEntity(EntityDefinition ed, LdtkGeneratorContext ctx)
    {
        ClassCompilationUnit classFragment = new()
        {
            Name = ed.Identifier
        };

        foreach (FieldDefinition fd in ed.FieldDefs)
        {
            classFragment.Fields.Add(ctx.TypeConverter.ToCompilationUnitField(fd, ctx));
        }

        return classFragment;
    }

    public virtual ClassCompilationUnit GenerateLevel(LDtkWorld ldtkJson, LdtkGeneratorContext ctx)
    {
        ClassCompilationUnit levelClass = new()
        {
            Name = ctx.LevelClassName
        };

        foreach (FieldDefinition fd in ldtkJson.Defs.LevelFields)
        {
            levelClass.Fields.Add(ctx.TypeConverter.ToCompilationUnitField(fd, ctx));
        }

        return levelClass;
    }
}
