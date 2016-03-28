using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using Cricket.Common;
using MathNet.Numerics.LinearAlgebra;

namespace Cricket.ViewModel.Core
{
    public class ProjectionControlVm : BindableBase
    {
        public ProjectionControlVm()
        {
            XSelectorVm = new GroupSelectorVm {Orientation = Orientation.Horizontal};
            YSelectorVm = new GroupSelectorVm();
            GraphVm = new GraphVm();
        }

        public ProjectionControlVm(IEnumerable<int> keys) : this()
        {
            var keyLst = keys.ToList();
            keyLst.ForEach(k =>
            {
                XSelectorVm.AddItem(k,k.ToString());
                YSelectorVm.AddItem(k, k.ToString());
            });
        }

        private IDisposable _xSelectorSubscr;
        private GroupSelectorVm _xSelectorVm;
        public GroupSelectorVm XSelectorVm
        {
            get { return _xSelectorVm; }
            set
            {
                SetProperty(ref _xSelectorVm, value);
                _xSelectorSubscr?.Dispose();
                _xSelectorSubscr =
                    _xSelectorVm.OnSelectionChanged
                        .Subscribe(sz => UpdateData(StateMatrix, ProjMatrix));
            }
        }

        private IDisposable _ySelectorSubscr;
        private GroupSelectorVm _ySelectorVm;
        public GroupSelectorVm YSelectorVm
        {
            get { return _ySelectorVm; }
            set
            {
                SetProperty(ref _ySelectorVm, value);
                _ySelectorSubscr?.Dispose();
                _ySelectorSubscr =
                    _ySelectorVm.OnSelectionChanged
                        .Subscribe(sz => UpdateData(StateMatrix, ProjMatrix));
            }
        }

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
                        .Subscribe(sz => UpdateData(StateMatrix, ProjMatrix));
            }
        }

        public void UpdateData(Matrix<float> state, Matrix<float> proj)
        {
            if(state==null || proj==null) return;
            if (! XSelectorVm.SelectedKeys.Any()) return;
            if (! YSelectorVm.SelectedKeys.Any()) return;
            StateMatrix = state;
            ProjMatrix = proj;
            var ject = MathNetUtils.ProjectTo2D
                    (
                        basis: ProjMatrix.Transpose(),
                        states: StateMatrix,
                        dexX: XSelectorVm.SelectedKeys,
                        dexY: YSelectorVm.SelectedKeys
                    );

            GraphVm.SetData(
                imageWidth: GraphVm.WbImageVm.ControlWidth,
                imageHeight: GraphVm.WbImageVm.ControlHeight,
                plotPoints: new List<WbVmUtils.PlotPoint<Color>>(), 
                plotLines: new List<WbVmUtils.PlotLine<Color>>(), 
                filledRects: MakePlotRectangles(points: ject),
                openRects: new List<WbVmUtils.PlotRectangle<Color>>());

        }

        static List<WbVmUtils.PlotRectangle<Color>> MakePlotRectangles(
            IEnumerable<Z2<float>> points)
        {
                    return
                        points.Select(
                            v => new WbVmUtils.PlotRectangle<Color>(
                                    x: v.X,
                                    y: v.Y,
                                    width: 2,
                                    height: 2,
                                    val: Colors.Aqua
                                )).ToList();
        }

        public Matrix<float> ProjMatrix { get; private set; }

        public Matrix<float> StateMatrix { get; private set; }

        public List<F2V<float>> Values { get; private set; }


        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _titleX;
        public string TitleX
        {
            get { return _titleX; }
            set { SetProperty(ref _titleX, value); }
        }

        private string _titleY;
        public string TitleY
        {
            get { return _titleY; }
            set { SetProperty(ref _titleY, value); }
        }
    }
}
