namespace LDtk.Codegen;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using CommandLine;

using LDtk;
using LDtk.Codegen.Generators;

public static class Program
{
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

    static void Run(Options options)
    {
        LDtkFile file = LDtkFile.FromFile(options.Input);

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
