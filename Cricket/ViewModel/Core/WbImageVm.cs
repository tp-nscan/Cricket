using System;
using System.Reactive.Subjects;
using System.Windows;
using Cricket.Common;
using TT;

namespace Cricket.ViewModel.Core
{
    public class WbImageVm : BindableBase
    {
        private readonly Subject<Size> _sizeChanged
            = new Subject<Size>();
        public IObservable<Size> OnSizeChanged => _sizeChanged;

        private readonly Subject<Point> _pointerChanged
            = new Subject<Point>();
        public IObservable<Point> OnPointerChanged => _pointerChanged;

        ImageData _imageData;
        public ImageData ImageData
        {
            get { return _imageData; }
            set { SetProperty(ref _imageData, value); }
        }

        private Point _pointerPosition;
        public Point PointerPosition
        {
            get { return _pointerPosition; }
            set
            {
                SetProperty(ref _pointerPosition, value);
                _pointerChanged.OnNext(value);
            }
        }

        private double _controlHeight;
        public double ControlHeight
        {
            get { return _controlHeight; }
            set
            {
                SetProperty(ref _controlHeight, value);
                _sizeChanged.OnNext(new Size(ControlWidth, ControlHeight));
            }
        }

        private double _controlWidth;
        public double ControlWidth
        {
            get { return _controlWidth; }
            set
            {
                SetProperty(ref _controlWidth, value);
                _sizeChanged.OnNext(new Size(ControlWidth, ControlHeight));
            }
        }

        public double ImageArea => ControlWidth* ControlHeight;
    }
}
