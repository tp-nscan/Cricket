﻿using Cricket.Common;
using Cricket.ViewModel.Common;
using Cricket.ViewModel.Design.Common;

namespace Cricket.ViewModel.Pages
{
    public class SandboxPageVm : BindableBase
    {
        public SandboxPageVm()
        {
            Grid2DVm = new Grid2DVmD();
        }

        private Grid2DVm<float> _graphVm;
        public Grid2DVm<float> Grid2DVm
        {
            get { return _graphVm; }
            set
            {
                SetProperty(ref _graphVm, value);
            }
        }
    }
}
