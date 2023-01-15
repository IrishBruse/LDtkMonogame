// This file was automatically generated, any modifications will be lost!
#pragma warning disable
namespace LDtkTypes.World;

using Microsoft.Xna.Framework;
using LDtk;

public class RefTest : ILDtkEntity
{
    public string Identifier { get; set; }
    public System.Guid Iid { get; set; }
    public int Uid { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public Vector2 Pivot { get; set; }
    public Rectangle Tile { get; set; }

    public Color SmartColor { get; set; }

    public EntityRef? Test { get; set; }
    public TilesetRectangle? TileTest { get; set; }
    public float? Float { get; set; }
    public EnemyType? EnemyType { get; set; }
}
#pragma warning restore
