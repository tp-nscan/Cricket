using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Cricket.Common;
using TT;

namespace Cricket.ViewModel.Common
{
    public class Hist2DVm : BindableBase
    {
        public Hist2DVm(int sharpness, R<float> bounds)
        {
            _enforceBounds = true;
            Sharpness = sharpness;
            GraphVm = new GraphVm();
            Bounds = bounds;
           // ColorSelector = ColorFunc.WeatherChannel;
        }

        public Hist2DVm(int sharpness)
        {
            _enforceBounds = false;
            Sharpness = sharpness;
            GraphVm = new GraphVm();
            //ColorSelector = ColorFunc.WeatherChannel;
        }

       // Func<int, Color> ColorSelector { get; }

        public int Sharpness { get; }

        public R<float> Bounds { get; private set; }

        public List<P2<float>> Values { get; private set; }

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

        public void UpdateData(IEnumerable<P2<float>> values)
        {
            //if (EnforceBounds)
            //{
            //    Values = values.Where(v=> XTUtils.IsInBox(v,Bounds))
            //                .ToList();
            //}
            //else
            //{
            //    Values = values.ToList();
            //    Bounds = XTUtils.BoundingBox(Values);
            //}

            UpdateGraphVm();
        }

        public void UpdateGraphVm()
        {
            //if( GraphVm.WbImageVm.ImageArea < 0.1)
            //{
            //    return;
            //}

            //if (XTUtils.Area(Bounds) < 0.1)
            //{
            //    return;
            //}

            //var binCounts = new P2<int>(
            //    (int)(GraphVm.WbImageVm.ControlWidth / Sharpness), 
            //    (int)(GraphVm.WbImageVm.ControlHeight / Sharpness));

            //var bins = Histos.Histogram2d(
            //    bounds: Bounds,
            //    binCount: binCounts,
            //    vals: Values).ToColumnMajorOrder();
            
            //GraphVm.Watermark = $"Bins count: [{binCounts.X}, {binCounts.Y}]";
            //GraphVm.SetData(
            //    imageWidth: GraphVm.WbImageVm.ControlWidth,
            //    imageHeight: GraphVm.WbImageVm.ControlHeight,
            //    boundingRect: Bounds.ToRectFloat(),
            //    plotPoints: null,
            //    plotLines: null,
            //    filledRects: MakePlotRectangles(hist: bins),
            //    openRects: null);

        }

        private List<RV<float, Color>> MakePlotRectangles(
            IEnumerable<RV<float, int>> hist)
        {
            return null;
            //hist.Select(
            //    v => new RV<float, Color>(
            //            minX: v.MinX,
            //            minY: v.MinY,
            //            maxX: v.MaxX,
            //            maxY: v.MaxY,
            //            v: ColorSelector(v.V)
            //        )).ToList();
        }



    }
}
