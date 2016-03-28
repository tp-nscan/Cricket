using System.Collections.Generic;
using Cricket.Graphics;
using Cricket.ViewModel.Core;
using TT;

namespace Cricket.ViewModel.Design.Core
{
    public class Grid2DVmD : Grid2DVm<float>
    {
        public Grid2DVmD() : base(Strides, ColorMap<>.RedBlueUnit, "Test title")
        {
            UpdateData(DesignData.Grid2DTestData(Strides));
        }

        public static Sz2<int> Strides = new Sz2<int>(10,15);
        
    }
}