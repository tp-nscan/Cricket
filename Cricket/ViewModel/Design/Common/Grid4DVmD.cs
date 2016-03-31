using System.Collections.Generic;
using Cricket.ViewModel.Common;
using TT;

namespace Cricket.ViewModel.Design.Common
{
    public class Grid4DVmD : Grid4DVm<float>
    {
        private static int GridStride = 15;
        public Grid4DVmD() : base(Bounds, CursorSt, ColorSets.RedBlueSigned, "Test title")
        {
           UpdateData(TestData);
        }

        public static IEnumerable<LS2V<int,float>> TestData
        {
            get
            {
                return null; //  Grid2dCnxn.Z4sForForGridWithRingNbrs(Bounds.X);
            }
        }

        public static P2<int> Bounds = new P2<int>(GridStride, GridStride);

        public static P2<int> CursorSt = new P2<int>(7, 4);

    }
}