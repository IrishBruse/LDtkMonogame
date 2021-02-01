using System.Runtime.CompilerServices;

using Microsoft.Xna.Framework;

namespace LDtk
{
    /// <summary>
    /// Utility for parsing ldtk json data into more typed versions
    /// </summary>
    internal class Utility
    {
        /// <summary>
        /// Revers hex color string
        /// ABGR -> RGBA
        /// </summary>
        /// <param name="hex">hex color number</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ReverseHex(uint hex)
        {
            return ((hex & 0x000000ff) << 24) | ((hex & 0x0000ff00) << 8) | ((hex & 0x00ff0000) >> 8) | ((hex & 0xff000000) >> 24);
        }

        /// <summary>
        /// Convert ldtk color string into <see cref="Color"/>
        /// </summary>
        /// <param name="hex">In LDtk format of #ABCDEF hex</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ConvertStringToColor(string hex)
        {
            if(uint.TryParse(hex[1..] + "FF", System.Globalization.NumberStyles.HexNumber, null, out uint color))
            {
                return new Color(ReverseHex(color));
            }
            else
            {
                return new Color(0xFF00FFFF);
            }
        }
    }
}
