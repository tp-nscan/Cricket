using Cricket.Common;
using Cricket.ViewModel.Common;
using Cricket.ViewModel.Design.Common;

namespace Cricket.ViewModel.Pages.Sandbox
{
    public class Grid4dPageVm : BindableBase
    {
        public Grid4dPageVm()
        {
            Grid4DVm = new Grid4DVmD();
        }

        private Grid4DVm<float> _graphVm;
        public Grid4DVm<float> Grid4DVm
        {
            get { return _graphVm; }
            set
            {
                SetProperty(ref _graphVm, value);
            }
        }
    }
}
