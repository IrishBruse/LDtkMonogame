#!/usr/bin/env -S dotnet --
#:property PublishAot=false

using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;

return await MonoGameUpdater.RunAsync(args);

static class MonoGameUpdater
{
    static readonly string Root =
        AppContext.GetData("EntryPointFileDirectoryPath") as string
        ?? Directory.GetCurrentDirectory();

    static readonly string[] MgcbTools =
    [
        "dotnet-mgcb",
        "dotnet-mgcb-editor",
        "dotnet-mgcb-editor-linux",
    ];

    public static async Task<int> RunAsync(string[] args)
    {
        Directory.SetCurrentDirectory(Root);

        if (args is ["-h"] or ["--help"])
        {
            Usage();
            return 1;
        }

        var current = CurrentMonogameVersion()
            ?? Die("could not find a MonoGame PackageReference version");

        string? target = args.Length > 0 ? args[0] : null;
        if (target is null)
        {
            Console.WriteLine("Resolving latest MonoGame from NuGet...");
            target = await LatestMonogameVersionAsync()
                ?? Die("could not resolve target MonoGame version");
        }

        if (current == target)
        {
            Console.WriteLine($"Already on MonoGame {current}");
            return 0;
        }

        var oldPkg = CurrentPackageVersion()
            ?? Die("could not read package version from changelog");
        var newPkg = BumpPatch(oldPkg);

        Console.WriteLine($"MonoGame  {current} -> {target}");
        Console.WriteLine($"Package   {oldPkg} -> {newPkg}");

        UpdateCsprojs(current, target);
        UpdateTools(current, target);
        UpdateDocs(current, target);
        UpdateChangelog(oldPkg, newPkg, target);

        Console.WriteLine("Restoring tools...");
        Run("dotnet", "tool", "restore");

        Console.WriteLine("Building...");
        Run("dotnet", "restore");
        Run("dotnet", "build", "--no-restore");

        Console.WriteLine($"Done: MonoGame {target} (package {newPkg})");
        return 0;
    }

    static void Usage()
    {
        Console.WriteLine("Usage: Update.cs [VERSION]");
        Console.WriteLine("  With no VERSION, updates to the latest MonoGame.Framework.DesktopGL on NuGet.");
    }

    static string Die(string message)
    {
        Console.Error.WriteLine($"error: {message}");
        Environment.Exit(1);
        throw new InvalidOperationException(message);
    }

    static string? CurrentMonogameVersion()
    {
        var versions = Directory.EnumerateFiles(Root, "*.csproj", SearchOption.AllDirectories)
            .SelectMany(File.ReadAllLines)
            .Select(line => Regex.Match(line, @"PackageReference Include=""MonoGame\.[^""]*"" Version=""([^""]+)"""))
            .Where(m => m.Success)
            .Select(m => m.Groups[1].Value)
            .Distinct()
            .ToList();

        return versions.Count > 0 ? versions[0] : null;
    }

    static async Task<string?> LatestMonogameVersionAsync()
    {
        var json = await RunCaptureAsync(
            "dotnet", "package", "search", "MonoGame.Framework.DesktopGL",
            "--take", "1", "--format", "json");

        using var doc = JsonDocument.Parse(json);

        foreach (var source in doc.RootElement.GetProperty("searchResult").EnumerateArray())
        {
            foreach (var package in source.GetProperty("packages").EnumerateArray())
            {
                if (package.TryGetProperty("latestVersion", out var version))
                    return version.GetString();
            }
        }

        return null;
    }

    static string? CurrentPackageVersion()
    {
        foreach (var line in File.ReadLines(Path.Combine(Root, "docs", "Changelog.md")))
        {
            var match = Regex.Match(line, @"^## \[(\d+\.\d+\.\d+)\]");
            if (match.Success)
                return match.Groups[1].Value;
        }

        return null;
    }

    static string BumpPatch(string version)
    {
        var parts = version.Split('.');
        return $"{parts[0]}.{parts[1]}.{int.Parse(parts[2]) + 1}";
    }

