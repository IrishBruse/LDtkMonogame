namespace LDtk;

using Microsoft.Xna.Framework;

/// <summary> LDtk IntGrid. </summary>
public class LDtkIntGrid
{
    /// <summary> Gets or sets the size of a tile in pixels. </summary>
    public int TileSize { get; set; }

    /// <summary> Gets or sets the underlying values of the int grid. </summary>
    public int[] Values { get; set; } = [];

    /// <summary> Gets or sets the worldspace start Position of the int grid. </summary>
    public Point WorldPosition { get; set; }

    /// <summary> Gets or sets the size of the int grid in tiles. </summary>
    public Point GridSize { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LDtkIntGrid"/> class. Used by json deserializer not for use by user!. </summary>
    public LDtkIntGrid() { }

    /// <summary> Gets the int value at location and return 0 if out of bounds. </summary>
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

    /// <summary> Gets the int value at location and return 0 if out of bounds. </summary>
    public int GetValueAt(Point position)
    {
        return GetValueAt(position.X, position.Y);
    }

    /// <summary> Gets the int value at location and return 0 if out of bounds. </summary>
    public int GetValueAt(Vector2 position)
    {
        return GetValueAt((int)position.X, (int)position.Y);
    }

    /// <summary> Check if point is inside of a grid. </summary>
    public bool Contains(Point point)
    {
        return point.X >= 0 && point.Y >= 0 && point.X < GridSize.X && point.Y < GridSize.Y;
    }

    /// <summary> Check if point is inside of a grid. </summary>
    public bool Contains(Vector2 point)
    {
        return point.X >= 0 && point.Y >= 0 && point.X < GridSize.X && point.Y < GridSize.Y;
    }

    /// <summary> Convert from world pixel space to int grid space. Floors the value based on <see cref="TileSize"/> to an Integer. </summary>
    public Point FromWorldToGridSpace(Vector2 position)
    {
        position -= WorldPosition.ToVector2(); // Convert from world space to local space.
        position /= TileSize; // Covert from local space to grid space.
        position.Floor();
        return position.ToPoint();
    }

    /// <summary> Convert from world pixel space to int grid space. Floors the value based on <see cref="TileSize"/> to an Integer. </summary>
    public Point FromWorldToGridSpace(Point position)
    {
        return FromWorldToGridSpace(position.ToVector2());
    }
}
