namespace LDtk;

using System;

using Microsoft.Xna.Framework;

/// <summary>
/// Interface that implements the Entity
/// </summary>
public interface ILDtkEntity
{
    /// <summary> Identifier </summary>
    string Identifier { get; set; }

    /// <summary> Iid </summary>
    Guid Iid { get; set; }

    /// <summary> Uid </summary>
    int Uid { get; set; }

    /// <summary> Position </summary>
    Vector2 Position { get; set; }

    /// <summary> Size </summary>
    Vector2 Size { get; set; }

    /// <summary> Pivot </summary>
    Vector2 Pivot { get; set; }

    /// <summary> Tile </summary>
    Rectangle Tile { get; set; }

    /// <summary> EditorVisualColor </summary>
    Color SmartColor { get; set; }
}
