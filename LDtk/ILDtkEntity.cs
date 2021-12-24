using Microsoft.Xna.Framework;

namespace LDtk;

/// <summary>
/// Interface that implements the Entity
/// </summary>
public interface ILDtkEntity
{
    /// <summary>
    /// Uid
    /// </summary>
    long Uid { get; set; }

    /// <summary>
    /// Identifier
    /// </summary>
    string Identifier { get; set; }

    /// <summary>
    /// Size
    /// </summary>
    Vector2 Size { get; set; }

    /// <summary>
    /// Position
    /// </summary>
    Vector2 Position { get; set; }

    /// <summary>
    /// Pivot
    /// </summary>
    Vector2 Pivot { get; set; }

    /// <summary>
    /// Tile
    /// </summary>
    Rectangle Tile { get; set; }

    /// <summary>
    /// EditorVisualColor
    /// </summary>
    Color EditorVisualColor { get; set; }
}
