using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Cricket.Graphics;
using Cricket.ViewModel.Common;
using TT;

namespace Cricket.ViewModel.Design.Common
{
    public class GraphVmD : GraphVm
    {
        public GraphVmD()
        {
            SetData(
                imageWidth: 100, 
                imageHeight:100, 
                plotPoints: PlotPoints, 
                plotLines: new List<LS2V<float, Color>>(), 
                filledRects: PlotRectangles,
                openRects: new List<RV<float, Color>>());
            Title = "Design Title";
            TitleX = "Design Title X";
            TitleY = "Design Title Y";
        }

        static List<P2V<float, Color>> PlotPoints
        {
            get
            {
                return ColorSequence.Dipolar(Colors.Red, Colors.Blue, 150)
                    .Colors
                    .ToList()
                    .Select((c, i) => new P2V<float, Color>(
                                        x: i,
                                        y: i,
                                        v: c
                        )
                     ).ToList();
            }
        }

        static List<RV<float, Color>> PlotRectangles
        {
            get
            {
                return ColorSequence.Dipolar(Colors.Red, Colors.Blue, 20)
                    .Colors
                    .ToList()
                    .Select((c, i) => new RV<float, Color>(
                                        minX: 200-i*9,
                                        minY: i*9,
                                        maxX: RectSize,
                                        maxY: RectSize,
                                        v: c
                        )
                     ).ToList();
            }
        }

        private const float RectSize = 10.0f;
    }
}
