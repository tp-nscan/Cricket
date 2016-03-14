using System;
using System.Windows.Media;

namespace Cricket.Graphics
{
    public static class ColorMap
    {
        private static readonly float Colorsteps = 256;
        static readonly IColorSequence RingColors = ColorSequence.Quadrupolar(
            Colors.Red, Colors.Orange, Colors.Green, Colors.Blue, (int) (Colorsteps / 4));

        public static Func<float, Color> Quadro
        {
            get { return v => RingColors.Colors[(int)(v*Colorsteps)]; }
        }


        private static readonly IColorSequence BlueColors = 
            Colors.Blue.ToUniformColorSequence((int) Colorsteps);

        public static Func<float, Color> Blue
        {
            get { return v => BlueColors.Colors[(int)(v * Colorsteps)]; }
        }


        private static readonly IColorSequence WhiteColors =
            Colors.White.ToUniformColorSequence((int)Colorsteps);

        public static Func<float, Color> White
        {
            get { return v => WhiteColors.Colors[(int)(v * Colorsteps)]; }
        }


        private static readonly IColorSequence RedBlueColors =
            ColorSequence.Dipolar(Colors.Red, Colors.Blue, (int)(Colorsteps));

        public static Func<float, Color> RedBlueUnit
        {
            get
            {
                return v =>
                {
                    if (v <= -1.0) return Colors.Brown;
                    if (v > 1.0) return Colors.DarkOrange;
                    return RedBlueColors.Colors[(int) ((v*0.9999 + 1)*Colorsteps)];
                };
            }
        }

        public static Func<float, Color> RedBlue(float unit)
        {
            return v =>
            {
                var uv = v/unit;
                if (uv <= -1.0) return Colors.Brown;
                if (uv >= 1.0) return Colors.DarkOrange;
                return RedBlueColors.Colors[(int)((uv + 1) * Colorsteps)];
            };
       }
    }
}
