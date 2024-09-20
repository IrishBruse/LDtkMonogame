namespace LDtk.Full;

using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

public partial class LDtkFileFull
{
    /// <summary> Gets or sets the absolute path to the ldtkFile. </summary>
    [JsonIgnore]
    public string FilePath { get; set; } = string.Empty;


    /// <summary> Loads the ldtk world file from disk directly using json source generator. </summary>
    /// <param name="filePath"> Path to the .ldtk file. </param>
    /// <returns> Returns the file loaded from the path. </returns>
    public static LDtkFileFull FromFile(string filePath)
    {
        LDtkFileFull? file = JsonSerializer.Deserialize(File.ReadAllText(filePath), Constants.JsonSourceGeneratorFull.LDtkFileFull);
        if (file == null)
        {
            throw new LDtkException($"Failed to Deserialize ldtk file from {filePath}");
        }
        ValidateFile(file);
        file.FilePath = Path.GetFullPath(filePath);
        return file;
    }

    static void ValidateFile(LDtkFileFull file)
    {
        if (Version.Parse(file.JsonVersion) < Version.Parse(Constants.SupportedLDtkVersion))
        {
            throw new LDtkException("LDtk file version is not supported. Please update your LDtk version.");
        }

        if (file.Flags == null)
        {
            throw new LDtkException("LDtk file is missing required flags. Please enable them in the ldtk file flags in the UI.");
        }

        if (!file.Flags.Contains(Flag.MultiWorlds))
        {
            throw new LDtkException("LDtk file is not a multiworld file. Please enable MultiWorlds in the ldtk file flags.");
        }
    }
}
