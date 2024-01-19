namespace LDtk.Renderer;

using Microsoft.Xna.Framework.Graphics;

/// <summary> The level struct containting the rendered out textures. </summary>
public struct RenderedLevel
{
    /// <summary> Gets or sets the layers of the level in order. </summary>
    public Texture2D[] Layers { get; set; }

    /// <summary> Checks if the two RenderedLevels are equal. </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    public static bool operator ==(RenderedLevel left, RenderedLevel right)
    {
        return left.Equals(right);
    }

    /// <summary> Checks if the two RenderedLevels are not equal. </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    public static bool operator !=(RenderedLevel left, RenderedLevel right)
    {
        return !(left == right);
    }

    /// <summary> Checks if the two RenderedLevels are equal. </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    public static bool Equals(RenderedLevel left, RenderedLevel right)
    {
        return left.Equals(right);
    }

    /// <summary> Comparison </summary>
    /// <param name="obj">Other</param>
    /// <returns>If equal</returns>
    public override readonly bool Equals(object? obj)
    {
        if (obj is not RenderedLevel)
        {
            return false;
        }
        return Equals((RenderedLevel)obj);
    }

    /// <summary> Get hashcode </summary>
    /// <returns></returns>
    public override readonly int GetHashCode()
    {
        return Layers.GetHashCode();
    }
}
