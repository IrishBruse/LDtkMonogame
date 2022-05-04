namespace QuickTypeGenerator;

using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;

public static class Quicktype
{
    public static readonly string MinimalFilePath = "bin/LDtkJson.cs";
    public static readonly string FullFilePath = "bin/LDtkJsonFull.cs";

    public static readonly string MinimalJson = "bin/LDtkJson.json";
    public static readonly string FullJson = "bin/LDtkJsonFull.json";

    static string[] minimalArgs = {
        "--lang cs",
        "--src", MinimalJson,
        "-s schema",
        "-o", MinimalFilePath,
        "-t LDtkFile",
        "--features just-types",
        "--namespace LDtk",
        "--alphabetize-properties"
    };

    static string[] fullArgs = {
        "--lang cs",
        "--src", FullJson,
        "-s schema",
        "-o", FullFilePath,
        "-t LDtkFile",
        "--features just-types",
        "--namespace LDtk.Codegen",
        "--alphabetize-properties"
    };

    public static void Generate()
    {
        using HttpClient client = new();

        string min = client.GetStringAsync("https://raw.githubusercontent.com/deepnight/ldtk/master/docs/MINIMAL_JSON_SCHEMA.json").Result;
        GenerateQuickType(min, minimalArgs, MinimalJson);

        string full = client.GetStringAsync("https://raw.githubusercontent.com/deepnight/ldtk/master/docs/JSON_SCHEMA.json").Result;
        GenerateQuickType(full, fullArgs, FullJson);
    }

    static void GenerateQuickType(string json, string[] args, string jsonFile)
    {
        json = Regex.Replace(json, "__", "Aaaaaaaaaaaa_");
        json = Regex.Replace(json, "([^a-zA-Z])Level\"", "$1LDtkLevel\"");
        json = Regex.Replace(json, "([^a-zA-Z])World\"", "$1LDtkWorld\"");

        File.WriteAllText(jsonFile, json);

        Process minimal = Process.Start(new ProcessStartInfo
        {
            FileName = "quicktype.cmd",
            Arguments = string.Join(" ", args),
            UseShellExecute = false,
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            RedirectStandardInput = false,
            CreateNoWindow = true
        });
        minimal.Start();
        minimal.WaitForExit();
    }
}
