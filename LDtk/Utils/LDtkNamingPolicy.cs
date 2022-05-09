namespace LDtk;

using System.Text.Json;

class LDtkNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        if (name.StartsWith("_"))
        {
            return "__" + char.ToLowerInvariant(name[1]) + name[2..];
        }
        return char.ToLowerInvariant(name[0]) + name[1..];
    }
}
