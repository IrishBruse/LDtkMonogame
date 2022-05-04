namespace LDtk.Codegen;

using System;
using System.Collections.Generic;
using CommandLine;
using LDtk.Codegen.Generators;

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

        errs.Output();
    }

    static void Run(Options options)
    {
        LDtkFile file = LDtkFile.FromFile(options.Input);

        EnumGenerator enumGenerator = new(file, options);
        enumGenerator.Generate();

        ClassGenerator classGenerator = new(file, options);
        classGenerator.Generate();
    }
}

public class Options
{
    [Option('i', "input", Required = true, HelpText = "Input LDtk world file.")]
    public string Input { get; set; }

    [Option('o', "output", Required = false, Default = "LDtkTypes/", HelpText = "The output folder/file depending on if single file is set.")]
    public string Output { get; set; }

    [Option('n', "namespace", Required = false, Default = "LDtkTypes", HelpText = "Namespace to put the generated files into.")]
    public string Namespace { get; set; }

    [Option("LevelClassName", Required = false, Default = "LDtkLevelData", HelpText = "The name to give the custom level file.")]
    public string LevelClassName { get; set; }

    [Option("PointAsVector2", Required = false, Default = false, HelpText = "Convert any Point fields or Point[] to Vector2 or Vector2[]")]
    public bool PointAsVector2 { get; set; }
}
