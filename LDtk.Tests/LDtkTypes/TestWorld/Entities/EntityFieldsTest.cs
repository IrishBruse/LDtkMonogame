// This file was automatically generated, any modifications will be lost!
#pragma warning disable
namespace LDtkTypes.TestWorld;

using Microsoft.Xna.Framework;
using LDtk;

public class EntityFieldsTest : ILDtkEntity
{
    public string Identifier { get; set; }
    public System.Guid Iid { get; set; }
    public int Uid { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public Vector2 Pivot { get; set; }
    public Rectangle Tile { get; set; }

    public Color SmartColor { get; set; }

    public int Integer { get; set; }
    public float Float { get; set; }
    public bool Boolean { get; set; }
    public string String_singleLine { get; set; }
    public string String_multiLines { get; set; }
    public SomeEnum Enum { get; set; }
    public AnExternEnum ExternEnum { get; set; }
    public Color Color { get; set; }
    public Vector2? Point { get; set; }
    public string? FilePath { get; set; }
    public int[] Array_Integer { get; set; }
    public SomeEnum[] Array_Enum { get; set; }
    public Vector2[] Array_points { get; set; }
    public string[]? Array_multilines { get; set; }
}
#pragma warning restore
