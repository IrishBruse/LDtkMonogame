namespace LDtk;

using System;

using Microsoft.Xna.Framework;

/// <summary> LDtk IntGrid </summary>
public class LDtkIntGrid
{
    /// <summary> Size of a tile in pixels </summary>
    public int TileSize { get; set; }

    /// <summary> The underlying values of the int grid </summary>
    public int[] Values { get; set; }

    /// <summary> Worldspace start Position of the intgrid </summary>
    public Point WorldPosition { get; set; }

    /// <summary> Worldspace start Position of the intgrid </summary>
    public Point GridSize { get; set; }

    /// <summary> Used by json deserializer not for use by user! </summary>
    [Obsolete("Used by json deserializer not for use by user!")]
    public LDtkIntGrid() { }

    /// <summary> Gets the int value at location and return 0 if out of bounds </summary>
    public int GetValueAt(int x, int y)
    {
        if (Values.Length == 0)
        {
            return 0;
        }

        if (Contains(new Point(x, y)))
        {
            return Values[(y * GridSize.X) + x];
        }
        else
        {
            return 0;
        }
    }

    /// <summary> Gets the int value at location and return 0 if out of bounds </summary>
    public int GetValueAt(Point position) => GetValueAt(position.X, position.Y);

    /// <summary> Gets the int value at location and return 0 if out of bounds </summary>
    public int GetValueAt(Vector2 position) => GetValueAt((int)position.X, (int)position.Y);

    /// <summary> Check if point is inside of a grid </summary>
    public bool Contains(Point point) => point.X >= 0 && point.Y >= 0 && point.X <= GridSize.X && point.Y <= GridSize.Y;

    /// <summary> Check if point is inside of a grid </summary>
    public bool Contains(Vector2 point) => point.X >= 0 && point.Y >= 0 && point.X <= GridSize.X && point.Y <= GridSize.Y;

    /// <summary> Convert from world pixel space to int grid space Floors the value based on <see cref="TileSize"/> to an Integer </summary>
    public Point FromWorldToGridSpace(Vector2 position)
    {
        int x = (int)Math.Floor(position.X / TileSize);
        int y = (int)Math.Floor(position.Y / TileSize);
        return new Point(x, y);
    }
}
