namespace LDtk.Codegen;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using CommandLine;
using LDtk.Codegen.Core;
using LDtk.Codegen.Outputs;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("-- LDtk Codegen --");
        Parser.Default.ParseArguments<Options>(args).WithParsed(Run).WithNotParsed(HandleParseError);
    }

    static void HandleParseError(IEnumerable<Error> errs)
    {
        if (errs.IsVersion())
        {
            Console.WriteLine("Supports LDtk version " + Constants.SupportedLDtkVersion);
            return;
        }
    }

    static void Run(Options options)
    {
        string outputDirectory = Path.GetDirectoryName(Path.GetFullPath(options.Output));

        Directory.CreateDirectory(outputDirectory);

        string[] files = Directory.GetFiles(outputDirectory);

        for (int i = 0; i < files.Length; i++)
        {
            File.Delete(files[i]);
        }

        LdtkTypeConverter typeConverter = new()
        {
            PointAsVector2 = options.PointAsVector2
        };

        LdtkGeneratorContext ctx = new()
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

        LdtkCodeGenerator cg = new();
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

    [Option("LevelClassName", Required = false, Default = "LDtkLevelData", HelpText = "The name to give the custom level file.")]
    public string LevelClassName { get; set; }

    [Option("SingleFile", Required = false, Default = false, HelpText = "Output all the LDtk files into a single file.")]
    public bool SingleFile { get; set; }

    [Option("PointAsVector2", Required = false, Default = false, HelpText = "Convert any Point fields or Point[] to Vector2 or Vector2[]")]
    public bool PointAsVector2 { get; set; }
}
