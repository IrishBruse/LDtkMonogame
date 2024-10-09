namespace LDtk.Codegen;

using System;
using System.Collections.Generic;
using System.Linq;

using CommandLine;

using LDtk.Codegen.Generators;
using LDtk.Full;

public static class Program
{
    public static Options Options { get; private set; }
    public static void Main(string[] args)
    {
        _ = Parser.Default.ParseArguments<Options>(args).WithParsed(Run).WithNotParsed(HandleParseError);
    }

    static void HandleParseError(IEnumerable<Error> errs)
    {
        if (errs.IsVersion())
        {
            Console.WriteLine("Supports LDtk version " + Constants.SupportedLDtkVersion);
            return;
        }

        _ = errs.Output();
    }

    static void Run(Options opt)
    {
        Options = opt;

        LDtkFileFull file = LDtkFileFull.FromFile(Options.Input);

        if (Version.Parse(file.JsonVersion) > Version.Parse(Constants.SupportedLDtkVersion))
        {
            string value = $"LDtkMonogame supports {Constants.SupportedLDtkVersion} your file is on {file.JsonVersion} it\n";
            value += "is probably supported but new features may be missing please make an issue on github to remind me to update it :)";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        if (file == null)
        {
            Console.Error.WriteLine("Failed to load LDtk file");
            return;
        }

        if (file.Flags == null)
        {
            Console.Error.WriteLine("LDtk File has no flags");
            return;
        }

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

        new ClassGenerator(file, opt).Generate();
        new EnumGenerator(file, opt).Generate();
        new IidGenerator(file, opt).Generate();
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

    [Option("EntityInterface", Required = false, Default = true, HelpText = "Use the ILDtkEntity interface")]
    public bool EntityInterface { get; set; }

    [Option("DisableDefaultInstance", Required = false, Default = false, HelpText = "Disable generation of default entity instance static function")]
    public bool DisableDefaultInstance { get; set; }
}
