using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Cricket.Common;
using Cricket.Graphics;

namespace Cricket.ViewModel.Core
{
    public class Hist1DVm : BindableBase
    {
        public Hist1DVm(float min, float max, int sharpness)
        {
            _enforceBounds = true;
            Sharpness = sharpness;
            GraphVm = new GraphVm();
            ColorSteps = 256;
            MinValue = min;
            MaxValue = max;
            Legend = Colors.White.ToUniformColorSequence((int)ColorSteps);
        }

        public Hist1DVm(int sharpness)
        {
            _enforceBounds = false;
            Sharpness = sharpness;
            GraphVm = new GraphVm();
            ColorSteps = 256;
            Legend = Colors.White.ToUniformColorSequence((int)ColorSteps);
        }

        public IColorSequence Legend { get; }

        public float ColorSteps { get; }

        public int Sharpness { get; }

        public float MinValue { get; private set; }

        public float MaxValue { get; private set; }
    
        public List<float> Values { get; private set; }

        private IDisposable _szChangedSubscr;
        private GraphVm _graphVm;
        public GraphVm GraphVm
        {
            get { return _graphVm; }
            set
            {
                SetProperty(ref _graphVm, value);
                _szChangedSubscr?.Dispose();
                _szChangedSubscr =
                    _graphVm.WbImageVm.OnSizeChanged
                        .Subscribe(sz => UpdateData(Values));
            }
        }

        private bool _enforceBounds;
        public bool EnforceBounds
        {
            get { return _enforceBounds; }
            set
            {
                _enforceBounds = value;
                UpdateData(Values);
            }
        }

        public void UpdateData(IEnumerable<float> values)
        {
            if (values == null) return;

            if (EnforceBounds)
            {
                Values = values.Where(t => t > MinValue && t < MaxValue).ToList();
            }
            else
            {
                Values = values.ToList();
                MinValue = Values.Min();
                MaxValue = Values.Max();
            }

            if ((GraphVm == null) || (GraphVm.WbImageVm.ControlHeight < 1) 
                                  || (GraphVm.WbImageVm.ControlWidth < 1))
            {
                return;
            }

            UpdateGraphVm();
        }

        private void UpdateGraphVm()
        {
            if (Values==null) return;

            var binCount = (int) (GraphVm.WbImageVm.ControlWidth / Sharpness);
            var bins = Histogram.Histogram1d(
                min: MinValue, 
                max: MaxValue, 
                vals: Values, 
                binCount: binCount);

            var maxFreq = bins.Max(p => p.Count);

            GraphVm.Watermark = "Bin count: " + binCount;
            GraphVm.SetData(
                imageWidth: GraphVm.WbImageVm.ControlWidth,
                imageHeight: GraphVm.WbImageVm.ControlHeight,
                boundingRect: new RectFloat(top:maxFreq, left: MinValue, bottom:0.0f, right:MaxValue),
                plotPoints: null,
                plotLines: null,
                filledRects: MakePlotRectangles(hist: bins),
                openRects: null );
        }


        static List<WbVmUtils.PlotRectangle<Color>> MakePlotRectangles(
            IEnumerable<Histogram.Bin1d> hist)
        {
            return 
                hist.Select(
                    v => new WbVmUtils.PlotRectangle<Color>(
                            x: v.Min,
                            y: 0,
                            width: v.Max - v.Min,
                            height: v.Count,
                            val: Colors.Aqua
                        )).ToList();
        }

    }
}
