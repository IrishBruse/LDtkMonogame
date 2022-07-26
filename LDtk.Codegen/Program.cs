namespace LDtk.Codegen;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using CommandLine;

using LDtk;
using LDtk.Codegen.Generators;

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
            Console.WriteLine("Supports LDtk version " + Constants.SupportedLDtkVersion);
            return;
        }

        _ = errs.Output();
    }

    private static void Run(Options options)
    {
        LDtkFile file = LDtkFile.FromFile(options.Input);

        if (!file.Flags.Contains(Flag.MultiWorlds))
        {
            Console.Error.WriteLine("LDtk Files must have the `MultiWorlds` flag enabled");
            return;
        }

        if (file.Flags.Contains(Flag.ExportPreCsvIntGridFormat))
        {
            Console.Error.WriteLine("LDtk Files must have the `ExportPreCsvIntGridFormat` flag disabled");
            return;
        }

        if (options.FileNameInNamespace)
        {
            options.Namespace += "." + Path.GetFileNameWithoutExtension(options.Input);
        }

        new ClassGenerator(file, options).Generate();
        new EnumGenerator(file, options).Generate();
        new IidGenerator(file, options).Generate();
    }
}

public class Options
{
    [Option('i', "input", Required = true, HelpText = "Input LDtk file (.ldtk)")]
    public string Input { get; set; }

    [Option('o', "output", Required = false, Default = "LDtkTypes/", HelpText = "The output folder")]
    public string Output { get; set; }

    [Option('n', "namespace", Required = false, Default = "LDtkTypes", HelpText = "Namespace to put the generated files into")]
    public string Namespace { get; set; }

    [Option("LevelClassName", Required = false, Default = "LDtkLevelData", HelpText = "The name to give the custom level file")]
    public string LevelClassName { get; set; }

    [Option("PointAsVector2", Required = false, Default = false, HelpText = "Convert any Point fields or Point[] to Vector2 or Vector2[]")]
    public bool PointAsVector2 { get; set; }

    [Option("FileNameInNamespace", Required = false, Default = false, HelpText = "Adds the file name of the world to the namespace eg 'Example.ldtk' will become 'namespace LDtkTypes.Example;'")]
    public bool FileNameInNamespace { get; set; }

    [Option("BlockScopeNamespace", Required = false, Default = false, HelpText = "Changes namespace to use block scoped namespace instead of newer c# file scoped namespace.`")]
    public bool BlockScopeNamespace { get; set; }
}
