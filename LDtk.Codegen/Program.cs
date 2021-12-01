using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using CommandLine;
using LDtk.Codegen.Core;
using LDtk.Codegen.Outputs;

namespace LDtk.Codegen;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("-- LDtk Codegen --");
        _ = Parser.Default.ParseArguments<Options>(args).WithParsed(Run).WithNotParsed(HandleParseError);
    }

    private static void HandleParseError(IEnumerable<Error> errs)
    {
        if (errs.IsVersion())
        {
            Console.WriteLine("Supports LDtk version " + Constants.supportedLDtkVersion);
            return;
        }
    }

    private static void Run(Options options)
    {
        string outputDirectory = Path.GetDirectoryName(Path.GetFullPath(options.Output));

        _ = Directory.CreateDirectory(outputDirectory);

        string[] files = Directory.GetFiles(outputDirectory);

        for (int i = 0; i < files.Length; i++)
        {
            File.Delete(files[i]);
        }

        LdtkTypeConverter typeConverter = new LdtkTypeConverter()
        {
            PointAsVector2 = options.PointAsVector2
        };

        LdtkGeneratorContext ctx = new LdtkGeneratorContext()
        {
            LevelClassName = options.LevelClassName,
            TypeConverter = typeConverter
        };
        ctx.CodeSettings.Namespace = options.Namespace;

        ICodeOutput output;

        if (options.SingleFile)
        {
            SingleFileOutput singleFileOutput = new()
            {
                OutputDir = outputDirectory,
                Filename = Path.GetFileNameWithoutExtension(options.Input)
            };
            output = singleFileOutput;
        }
        else
        {
            MultiFileOutput multiFileOutput = new()
            {
                PrintFragments = true,
                OutputDir = outputDirectory,
            };
            output = multiFileOutput;
        }

        LDtkWorld ldtkWorld = JsonSerializer.Deserialize<LDtkWorld>(File.ReadAllText(options.Input), LDtkWorld.SerializeOptions);

        LdtkCodeGenerator cg = new LdtkCodeGenerator();
        cg.GenerateCode(ldtkWorld, ctx, output);
    }
}

internal class Options
{
    [Option('i', "input", Required = true, HelpText = "Input LDtk world file.")]
    public string Input { get; set; }

    [Option('o', "output", Required = false, Default = "LDtkTypes/", HelpText = "The output folder/file depending on if single file is set.")]
    public string Output { get; set; }

    [Option('n', "namespace", Required = false, Default = "LDtkTypes", HelpText = "Namespace to put the generated files into.")]
    public string Namespace { get; set; }

    [Option("LevelClassName", Required = false, Default = "LDtkLevelData", HelpText = "The name to give the custom level file.")]
    public string LevelClassName { get; set; }

    [Option("SingleFile", Required = false, Default = false, HelpText = "Output all the LDtk files into a single file.")]
    public bool SingleFile { get; set; }

    [Option("PointAsVector2", Required = false, Default = false, HelpText = "Convert any Point fields or Point[] to Vector2 or Vector2[]")]
    public bool PointAsVector2 { get; set; }
}
