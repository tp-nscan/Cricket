using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Cricket.Common;
using Cricket.Graphics;
using TT;

namespace Cricket.ViewModel.Common
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
            var bins = Histos.Histogram1d(
                min: MinValue, 
                max: MaxValue, 
                vals: Values, 
                binCount: binCount);

            var maxFreq = bins.Max(p => p.V);

            GraphVm.Watermark = "Bin count: " + binCount;
            GraphVm.SetData(
                imageWidth: GraphVm.WbImageVm.ControlWidth,
                imageHeight: GraphVm.WbImageVm.ControlHeight,
                boundingRect: new R<float>(maxY:maxFreq, minX: MinValue, minY: 0.0f, maxX: MaxValue),
                plotPoints: null,
                plotLines: null,
                filledRects: MakePlotRectangles(hist: bins),
                openRects: null );
        }


        static List<RV<float, Color>> MakePlotRectangles(
            IEnumerable<IV<float,int>> hist)
        {
            return 
                hist.Select(
                    v => new RV<float, Color>(
                            minX: v.Min,
                            minY: 0,
                            maxX: v.Max,
                            maxY: v.V,
                            v: Colors.Aqua
                        )).ToList();
        }

    }
}
