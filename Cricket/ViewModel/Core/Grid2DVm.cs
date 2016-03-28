using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Cricket.Common;
using TT;

namespace Cricket.ViewModel.Core
{
    public class Grid2DVm<T> : BindableBase
    {
        public Grid2DVm(Sz2<int> strides, Func<T, Color> colorMapper, string title = "")
        {
            Strides = strides;
            ColorMapper = colorMapper;
            WbImageVm = new WbImageVm();
            Title = title;
        }

        private IDisposable _szChangedSubscr;
        private WbImageVm _wbImageVm;
        public WbImageVm WbImageVm
        {
            get { return _wbImageVm; }
            set
            {
                SetProperty(ref _wbImageVm, value);
                _szChangedSubscr?.Dispose();
                _szChangedSubscr =
                    _wbImageVm.OnSizeChanged
                        .Subscribe(sz => UpdateData(Values));
            }
        }

        public Sz2<int> Strides { get; }

        public Func<T, Color> ColorMapper { get;}

        public List<P2V<int,T>> Values { get; private set; }

        public void UpdateData(IEnumerable<P2V<int,T>> values)
        {
            if (values == null) return;

            Values = values.ToList();

            if (WbImageVm.ImageArea < 0.1)
            {
                return;
            }

            WbImageVm.WbImageData = new WbImageData(
                    imageWidth: WbImageVm.ControlWidth,
                    imageHeight: WbImageVm.ControlHeight,
                    plotPoints: new List<P2V<float, Color>>(), 
                    filledRects: Values.Select(MakeRectangle).ToList(),
                    openRects: new List<RV<float, Color>>(), 
                    plotLines: new List<LS2V<float, Color>>()
                );
        }

        public RV<float, Color> MakeRectangle(P2V<int, T> v)
        {
            return new RV<float, Color>(
                    minX: v.X,
                    minY: v.Y,
                    maxX: v.X + 1.0f,
                    maxY: v.Y + 1.0f,
                    v: ColorMapper(v.V)
                );
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}
