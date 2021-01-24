using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDtk
{
    public struct Level
    {
        public Color BgColor { get; internal set; }

        public RenderTarget2D[] layers
        {
            get; internal set;
        }
    }
}
