using System;
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
        /// Convert ldtk color string into <see cref="Color"/>
        /// </summary>
        /// <param name="hex">In LDtk format of #BBGGRR hex</param>
        /// <returns></returns>
        public static Color ConvertStringToColor(string hex)
        {
            return ConvertStringToColor(hex, 255);
        }

        /// <summary>
        /// Convert ldtk color string into <see cref="Color"/>
        /// </summary>
        /// <param name="hex">In LDtk format of #BBGGRR hex</param>
        /// <param name="alpha">Alpha</param>
        /// <returns></returns>
        public static Color ConvertStringToColor(string hex, int alpha)
        {
            if (uint.TryParse(hex.Replace("#", ""), System.Globalization.NumberStyles.HexNumber, null, out uint color))
            {
                byte red = (byte)((color & 0xFF0000) >> 16);
                byte green = (byte)((color & 0x00FF00) >> 8);
                byte blue = (byte)((color & 0xFF));

                return new Color(red, green, blue, alpha);
            }
            else
            {
                return new Color(0xFF00FFFF);
            }
        }
    }
}
