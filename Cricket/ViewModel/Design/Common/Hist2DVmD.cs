﻿using System.Collections.Generic;
using System.Linq;
using Cricket.ViewModel.Common;
using TT;

namespace Cricket.ViewModel.Design.Common
{
    public class Hist2DvmD : Hist2DVm
    {
        public Hist2DvmD() : base(5)
        {
            UpdateData(TestData());
        }

        public static IEnumerable<P2<float>> TestData()
        {
            var g = new MathNet.Numerics.Distributions.Normal(0.0, 1.0);
            var randy = new MathNet.Numerics.Random.MersenneTwister();

            g.RandomSource = randy;
            var dblsX = new double[100000];
            var dblsY = new double[100000];
            g.Samples(dblsX);
            g.Samples(dblsY);
            return dblsX.Select((d,i) => 
                new P2<float>((float)d, (float)dblsY[i]));
        }
    }
}