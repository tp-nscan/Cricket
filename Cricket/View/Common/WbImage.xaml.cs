using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Cricket.Common;
using TT;

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

        public ImageData ImageData
        {
            get { return (ImageData) GetValue(ImageDataProperty); }
            set { SetValue(ImageDataProperty, value); }
        }

        public static readonly DependencyProperty ImageDataProperty =
            DependencyProperty.Register("ImageData", typeof(ImageData), typeof(WbImage),
                new PropertyMetadata(null, OnImageDataChanged));

        private static void OnImageDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var imageData = (ImageData)e.NewValue;
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
            return wbImage?.ImageData != null;
        }

        private WriteableBitmap _writeableBmp;
        private void MakeBitmap()
        {
            if ((ActualWidth < 1) || (ActualHeight < 1))
            {
                return;
            }

            if (ImageData == null)
            {
                return;
            }

            _writeableBmp = BitmapFactory.New((int)ImageData.imageSize.X, (int)ImageData.imageSize.Y);
            RootImage.Source = _writeableBmp;

            using (_writeableBmp.GetBitmapContext())
            {
                XFactor = ActualWidth / ImageData.boundingRect.Width();
                YFactor = ActualHeight / ImageData.boundingRect.Height();

                foreach (var plotRectangle in ImageData.filledRects)
                {
                    _writeableBmp.FillRectangle(
                            XWindow(plotRectangle.MinX, ImageData.boundingRect.MinX),
                            YWindow(plotRectangle.MinY, ActualHeight, ImageData.boundingRect.MinY),
                            XWindow(plotRectangle.MinX + plotRectangle.Width(), ImageData.boundingRect.MinX),
                            YWindow(plotRectangle.MinY + plotRectangle.Height(), ActualHeight, ImageData.boundingRect.MinY),
                            plotRectangle.V
                        );
                }

                foreach (var plotRectangle in ImageData.openRects)
                {
                    _writeableBmp.DrawRectangle(
                            XWindow(plotRectangle.MinX, ImageData.boundingRect.MinX),
                            YWindow(plotRectangle.MinY, ActualHeight, ImageData.boundingRect.MinY),
                            XWindow(plotRectangle.MinX + plotRectangle.Width(), ImageData.boundingRect.MinX),
                            YWindow(plotRectangle.MinY + plotRectangle.Height(), ActualHeight, ImageData.boundingRect.MinY),
                            plotRectangle.V
                        );
                }

                foreach (var plotLine in ImageData.plotLines)
                {
                    _writeableBmp.DrawLineAa(
                        XWindow(plotLine.X1, ImageData.boundingRect.MinX),
                        YWindow(plotLine.Y1, ActualHeight, ImageData.boundingRect.MinY),
                        XWindow(plotLine.X2, ImageData.boundingRect.MinX),
                        YWindow(plotLine.Y2, ActualHeight, ImageData.boundingRect.MinY),
                        plotLine.V
                        );
                }


                foreach (var plotPoint in ImageData.plotPoints)
                {
                    _writeableBmp.FillRectangle(
                        XWindow(plotPoint.X, ImageData.boundingRect.MinX),
                        YWindow(plotPoint.Y, ActualHeight, ImageData.boundingRect.MinY),
                        XWindow(plotPoint.X + 1, ImageData.boundingRect.MinX),
                        YWindow(plotPoint.Y + 1, ActualHeight, ImageData.boundingRect.MinY),
                        plotPoint.V
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
                X = ImageData.boundingRect.MinX + s.X / XFactor,
                Y = ImageData.boundingRect.MaxY - s.Y / YFactor
            };
        }
    }
}
