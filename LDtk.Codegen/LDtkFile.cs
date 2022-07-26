namespace LDtk.Codegen;

using System.IO;
using System.Text.Json;

using LDtk;

public partial class LDtkFile
{
    public static LDtkFile FromFile(string filePath) => JsonSerializer.Deserialize<LDtkFile>(File.ReadAllText(filePath), Constants.SerializeOptions);
}
