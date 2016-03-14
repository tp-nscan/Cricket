//using System;
//using System.Collections.Generic;
//using System.Linq;
//using TT;

//namespace Cricket.Graphics
//{
//    public static class RealIntervalExt
//    {

//        public static I<float> Pad(this I<float> realInterval, double leftSide, double rightSide)
//        {
//            return I<float>.Make(realInterval.Min - leftSide, realInterval.Span() + leftSide + rightSide);
//        }


//        public static I<float> Match(this IEnumerable<I<float>> buckets, double value)
//        {
//            foreach (var bucket in buckets)
//            {
//                if (bucket.ClosedContains(value))
//                {
//                    return bucket;
//                }
//            }
//            return RealInterval.Empty;
//        }


//        public static string BracketFormat(this I<float> realInterval, string numberFormat)
//        {
//            return String.Format("[{0}-{1}]", realInterval.Min.ToString(numberFormat),
//                                 realInterval.Max.ToString(numberFormat));
//        }

//        /// <summary>
//        /// Zooms out for factor > 1, zooms in for factor < 1
//        /// </summary>
//        public static I<float> ZoomBy(this I<float> interval, double factor)
//        {
//            factor = Math.Abs(factor);
//            return I<float>.Make
//                (
//                    interval.Center() - interval.Span() * factor / 2,
//                    interval.Center() + interval.Span() * factor / 2
//               );
//        }

//        public static I<float> AdjustBy(this I<float> interval, double minDelta, double maxDelta)
//        {
//            return I<float>.Make
//                (
//                    interval.Min + minDelta,
//                    interval.Max + maxDelta
//               );
//        }

//        public static I<float> Offset(this I<float> realInterval, double delta)
//        {
//            return I<float>.Make(realInterval.Min + delta, realInterval.Max + delta);
//        }
//    }
//}
