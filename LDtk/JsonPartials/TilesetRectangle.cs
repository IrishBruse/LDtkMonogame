namespace LDtk;

public partial class TilesetRectangle
{
    /// <summary> Monogame Implicit Rectangle Cast </summary>
    public static implicit operator Rectangle(TilesetRectangle r)
    {
        return new Rectangle(r.X, r.Y, r.W, r.H);
    }
}
