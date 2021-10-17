using System;
using System.ComponentModel;
using System.IO;
using LDtk.Generator;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace LDtk.ContentPipeline
{
    [ContentProcessor(DisplayName = "LDtk World Processor")]
    public class LDtkWorldProcessor : ContentProcessor<string, LDtkWorld>
    {
        [DisplayName("Generate Entities")]
        public bool GenerateEntities { get; set; } = true;

        [DisplayName("Worlds Entity Namespace")]
        public string LDtkNamespace { get; set; } = "LDtkTypes";


        [DisplayName("Level Class Name")]
        public string LevelClassName { get; set; } = "MyLevelClass";

        public override LDtkWorld Process(string input, ContentProcessorContext context)
        {
            LDtkWorld world;

            try
            {
                ContentLogger.Logger = context.Logger;
                ContentLogger.LogMessage($"Processing");

                world = System.Text.Json.JsonSerializer.Deserialize<LDtkWorld>(input, LDtkWorld.SerializeOptions);
            }
            catch (Exception ex)
            {
                context.Logger.LogImportantMessage(ex.Message);
                throw;
            }

            if (GenerateEntities)
            {
                try
                {
                    LdtkGeneratorContext ctx = new LdtkGeneratorContext();
                    ctx.LevelClassName = LevelClassName;
                    ctx.TypeConverter = new LdtkTypeConverter();
                    ctx.CodeSettings.Namespace = LDtkNamespace;

                    MultiFileOutput fOut = new MultiFileOutput();
                    fOut.OutputDir = Path.GetFullPath(context.OutputDirectory.Split("Content")[0] + "/" + LDtkNamespace);

                    LdtkCodeGenerator cg = new LdtkCodeGenerator();
                    cg.GenerateCode(world, ctx, fOut);

                    context.Logger.LogMessage("Generated ldtkTypes .cs files -> /" + LDtkNamespace);
                }
                catch (Exception ex)
                {
                    context.Logger.LogImportantMessage(ex.Message);
                    throw;
                }
            }

            return world;
        }
    }
}