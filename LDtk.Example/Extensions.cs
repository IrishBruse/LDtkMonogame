namespace LDtkMonogameExample;

using System;

using Microsoft.Xna.Framework;

internal static class Extensions
{

    /// <summary>
    /// Magic lerp that doesnt use start position but instead uses current position and <paramref name="maxDistanceDelta"/>
    /// </summary>
    public static Vector2 MoveTowards(this Vector2 current, Vector2 end, float maxDistanceDelta, out bool done)
    {
        float diffX = end.X - current.X;
        float diffY = end.Y - current.Y;

        float sqDist = (diffX * diffX) + (diffY * diffY);

        if (sqDist == 0 || (maxDistanceDelta >= 0 && sqDist <= maxDistanceDelta * maxDistanceDelta))
        {
            done = true;
            return end;
        }

        float dist = MathF.Sqrt(sqDist);

        done = false;
        return new Vector2(current.X + (diffX / dist * maxDistanceDelta), current.Y + (diffY / dist * maxDistanceDelta));
    }
}
