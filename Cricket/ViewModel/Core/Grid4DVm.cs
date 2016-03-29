﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Cricket.Common;
using TT;

namespace Cricket.ViewModel.Core
{
    public class Grid4DVm<T> : BindableBase
    {
        public Grid4DVm(P2<int> strides, P2<int> cursor, ColorLeg<T> colorLeg, string title = "")
        {
            _x1Y1 = true;
            Strides = strides;
            Cursor = cursor;
            Values = new List<LS2V<int,T>>();
            ColorLeg = colorLeg;
            WbImageVm = new WbImageVm();
            Title = title;
        }

        private IDisposable _szChangedSubscr;
        private IDisposable _pointerChangedSubscr;
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

                _pointerChangedSubscr?.Dispose();
                _pointerChangedSubscr = WbImageVm.OnPointerChanged.Subscribe(PointerChanged);
            }
        }

        void PointerChanged(Point pt)
        {
            Cursor = new P2<int>((int) pt.X, (int) pt.Y);
            UpdateData(Values);
        }

        public P2<int> Cursor { get; private set; }

        public P2<int> Strides { get; }

        public ColorLeg<T> ColorLeg { get; }

        public List<LS2V<int,T>> Values { get; private set; }

        public void UpdateData(IEnumerable<LS2V<int, T>> values)
        {
            if (values != null) Values = values.ToList();

            if (WbImageVm.ImageArea < 0.1)
            {
                return;
            }

            var cursorFilter = FilterByCursor();
            var rectangler = MakeFilledRectangle();
            WbImageVm.WbImageData = new WbImageData(
                    boundingRect: new R<float>(Strides.Y, 0, 0, Strides.X), 
                    imageWidth: WbImageVm.ControlWidth,
                    imageHeight: WbImageVm.ControlHeight,
                    openRects: MakeOpenRectangle(),
                    filledRects: Values
                                    .Where(cursorFilter)
                                    .Select(rectangler).ToList(),
                    plotLines: new List<LS2V<float, Color>>(), 
                    plotPoints: new List<P2V<float, Color>>()
                );
        }

        public Func<LS2V<int, T>, bool> FilterByCursor()
        {
            if (X1Y1)
            {
                return val => val.X1 == Cursor.X && val.Y1 == Cursor.Y;
            }
            if (X1X2)
            {
                return val => val.X1 == Cursor.X && val.X2 == Cursor.Y;
            }
            if (X1Y2)
            {
                return val => val.X1 == Cursor.X && val.Y2 == Cursor.Y;
            }
            if (Y1X2)
            {
                return val => val.Y1 == Cursor.X && val.X2 == Cursor.Y;
            }
            if (Y1Y2)
            {
                return val => val.Y1 == Cursor.X && val.Y2 == Cursor.Y;
            }
            if (X2Y2)
            {
                return val => val.X2 == Cursor.X && val.Y2 == Cursor.Y;
            }
            throw new Exception("case not handled in FilterByCursor");
        }

        public Func<LS2V<int, T>, RV<float, Color>> MakeFilledRectangle()
        {
            if (X1Y1)
            {
                return v => new RV<float, Color>(
                        minX: v.X2,
                        minY: v.Y2,
                        maxX: v.X2 + 1.0f,
                        maxY: v.Y2 + 1.0f,
                        v: ColorSets.GetColor(ColorLeg, v.V)
                    );
            }
            if (X1X2)
            {
                return v => new RV<float, Color>(
                        minX: v.Y1,
                        minY: v.Y2,
                        maxX: v.Y1 + 1.0f,
                        maxY: v.Y2 + 1.0f,
                        v: ColorSets.GetColor(ColorLeg, v.V)
                    );
            }
            if (X1Y2)
            {
                return v => new RV<float, Color>(
                        minX: v.Y1,
                        minY: v.X2,
                        maxX: v.Y1 + 1.0f,
                        maxY: v.X2 + 1.0f,
                        v: ColorSets.GetColor(ColorLeg, v.V)
                    );
            }
            if (Y1X2)
            {
                return v => new RV<float, Color>(
                        minX: v.X1,
                        minY: v.Y2,
                        maxX: v.X1 + 1.0f,
                        maxY: v.Y2 + 1.0f,
                        v: ColorSets.GetColor(ColorLeg, v.V)
                    );
            }
            if (Y1Y2)
            {
                return v => new RV<float, Color>(
                        minX: v.X1,
                        minY: v.X2,
                        maxX: v.X1 + 1.0f,
                        maxY: v.X2 + 1.0f,
                        v: ColorSets.GetColor(ColorLeg, v.V)
                    );
            }
            if (X2Y2)
            {
                return v => new RV<float, Color>(
                        minX: v.X1,
                        minY: v.Y1,
                        maxX: v.X1 + 1.0f,
                        maxY: v.Y1 + 1.0f,
                        v: ColorSets.GetColor(ColorLeg, v.V)
                    );
            }
            throw new Exception("case not handled in FilterByCursor");
        }

        public List<RV<float,Color>> MakeOpenRectangle()
        {
            return new[]
            {
                new RV<float, Color>(
                    minX: Cursor.X,
                    minY: Cursor.Y,
                    maxX: Cursor.X + 1.0f,
                    maxY: Cursor.Y + 1.0f,
                    v: Colors.MediumPurple
                ),

                new RV<float, Color>(
                    minX: Cursor.X + 0.05f,
                    minY: Cursor.Y + 0.05f,
                    maxX: Cursor.X + 0.90f,
                    maxY: Cursor.Y + 0.90f,
                    v: Colors.MediumPurple
                )

            }.ToList();
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _x1Y1;
        public bool X1Y1 
        {
            get { return _x1Y1; }
            set
            {
                if (value)
                {
                    _x1Y1 = _x1X2 = _x1Y2 = _y1X2 = _y1Y2 = _x2Y2 = false;
                    SetProperty(ref _x1Y1, true);
                    UpdateData(Values);
                }
            }
        }

        private bool _x1X2;
        public bool X1X2
        {
            get { return _x1X2; }
            set
            {
                if (value)
                {
                    _x1Y1 = _x1X2 = _x1Y2 = _y1X2 = _y1Y2 = _x2Y2 = false;
                    SetProperty(ref _x1X2, true);
                    UpdateData(Values);
                }
            }
        }

        private bool _x1Y2;
        public bool X1Y2
        {
            get { return _x1Y2; }
            set
            {
                if (value)
                {
                    _x1Y1 = _x1X2 = _x1Y2 = _y1X2 = _y1Y2 = _x2Y2 = false;
                    SetProperty(ref _x1Y2, true);
                    UpdateData(Values);
                }
            }
        }

        private bool _y1X2;
        public bool Y1X2
        {
            get { return _y1X2; }
            set
            {
                if (value)
                {
                    _x1Y1 = _x1X2 = _x1Y2 = _y1X2 = _y1Y2 = _x2Y2 = false;
                    SetProperty(ref _y1X2, true);
                    UpdateData(Values);
                }
            }
        }

        private bool _y1Y2;
        public bool Y1Y2
        {
            get { return _y1Y2; }
            set
            {
                if (value)
                {
                    _x1Y1 = _x1X2 = _x1Y2 = _y1X2 = _y1Y2 = _x2Y2 = false;
                    SetProperty(ref _y1Y2, true);
                    UpdateData(Values);
                }
            }
        }

        private bool _x2Y2;
        public bool X2Y2
        {
            get { return _x2Y2; }
            set
            {
                if (value)
                {
                    _x1Y1 = _x1X2 = _x1Y2 = _y1X2 = _y1Y2 = _x2Y2 = false;
                    SetProperty(ref _x2Y2, true);
                    UpdateData(Values);
                }
            }
        }

        public IEnumerable<int> AllCbs
        {
            get
            {
                yield return _x1Y1 ? 1:0;
                yield return _x1X2 ? 1 : 0;
                yield return _x1Y2 ? 1 : 0;
                yield return _y1X2 ? 1 : 0;
            }

        } 

        public bool TwoAreChecked => AllCbs.Sum() == 2;
    }
}
