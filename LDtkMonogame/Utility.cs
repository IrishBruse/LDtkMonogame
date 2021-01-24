using System.Runtime.CompilerServices;

using Microsoft.Xna.Framework;

namespace LDtk
{
    internal class Utility
    {
        /// <summary>
        /// Convert ldtk color string to monogame
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ReverseHex(uint hex)
        {
            return ((hex & 0x000000ff) << 24) | ((hex & 0x0000ff00) << 8) | ((hex & 0x00ff0000) >> 8) | ((hex & 0xff000000) >> 24);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hex">In LDtk format of #ABCDEF hex</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ConvertStringToColor(string hex)
        {
            uint color;

            if(uint.TryParse(hex.Substring(1) + "FF", System.Globalization.NumberStyles.HexNumber, null, out color))
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