    static void UpdateCsprojs(string current, string target)
    {
        var count = 0;
        var pattern = new Regex(
            $@"(PackageReference Include=""MonoGame\.[^""]*"" Version=""){Regex.Escape(current)}("")");

        foreach (var csproj in Directory.EnumerateFiles(Root, "*.csproj", SearchOption.AllDirectories))
        {
            var text = File.ReadAllText(csproj);
            if (!text.Contains("MonoGame.", StringComparison.Ordinal))
                continue;

            var updated = pattern.Replace(
                text,
                match => match.Groups[1].Value + target + match.Groups[2].Value);

            if (updated == text)
                continue;

            File.WriteAllText(csproj, updated);
            count++;
        }

        Console.WriteLine($"Updated {count} project(s)");
    }

    static void UpdateTools(string current, string target)
    {
        var path = Path.Combine(Root, ".config", "dotnet-tools.json");
        var text = File.ReadAllText(path);

        foreach (var tool in MgcbTools)
        {
            if (!text.Contains($"\"{tool}\"", StringComparison.Ordinal))
                Die($"missing tool {tool} in .config/dotnet-tools.json");
        }

        text = text.Replace($"\"version\": \"{current}\"", $"\"version\": \"{target}\"");
        File.WriteAllText(path, text);
        Console.WriteLine("Updated .config/dotnet-tools.json");
    }

    static void UpdateDocs(string current, string target)
    {
        var path = Path.Combine(Root, "docs", "Documentation", "content.md");
        var text = File.ReadAllText(path);
        var marker = $"mgcb-editor` at minimum version `{current}`";

        if (!text.Contains(marker, StringComparison.Ordinal))
            Die($"content.md version marker not found: {current}");

        text = text.Replace(marker, $"mgcb-editor` at minimum version `{target}`");
        File.WriteAllText(path, text);
        Console.WriteLine($"Updated {Path.GetRelativePath(Root, path)}");
    }

    static void UpdateChangelog(string oldPkg, string newPkg, string monogame)
    {
        var path = Path.Combine(Root, "docs", "Changelog.md");
        var lines = File.ReadAllLines(path).ToList();

        if (!lines.Contains("Changelog for versions"))
            Die("changelog marker not found");
        if (lines.Any(line => line.StartsWith($"## [{newPkg}]", StringComparison.Ordinal)))
            Die($"changelog already has {newPkg}");

        var index = lines.IndexOf("Changelog for versions");
        var entry = new[]
        {
            "",
            $"## [{newPkg}](https://github.com/IrishBruse/LDtkMonogame/compare/v{oldPkg}...v{newPkg})",
            "",
            $"-   Update MonoGame to v{monogame}",
            "",
        };

        lines.InsertRange(index + 1, entry);
        File.WriteAllLines(path, lines);
        Console.WriteLine($"Updated {Path.GetRelativePath(Root, path)}");
    }

    static void Run(string fileName, params string[] arguments)
    {
        var psi = new ProcessStartInfo(fileName)
        {
            WorkingDirectory = Root,
        };
        foreach (var argument in arguments)
            psi.ArgumentList.Add(argument);

        using var process = Process.Start(psi)
            ?? throw new InvalidOperationException($"failed to start {fileName}");

        process.WaitForExit();
        if (process.ExitCode != 0)
            Environment.Exit(process.ExitCode);
    }

    static async Task<string> RunCaptureAsync(string fileName, params string[] arguments)
    {
        var psi = new ProcessStartInfo(fileName)
        {
            WorkingDirectory = Root,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };
        foreach (var argument in arguments)
            psi.ArgumentList.Add(argument);

        using var process = Process.Start(psi)
            ?? throw new InvalidOperationException($"failed to start {fileName}");

        var stdout = await process.StandardOutput.ReadToEndAsync();
        var stderr = await process.StandardError.ReadToEndAsync();
        await process.WaitForExitAsync();

        if (process.ExitCode != 0)
            Die($"{fileName} failed: {stderr.Trim()}");

        return stdout;
    }
}
