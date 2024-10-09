namespace LDtkTypes;

// This file was automatically generated, any modifications will be lost!
#pragma warning disable

using LDtk;
using Microsoft.Xna.Framework;

public partial class RefTest: ILDtkEntity
{
    public static readonly RefTest Default = new()
    {
        Identifier = "RefTest",
        Uid = 123,
        Size = new Vector2(16f, 16f),
        Pivot = new Vector2(0f, 0f),
        Tile = new TilesetRectangle()
        {
            X = 16,
            Y = 48,
            W = 16,
            H = 16
        },
        SmartColor = new Color(148, 217, 179, 255),

        TileTest = new TilesetRectangle()
        {
            X = 112,
            Y = 32,
            W = 16,
            H = 16
        },
    };

    public string Identifier { get; set; }
    public System.Guid Iid { get; set; }
    public int Uid { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public Vector2 Pivot { get; set; }
    public Rectangle Tile { get; set; }

    public Color SmartColor { get; set; }

    public EntityReference? Test { get; set; }
    public TilesetRectangle? TileTest { get; set; }
    public float? Float { get; set; }
    public EnemyType? EnemyType { get; set; }
}
#pragma warning restore
