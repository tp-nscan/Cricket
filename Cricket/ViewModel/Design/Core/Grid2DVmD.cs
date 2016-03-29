
using Cricket.ViewModel.Core;
using TT;

namespace Cricket.ViewModel.Design.Core
{
    public class Grid2DVmD : Grid2DVm<float>
    {
        public Grid2DVmD() : base(TestStrides, ColorSets.RedBlueSigned, "Test title")
        {
            UpdateData(DesignData.Grid2DTestData(TestStrides));
        }

        public static Sz2<int> TestStrides = new Sz2<int>(10,15);
        
    }
}