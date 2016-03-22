using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace Cricket.Graphics
{
    public interface IColorSequence
    {
        IReadOnlyList<Color> Colors { get; }
    }

    public interface IN1ColorSequence : IColorSequence
    {
        IColorSequence PositiveColors { get; }
        IColorSequence NegativeColors { get; } 
    }

    public interface IColorSequence2D : IColorSequence
    {
        int Width { get; }
    }

    public static class ColorFunc
    {
        public static Func<int, Color> WeatherChannel
        {
            get
            {
                return i =>
                {
                    if (i > 49) return Colors.DarkRed;
                    if (i > 37) return Colors.Red;
                    if (i > 28) return Colors.OrangeRed;
                    if (i > 21) return Colors.Orange;
                    if (i > 16) return Colors.Yellow;
                    if (i > 12) return Colors.LightYellow;
                    if (i > 9) return Colors.DarkGreen;
                    if (i > 7) return Colors.Green;
                    if (i > 5) return Colors.Chartreuse;
                    if (i > 4) return Colors.DarkBlue;
                    if (i > 3) return Colors.MediumBlue;
                    if (i > 2) return Colors.DodgerBlue;
                    if (i > 1) return Colors.SteelBlue;
                    if (i > 0) return Colors.DarkSlateGray;
                    return Colors.Transparent;
                };
            }
        }
    }

    public static class ColorSequence
    {
        public static IColorSequence Dipolar(Color negativeColor, Color positiveColor, int halfStepCount)
        {
            return null;
            //new Ir1ColorSequence(
            //        new ColorSequenceImpl(positiveColor.LessFadingSpread(halfStepCount)),
            //        new ColorSequenceImpl(negativeColor.LessFadingSpread(halfStepCount))
            //    );
        }

        public static IColorSequence Quadrupolar(
            Color color1,
            Color color2,
            Color color3,
            Color color4,
            int quarterStepCount)
        {
            return null;
            //; new ColorSequenceImpl
            //    (
            //        ColorEx.UniformSpread(color1, color2, quarterStepCount)
            //            .Concat(ColorEx.UniformSpread(color2, color3, quarterStepCount))
            //            .Concat(ColorEx.UniformSpread(color3, color4, quarterStepCount))
            //            .Concat(ColorEx.UniformSpread(color4, color1, quarterStepCount))
            //    );
        }

        public static IColorSequence2D TriPolar(int width)
        {
            return new ColorSequenceImpl2D
                (
                   colors: ColorEx.Z2(width),
                   width: width
                );
        }

        public static Color ToUnitColor(this IN1ColorSequence in1ColorSequence, double value)
        {
            return (value > 0)
                ? in1ColorSequence.PositiveColors.Colors[(int)( value * 0.999 * in1ColorSequence.PositiveColors.Colors.Count)]
                : in1ColorSequence.NegativeColors.Colors[(int)(-value * 0.999 * in1ColorSequence.NegativeColors.Colors.Count)];
        }

        public static Color ToUnitColor(this IColorSequence colorSequence, double value)
        {
            return colorSequence.Colors[(int)(value * 0.999 * colorSequence.Colors.Count)];
        }

        public static Color ToUnitColor2D(this IColorSequence colorSequence, double valueX, double valueY, int colorWidth)
        {
            return colorSequence.Colors[((int)((valueX + 0.5) * 0.999 * colorWidth) * colorWidth) + (int)((valueY + 0.5) * 0.999 * colorWidth)];
        }

        public static Color ToUnitColor2D(
            this IColorSequence colorSequence, 
            int valueX, 
            int valueY, 
            int colorBits)
        {
            var xoff = (valueX) << (colorBits);
            var yoff = (valueY);

            return colorSequence.Colors[xoff + yoff];
        }

        public static IColorSequence ToUniformColorSequence(this Color maxColor, int steps)
        {
            return null; // new ColorSequenceImpl(maxColor.FadingSpread(steps));
        }

    }

    public class ColorSequenceImpl : IColorSequence
    {
        public ColorSequenceImpl(IEnumerable<Color> colors)
        {
            _colors = colors.ToList();
        }

        private readonly List<Color> _colors;
        public IReadOnlyList<Color> Colors => _colors;
    }

    public class Ir1ColorSequence : IN1ColorSequence
    {
        public Ir1ColorSequence
            (
                IColorSequence positiveColors, 
                IColorSequence negativeColors
            )
        {
            PositiveColors = positiveColors;
            NegativeColors = negativeColors;
            Colors = NegativeColors
                            .Colors
                            .Reverse()
                            .Concat
                            (
                                PositiveColors.Colors
                            ).ToList();
        }

        public IColorSequence PositiveColors { get; }

        public IColorSequence NegativeColors { get; }

        public IReadOnlyList<Color> Colors { get; }
    }

    public class ColorSequenceImpl2D : IColorSequence2D
    {
        public ColorSequenceImpl2D(IEnumerable<Color> colors, int width)
        {
            Width = width;
            _colors = colors.ToList();
        }

        private readonly List<Color> _colors;
        public IReadOnlyList<Color> Colors => _colors;

        public int Width { get; }
    }
}
