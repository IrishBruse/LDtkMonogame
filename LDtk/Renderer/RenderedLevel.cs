namespace LDtk.Renderer;

using Microsoft.Xna.Framework.Graphics;

/// <summary> The level struct containting the rendered out textures </summary>
public struct RenderedLevel
{
    /// <summary> The layers of the level in order </summary>
    public Texture2D[] Layers { get; set; }
}
