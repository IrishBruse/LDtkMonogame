namespace LDtk;

using System;

using Microsoft.Xna.Framework;

/// <summary>
/// Interface that implements the Entity.
/// </summary>
public interface ILDtkEntity
{
    /// <summary> Gets or sets identifier. </summary>
    string Identifier { get; set; }

    /// <summary> Gets or sets iid. </summary>
    Guid Iid { get; set; }

    /// <summary> Gets or sets uid. </summary>
    int Uid { get; set; }

    /// <summary> Gets or sets position. </summary>
    Vector2 Position { get; set; }

    /// <summary> Gets or sets size. </summary>
    Vector2 Size { get; set; }

    /// <summary> Gets or sets pivot. </summary>
    Vector2 Pivot { get; set; }

    /// <summary> Gets or sets tile. </summary>
    Rectangle Tile { get; set; }

    /// <summary> Gets or sets editorVisualColor. </summary>
    Color SmartColor { get; set; }
}
