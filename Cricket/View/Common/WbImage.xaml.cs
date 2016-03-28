using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Cricket.ViewModel.Core;

namespace Cricket.View.Common
{
    public sealed partial class WbImage
    {
        public WbImage()
        {
            InitializeComponent();
            SizeChanged += WbImage_SizeChanged;
        }

        private void WbImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MakeBitmap();
            ControlWidth = ActualWidth;
            ControlHeight = ActualHeight;
        }

        public WbImageData WbImageData
        {
            get { return (WbImageData) GetValue(WbImageDataProperty); }
            set { SetValue(WbImageDataProperty, value); }
        }

        public static readonly DependencyProperty WbImageDataProperty =
            DependencyProperty.Register("WbImageData", typeof(WbImageData), typeof(WbImage),
                new PropertyMetadata(null, OnWbImageDataChanged));

        private static void OnWbImageDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var imageData = (WbImageData)e.NewValue;
            if (imageData == null)
            {
                return;
            }

            var wbImage = d as WbImage;

            if (!ReadyToDisplay(wbImage)) return;

            wbImage?.MakeBitmap();
        }

        private static bool ReadyToDisplay(WbImage wbImage)
        {
            return wbImage?.WbImageData != null;
        }

        private WriteableBitmap _writeableBmp;
        private void MakeBitmap()
        {
            if ((ActualWidth < 1) || (ActualHeight < 1))
            {
                return;
            }

            if (WbImageData == null)
            {
                return;
            }

            _writeableBmp = BitmapFactory.New((int)WbImageData.ImageWidth, (int)WbImageData.ImageHeight);
            RootImage.Source = _writeableBmp;

            using (_writeableBmp.GetBitmapContext())
            {

                XFactor = ActualWidth / WbImageData.BoundingRect.Width();
                YFactor = ActualHeight / WbImageData.BoundingRect.Height();

                foreach (var plotRectangle in WbImageData.FilledRectangles)
                {
                    _writeableBmp.FillRectangle(
                            XWindow(plotRectangle.X, WbImageData.BoundingRect.Left),
                            YWindow(plotRectangle.Y, ActualHeight, WbImageData.BoundingRect.Bottom),
                            XWindow(plotRectangle.X + plotRectangle.Width, WbImageData.BoundingRect.Left),
                            YWindow(plotRectangle.Y + plotRectangle.Height, ActualHeight, WbImageData.BoundingRect.Bottom),
                            plotRectangle.Val
                        );
                }

                foreach (var plotRectangle in WbImageData.OpenRectangles)
                {
                    _writeableBmp.DrawRectangle(
                            XWindow(plotRectangle.X, WbImageData.BoundingRect.Left),
                            YWindow(plotRectangle.Y, ActualHeight, WbImageData.BoundingRect.Bottom),
                            XWindow(plotRectangle.X + plotRectangle.Width, WbImageData.BoundingRect.Left),
                            YWindow(plotRectangle.Y + plotRectangle.Height, ActualHeight, WbImageData.BoundingRect.Bottom),
                            plotRectangle.Val
                        );
                }

                foreach (var plotLine in WbImageData.PlotLines)
                {
                    _writeableBmp.DrawLineAa(
                        XWindow(plotLine.X1, WbImageData.BoundingRect.Left),
                        YWindow(plotLine.Y1, ActualHeight, WbImageData.BoundingRect.Bottom),
                        XWindow(plotLine.X2, WbImageData.BoundingRect.Left),
                        YWindow(plotLine.Y2, ActualHeight, WbImageData.BoundingRect.Bottom),
                        plotLine.Val
                        );
                }


                foreach (var plotPoint in WbImageData.PlotPoints)
                {
                    _writeableBmp.FillRectangle(
                        XWindow(plotPoint.X, WbImageData.BoundingRect.Left),
                        YWindow(plotPoint.Y, ActualHeight, WbImageData.BoundingRect.Bottom),
                        XWindow(plotPoint.X + 1, WbImageData.BoundingRect.Left),
                        YWindow(plotPoint.Y + 1, ActualHeight, WbImageData.BoundingRect.Bottom),
                        plotPoint.Val
                        );
                }


            } // Invalidates on exit of using block
        }


        private double XFactor { get; set; }

        private double YFactor { get; set; }

        public int XWindow(double xVal, double minX)
        {
            return (int)((xVal - minX) * XFactor);
        }

        public int YWindow(double yVal, double imageHeight, double minY)
        {
            return (int)(imageHeight - (yVal - minY) * YFactor);
        }

        public double ControlWidth
        {
            get { return (double)GetValue(ControlWidthProperty); }
            set { SetValue(ControlWidthProperty, value); }
        }

        public static readonly DependencyProperty ControlWidthProperty =
            DependencyProperty.Register("ControlWidth", typeof(double), typeof(WbImage),
                new PropertyMetadata(Double.NaN));


        public double ControlHeight
        {
            get { return (double)GetValue(ControlHeightProperty); }
            set { SetValue(ControlHeightProperty, value); }
        }

        public static readonly DependencyProperty ControlHeightProperty =
            DependencyProperty.Register("ControlHeight", typeof(double), typeof(WbImage),
                new PropertyMetadata(Double.NaN));

        public Point PointerPosition
        {
            get { return (Point)GetValue(PointerPositionProperty); }
            set { SetValue(PointerPositionProperty, value); }
        }

        public static readonly DependencyProperty PointerPositionProperty =
            DependencyProperty.Register("PointerPosition", typeof(Point), typeof(WbImage), null);

        private void RootImage_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var s = e.GetPosition(this);
            PointerPosition = new Point
            {
                X = WbImageData.BoundingRect.Left + s.X / XFactor,
                Y = WbImageData.BoundingRect.Top - s.Y / YFactor
            };
        }
    }
}
