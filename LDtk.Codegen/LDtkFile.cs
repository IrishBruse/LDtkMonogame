namespace LDtk.Codegen;

using System.IO;
using System.Text.Json;

public partial class LDtkFile
{
    public static LDtkFile FromFile(string filePath)
    {
        return JsonSerializer.Deserialize<LDtkFile>(File.ReadAllText(filePath), Constants.SerializeOptions);
    }
}
