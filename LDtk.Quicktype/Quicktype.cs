namespace QuickTypeGenerator;

using System.Diagnostics;
using System.IO;

public static class Quicktype
{
    public static readonly string MinimalFilePath = Path.GetTempPath() + "LDtkJson.cs";
    public static readonly string FullFilePath = Path.GetTempPath() + "LDtkJsonFull.cs";

    public static void Generate()
    {
        string[] minimalArgs = {
            "--lang cs",
            "--src https://raw.githubusercontent.com/deepnight/ldtk/master/docs/MINIMAL_JSON_SCHEMA.json",
            "-s schema",
            "-o", MinimalFilePath,
            "-t LDtkFile",
            "--features just-types",
            "--namespace LDtk"
        };

        Process minimal = Process.Start(new ProcessStartInfo
        {
            FileName = "quicktype.cmd",
            Arguments = string.Join(" ", minimalArgs),
            UseShellExecute = false,
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            RedirectStandardInput = false,
            CreateNoWindow = true
        });
        minimal.Start();
        minimal.WaitForExit();

        string[] fullArgs = {
            "--lang cs",
            "--src https://raw.githubusercontent.com/deepnight/ldtk/master/docs/JSON_SCHEMA.json",
            "-s schema",
            "-o", FullFilePath,
            "-t LDtkFile",
            "--features just-types",
            "--namespace LDtk.Codegen"
        };

        Process full = Process.Start(new ProcessStartInfo
        {
            FileName = "quicktype.cmd",
            Arguments = string.Join(" ", fullArgs),
            UseShellExecute = false,
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            RedirectStandardInput = false,
            CreateNoWindow = true
        });
        full.Start();
        full.WaitForExit();
    }
}
