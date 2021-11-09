using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using CommandLine;
using LDtk.Generator;

namespace LDtk.Codegen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(Run).WithNotParsed(HandleParseError);
        }

        private static void HandleParseError(IEnumerable<Error> errs)
        {
            if (errs.IsVersion())
            {
                Console.WriteLine("Supports LDtk version " + Constants.LDtkMinVersionSupported);
                return;
            }
        }

        private static void Run(Options options)
        {
            LdtkGeneratorContext ctx = new LdtkGeneratorContext();
            ctx.LevelClassName = options.LevelClassName;
            ctx.TypeConverter = new LdtkTypeConverter();
            ctx.CodeSettings.Namespace = options.Namespace;

            ICodeOutput output;

            Console.WriteLine(Path.GetFileNameWithoutExtension(options.Input));

            if (options.SingleFile)
            {
                var singleFileOutput = new SingleFileOutput();
                singleFileOutput.OutputDir = Path.GetDirectoryName(Path.GetFullPath(options.Output));
                singleFileOutput.Filename = Path.GetFileNameWithoutExtension(options.Input);
                output = singleFileOutput;
            }
            else
            {
                var multiFileOutput = new MultiFileOutput();
                multiFileOutput.PrintFragments = true;
                multiFileOutput.OutputDir = Path.GetDirectoryName(Path.GetFullPath(options.Input));
                output = multiFileOutput;
            }

            LDtkWorld ldtkWorld = JsonSerializer.Deserialize<LDtkWorld>(File.ReadAllText(options.Input), LDtkWorld.SerializeOptions);

            LdtkCodeGenerator cg = new LdtkCodeGenerator();
            cg.GenerateCode(ldtkWorld, ctx, output);
        }
    }

    class Options
    {
        [Option('i', "input", Required = true, HelpText = "Input LDtk world file.")]
        public string Input { get; set; }

        [Option('o', "output", Required = false, Default = "LDtkTypes/", HelpText = "The output folder/file depending on if single file is set.")]
        public string Output { get; set; }

        [Option('n', "namespace", Required = false, Default = "LDtkTypes", HelpText = "Namespace to put the generated files into.")]
        public string Namespace { get; set; }

        [Option("levelclassname", Required = false, Default = "LDtkLevelData", HelpText = "The name to give the custom level file.")]
        public string LevelClassName { get; set; }

        [Option("singlefile", Required = false, Default = false, HelpText = "Output all the LDtk files into a single file.")]
        public bool SingleFile { get; set; }
    }
}
