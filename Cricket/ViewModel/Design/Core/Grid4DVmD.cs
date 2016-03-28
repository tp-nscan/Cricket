using System.Collections.Generic;
using Cricket.ViewModel.Core;
using TT;

namespace Cricket.ViewModel.Design.Core
{
    public class Grid4DVmD : Grid4DVm<float>
    {
        private static int GridStride = 15;
        public Grid4DVmD() : base(Bounds, CursorSt, ColorMap<>.RedBlueUnit, "Test title")
        {
           UpdateData(TestData);
        }

        public static IEnumerable<V2_2<float>> TestData
        {
            get
            {
                return null; //  Grid2dCnxn.Z4sForForGridWithRingNbrs(Bounds.X);
            }
        }

        public static Z2<int> Bounds = new Z2<int>(GridStride, GridStride);

        public static Z2<int> CursorSt = new Z2<int>(7, 4);

    }
}