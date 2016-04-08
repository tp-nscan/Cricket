using Cricket.Common;
using Cricket.ViewModel.Common;
using Cricket.ViewModel.Design.Common;

namespace Cricket.ViewModel.Pages.Sandbox
{
    public class LegendPageVm : BindableBase
    {
        public LegendPageVm()
        {
            LegendVm = new LegendVmD();
        }

        private LegendVm _legendVm;
        public LegendVm LegendVm
        {
            get { return _legendVm; }
            set
            {
                SetProperty(ref _legendVm, value);
            }
        }
    }
}
