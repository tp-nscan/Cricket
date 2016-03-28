using System.Collections.Generic;
using System.Windows.Media;
using Cricket.Common;
using TT;

namespace Cricket.ViewModel.Core
{
    public class GraphVm : BindableBase
    {
        public GraphVm()
        {
            WbImageVm = new WbImageVm();
        }

        public WbImageVm WbImageVm { get; }

        public void SetData(
            double imageWidth,
            double imageHeight,
            List<P2V<float,Color>> plotPoints,
            List<LS2V<float, Color>> plotLines,
            List<RV<float, Color>> filledRects,
            List<RV<float, Color>> openRects
        )
        {
            WbImageVm.WbImageData = new WbImageData(
                    imageWidth: imageWidth,
                    imageHeight: imageHeight,
                    plotPoints: plotPoints,
                    filledRects: filledRects,
                    openRects: openRects,
                    plotLines: plotLines
                );

            MinX = WbImageVm.WbImageData.BoundingRect.MinX;
            MinY = WbImageVm.WbImageData.BoundingRect.MinY;
            MaxX = WbImageVm.WbImageData.BoundingRect.MaxX;
            MaxY = WbImageVm.WbImageData.BoundingRect.MaxY;
        }

        public void SetData(
            R<float> boundingRect,
            double imageWidth,
            double imageHeight,
            List<P2V<float, Color>> plotPoints,
            List<LS2V<float, Color>> plotLines,
            List<RV<float, Color>> filledRects,
            List<RV<float, Color>> openRects
        )
        {

            WbImageVm.WbImageData = new WbImageData(
                    boundingRect: boundingRect,
                    plotPoints: plotPoints,
                    filledRects: filledRects,
                    openRects: openRects,
                    plotLines: plotLines,
                    imageWidth: imageWidth,
                    imageHeight: imageHeight
                );

            MinX = boundingRect.MinX;
            MinY = boundingRect.MinY;
            MaxX = boundingRect.MaxX;
            MaxY = boundingRect.MaxY;
        }

        private float _minX;
        public float MinX
        {
            get { return _minX; }
            set
            {
                SetProperty(ref _minX, value);
                MinStrX = value.ToLegendFormatCode();
            }
        }

        private float _minY;
        public float MinY
        {
            get { return _minY; }
            set
            {
                SetProperty(ref _minY, value);
                MinStrY = value.ToLegendFormatCode();
            }
        }

        private float _maxX;
        public float MaxX
        {
            get { return _maxX; }
            set
            {
                SetProperty(ref _maxX, value);
                MaxStrX = value.ToLegendFormatCode();
            }
        }

        private float _maxY;
        public float MaxY
        {
            get { return _maxY; }
            set
            {
                SetProperty(ref _maxY, value);
                MaxStrY = value.ToLegendFormatCode();
            }
        }

        private string _maxStrX;
        public string MaxStrX
        {
            get { return _maxStrX; }
            set { SetProperty(ref _maxStrX, value); }
        }

        private string _minStrX;
        public string MinStrX
        {
            get { return _minStrX; }
            set { SetProperty(ref _minStrX, value); }
        }

        private string _minStrY;
        public string MinStrY
        {
            get { return _minStrY; }
            set { SetProperty(ref _minStrY, value); }
        }

        private string _maxStrY;
        public string MaxStrY
        {
            get { return _maxStrY; }
            set { SetProperty(ref _maxStrY, value); }
        }

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

        private string _watermark;
        public string Watermark
        {
            get { return _watermark; }
            set { SetProperty(ref _watermark, value); }
        }
    }
}
