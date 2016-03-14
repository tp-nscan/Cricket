using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace Cricket.Graphics
{
    public static class ColorEx
    {
        public static Color IntToColor(this int colorVal)
        {
            return Color.FromArgb(255, (byte) (colorVal >> 0), (byte) (colorVal >> 8), (byte) (colorVal >> 16));
        }

        public static uint ToUint(this Color c)
        {
            return (uint) c.A << 24 | ((uint) c.R << 16) | ((uint) c.G << 8) | c.B;
        }

        public static int ToInt(this Color c)
        {
            return c.A << 24 | (c.R << 16) | (c.G << 8) | c.B;
        }

        public static IEnumerable<Color> UniformSpread(Color lowColor, Color hiColor, int stepCount)
        {

            yield break;
        //    return Enumerable.Range(0, stepCount).Select
        //        (
        //            i => new Color
        //            {
        //                A = (byte) RealIntervalExt.TicAtIndex(lowColor.A, hiColor.A, i, stepCount + 1),
        //                R = (byte) RealIntervalExt.TicAtIndex(lowColor.R, hiColor.R, i, stepCount + 1),
        //                G = (byte) RealIntervalExt.TicAtIndex(lowColor.G, hiColor.G, i, stepCount + 1),
        //                B = (byte) RealIntervalExt.TicAtIndex(lowColor.B, hiColor.B, i, stepCount + 1)
        //            }
        //        );
        }

        public static IEnumerable<Color> Z2(int width)
        {
            float w2 = width*width;
            return Enumerable.Range(0, width*width).Select
                (
                    i => new Color
                    {
                        A = (byte) 1.0,
                        R = (byte) ((1.0 + Math.Sin(i*Math.PI*2.0/width))/2.0),
                        G = (byte) ((1.0 + Math.Sin(i*Math.PI*2.0/w2))/2.0),
                        B = (byte) ((2.0 + Math.Cos(i*Math.PI*2.0/w2) + Math.Cos(i*Math.PI*2.0/width))/4.0)
                    }

                );
        }

        public static IEnumerable<Color> D3(int width)
        {
            float w2 = width*width;

            return Enumerable.Range(0, width*width).Select
                (

                    i => new Color
                    {
                        A = (byte) 1.0,
                        R = (byte) (i%2),
                        G = (byte) ((i/width)%2),
                        B = (byte) ((2.0 + Math.Cos(i*Math.PI*2.0/w2) + Math.Cos(i*Math.PI*2.0/width))/4.0)
                    }

                );
        }

        public static IEnumerable<Color> FadingSpread(this Color color, int stepCount)
        {
            yield break;
            //    return Enumerable.Range(0, stepCount).Select
            //        (
            //            i => new Color
            //            {
            //                A = (byte) RealIntervalExt.TicAtIndex(0, color.A, i, stepCount + 1),
            //                R = color.R,
            //                G = color.G,
            //                B = color.B
            //            }
            //        );
        }

        public static IEnumerable<Color> LessFadingSpread(this Color color, int stepCount)
        {
            yield break;
            //return Enumerable.Range(0, stepCount).Select
            //    (
            //        i => new Color
            //        {
            //            A = (byte)RealIntervalExt.TicAtIndex(0, color.A, (int)(Math.Pow((double)i / stepCount, 0.7) * stepCount), stepCount + 1),
            //            R = color.R,
            //            G = color.G,
            //            B = color.B
            //        }
            //    );
        }

    }
}