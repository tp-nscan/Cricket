using Cricket.Common;
using Cricket.ViewModel.Common;
using Cricket.ViewModel.Design.Common;

namespace Cricket.ViewModel.Pages.Sandbox
{
    public class GraphPageVm : BindableBase
    {
        public GraphPageVm()
        {
            GraphVm = new GraphVmD();
        }

        private GraphVmD _graphVm;
        public GraphVmD GraphVm
        {
            get { return _graphVm; }
            set
            {
                SetProperty(ref _graphVm, value);
            }
        }
    }
}
